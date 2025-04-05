using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerceAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        [JsonIgnore]
        public Cart? Cart { get; set; }
        [JsonIgnore]
        public List<Order>? Orders { get; set; }

    }
}
