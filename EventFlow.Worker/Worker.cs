using EventFlow.Infrastructure.Data;
using EventFlow.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventFlow.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    EventData[] events = new EventData[]
     {
        new EventData
        {
            Type = "Order Created",
            Payload = "order has been created proceed further!"
        },
        new EventData
        {
            Type = "Notification Sent",
            Payload = "notification has been sent."
        },
        new EventData
        {
            Type = "Payment Successful",
            Payload = "payment has been made successfully."
        }
     };
    Random random = new Random();

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

            int randomIndex = random.Next(events.Length);
            EventData randomEvent = events[randomIndex];

            await db.Events.AddAsync(new Event
            {
                Id = Guid.NewGuid(),
                Type = randomEvent.Type,
                CurrStatus = Status.Pending,
                CreatedAt = DateTime.UtcNow,
                Payload = randomEvent.Payload
            });

            await db.SaveChangesAsync(stoppingToken);

            await Task.Delay(1000,stoppingToken);
        }
    }
}

public class EventData
{
    public string Type { get; set; }
    public string Payload { get; set; }
}
