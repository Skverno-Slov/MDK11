using LabWork12.Contexts;
using LabWork12.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LabWork12.Services
{
    public class MovieService(AppDbContext context)
    {
        readonly AppDbContext _context = context;

        public async Task<List<Movie>> GetMoviesAsync(string sortColumn)
            => await _context.Movies
                .FromSqlRaw($"SELECT * FROM Movie ORDER BY '{sortColumn}'")
                .ToListAsync();

        public async Task<List<Movie>> GetMoviesByTitleAndYearAsync(string title, short year)
            => await _context.Movies
                .FromSqlRaw(@"
                    SELECT * FROM Movie 
                    WHERE Title = @p0 AND Year >= @p1",
                    title, year)
                    .ToListAsync();
    }
}
