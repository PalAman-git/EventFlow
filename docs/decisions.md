## Why .NET Asp Core Web Api Over Node.js? 
I chose .NET ASP.NET Core for EventFlow primarily because I am already working with .NET Core MVC at my workplace. Building this project with the same ecosystem gives me an opportunity to go deeper into the technology and understand how it can be used for building high-throughput backend systems.

Other reasons include: 

- **Asynchronous programming**: ASP.NET Core has strong support for asynchronous I/O through async/await. This allows threads to be released while wating for operation such as database queries, network calls etc, which helps application to handle a large number of concurrent requests efficiently.

- **Microservices Support**: .NET provides mature ecosystem for building and communicating between microservices, which is suitable choice for an event driven system like EventFlow.