using Microsoft.EntityFrameworkCore;
using UsersLibrary.UsersTPH.Models;

namespace UsersLibrary.UsersTPH.Contexts
{
    public class UsersTPHContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<TicketSeller> TicketSellers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=UsersTPH.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .UseTphMappingStrategy();
        }

        public UsersTPHContext()
        {
            Database.EnsureCreated();
        }
    }
}
