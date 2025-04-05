using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public List<CartItem>? CartItems { get; set; }

        //public decimal TotalPrice { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        //public void UpdateTotalPrice()
        //{
        //    TotalPrice = CartItems?.Sum(item => item.Price * item.Quantity) ?? 0;
        //    UpdatedAt = DateTime.UtcNow;
        //}
    }
}
