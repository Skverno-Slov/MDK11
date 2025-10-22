using LabWork12.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LabWork12.Services
{
    public class GenreService(AppDbContext context)
    {
        readonly AppDbContext _context = context;

        public async Task<string> GetGenresByMovieIdAsync(int movieId)
            => await _context.Database.SqlQuery<<string>($"SELECT * FROM Genre where GenreId IN(Select * FROM MovieGenre WHERE MovieId == {movieId})").ToListAsync();
    }
}
