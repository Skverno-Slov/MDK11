using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Models;
using CinemaDbLibrary.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CinemaDbLibrary.Services
{
    public class MovieService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Movie>> GetMoviesAsync(Paginator paginator)
            => await _context.Movies.ToListAsync();

        public async Task<Movie?> GetMovieAsync(int id)
            => await _context.Movies.FindAsync(id);
    }
}
