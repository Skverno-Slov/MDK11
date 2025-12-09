using LabWork17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LabWork17.Pages.Sessions
{
    public class EditModel(Contexts.CinemaDbContext context) : PageModel
    {
        private readonly Contexts.CinemaDbContext _context = context;

        [BindProperty]
        public Session Session { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .FirstOrDefaultAsync(m => m.SessionId == id);
            if (session is null)
            {
                return NotFound();
            }
            Session = session;
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "CinemaHallNumber");
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Session.Hall");
            ModelState.Remove("Session.Movie");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Session).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(Session.SessionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SessionExists(int id)
        {
            return _context.Sessions.Any(e => e.SessionId == id);
        }
    }
}
