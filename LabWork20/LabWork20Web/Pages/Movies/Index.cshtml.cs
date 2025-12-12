using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LabWork20Web.Contexts;
using LabWork20Web.Models;

namespace LabWork20Web.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly LabWork20Web.Contexts.CinemaDbContext _context;

        public IndexModel(LabWork20Web.Contexts.CinemaDbContext context)
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
