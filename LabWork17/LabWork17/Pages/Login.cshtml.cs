using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LabWork17.Contexts;
using LabWork17.Models;
using Microsoft.EntityFrameworkCore;

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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CinemaUsers.Add(CinemaUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public IActionResult OnPostLogin()
        {
            var user = _context.CinemaUsers
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Login == CinemaUser.Login);
            if (user is null || BCrypt.Net.BCrypt.EnhancedHashPassword(user.HashPassword, 5, BCrypt.Net.HashType.SHA512)
                != CinemaUser.HashPassword)
                return Page();

            HttpContext.Session.SetString("Role", user.Role.Name);
            return RedirectToPage("/Index");
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
            return RedirectToPage("./Movies/Index");
        }
    }
}
