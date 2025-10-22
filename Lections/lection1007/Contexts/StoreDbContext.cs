using lection1007.Models;
using Microsoft.EntityFrameworkCore;

namespace lection1007.Contexts
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Category> Categories => Set<Category>(); 
        public DbSet<Game> Games => Set<Game>();

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }
    }
}
