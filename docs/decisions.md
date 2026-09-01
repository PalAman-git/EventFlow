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