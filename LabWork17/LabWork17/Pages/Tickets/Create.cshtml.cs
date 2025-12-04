using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LabWork17.Contexts;
using LabWork17.Models;

namespace LabWork17.Pages.Tickets
{
    public class CreateModel : PageModel
    {
        private readonly LabWork17.Contexts.CinemaDbContext _context;

        public CreateModel(LabWork17.Contexts.CinemaDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["SessionId"] = new SelectList(_context.Sessions, "SessionId", "SessionId");
        ViewData["VisitorId"] = new SelectList(_context.Visitors, "VisitorId", "VisitorId");
            return Page();
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Tickets.Add(Ticket);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
