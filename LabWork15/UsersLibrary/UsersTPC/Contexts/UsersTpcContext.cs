using Microsoft.EntityFrameworkCore;
using UsersLibrary.UsersTPC.Models;

namespace UsersLibrary.UsersTPC.Contexts
{
    public class UsersTPCContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<TicketSeller> TicketSellers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=UsersTPC.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .UseTpcMappingStrategy();
        }

        public UsersTPCContext()
        {
            Database.EnsureCreated();
        }
    }
}
