using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<Order> GetOrderWithItemsAsync(int orderId); // Add eager loading
        Task<IEnumerable<Order>> GetUserOrdersAsync(string userId); // User-specific
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
    }
}
