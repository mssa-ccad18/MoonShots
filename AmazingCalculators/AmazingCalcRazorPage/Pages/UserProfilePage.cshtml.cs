using AmazingCalculatorLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AmazingCalcRazorPage.Pages
{
    public class UserProfilePageModel : PageModel
    {
        private readonly FitnessDbContext _context;

        public UserProfilePageModel(FitnessDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserProfiles UserProfile { get; set; }

        public string? Message { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var username = TempData["LoggedInUser"] as string;
            if (string.IsNullOrEmpty(username))
                return RedirectToPage("/Login");

            TempData.Keep("LoggedInUser");

            UserProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (UserProfile == null)
            {
                Message = "User not found.";
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var username = TempData["LoggedInUser"] as string;
            if (string.IsNullOrEmpty(username))
                return RedirectToPage("/Login");

            TempData.Keep("LoggedInUser");

            var user = await _context.UserProfiles.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                Message = "User not found.";
                return Page();
            }

            // Update only editable fields
            user.FirstName = UserProfile.FirstName;
            user.LastName = UserProfile.LastName;
            user.DateOfBirth = UserProfile.DateOfBirth;
            user.HeightInInches = UserProfile.HeightInInches;
            user.WeightInPounds = UserProfile.WeightInPounds;
            user.ActivityLevel = UserProfile.ActivityLevel;
            user.GoalType = UserProfile.GoalType;
            user.GoalTargetDate = UserProfile.GoalTargetDate;
            user.IsVegan = UserProfile.IsVegan;
            user.IsVegetarian = UserProfile.IsVegetarian;
            user.IsGlutenFree = UserProfile.IsGlutenFree;
            user.IsDairyFree = UserProfile.IsDairyFree;

            await _context.SaveChangesAsync();
            Message = "Profile updated successfully!";
            return Page();
        }
    }
}