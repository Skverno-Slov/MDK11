using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LabWork20Web.Contexts;
using LabWork20Web.Models;

namespace LabWork20Web.Pages.Frames
{
    public class CreateModel : PageModel
    {
        private readonly LabWork20Web.Contexts.CinemaDbContext _context;

        private const long MaxFileSize = 2 * 1024 * 1024;

        public CreateModel(LabWork20Web.Contexts.CinemaDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Name");
            return Page();
        }

        [BindProperty]
        public Frame Frame { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            if (file?.Length > 0)
            {
                if (file.Length > MaxFileSize) 
                {
                    return Page();
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "images", file.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                Frame.FileName = file.FileName;
            }

            _context.Frames.Add(Frame);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
