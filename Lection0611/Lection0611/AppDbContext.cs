using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection0611
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected string db = @"C:\temp\ispp31\Repo\Lections\Lection0611\app.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource = app.db");
        }
    }
}
