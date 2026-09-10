## Why .NET Asp Core Web Api Over Node.js? 
I chose .NET ASP.NET Core for EventFlow primarily because I am already working with .NET Core MVC at my workplace. Building this project with the same ecosystem gives me an opportunity to go deeper into the technology and understand how it can be used for building high-throughput backend systems.

Other reasons include: 

- **Asynchronous programming**: ASP.NET Core has strong support for asynchronous I/O through async/await. This allows threads to be released while wating for operation such as database queries, network calls etc, which helps application to handle a large number of concurrent requests efficiently.

- **Microservices Support**: .NET provides mature ecosystem for building and communicating between microservices, which is suitable choice for an event driven system like EventFlow.

## Why I am going with Postgres Sql for this application?
- I did compared other databases like nosql with postsql but there was not many things that forced me to go for noSql database.
- My application naturally has relationships like events -> consumer, event -> delivery.
- Postgres database has feature for JSONB field so naturally, I can store the payload of the event in the JSONB format if I want that in future
- For now I havent thought of how would I store but yeah it can help me.
- On the top of all that I have seen several other microservice applications use postgres so yeah I am going with it.

## I have a choice to choose Webhooks or polling for consumers to receive the events produced by producer.
I choose webhooks for the following reasons:

- It is way more efficient that polling and saves the system resources.
- I dont want each of my consumers to repeadly ask for weather or not there is any event for them, I will use webhooks to send event
to the consumers when event is produced.
- Webhooks are in defination real time update , like as soon as event is created I can send to the consumers.
- In Api there would be some latency due to their recuring period of polling like it could be after every 5 seconds.
- There could be data loss if the consumers is not ready to accept the event so I have to solve that issue.

[geeks for geeks article, Webhooks vs API polling](https://www.geeksforgeeks.org/blogs/what-is-a-webhook-and-how-to-use-it/)