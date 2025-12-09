using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LabWork17.Contexts;
using LabWork17.Models;

namespace LabWork17.Pages.Visitors
{
    public class DetailsModel : PageModel
    {
        private readonly LabWork17.Contexts.CinemaDbContext _context;

        public DetailsModel(LabWork17.Contexts.CinemaDbContext context)
        {
            _context = context;
        }

        public Visitor Visitor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var visitor = await _context.Visitors.FirstOrDefaultAsync(m => m.VisitorId == id);

            if (visitor is not null)
            {
                Visitor = visitor;

                return Page();
            }

            return NotFound();
        }
    }
}
