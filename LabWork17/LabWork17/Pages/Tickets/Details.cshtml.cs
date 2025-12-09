using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LabWork17.Contexts;
using LabWork17.Models;

namespace LabWork17.Pages.Tickets
{
    public class DetailsModel : PageModel
    {
        private readonly LabWork17.Contexts.CinemaDbContext _context;

        public DetailsModel(LabWork17.Contexts.CinemaDbContext context)
        {
            _context = context;
        }

        public Ticket Ticket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.TicketId == id);

            if (ticket is not null)
            {
                Ticket = ticket;

                return Page();
            }

            return NotFound();
        }
    }
}
