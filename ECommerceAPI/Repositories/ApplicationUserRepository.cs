using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<ApplicationUser> GetUserWithCartAsync(string userId)
        {
            return await _context.ApplicationUsers
            .Include(u => u.Cart)
            .ThenInclude(c => c.CartItems)
            .ThenInclude(m=>m.Product)
            .FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task CreateUserAsync(ApplicationUser user)
        {
            await _context.ApplicationUsers.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user =await GetUserByIdAsync(userId);
            if (user != null)
            {
                _context.ApplicationUsers.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _context.ApplicationUsers.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return _context.ApplicationUsers.FirstOrDefault(e=>e.Id==userId) ?? null;
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            _context.ApplicationUsers.Update(user);
            await _context.SaveChangesAsync();
        }
    }
    
}
