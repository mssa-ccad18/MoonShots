using AmazingCalcRazorPage.ViewModels;
using AmazingCalculatorLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AmazingCalcRazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly FitnessDbContext _context;

        public IndexModel(FitnessDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserCredentials Credentials { get; set; } = new();

        public string? Message { get; set; }

        public async Task<IActionResult> OnPostAsync(string action)
        {
            if (action == "register")
            {
                if (await _context.UserProfiles.AnyAsync(u => u.UserName == Credentials.UserName))
                {
                    Message = "Username already exists.";
                    return Page();
                }

                var user = new UserProfiles
                {
                    UserName = Credentials.UserName,
                    PasswordHash = HashPassword(Credentials.Password),
                    Email = Credentials.Email,
                };

                _context.UserProfiles.Add(user);
                await _context.SaveChangesAsync();
                Message = "Registration successful! Please log in.";
                return Page();
            }

            if (action == "login")
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

            Message = "Unknown action.";
            return Page();
        }

        private string HashPassword(string password)
        {
            // NOTE: Replace this with a real hashing algorithm
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string plain, string hashed)
        {
            return HashPassword(plain) == hashed;
        }
    }
}
