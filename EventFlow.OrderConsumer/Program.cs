using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.UseHttpsRedirection();

var eventFlowUrl = Environment.GetEnvironmentVariable("EVENTFLOW_URL")
    ?? throw new InvalidOperationException("EVENTFLOW_URL not configured");

var eventType = Environment.GetEnvironmentVariable("EVENT_TYPE")
    ?? throw new InvalidOperationException("EVENT_TYPE not configured");

var consumerName = Environment.GetEnvironmentVariable("CONSUMER_NAME")
    ?? throw new InvalidOperationException("CONSUMER_NAME not configured");

var webhookUrl = $"http://{consumerName}:8080/webhook";


using var client = new HttpClient();

var subscription = new SubscriptionRequest
{
    EventType = eventType,
    WebhookUrl = webhookUrl
};

var response = await client.PostAsJsonAsync(
    $"{eventFlowUrl}/api/subscriptions",
    subscription);

response.EnsureSuccessStatusCode();

Console.WriteLine($"Registered {eventType} subscription at {webhookUrl}");

app.MapPost("/webhook", (EventDto eventDto) =>
{
    Console.WriteLine($"[{consumerName}] Received: {eventDto.Type}");
    Console.WriteLine(eventDto.Payload);

    return Results.Ok();
});


app.Run();


public class EventDto
{
    public Guid Id{get;set;}
    public string Type {get;set;} = string.Empty;
    public JsonDocument Payload{get;set;} = null!;
}

public class SubscriptionRequest
{
    public string EventType{get;set;} = string.Empty;
    public string WebhookUrl{get;set;} = string.Empty;
}