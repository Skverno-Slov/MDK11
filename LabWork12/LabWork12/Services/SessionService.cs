using LabWork12.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LabWork12.Services
{
    public class SessionService(AppDbContext context)
    {
        readonly AppDbContext _context = context;

        public async Task<int> IncreasePricesByHallAsync(byte hallId, decimal amount)
            => await _context.Database.ExecuteSqlRawAsync(@"UPDATE Session 
                SET Price = Price + @p0 
                WHERE HallId = @p1",
                amount, hallId);

        public async Task<DateTime?> GetSessionDateTimeByTicketIdAsync(int ticketId)
        {
            var parameter = new SqlParameter("@TicketId", ticketId);

            var result = await _context.Database.SqlQueryRaw<DateTime>(@"
                SELECT s.StartDate 
                FROM Session s
                INNER JOIN Ticket t ON s.Id = t.SessionId
                WHERE t.Id = @TicketId",
                parameter).FirstOrDefaultAsync();

            return result;
        }
    }
}
