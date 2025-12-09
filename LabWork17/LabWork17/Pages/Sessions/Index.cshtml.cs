using LabWork17.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LabWork17.Pages.Sessions
{
    public class IndexModel : PageModel
    {
        private readonly LabWork17.Contexts.CinemaDbContext _context;

        public IndexModel(LabWork17.Contexts.CinemaDbContext context)
        {
            _context = context;
        }

        public IList<Session> Session { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Session = await _context.Sessions
                .Include(s => s.Hall)
                .Include(s => s.Movie).ToListAsync();
        }
    }
}
