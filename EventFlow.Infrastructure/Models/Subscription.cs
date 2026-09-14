public class Subscription
{
    public Guid Id{get;set;} = new Guid();
    public string EventType {get;set;}
    public string WebhookUrl{get;set;}
}