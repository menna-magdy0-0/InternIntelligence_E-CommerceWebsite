using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext context;
        public CategoryRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task CreateCategoryAsync(Category category)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();

        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await context.Categories.FindAsync(id);

        }

        public async Task UpdateCategoryAsync(Category category)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();
        }
    }
    
}
