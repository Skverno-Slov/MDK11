using LabWork12.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LabWork12.Services
{
    public class SessionService(AppDbContext context)
    {
        readonly AppDbContext _context = context;

        public async Task<int> IncreasePricesByHallIdAsync(byte hallId, decimal amount)
            => await _context.Database
            .ExecuteSqlAsync($"""
                UPDATE Session 
                SET Price += {amount} 
                WHERE HallId = { hallId }
                """);
                
    }
}
