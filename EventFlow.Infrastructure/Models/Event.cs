using System.Text.Json;

namespace EventFlow.Infrastructure.Models;

public class Event
{
    public Guid Id{get;set;}
    public string Type {get;set;} = string.Empty;
    public JsonDocument Payload{get;set;} = null!;
    public DateTime CreatedAt{get;set;} = DateTime.UtcNow;
}