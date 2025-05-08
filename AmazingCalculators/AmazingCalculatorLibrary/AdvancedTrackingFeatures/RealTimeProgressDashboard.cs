using AmazingCalculatorLibrary.Models;
using Azure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingCalculatorLibrary.AdvancedTrackingFeatures
{
    public class RealTimeProgressDashboard
    {
        private readonly FitnessDbContext _context;

        public RealTimeProgressDashboard(FitnessDbContext context)
        {
            _context = context;
        }

        public static double GetBMIValue(UserProfiles bmi);
        {
            BMIValue = bmi;
        }

        // Properties to store progress data
        public double CurrentBMI { get; private set; }
        public string BMICategory { get; private set; }
        public double BMR { get; private set; }
        public int TotalCaloriesBurned { get; private set; }

        // Method to update progress data from the database
        public void UpdateProgress(int userId)
        {
            // Fetch user profile
            var userProfile = _context.UserProfiles.FirstOrDefault(u => u.UserId == userId);
            if (userProfile == null) throw new Exception("User not found.");

            // Calculate BMI
            var bmi = new BMI(userProfile.WeightInPounds,
                              Math.Floor(userProfile.HeightInInches / 12),
                              userProfile.HeightInInches % 12,
                              true); // Assuming male for simplicity
            CurrentBMI = bmi.BMIValue;
            BMICategory = bmi.BMICategory;

            // Calculate BMR (using Mifflin-St Jeor Equation for demonstration)
            BMR = CalculateBMR(userProfile.WeightInPounds, userProfile.HeightInInches, userProfile.DateOfBirth, true);

            // Calculate total calories burned from workout history
            TotalCaloriesBurned = _context.WorkoutSessions
                .Where(ws => ws.WorkoutDate >= DateTime.Now.AddMonths(-1) && ws.WorkoutType != null)
                .Sum(ws => ws.CaloriesBurned);
        }

        private double CalculateBMR(double weightLbs, double heightInches, DateTime dateOfBirth, bool isMale)
        {
            var age = DateTime.Now.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Now.AddYears(-age)) age--;

            if (isMale)
            {
                return 66 + (6.23 * weightLbs) + (12.7 * heightInches) - (6.8 * age);
            }
            else
            {
                return 655 + (4.35 * weightLbs) + (4.7 * heightInches) - (4.7 * age);
            }
        }
    }
    }


