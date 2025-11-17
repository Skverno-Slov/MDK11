using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersLibrary.UsersTPT.Models;

namespace UsersLibrary.UsersTPT.Contexts
{
    public class UsersTPTContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<TicketSeller> TicketSellers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=UsersTPT.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().UseTptMappingStrategy();
        }

        public UsersTPTContext()
        {
            Database.EnsureCreated();
        }
    }
}
