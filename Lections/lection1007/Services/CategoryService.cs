using lection1007.Contexts;
using lection1007.Models;
using Microsoft.EntityFrameworkCore;

namespace lection1007.Services
{
    public class CategoryService(StoreDbContext context)
    {
        private readonly StoreDbContext _context = context;

        public async Task<List<Category>> GetCategoriesAsync()
            => await _context.Categories.ToListAsync();
    }
}
