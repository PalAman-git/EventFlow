using Microsoft.AspNetCore.Mvc;
using EventFlow.Infrastructure.Data;
using EventFlow.Infrastructure.Models;
using System.Threading.Tasks;

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
    public async Task<IActionResult> postEvent(Event eventData){

        _db.Events.Add(eventData);
        await _db.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult getEvent(int id)
    {
        return Ok($"got event with id : {id}");
    }
}