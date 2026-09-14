## Events
To store the events.

| id                                   | Type                 | Payload (jsonb)                                                                 | curr_status | createdAt            |
|--------------------------------------|----------------------|---------------------------------------------------------------------------------|-------------|----------------------|
| 8f7c2a1e-3b4d-4c6f-9a12-1d2e3f4a5b6c | OrderCreated         | `{"orderId":"ORD-1001","amount":2499,"currency":"INR"}`                       | Pending     | 2026-09-14 09:15:23 |
| 2a9d6e4f-7b1c-4e8a-b3d5-6f7a8c9e0b12 | PaymentSuccessful    | `{"paymentId":"PAY-2001","orderId":"ORD-1001","method":"UPI","amount":2499}`  | Delivered   | 2026-09-14 09:16:04 |
| 5c1e8a7b-2d4f-4b6a-9c3e-7f8d1a2b4e5c | InventoryReserved    | `{"reservationId":"INV-3001","orderId":"ORD-1001","items":2}`                | Pending     | 2026-09-14 09:16:42 |
| 7d3f9b2a-6c1e-4a8d-b5f7-9e0a1c2d3b4e | NotificationSent     | `{"notificationId":"NOT-4001","userId":"USR-101","channel":"Email"}`          | Delivered   | 2026-09-14 09:17:11 |
| 1b4e7c9a-5d2f-4a6b-8e3c-9f1d2a7b6c5e | OrderCreated         | `{"orderId":"ORD-1002","amount":1599,"currency":"INR"}`                       | Failed      | 2026-09-14 09:18:35 |
| 3e6a9c1b-8d4f-2b7c-a5e1-0f9d3c6b8a2e | PaymentSuccessful    | `{"paymentId":"PAY-2002","orderId":"ORD-1002","method":"Card","amount":1599}` | Pending     | 2026-09-14 09:19:07 |



## Subscriptions

To store the Consumer's Webhook and the type of event they are subscribed to.

| id                                   | EventType          | WebhookUrl                     |
|--------------------------------------|--------------------|--------------------------------|
| `a12f4c8e-7b3d-4e91-9f25-6c8d1a2b3e4f` | `OrderCreated`      | `http://localhost:5001/webhook` |
| `b23e5d9f-8c4e-4fa2-a036-7d9e2b3c4f5a` | `PaymentSuccessful` | `http://localhost:5002/webhook` |
| `c34f6a0e-9d5f-4ab3-b147-8e0f3c4d5a6b` | `OrderCreated`      | `http://localhost:5003/webhook` |
| `d45a7b1f-0e6a-4bc2-c258-9f1a4d5e6b7c` | `InventoryReserved` | `http://localhost:5003/webhook` |
| `e56b8c2f-1f7b-4cd3-d369-0a2b5e6f7c8d` | `PaymentSuccessful` | `http://localhost:5003/webhook` |
| `f67c9d3a-2a8c-4de4-e470-1b3c6f7d8e9f` | `NotificationSent`  | `http://localhost:5003/webhook` |



## EventDeliveries

To track the delivery status of each event for a specific subscription.

| id                                   | eventId                              | subscriptionId                       | status     | retryCount | lastAttemptAt       |
|--------------------------------------|--------------------------------------|--------------------------------------|------------|------------|---------------------|
| `d101a1b2-c3d4-4e5f-8a6b-7c8d9e0f1a2b` | `8f7c2a1e-3b4d-4c6f-9a12-1d2e3f4a5b6c` | `a12f4c8e-7b3d-4e91-9f25-6c8d1a2b3e4f` | `Delivered` | `0` | `2026-09-14 09:15:45` |
| `d202b2c3-d4e5-4f6a-9b7c-8d9e0f1a2b3c` | `2a9d6e4f-7b1c-4e8a-b3d5-6f7a8c9e0b12` | `b23e5d9f-8c4e-4fa2-a036-7d9e2b3c4f5a` | `Delivered` | `0` | `2026-09-14 09:16:28` |
| `d303c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d` | `5c1e8a7b-2d4f-4b6a-9c3e-7f8d1a2b4e5c` | `c34f6a0e-9d5f-4ab3-b147-8e0f3c4d5a6b` | `Pending`   | `0` | `2026-09-14 09:17:02` |
| `d404d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e` | `5c1e8a7b-2d4f-4b6a-9c3e-7f8d1a2b4e5c` | `d45a7b1f-0e6a-4bc2-c258-9f1a4d5e6b7c` | `Delivered` | `1` | `2026-09-14 09:17:31` |
| `d505e5f6-a7b8-4c9d-0e1f-2a3b4c5d6e7f` | `1b4e7c9a-5d2f-4a6b-8e3c-9f1d2a7b6c5e` | `a12f4c8e-7b3d-4e91-9f25-6c8d1a2b3e4f` | `Failed`    | `3` | `2026-09-14 09:19:12` |
| `d606f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a` | `3e6a9c1b-8d4f-2b7c-a5e1-0f9d3c6b8a2e` | `b23e5d9f-8c4e-4fa2-a036-7d9e2b3c4f5a` | `Pending`   | `1` | `2026-09-14 09:20:05` |
| `d707a7b8-c9d0-4e1f-2a3b-4c5d6e7f8a9b` | `3e6a9c1b-8d4f-2b7c-a5e1-0f9d3c6b8a2e` | `e56b8c2f-1f7b-4cd3-d369-0a2b5e6f7c8d` | `Delivered` | `0` | `2026-09-14 09:20:32` |