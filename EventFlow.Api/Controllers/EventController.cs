using Microsoft.AspNetCore.Mvc;
using EventFlow.Infrastructure.Data;
using EventFlow.Infrastructure.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("/api/[controller]")]
public class EventsController: ControllerBase
{
    private readonly EventFlowDbContext _db;
    public EventsController(EventFlowDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> PostEvent(Event eventData){

        await using IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync();

        try
        {
            await _db.Events.AddAsync(eventData);
            List<Subscription> subcribers = await _db.Subscriptions.Where(sub => sub.EventType == eventData.Type).ToListAsync();

            foreach(var subscriber in subcribers)
            {
                await _db.EventDeliveries.AddAsync(new EventDelivery
                {
                    Id = new Guid(),
                    EventId = eventData.Id,
                    SubscriptionId = subscriber.Id,
                    Status = DeliveryStatus.Pending,
                    RetryCount = 0
                });
            }

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();
            Console.WriteLine("Transaction commited successfully");
            return Ok();

        }catch(Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception($"The transaction rolled back due to {ex.Message}");  
        }

    }
}