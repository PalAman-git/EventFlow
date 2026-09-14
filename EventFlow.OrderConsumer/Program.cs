using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.UseHttpsRedirection();



app.MapPost("/webhook", (EventDto eventDto) =>
{
    Console.WriteLine(eventDto.Type);
    Console.WriteLine(eventDto.Payload);

    return Results.Ok();
})
.WithName("Webhook");


app.Run();


public class EventDto
{
    public Guid Id{get;set;}
    public string Type {get;set;} = string.Empty;
    public JsonDocument Payload{get;set;} = null!;
}