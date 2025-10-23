using LabWork9.Contexts;
using LabWork9.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork9.Services
{
    public class VisitorService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;
        public async Task<List<Visitor>> GetVisitorAsync()
            => await _context.Visitors.ToListAsync();

        public async Task AddVisitorAsync(Visitor visitor)
        {
            await _context.Visitors.AddAsync(visitor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVisitorsAsync(int id)
        {
            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor is null)
                throw new ArgumentException("Visitor is not found");
            visitor.Name = "John Doe";
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVisitorAsync(int id)
        {
            await _context.Visitors
                .Where(g => g.VisitorId == id)
                .ExecuteDeleteAsync();
        }
    }
}
