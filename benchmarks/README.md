# EventFlow Benchmarks

Performance benchmarks for EventFlow using **k6**.

The benchmarks are divided into two separate areas:
- **Ingestion** — measuring how quickly EventFlow can accept incoming events.
- **Delivery** — measuring how quickly workers can process and deliver events to subscribed webhooks.


## 1. Event Ingestion Benchmark

### What is being measured?
The ingestion benchmark measures the following path:
```text
k6
 │
 │ POST /api/events
 ▼
EventFlow API
 │
 ├── Persist Event
 │
 ├── Find matching subscriptions
 │
 └── Create EventDelivery records
 │
 ▼
PostgreSQL
```

> **Note:** This benchmark measures event ingestion and persistence. It does not measure end-to-end webhook delivery.

### Test Configuration

| Configuration | Value |
| :--- | :--- |
| **Tool** | k6 |
| **Virtual Users** | 10 |
| **Total Requests** | 49,587 |
| **Successful Requests** | 49,587 |
| **Failed Requests** | 0 |

### Results

| Metric | Result |
| :--- | :--- |
| **Throughput** | 1,652.76 req/s |
| **Average Latency** | 5.92 ms |
| **Median Latency (p50)** | 4.91 ms |
| **p90 Latency** | 8.35 ms |
| **p95 Latency** | 9.77 ms |
| **Maximum Latency** | 892.96 ms |
| **HTTP Failure Rate** | 0% |

#### Latency Distribution
| Latency | Result |
| :--- | :--- |
| **p50** | 4.91 ms |
| **p90** | 8.35 ms |
| **p95** | 9.77 ms |
| **max** | 892.96 ms |

### Interpretation
- Under this workload, EventFlow achieved approximately **1,653 requests/sec** with a **100% HTTP success rate**.
- The **p95 latency was 9.77 ms**, meaning 95% of ingestion requests completed within 9.77 ms.
- The maximum observed latency was **892.96 ms**, which indicates that a small number of requests experienced significantly higher latency than the majority.
- These outliers are retained in the results rather than excluded and will be investigated in future profiling runs.

## 2. Event Delivery Benchmark

### What is being measured?

The delivery benchmark measures the time from when an event is created until its subscribed webhook is successfully delivered.

```text
Event Created
│
▼
EventDelivery (Pending)
│
▼
Worker
│
▼
HTTP Webhook
│
▼
DeliveredAt
```
The measured latency is:
DeliveredAt - Event.CreatedAt

> **Note:** This represents end-to-end delivery latency. It includes time spent waiting for the worker to pick up the pending delivery, worker processing time, and HTTP webhook delivery time.

### Initial Results
The initial worker run produced the following result:

| **Event Generated/s** | **Woker Instances** | **Avg delivery latency** | **Max Batch Size** |
| :--- | :--- | :--- | :--- |
| 10 | 1 | 518.8 ms | 100 event |


The average was calculated using:

```sql
SELECT 
    AVG(ed."DeliveredAt" - e."CreatedAt") AS delivery_time
FROM "Events" e
JOIN "EventDeliveries" ed
    ON e."Id" = ed."EventId"
WHERE ed."DeliveredAt" IS NOT NULL;
```


## 3. Running the Benchmark

Start EventFlow using Docker Compose:
```bash
docker compose up -d
```

Run the ingestion benchmark:
```bash
docker run --rm -i -v "${PWD}/benchmarks/k6:/scripts" grafana/k6 run /scripts/ingestion.js
```
The workload configuration can be found at:
```bash
benchmarks/k6/ingestion.js
```

## 4. Benchmark Environment
The benchmark was executed locally with EventFlow and PostgreSQL running
inside Docker containers. k6 generated the HTTP load from the host machine.

## 5. Future Benchmarks
- Future tests will evaluate:

- Sustained ingestion throughput

- Increasing concurrent users

- Worker delivery throughput

- End-to-end event delivery latency

- Webhook failure and retry behaviour

- Database contention

- Latency under increasing load

- Multiple worker instances

- Database performance under increasing event volume

## 6. Benchmark History
Future optimization experiments will be recorded here to make performance changes measurable and reproducible.

| Version |	Throughput | p50 | p95 | Error Rate | Notes |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Initial** | 1,652.76 req/s | 4.91ms | 9.77ms | 0% | baseline |
