using AmazingCalculatorLibrary.Models;
using AmazingCalculatorLibrary.AdvancedTrackingFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AmazingCalcRazorPage.Pages
{
    public class RealTimeProgressModel : PageModel
    {
        private readonly FitnessDbContext _context;

        public RealTimeProgressModel(FitnessDbContext context)
        {
            _context = context;
        }

        public UserProfiles? UserProfile { get; set; }
        public string? Message { get; set; }

        public double? BMI { get; set; }
        public double? BMR { get; set; }
        public int TotalCaloriesBurned { get; set; }
        public List<string>? WorkoutSuggestions { get; set; }

        // For Chart
        public List<string>? WorkoutDates { get; set; }
        public List<int>? CaloriesByDate { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var username = TempData["LoggedInUser"] as string;
            if (string.IsNullOrEmpty(username))
                return RedirectToPage("/Login");

            TempData.Keep("LoggedInUser");

            UserProfile = await _context.UserProfiles
                .Include(u => u.WorkoutHistory)
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (UserProfile == null)
            {
                Message = "User profile not found.";
                return Page();
            }

            // Calculate BMI
            BMI = UserProfile.BMIValue;

            // Calculate BMR
            if (UserProfile.WeightInPounds.HasValue &&
                UserProfile.HeightInInches.HasValue &&
                UserProfile.DateOfBirth.HasValue)
            {
                var age = DateTime.Now.Year - UserProfile.DateOfBirth.Value.Year;
                BMR = UserProfile.IsMale
                    ? 66 + (6.23 * UserProfile.WeightInPounds.Value) + (12.7 * UserProfile.HeightInInches.Value) - (6.8 * age)
                    : 655 + (4.35 * UserProfile.WeightInPounds.Value) + (4.7 * UserProfile.HeightInInches.Value) - (4.7 * age);
            }

            // Total calories burned
            TotalCaloriesBurned = UserProfile.WorkoutHistory?.Sum(w => w.CaloriesBurned) ?? 0;

            // Chart data
            WorkoutDates = UserProfile.WorkoutHistory?
                .OrderBy(w => w.WorkoutDate)
                .Select(w => w.WorkoutDate.ToShortDateString())
                .ToList();

            CaloriesByDate = UserProfile.WorkoutHistory?
                .OrderBy(w => w.WorkoutDate)
                .Select(w => w.CaloriesBurned)
                .ToList();

            // Workout suggestions
            if (UserProfile.ActivityLevel.HasValue &&
                UserProfile.WeightInPounds.HasValue &&
                UserProfile.HeightInInches.HasValue)
            {
                var suggester = new PersonalizedWorkoutSuggestions(UserProfile);
                WorkoutSuggestions = suggester.GetWorkoutSuggestions();
            }

            return Page();
        }
    }
}
