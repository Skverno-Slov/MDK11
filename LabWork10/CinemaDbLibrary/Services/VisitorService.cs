using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaDbLibrary.Services
{
    public class VisitorService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Visitor>> GetVisitorsAsync()
            => await _context.Visitors.ToListAsync();

        public async Task<Visitor?> GetVisitorAsync(int id)
            => await _context.Visitors.FindAsync(id);
    }
}
