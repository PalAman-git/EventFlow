using System.Threading.Tasks;
using EventFlow.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventFlow.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SubscriptionController(EventFlowDbContext db) : ControllerBase
{
    private readonly EventFlowDbContext _db = db;

    [HttpPost]
    public async Task<IActionResult> PostSubscription([FromBody] SubscriptionDto body,CancellationToken cancellationToken)
    {
        try
        {
            bool exists = await _db.Subscriptions.AnyAsync( s => s.EventType == body.EventType && s.WebhookUrl == body.WebhookUrl,cancellationToken);

            if (exists)
            {
                return Ok();
            }

            await _db.Subscriptions.AddAsync(new Subscription {
                EventType = body.EventType,
                WebhookUrl = body.WebhookUrl
            },cancellationToken);

            await _db.SaveChangesAsync(cancellationToken);

            return Ok();
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
}

public class SubscriptionDto
{
    public string EventType {get;set;}
    public string WebhookUrl{get;set;}
}