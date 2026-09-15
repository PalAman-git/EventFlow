using System.Threading.Tasks;
using EventFlow.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

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