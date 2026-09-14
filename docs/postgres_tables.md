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
To store the consumers subscriptions, 
