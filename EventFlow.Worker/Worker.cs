using EventFlow.Infrastructure.Data;
using EventFlow.Infrastructure.Models;
using EventFlow.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

namespace EventFlow.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly EventData[] events = EventSeedData.GetSeedData(); 
    private readonly Random random = new();

    public Worker(ILogger<Worker> logger,IServiceScopeFactory scopeFactory){
        _logger = logger;
        _scopeFactory = scopeFactory;
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

            List<EventDelivery> pendingEvent = await db.EventDeliveries.Where(e => e.Status == DeliveryStatus.Pending).ToListAsync();
            foreach(var eventToDeliver in pendingEvent)
            {
                var Subscriber = eventToDeliver.SubscriptionId;
            }
        
            await Task.Delay(5000,stoppingToken);
        }
    }
}

