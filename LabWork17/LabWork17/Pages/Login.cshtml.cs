using LabWork17.Contexts;
using LabWork17.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabWork17.Pages
{
    public class LoginModel : PageModel
    {
        private readonly LabWork17.Contexts.CinemaDbContext _context;

        public LoginModel(LabWork17.Contexts.CinemaDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RoleId"] = new SelectList(_context.CinemaUserRoles, "RoleId", "RoleId");
            return Page();
        }

        [BindProperty]
        public CinemaUser CinemaUser { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var user = _context.CinemaUsers
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Login == CinemaUser.Login);
            if (user is null || BCrypt.Net.BCrypt.EnhancedVerify(CinemaUser.HashPassword, user.HashPassword))
                return Page();

            HttpContext.Session.SetString("Login", user.Login);
            HttpContext.Session.SetString("Role", user.Role.Name);
            return RedirectToPage("/Movies/Index");
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostGuest()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.SetString("Role", "Гость");
            return RedirectToPage("/Movies/Index");
        }
    }
}
