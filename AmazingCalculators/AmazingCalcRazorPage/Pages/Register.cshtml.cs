using AmazingCalcRazorPage.ViewModels;
using AmazingCalculatorLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AmazingCalcRazorPage.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly FitnessDbContext _context;

        public RegisterModel(FitnessDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserCredentials Credentials { get; set; } = new();

        public string? Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (await _context.UserProfiles.AnyAsync(u => u.UserName == Credentials.UserName))
            {
                Message = "Username already exists.";
                return Page();
            }

            var newUser = new UserProfiles
            {
                UserName = Credentials.UserName,
                PasswordHash = HashPassword(Credentials.Password),
                Email = Credentials.Email
            };

            _context.UserProfiles.Add(newUser);
            await _context.SaveChangesAsync();
            Message = "Registration successful! You may now log in.";
            return Page();
        }

        private string HashPassword(string password) =>
            Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
    }
}
