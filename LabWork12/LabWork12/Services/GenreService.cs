using LabWork12.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LabWork12.Services
{
    public class GenreService(AppDbContext context)
    {
        readonly AppDbContext _context = context;

        public async Task<List<string>> GetGenresByMovieIdAsync(int movieId)
        {
            var parameter = new SqlParameter("@MovieId", movieId);

            return await _context.Database.SqlQueryRaw<string>(@"
                SELECT g.Name 
                FROM Genre g
                INNER JOIN MovieGenre mg ON g.Id = mg.GenreId
                WHERE mg.MovieId = @MovieId
                ORDER BY g.Name",
                parameter).ToListAsync();
        }
    }
}
