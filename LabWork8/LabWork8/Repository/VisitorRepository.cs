
using Dapper;
using LabWork8.Model;
using System.Data;

namespace LabWork8.Repository
{
    public class VisitorRepository(DatabaseContext databaseContext) : IRepository<Visitor>
    {
        private readonly DatabaseContext _databaseContext = databaseContext;

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Visitor>> GetAllAsync()
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            return await connection
                .QueryAsync<Visitor>("SELECT * FROM Visitor");
        }

        public async Task<Visitor?> GetByIdAsync(int id)
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            return await connection
                .QuerySingleOrDefaultAsync<Visitor>("SELECT * FROM Visitor WHERE VisitorId = @VisitorId", new { VisitorId = id });
        }


        Task<int> IRepository<Visitor>.AddAsync(Visitor entity)
        {
            throw new NotImplementedException();
        }

        Task IRepository<Visitor>.UpdateAsync(Visitor entity)
        {
            throw new NotImplementedException();
        }
    }
}
