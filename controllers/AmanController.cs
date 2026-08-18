using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class AmanController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Aman from this side, watch");
    }

    [HttpGet("books")]
    public IActionResult GetBooks()
    {
        return Ok(new []{
            "Mathematics",
            "HC Verma",
            "Clean Code",
            "Epic Shit"
        });
    }
}