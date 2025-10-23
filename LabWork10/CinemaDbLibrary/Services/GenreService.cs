using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaDbLibrary.Services
{
    public class GenreService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Genre>> GetGenresAsync()
            => await _context.Genres.ToListAsync();

        public async Task<Genre?> GetGenreAsync(int id)
            => await _context.Genres.FindAsync(id);
    }
}
