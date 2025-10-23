using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaDbLibrary.Services
{
    public class TicketService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Ticket>> GetTicketsAsync()
            => await _context.Tickets.ToListAsync();

        public async Task<Ticket?> GetTicketAsync(int id) 
            => await _context.Tickets.FindAsync(id);
    }
}
