## Using Webhooks will give me some issues: 
- What if EventFlow sent the event with webhooks but consumer does not receive it? Now should I mark the event as processed 
 or retry?

- How to ensure that the consumer has processed the event so that I could mark that event as delivered reliably?
- Could I return some kind of response from consumer, when it successfully processed the event? or there could be some other way?

## How to ensure that , If some event is posted, enteries in the EventDelivery Table fills up with the event along with its subscribers?
- Considered using trigger on the post on an event.
- but using trigger hides the business login in the sql layer and it makes debugging harder in later stages.
- So that is why I am using transaction , to ensure that on the event creation, all its subscribers are put into the EventDelivery table.
- Transaction ensures either all the subscribers will get into the Event Delivery table or Event itself will not be inserted in the Events table.
