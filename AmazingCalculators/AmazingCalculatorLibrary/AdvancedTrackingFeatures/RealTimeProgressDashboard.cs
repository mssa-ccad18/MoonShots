
using AmazingCalculatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingCalculatorLibrary.AdvancedTrackingFeatures
{
    public class RealTimeProgressDashboard
    {
        // Method that reads the input from the WorkoutSession SQL database
        private readonly FitnessDbContext _context;


        public RealTimeProgressDashboard(FitnessDbContext context)
        {
            _context = context;
        }
        public void DisplayWorkoutHistory(int userId)
        {
            var userProfile = _context.UserProfiles
                .FirstOrDefault(u => u.UserId == userId);

            if (userProfile == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            // Access BMI
            var bmi = userProfile.BMIValue;

            // Calculate BMR (if not stored, you can calculate it here)
            // Example formula for BMR (Mifflin-St Jeor Equation):
            double? bmr = null;
            if (userProfile.HeightInInches.HasValue && userProfile.WeightInPounds.HasValue && userProfile.DateOfBirth.HasValue)
            {
                var age = DateTime.Now.Year - userProfile.DateOfBirth.Value.Year;
                if (userProfile.IsMale)
                {
                    bmr = 66 + (6.23 * userProfile.WeightInPounds.Value) + (12.7 * userProfile.HeightInInches.Value) - (6.8 * age);
                }
                else
                {
                    bmr = 655 + (4.35 * userProfile.WeightInPounds.Value) + (4.7 * userProfile.HeightInInches.Value) - (4.7 * age);
                }
            }

            // Calculate total calories burned from workout history
            var totalCaloriesBurned = userProfile.WorkoutHistory?.Sum(w => w.CaloriesBurned) ?? 0;

            // Access workout history
            var workouts = userProfile.WorkoutHistory;

            // Display the information
            Console.WriteLine($"BMI: {bmi}");
            Console.WriteLine($"BMR: {bmr}");
            Console.WriteLine($"Total Calories Burned: {totalCaloriesBurned}");
            Console.WriteLine("Workout History:");
            if (workouts != null)
            {
                foreach (var workout in workouts)
                {
                    Console.WriteLine($"- {workout.WorkoutDate.ToShortDateString()}: {workout.WorkoutType}, {workout.DurationInMinutes} mins, {workout.CaloriesBurned} calories");
                }
            }
            else
            {
                Console.WriteLine("No workouts found.");
            }
        }




         
    }
}


