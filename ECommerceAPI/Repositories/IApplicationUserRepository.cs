using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<ApplicationUser> GetUserWithCartAsync(string userId);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task CreateUserAsync(ApplicationUser user);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(string userId);
        
    }
}
