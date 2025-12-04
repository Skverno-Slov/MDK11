using LabWork17.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LabWork17.Pages.Tickets
{
    public class IndexModel : PageModel
    {
        private readonly LabWork17.Contexts.CinemaDbContext _context;

        public IndexModel(LabWork17.Contexts.CinemaDbContext context)
        {
            _context = context;
        }

        public IList<Ticket> Ticket { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Ticket = await _context.Tickets
                .Include(t => t.Session)
                .Include(t => t.Visitor).ToListAsync();
        }
    }
}
