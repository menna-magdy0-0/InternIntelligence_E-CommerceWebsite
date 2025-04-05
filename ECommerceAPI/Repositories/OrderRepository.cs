using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;

        public OrderRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task AddOrderAsync(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();

        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order != null)
            {
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await context.Orders.FirstOrDefaultAsync(e=>e.Id == orderId);
        }

        public async Task<Order> GetOrderWithItemsAsync(int orderId)
        {
            return await context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(string userId)
        {
            return await context.Orders
            .Where(o => o.ApplicationUserId == userId)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .ToListAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }
    }
}
