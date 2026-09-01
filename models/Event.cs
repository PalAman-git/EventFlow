namespace EventFlow.Models;
public class Event
{
    public Guid Id{get;set;}
    public string Type {get;set;} = string.Empty;
    public string Payload{get;set;} = string.Empty;
    public Status CurrStatus {get;set;} = Status.Pending;
    public DateTime CreatedAt{get;set;} = DateTime.UtcNow;
}

public enum Status
{
    Pending,
    Failed,
    Delivered,
}