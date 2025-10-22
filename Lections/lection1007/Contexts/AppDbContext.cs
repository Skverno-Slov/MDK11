using lection1007.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace lection1007.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories => Set<Category>(); //get set
        public DbSet<Game> Games => Set<Game>();

        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source = test.db");
            optionsBuilder
                .UseSqlServer("Data Source=mssql;User ID=ispp3114;Password=3114;Trust Server Certificate=True")
                .LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}
