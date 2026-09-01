using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class EventsController: ControllerBase
{
    [HttpPost]
    public IActionResult postEvent(Event eventData){
        return Ok("event posted");
    }

    [HttpGet("{id}")]
    public IActionResult getEvent(int id)
    {
        return Ok($"got event with id : {id}");
    }
}