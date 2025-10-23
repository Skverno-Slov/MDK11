using Dapper;
using LabWork8.Model;
using System.Data;


namespace LabWork8.Repository
{
    public class GenreRepository(DatabaseContext databaseContext) : IRepository<Genre>
    {
        private readonly DatabaseContext _databaseContext = databaseContext;

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            return await connection
                .QueryAsync<Genre>("SELECT * FROM Genre");
        }

        public async Task<Genre?> GetByIdAsync(int id)
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            return await connection
                .QuerySingleOrDefaultAsync<Genre>("SELECT * FROM Genre WHERE GenreId = @GenreId", new { GenreId = id });
        }

        public Task<int> AddAsync(Genre entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Genre entity)
        {
            throw new NotImplementedException();
        }
    }
}
