using LabWork17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LabWork17.Pages.Sessions
{
    public class IndexModel(Contexts.CinemaDbContext context) : PageModel
    {
        private readonly Contexts.CinemaDbContext _context = context;

        private const int PageSize = 2;

        [BindProperty(SupportsGet = true)]
        public string? FilmTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public byte? Hall { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }

        public IList<Session> Session { get; set; } = default!;

        public async Task OnGetAsync()
        {
            ViewData["Halls"] = new SelectList(_context.Halls, "HallId", "CinemaHallNumber");

            var sessions = _context.Sessions
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .AsQueryable();

            if (!String.IsNullOrEmpty(FilmTitle))
                sessions = sessions.Where(m => m.Movie.Name.Contains(FilmTitle));

            switch (SortColumn)
            {
                case "price":
                    sessions = sessions.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    sessions = sessions.OrderByDescending(s => s.Price);
                    break;
            }

            if (Hall > 0)
                sessions = sessions.Where(h => h.Hall.HallId == Hall);

            //TotalPages = Math.Ceiling(sessions.Count() / PageSize);

            sessions = sessions.Skip((PageIndex - 1) * PageSize).Take(PageSize);

            Session = await sessions.ToListAsync();
        }
    }
}
