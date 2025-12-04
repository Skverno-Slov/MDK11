using Lection1202.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lection1202.Pages
{
    public class LoginModel : PageModel
    {
        private readonly Lection1202.Contexts.GamesDbContext _context;

        public LoginModel(Lection1202.Contexts.GamesDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //_context.Users.Add(User);
            //await _context.SaveChangesAsync();

            var user = _context.Users
                //.Include(u => u.Role)
                .FirstOrDefault(u => u.Login == User.Login);
            if (user is null || user.Password != User.Password)
                return Page();

            HttpContext.Session.SetString("Role", user.Role);
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostGuest()
        {
            HttpContext.Session.SetString("Role", "Гость");
            return RedirectToPage("/Index");
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return Page();
        }
    }
}
