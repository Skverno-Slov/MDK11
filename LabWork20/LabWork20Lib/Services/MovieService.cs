using LabWork20Lib.Contexts;
using LabWork20Lib.Models;

namespace LabWork20Lib.Services
{
    public class MovieService(CinemaDbContext context)
    {
        CinemaDbContext _context = context;

        public List<Movie> GetMovies()
            => _context.Movies.ToList();

        public Movie GetMovie(int id)
            => _context.Movies.FirstOrDefault(m => m.MovieId == id);

        public void SaveFrameImage(int movieId, string fileName)
        {
            var frame = new Frame()
            {
                MovieId = movieId,
                FileName = fileName
            };

            _context.Frames.Add(frame);
            _context.SaveChanges();
        }
    }
}
