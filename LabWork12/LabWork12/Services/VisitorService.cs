using LabWork12.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LabWork12.Services
{
    public class VisitorService(AppDbContext context)
    {
        readonly AppDbContext _context = context;

        public async Task<int> CreateVisitorByPhone(string phone)
        {
            var phoneParameter = new SqlParameter("@phone", phone);

            var idParameter = new SqlParameter("@id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database
                .ExecuteSqlRawAsync($"dbo.CreateVisitor @phone, @id OUT", phoneParameter, idParameter);

            return (int)idParameter.Value;
        }
    }
}
