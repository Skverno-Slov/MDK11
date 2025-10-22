using LabWork12.Contexts;
using LabWork12.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork12.Services
{
    public class MovieService(AppDbContext context)
    {
        readonly AppDbContext _context = context;

        public async Task<List<Movie>> GetMoviesAsync(string sortColumn)
            => await _context.Movies
                .FromSqlRaw($"""
                select * 
                from Movie 
                order by {sortColumn}
                """)
                .ToListAsync();

        public async Task<List<Movie>> GetMoviesByNameAndYearAsync(string title,
                                                                   short year)
            => await _context.Movies
                .FromSql($"""
                select * 
                from Movie 
                where Name = {title} and Year >= {year}
                """)
                .ToListAsync();

        public async Task<List<string>> GetGenresByMovieIdAsync(int movieId)
            => await _context.Database
                .SqlQuery<string>($"""
                select g.Name 
                from Genre as g 
                join MovieGenre as mg on g.GenreId = mg.GenreId 
                where mg.MovieId = {movieId}
                """)
                .ToListAsync();

        public async Task<List<Movie>> GetMovieFromRangeAsync(char startLetter,
                                                              char endLetter)
            => await _context.Movies
                    .Where(f => EF.Functions.Like(f.Name,
                    $"[{startLetter}-{endLetter}]%"))
                    .ToListAsync();
    }
}
