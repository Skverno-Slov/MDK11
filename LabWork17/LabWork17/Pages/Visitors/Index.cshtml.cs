using LabWork17.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LabWork17.Pages.Visitors
{
    public class IndexModel(Contexts.CinemaDbContext context) : PageModel
    {
        private readonly Contexts.CinemaDbContext _context = context;

        public IList<Visitor> Visitor { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Visitor = await _context.Visitors.ToListAsync();
        }
    }
}
