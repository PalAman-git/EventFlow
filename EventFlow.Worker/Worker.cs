using System.Data.Common;
using System.Net.Http.Json;
using System.Text.Json;
using EventFlow.Infrastructure.Data;
using EventFlow.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventFlow.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHttpClientFactory _httpClientFactory;

    public Worker(ILogger<Worker> logger,IServiceScopeFactory scopeFactory,IHttpClientFactory httpClientFactory){
        _logger = logger;
        _scopeFactory = scopeFactory;
        _httpClientFactory = httpClientFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<EventFlowDbContext>();

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            List<EventDelivery> pendingEvent = await db.EventDeliveries
                .Where(e =>
                    e.Status == DeliveryStatus.Pending || 
                    e.Status == DeliveryStatus.Failed
                ).ToListAsync(stoppingToken);
                
            foreach(var eventToDeliver in pendingEvent)
            {
                Event? eventData = await db.Events.FindAsync([eventToDeliver.EventId],stoppingToken);
                var SubscriberUrl = await db.Subscriptions.Where(sub => sub.Id == eventToDeliver.SubscriptionId).Select(sub => sub.WebhookUrl).FirstOrDefaultAsync(stoppingToken);

                var client = _httpClientFactory.CreateClient();
                
                if(eventData == null) return;

                var webhookEvent = new EventWebhookDto
                {
                    Id = eventData.Id,
                    Type = eventData.Type,
                    Payload = eventData.Payload
                };

                var response = await client.PostAsJsonAsync(
                    SubscriberUrl,
                    webhookEvent,
                    stoppingToken
                );


                if (response.IsSuccessStatusCode)
                {
                    //consumer successfully responded
                    await db.EventDeliveries.Where(ed => ed.Id ==eventToDeliver.Id).ExecuteUpdateAsync(ed => ed
                        .SetProperty( x => x.Status,DeliveryStatus.Delivered),
                        stoppingToken
                    );
                }
                else
                {
                    //Delivery failed
                    await db.EventDeliveries.Where(ed => ed.Id ==eventToDeliver.Id).ExecuteUpdateAsync(ed => ed
                        .SetProperty( x => x.Status,DeliveryStatus.Failed)
                        .SetProperty( x => x.LastAttemptedAt,DateTime.UtcNow)
                        .SetProperty( x => x.RetryCount,x => x.RetryCount + 1),
                        stoppingToken
                    );
                }
            }
        
            await Task.Delay(5000,stoppingToken);
        }
    }
}


public class EventWebhookDto
{
    public Guid Id{get;set;}
    public string Type {get;set;} = string.Empty;
    public JsonDocument Payload{get;set;} = null!;
}

