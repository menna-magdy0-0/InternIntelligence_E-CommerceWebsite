using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public DateTime? PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        
        public List<OrderItem>? OrderItems { get; set; }
    }
    public enum OrderStatus
    {
        Pending,    // Order created but payment not started
        Processing, // Order being prepared
        Delivered,  // Order received by customer
        Cancelled,  // Order cancelled
        Paid,       // Payment succeeded
        Failed      // Payment failed or processing error
    }
}
