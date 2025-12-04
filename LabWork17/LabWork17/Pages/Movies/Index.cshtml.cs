using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LabWork17.Contexts;
using LabWork17.Models;

namespace LabWork17.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly LabWork17.Contexts.CinemaDbContext _context;

        public IndexModel(LabWork17.Contexts.CinemaDbContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Movie = await _context.Movies.ToListAsync();
        }
    }
}
