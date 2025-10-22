using LabWork12.Contexts;
using LabWork12.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork12.Services
{
    public class TicketService(AppDbContext context)
    {
        readonly AppDbContext _context = context;

        public async Task<DateTime> GetSessionDateTimeByTicketIdAsync(int ticketId)
            => await _context.Database
                .SqlQuery<DateTime>($"""
                select StartDate as value
                from Ticket as t 
                join Session on t.SessionId = Session.SessionId 
                where t.TicketId = {ticketId}
                """)
                .FirstOrDefaultAsync();

        public async Task<List<Ticket>> GetTicketsByPhoneAsync(string phone)
            => await _context.Tickets
                .FromSql($"dbo.GetVisitorTicketsByPhone {phone}")
                .ToListAsync();
    }
}
