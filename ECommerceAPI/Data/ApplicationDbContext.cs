using ECommerceAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ECommerceAPI.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OrderItem>()
               .HasKey(rp => new { rp.ProductId, rp.OrderId });
            builder.Entity<CartItem>()
                .HasKey(rp => new { rp.ProductId, rp.CartId });

            // Disabling cascade delete for the reverse relationship
            builder.Entity<Cart>()
                .HasOne(c => c.ApplicationUser)
                .WithOne(u => u.Cart)
                .HasForeignKey<Cart>(c => c.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Disabling cascade delete for the reverse relationship
            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.ApplicationUser)
                .HasForeignKey<ApplicationUser>(u => u.CartId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevents cascade delete on ApplicationUser
            

        }
    }
}
