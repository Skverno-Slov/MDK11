using LabWork24.Contexts;
using LabWork24.DTOs;
using LabWork24.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork24.Services
{
    public class CinemaService(CinemaDbContext context)
    {
        readonly CinemaDbContext _context = context;

        public List<Ticket> GetTickets()
            => _context.Tickets.ToList();

        public TicketDto GetTicketById(int id)
        {
            var ticket = _context.Tickets
                .Include(s => s.Session)
                .ThenInclude(h => h.Hall)
                .Include(s => s.Session)
                .ThenInclude(m => m.Movie)
                .FirstOrDefault(t => t.TicketId == id);

            if (ticket is null)
                return null;

            return new TicketDto()
            {
                TicketId = ticket.TicketId,
                Name = ticket.Session.Movie.Name,
                StartDate = ticket.Session.StartDate,
                Cinema = ticket.Session.Hall.Cinema,
                HallNumber = ticket.Session.Hall.HallNumber,
                Row = ticket.Row,
                Seat = ticket.Seat
            };
        }
    }
}
