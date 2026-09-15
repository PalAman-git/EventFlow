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
- [x] add worker to process the events
- [x] ensure worker delivers event to the consumer ✅
- [x] complete the most basic flow of the app

## Getting Started

### Prerequisites

Make sure you have the following installed:

* [.NET SDK](https://dotnet.microsoft.com/download)
* [Docker](https://www.docker.com/get-started/)

### 1. Clone the repository

```bash
git clone https://github.com/PalAman-git/EventFlow.git
cd EventFlow
```

### 2. Start PostgreSQL

EventFlow uses PostgreSQL for persistent event storage.

The repository includes a `docker-compose.yaml` file that configures the PostgreSQL service.

Start the database:

```bash
docker compose up -d
```

This creates:

| Configuration | Value           |
| ------------- | --------------- |
| Database      | `eventflow`     |
| Username      | `eventflow`     |
| Port          | `5432`          |
| Docker Volume | `postgres_data` |

The `postgres_data` Docker volume persists PostgreSQL data even if the container is removed.

Verify that the PostgreSQL container is running:

```bash
docker ps
```

### 3. Configure the database connection

The application uses the following connection string:

```json
{
  "ConnectionStrings": {
    "EventFlow": "Host=localhost;Port=5432;Database=eventflow;Username=eventflow;Password=eventflow_password"
  }
}
```

Since the ASP.NET Core application runs on the host machine while PostgreSQL runs inside Docker, the application connects to PostgreSQL through `localhost:5432`.

### 4. Install Entity Framework Core CLI

If `dotnet ef` is not already installed:

```bash
dotnet tool install --global dotnet-ef
```

Verify the installation:

```bash
dotnet ef --version
```

### 5. Create the database schema

EventFlow uses **Entity Framework Core migrations** to create and update the PostgreSQL database schema.

Create the initial migration:

```bash
dotnet ef migrations add InitialCreate
```

Apply the migration:

```bash
dotnet ef database update
```

This creates the required EventFlow tables in the `eventflow` PostgreSQL database.

### 6. Run the application

Start the ASP.NET Core API:

```bash
dotnet run
```

The API will start at the URL displayed in the terminal.

### 7. Test the API

Create an event using:

```http
POST /api/events
```

Example request:

```json
{
  "type": "OrderCreated",
  "payload": "{\"orderId\":12345,\"customerId\":789,\"amount\":2499}"
}
```

The event is persisted in PostgreSQL and can then be processed by EventFlow's event delivery workflow.

---

## Database

EventFlow currently uses the following tables:

### [Events](docs/postgres_tables.md) - stores the event produced by applications.

### [Subscriptions](docs/postgres_tables.md) - stores consumers webhook url and type of events they are subscribed to.

### [EventDeliveries](docs/postgres_tables.md) - tracks the delivery of each event for specific subscription.

---

## Useful Docker Commands

### Check running containers

```bash
docker ps
```

### Stop PostgreSQL

```bash
docker compose down
```

### Start PostgreSQL again

```bash
docker compose up -d
```

### View PostgreSQL logs

```bash
docker logs eventflow-postgres
```

### Connect to PostgreSQL

You can access the PostgreSQL database directly using `psql`:

```bash
docker exec -it eventflow-postgres psql -U eventflow -d eventflow
```

Once connected:

### List tables

```sql
\dt
```

### View events

```sql
SELECT * FROM "Events";
```

### View subscriptions

```sql
SELECT * FROM "Subscriptions";
```

### View event deliveries

```sql
SELECT * FROM "EventDeliveries";
```

### Exit PostgreSQL

```sql
\q
```
