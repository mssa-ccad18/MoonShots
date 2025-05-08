using System;
using AmazingCalculatorLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingCalculatorLibrary.AdvancedTrackingFeatures
{
    internal class PersonalizedWorkoutSuggestions
    {
        private readonly UserProfiles userProfile;

        public PersonalizedWorkoutSuggestions(UserProfiles userProfile)
        {
            this.userProfile = userProfile ?? throw new ArgumentNullException(nameof(userProfile));
        }

        public List<string> GetWorkoutSuggestions()
        {
            // Calculate BMI using the BMI class
            var bmiCalculator = new BMI(
                userProfile.WeightInPounds ?? throw new InvalidOperationException("Weight is not set."),
                (userProfile.HeightInInches ?? 0) / 12, // Convert inches to feet
                (userProfile.HeightInInches ?? 0) % 12, // Remaining inches
                userProfile.IsMale
            );

            string bmiCategory = bmiCalculator.BMICategory;

            // Determine fitness level
            string fitnessLevel = userProfile.ActivityLevel switch
            {
                1 => "Low",
                2 => "Moderate",
                3 => "High",
                _ => "Unknown"
            };

            // Generate workout suggestions
            var suggestions = new List<string>();

            // Underweight
            if (bmiCategory == "UnderWeight" && fitnessLevel == "Low")
            {
                suggestions.Add("Strength training with light weights");
                suggestions.Add("Pilates for core strength");
                suggestions.Add("Walking with light resistance bands");
            }
            else if (bmiCategory == "UnderWeight" && fitnessLevel == "Moderate")
            {
                suggestions.Add("Strength training with moderate weights");
                suggestions.Add("Yoga for flexibility and balance");
                suggestions.Add("Cycling at a moderate pace");
            }
            else if (bmiCategory == "UnderWeight" && fitnessLevel == "High")
            {
                suggestions.Add("Strength training with heavy weights (focus on muscle gain)");
                suggestions.Add("Calisthenics (e.g., pull-ups, dips)");
                suggestions.Add("Short sprints for explosive power");
            }

            // Normal weight
            else if (bmiCategory == "Normal weight" && fitnessLevel == "Low")
            {
                suggestions.Add("Walking (20-30 minutes, 5 times a week)");
                suggestions.Add("Light yoga or stretching");
                suggestions.Add("Beginner bodyweight exercises (e.g., wall push-ups)");
            }
            else if (bmiCategory == "Normal weight" && fitnessLevel == "Moderate")
            {
                suggestions.Add("Jogging (20-30 minutes, 3-4 times a week)");
                suggestions.Add("Bodyweight exercises (e.g., push-ups, squats)");
                suggestions.Add("Cycling at a moderate pace");
            }
            else if (bmiCategory == "Normal weight" && fitnessLevel == "High")
            {
                suggestions.Add("Running (30-40 minutes, 4-5 times a week)");
                suggestions.Add("Advanced strength training (e.g., deadlifts, bench press)");
                suggestions.Add("Swimming laps at a fast pace");
            }

            // Overweight/Obese
            else if ((bmiCategory == "Overweight" || bmiCategory == "Obese") && fitnessLevel == "Low")
            {
                suggestions.Add("Walking (30 minutes, 5 times a week)");
                suggestions.Add("Yoga for beginners");
                suggestions.Add("Light stretching exercises");
            }
            else if ((bmiCategory == "Overweight" || bmiCategory == "Obese") && fitnessLevel == "Moderate")
            {
                suggestions.Add("Brisk walking (30-40 minutes, 4-5 times a week)");
                suggestions.Add("Low-impact aerobics");
                suggestions.Add("Strength training with light weights");
            }
            else if ((bmiCategory == "Overweight" || bmiCategory == "Obese") && fitnessLevel == "High")
            {
                suggestions.Add("High-intensity interval training (HIIT)");
                suggestions.Add("Strength training with moderate weights");
                suggestions.Add("Cycling at a vigorous pace");
            }

            // Default case
            else
            {
                suggestions.Add("Consult a fitness professional for personalized advice.");
            }

            return suggestions;
        }
    }
}
