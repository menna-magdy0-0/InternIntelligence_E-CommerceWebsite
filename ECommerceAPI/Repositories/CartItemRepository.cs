using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext context;

        public CartItemRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task CreateCartItemAsync(CartItem cartItem)
        {
            context.CartItems.Add(cartItem);    
            await context.SaveChangesAsync();
        }

        public async Task DeleteCartItemAsync(int id)
        {
            var cartItem = await context.CartItems.FindAsync(id);
            if (cartItem != null)
            {
                context.CartItems.Remove(cartItem);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CartItem>> GetAllCartItemsAsync()
        {
            return await context.CartItems
                .Include(c => c.Product)
                .Include(c => c.Cart)
                .ToListAsync();
        }


        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            context.CartItems.Update(cartItem);
            await context.SaveChangesAsync();
        }
    }
}
