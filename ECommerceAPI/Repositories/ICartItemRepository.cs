using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories
{
    public interface ICartItemRepository
    {
        Task<IEnumerable<CartItem>> GetAllCartItemsAsync();
        Task CreateCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task DeleteCartItemAsync(int id);
    }
}
