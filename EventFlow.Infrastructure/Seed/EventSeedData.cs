using System.Text.Json;

namespace EventFlow.Infrastructure.Seed;

public class EventSeedData
{
    public static EventData[] GetSeedData()
    {
        return
        [
            new EventData
            {
                Type = "Order Created",
                Payload = JsonDocument.Parse("""
                {
                    "orderId": "ORD-1001",
                    "customerId": "CUS-501",
                    "productId": "PROD-101",
                    "quantity": 2,
                    "amount": 2499.00,
                    "currency": "INR"
                }
                """)
            },

            new EventData
            {
                Type = "Payment Successful",
                Payload = JsonDocument.Parse("""
                {
                    "orderId": "ORD-1001",
                    "paymentId": "PAY-9001",
                    "amount": 2499.00,
                    "method": "UPI",
                    "currency": "INR"
                }
                """)
            },

            new EventData
            {
                Type = "Inventory Reserved",
                Payload = JsonDocument.Parse("""
                {
                    "orderId": "ORD-1001",
                    "productId": "PROD-101",
                    "quantity": 2,
                    "warehouseId": "WH-01"
                }
                """)
            },

            new EventData
            {
                Type = "Order Confirmed",
                Payload = JsonDocument.Parse("""
                {
                    "orderId": "ORD-1001",
                    "customerId": "CUS-501",
                    "status": "confirmed"
                }
                """)
            },

            new EventData
            {
                Type = "Order Shipped",
                Payload = JsonDocument.Parse("""
                {
                    "orderId": "ORD-1001",
                    "shipmentId": "SHIP-7001",
                    "carrier": "Delhivery",
                    "trackingId": "DLV123456789",
                    "estimatedDelivery": "2026-09-14"
                }
                """)
            },

            new EventData
            {
                Type = "Notification Sent",
                Payload = JsonDocument.Parse("""
                {
                    "orderId": "ORD-1001",
                    "customerId": "CUS-501",
                    "channel": "Email",
                    "notificationType": "OrderConfirmation",
                    "recipient": "customer@example.com"
                }
                """)
            },

            new EventData
            {
                Type = "Payment Failed",
                Payload = JsonDocument.Parse("""
                {
                    "orderId": "ORD-1002",
                    "paymentId": "PAY-9002",
                    "amount": 1599.00,
                    "method": "Card",
                    "reason": "Insufficient funds"
                }
                """)
            },

            new EventData
            {
                Type = "Order Cancelled",
                Payload = JsonDocument.Parse("""
                {
                    "orderId": "ORD-1002",
                    "customerId": "CUS-502",
                    "reason": "Payment failed"
                }
                """)
            }
        ];
    }
}

public class EventData
{
    public string Type { get; set; }
    public JsonDocument Payload { get; set; }
}
