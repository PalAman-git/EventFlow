# EventFlow
EventFlow is a high-throughput event processing system that receives events from external sources, processes them asynchronously, and reliably delivers them to the desired consumers with low latency.

## Problem
Modern applications often use a microservice architecture, where multiple services continuously generate events and other services need to react to or process those events.

Handling this event processing directly within the producer application can introduce several challenges, including managing consumers, error handling, retries, delivery status, throughput, and failures.

It can also increase latency in the producer application and tightly couple the producer to its downstream consumers.

## Solution

EventFlow acts as a dedicated layer between event producers and consumers.

Producers send events to EventFlow, which takes responsibility for receiving, processing, and reliably delivering those events to the appropriate consumers.

This allows producer applications to remain independent of downstream consumers while EventFlow handles the complexity of asynchronous event processing.

## Ultimate Goal
Producer produces event and it does not have to worry about 
- who consumes the event?
- where are they?
- Are they currently available?
- Did they successfully process it?
- should I retry?
- what happens if they fail? 
- How many times should I retry?
- should I wait for them?

These are the problem that my application is trying to solve

### Tasks
- [x] write get and post method for an event