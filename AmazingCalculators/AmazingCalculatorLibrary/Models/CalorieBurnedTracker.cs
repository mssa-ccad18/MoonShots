using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingCalculatorLibrary.Models
{
    class User
    {
        public double Weight { get; set; }
        public double Height { get; set; }
        public string ActivityLevel { get; set; }

        public User(double weight, double height, string activityLevel)
        {
            Weight = weight;
            Height = height;
            ActivityLevel = activityLevel.ToLower();
        }
    }
    class CalorieBurnedTracker
    {
        static void Main()
        {
            // Example user
            User user = new User(70, 175, "moderate"); // Assume weight in kg, height in cm

            double caloriesBurned = CalculateCalories(user);

            if (caloriesBurned > 0)
            {
                Console.WriteLine($"Estimated calories burned per hour: {caloriesBurned:F2} kcal");
            }
            else
            {
                Console.WriteLine("Invalid activity level assigned.");
            }
        }
        static double CalculateCalories(User user)
        {
            double metabolicEquivalent;

            switch (user.ActivityLevel)
            {
                case "light":
                    metabolicEquivalent = 3.0;
                    break;
                case "moderate":
                    metabolicEquivalent = 6.0;
                    break;
                case "intense":
                    metabolicEquivalent = 9.0;
                    break;
                default:
                    return -1; // Error case
            }
            return metabolicEquivalent * user.Weight;
        }
    }
}


