using LabWork12.Contexts;
using LabWork12.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LabWork12.Services
{
    public class SessionService(AppDbContext context)
    {
        readonly AppDbContext _context = context;

        public async Task<int> AddPricesByHallIdAsync(byte hallId,
                                                      decimal amount)
            => await _context.Database
            .ExecuteSqlAsync($"""
                update Session 
                set Price += {amount} 
                where HallId = { hallId }
                """);

        public async Task<decimal> GetMinMoviePriceAsync(int movieId)
            => await _context.Sessions
                .Where(s => s.MovieId == movieId)
                .MinAsync(s => s.Price);

        public async Task<decimal> GetMaxMoviePriceAsync(int movieId)
            => await _context.Sessions
                .Where(s => s.MovieId == movieId)
                .MaxAsync(s => s.Price);

        public async Task<decimal> GetAverageMoviePriceAsync(int movieId)
            => await _context.Sessions
                .Where(s => s.MovieId == movieId)
                .AverageAsync(s => s.Price);

        public async Task<List<Session>> GetSessionsByMovieIdAsync(int movieId)
            => await _context.Sessions
                .FromSql($"select * from dbo.GetSessionsByMovieId({movieId})")
                .ToListAsync();
    }
}
