using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models
{
    public class PaymentRequest
    {
        [Required]
        public string PaymentMethod { get; set; } // "card", "paypal", etc.

        [Required]
        public string PaymentToken { get; set; } // From payment processor (e.g. Stripe)
    }
}
