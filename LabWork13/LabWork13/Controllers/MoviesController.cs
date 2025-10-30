using LabWork13CinemaLibrary.Contexts;
using LabWork13CinemaLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabWork13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController(CinemaDbContext context) : ControllerBase
    {
        private readonly CinemaDbContext _context = context;
        const int PageSize = 3;

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        [HttpGet("pages")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesWithPagination(
            [FromQuery] string? sortBy = null,
            [FromQuery] int page = 1)
        {
            var movies = _context.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                movies = sortBy?.ToLower() switch
                {
                    "name" => movies.OrderBy(m => m.Name),
                    "year" => movies.OrderBy(m => m.Year),
                    "year_desc" => movies.OrderByDescending(m => m.Year),
                    _ => movies
                };
            }

            movies = movies
                .Skip(PageSize * (page - 1))
                .Take(PageSize);

            return await movies.ToListAsync();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetFilteredMovies(
            [FromQuery] string? name = null,
            [FromQuery] int? year = null)
        {
            var movies = _context.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                movies = _context.Movies
                    .Where(m => m.Name.Contains(name));

            if (year.HasValue)
                movies = _context.Movies
                    .Where(m => m.Year == year);

            return await movies.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpGet("{id}/genres")]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenresByMovieId(int id)
        {
            var movie = await _context.Movies
               .Include(m => m.Genres)
               .FirstOrDefaultAsync(m => m.MovieId == id);

            if (movie is null)
                return NotFound();

            return movie.Genres.ToList();
        }

        [HttpGet("{id}/sessions")]
        public async Task<ActionResult<IEnumerable<Session>>> GetSessionsByMovieId(int id)
        {
            var movie = await _context.Movies
               .Include(m => m.Sessions)
               .FirstOrDefaultAsync(m => m.MovieId == id);

            if (movie is null)
                return NotFound();

            return movie.Sessions
                .Where(s => s.StartDate > DateTime.Now)
                .ToList();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovieByYearsAndGenres(string year, string genre)
        {
            var years = year.Split('-');
            if (years.Length != 2)
                return BadRequest();

            int minYear, maxYear;
            if (!Int32.TryParse(years[0], out minYear) ||
                !Int32.TryParse(years[1], out maxYear))
                return BadRequest();

            var genres = genre.Split(",");
            if (genres.Length == 0)
                return BadRequest();

            return await _context.Movies
                .Where(m => m.Year >= minYear && m.Year <= maxYear)
                .ToListAsync();
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.MovieId)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.MovieId }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }
    }
}
