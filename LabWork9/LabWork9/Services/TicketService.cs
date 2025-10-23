using LabWork9.Contexts;
using LabWork9.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork9.Services
{
    public class TicketService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;
        public async Task<List<Ticket>> GetTicketsAsync()
            => await _context.Tickets.ToListAsync();

        public async Task AddTicketAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTicketAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket is null)
                throw new ArgumentException("Ticket is not found");
            ticket.Row = 1;
            ticket.Seat = 40;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(int id)
        {
            await _context.Tickets
                .Where(g => g.TicketId == id)
                .ExecuteDeleteAsync();
        }
    }
}
