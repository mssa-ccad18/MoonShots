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

        // For chart display
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

            //  BMI calculation and update
            if (UserProfile.WeightInPounds.HasValue && UserProfile.HeightInInches.HasValue)
            {
                var feet = (int)(UserProfile.HeightInInches.Value / 12);
                var inches = UserProfile.HeightInInches.Value % 12;

                var bmiCalc = new BMI(
                    UserProfile.WeightInPounds.Value,
                    feet,
                    inches,
                    UserProfile.IsMale
                );

                UserProfile.UpdateFromBMI(bmiCalc); // Automatically sets BMIValue and BMICategory
                BMI = UserProfile.BMIValue;

                await _context.SaveChangesAsync(); //  Save updates to DB
            }

            //  BMR calculation
            if (UserProfile.WeightInPounds.HasValue &&
                UserProfile.HeightInInches.HasValue &&
                UserProfile.DateOfBirth.HasValue)
            {
                var age = DateTime.Now.Year - UserProfile.DateOfBirth.Value.Year;
                if (UserProfile.DateOfBirth.Value.Date > DateTime.Today.AddYears(-age)) age--;

                BMR = UserProfile.IsMale
                    ? 66 + (6.23 * UserProfile.WeightInPounds.Value) + (12.7 * UserProfile.HeightInInches.Value) - (6.8 * age)
                    : 655 + (4.35 * UserProfile.WeightInPounds.Value) + (4.7 * UserProfile.HeightInInches.Value) - (4.7 * age);
            }

            //  Total calories burned
            TotalCaloriesBurned = UserProfile.WorkoutHistory?.Sum(w => w.CaloriesBurned) ?? 0;

            //  Chart data
            WorkoutDates = UserProfile.WorkoutHistory?
                .OrderBy(w => w.WorkoutDate)
                .Select(w => w.WorkoutDate.ToShortDateString())
                .ToList();

            CaloriesByDate = UserProfile.WorkoutHistory?
                .OrderBy(w => w.WorkoutDate)
                .Select(w => w.CaloriesBurned)
                .ToList();

            //  Workout suggestions
            if (UserProfile.ActivityLevel.HasValue &&
                UserProfile.WeightInPounds.HasValue &&
                UserProfile.HeightInInches.HasValue)
            {
                var suggester = new PersonalizedWorkoutSuggestions(UserProfile);
                WorkoutSuggestions = suggester.GetWorkoutSuggestions();
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
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

            // Extract form values
            var form = Request.Form;
            string workoutType = form["WorkoutType"];
            bool durationParsed = double.TryParse(form["DurationInMinutes"], out double duration);
            bool caloriesParsed = int.TryParse(form["CaloriesBurned"], out int calories);

            if (!durationParsed || !caloriesParsed || string.IsNullOrWhiteSpace(workoutType))
            {
                Message = "Please enter valid values for all workout fields.";
                return await OnGetAsync(); // Reload with message
            }

            // Create and add workout session
            var newWorkout = new WorkoutSession
            {
                WorkoutDate = DateTime.Now,
                WorkoutType = workoutType,
                DurationInMinutes = duration,
                CaloriesBurned = calories
            };

            UserProfile.WorkoutHistory.Add(newWorkout);
            await _context.SaveChangesAsync();

            // Reload page with updated data
            return RedirectToPage();
        }

    }
}
