using EventFlow.Infrastructure.Models;

public class EventDelivery
{
    public Guid Id {get;set;}
    public Guid EventId{get;set;}
    public Guid SubscriptionId{get;set;}
    public DeliveryStatus Status{get;set;}
    public int RetryCount{get;set;}

    public DateTime? LastAttemptedAt {get;set;}
    public Event Event {get;set;} = null!;
    public Subscription Subscription{get;set;} = null!;
}

public enum DeliveryStatus
{
    Pending,
    Failed,
    Delivered,
}