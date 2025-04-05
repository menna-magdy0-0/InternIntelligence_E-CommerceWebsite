using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace ECommerceAPI.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext context;

        public OrderItemRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task CreateOrderItemAsync(OrderItem orderItem)
        {
            context.OrderItems.Add(orderItem);
            await context.SaveChangesAsync();
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var orderItem = context.OrderItems.Find(id);
            if (orderItem != null)
            {
                context.OrderItems.Remove(orderItem);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
            return await context.OrderItems
                .Include(o => o.Product)
                .Include(o => o.Order)
                .ToListAsync();
        }

        public async Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            context.OrderItems.Update(orderItem);
            await context.SaveChangesAsync();

        }
    }
}
