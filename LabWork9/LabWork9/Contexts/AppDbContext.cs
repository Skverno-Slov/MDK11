using LabWork9.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork9.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Visitor> Visitors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=mssql;User ID=ispp3114;Password=3114;Trust Server Certificate=True");
        }
    }
}
