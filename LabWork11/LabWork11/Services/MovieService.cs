using LabWork11.Contexts;
using LabWork11.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace LabWork11.Services
{
    public class MovieService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public ObservableCollection<Movie> GetMovies()
            => _context.Movies.ToList();

        public ObservableCollection<string>? GetAgeRaitings()
            => _context.Movies.Select(x => x.AgeRating)
                .Distinct()
                .ToList();

        public async Task<ObservableCollection<Movie>> GetMoviesAsync()
            => await _context.Movies.ToListAsync();

        public async Task AddMovieAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveMoviesAsync(ObservableCollection<Movie> movies)
        {
            foreach (Movie movie in movies)
                _context.Movies.Remove(movie);

            await _context.SaveChangesAsync();
        }
    }
}
