using AmazingCalcRazorPage.ViewModels;
using AmazingCalculatorLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AmazingCalcRazorPage.Pages
{
    public class LoginModel : PageModel
    {
        private readonly FitnessDbContext _context;

        public LoginModel(FitnessDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserCredentials Credentials { get; set; } = new();

        public string? Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _context.UserProfiles
                .FirstOrDefaultAsync(u => u.UserName == Credentials.UserName);

            if (user != null && VerifyPassword(Credentials.Password, user.PasswordHash))
            {
                TempData["LoggedInUser"] = user.UserName;
                return RedirectToPage("/HomePage");
            }

            Message = "Invalid username or password.";
            return Page();
        }

        private bool VerifyPassword(string plain, string hashed) =>
            Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plain)) == hashed;
    }
}
