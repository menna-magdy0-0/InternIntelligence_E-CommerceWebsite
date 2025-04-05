using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByIdAsync(int cartId);
        Task<IEnumerable<Cart>> GetAllCartsAsync();
        Task CreateCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(int cartId);

        Task AddItemToCartAsync(int cartId, CartItem item); // Cart-specific operations
        Task RemoveItemFromCartAsync(int cartId, int productId);
        Task<Cart> GetCartWithItemsAsync(int cartId); // Add this for eager loading
        Task<Cart> GetUserCartAsync(string userId); // User-specific cart
        Task UpdateItemQuantityAsync(int cartId, int productId, int newQuantity);
        Task ClearCartAsync(int cartId);
        Task<bool> CartExistsAsync(int cartId);
        Task<bool> ProductExistsInCartAsync(int cartId, int productId);
        Task<decimal> CalculateCartTotalAsync(int cartId);
    }
}
