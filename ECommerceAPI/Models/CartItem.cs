using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.Models
{
    public class CartItem
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public Cart? Cart { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
