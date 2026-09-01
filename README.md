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
- [x] add postgres database using docker
- [ ] add worker to process the events

## Getting Started

### Prerequisites
Make sure you have the following installed:
- .NET SDK
- Docker

1. Clone the repository

```
git clone [https://github.com/PalAman-git/EventFlow]
cd EventFlow
```

2. Start PostgreSQL

EventFlow uses PostgreSQL for persistent event storage.
Start the PostgreSQL container using Docker Compose:
```
docker compose up -d
```
I have made docker-compose.yaml, this file contain the service postgreSQL.
This creates:
- Database: eventflow
- User: eventflow
- Port: 5432

The postgres_data Docker volume persists PostgreSQL data even if the container is removed.

3. Configure the database connection

The application uses the following connection string:
```
{
  "ConnectionStrings": {
    "EventFlow": "Host=localhost;Port=5432;Database=eventflow;Username=eventflow;Password=eventflow_password"
  }
}
```
Since the .NET API currently runs on the host machine and PostgreSQL runs inside Docker, the host is localhost.

4. Install EF Core CLI

If dotnet ef is not already installed:

dotnet tool install --global dotnet-ef

Verify the installation:

dotnet ef --version

5. Create the database schema

EventFlow uses Entity Framework Core migrations to create and update the database schema.

Create the initial migration:

dotnet ef migrations add InitialCreate

Apply the migration:

dotnet ef database update

This creates the required tables in the eventflow PostgreSQL database.

6. Run the application

Start the ASP.NET Core application:

dotnet run

The API will start on the URL shown in the terminal.

7. Test the API

Create an event using:

POST /api/events

Example request:

{
  "type": "OrderCreated",
  "payload": "{\"orderId\":12345,\"customerId\":789,\"amount\":2499}"
}

The event is persisted in PostgreSQL with an initial status of Pending.

Useful Docker commands

Check running containers:

docker ps

Stop the PostgreSQL container:

docker compose down

Start it again:

docker compose up -d

View PostgreSQL logs:

docker logs eventflow-postgres
Connect to PostgreSQL

You can access the PostgreSQL database using psql:

docker exec -it eventflow-postgres psql -U eventflow -d eventflow

List tables:

\dt

View events:

SELECT * FROM "Events";

Exit:

\q