using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddItemToCartAsync(int cartId, CartItem item)
        {
            var cart = await GetCartWithItemsAsync(cartId);
            var product = await _context.Products.FindAsync(item.ProductId)
                ?? throw new Exception($"Product with ID {item.ProductId} not found");

            var existingItem = cart.CartItems.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                item.Price = product.Price; // Set current product price
                cart.CartItems.Add(item);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<decimal> CalculateCartTotalAsync(int cartId)
        {
            var cart = await GetCartWithItemsAsync(cartId);
            return cart.CartItems.Sum(item => item.Price * item.Quantity);
        }

        public async Task<bool> CartExistsAsync(int cartId)
        {
            return await _context.Carts.AnyAsync(c => c.Id == cartId);
        }

        public async Task ClearCartAsync(int cartId)
        {
            var cart = await GetCartWithItemsAsync(cartId);
            cart.CartItems.Clear();
            await _context.SaveChangesAsync();
        }

        public async Task CreateCartAsync(Cart cart)
        {
            if (cart == null)
                throw new ArgumentNullException(nameof(cart));

            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public  async Task DeleteCartAsync(int cartId)
        {
            var cart = await GetCartByIdAsync(cartId);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Cart>> GetAllCartsAsync()
        {
            return await _context.Carts
               .Include(c => c.CartItems)
               .ThenInclude(i => i.Product)
               .ToListAsync();
        }

        public async Task<Cart> GetCartByIdAsync(int cartId)
        {
            return await _context.Carts
                .FirstOrDefaultAsync(c => c.Id == cartId)
                ?? throw new Exception($"Cart with ID {cartId} not found");
        }

        public async Task<Cart> GetCartWithItemsAsync(int cartId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.Id == cartId)
                ?? throw new Exception($"Cart with ID {cartId} not found");
        }

        public async Task<Cart> GetUserCartAsync(string userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);
        }

        public async Task<bool> ProductExistsInCartAsync(int cartId, int productId)
        {
            return await _context.CartItems
                .AnyAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
        }

        public async Task RemoveItemFromCartAsync(int cartId, int productId)
        {
            var cart = await GetCartWithItemsAsync(cartId);
            var item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId)
                ?? throw new Exception($"Product with ID {productId} not found in cart");

            cart.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            var existingCart = await GetCartByIdAsync(cart.Id);
            _context.Entry(existingCart).CurrentValues.SetValues(cart);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemQuantityAsync(int cartId, int productId, int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");

            var cart = await GetCartWithItemsAsync(cartId);
            var item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId)
                ?? throw new Exception($"Product with ID {productId} not found in cart");

            item.Quantity = newQuantity;
            await _context.SaveChangesAsync();
        }
    }
    
}
