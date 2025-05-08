using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazingCalculatorLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazingCalculatorLibrary.Models
{
    public class UserProfiles
    {

        [Key]
        public int UserId { get; set; } // Unique identifier for the user profile




        // User details
        [Required]
        public string UserName { get; set; }
        ///ADDED a required attribute to the password hash
        [Required]
        public string PasswordHash { get; set; }
        /// ////
        
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }


        // Health and fitness data
        public double HeightInInches { get; set; }
        public double WeightInPounds { get; set; }
        public double BMIValue { get; set; }
        public string BMICategory { get; set; } // e.g., Underweight, Normal, Overweight, Obese
        public int ActivityLevel { get; set; } // 1: Sedentary, 2: Light, 3: Moderate, 4: Heavy

        //Fitness Goals
        public string GoalType { get; set; } // e.g., Weight Loss, Muscle Gain, Maintenance
        public DateTime GoalTargetDate { get; set; }

        //Nutrition Preferences
        public bool IsVegan { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsDairyFree { get; set; }

        // Workout Preferences
        public List<string> FavoriteWorkoutTypes { get; set; } = new List<string>(); // e.g., Cardio, Strength, Flexibility

        //Progress Tracking
        public List<WorkoutSession> WorkoutHistory { get; set; } = new List<WorkoutSession>();

    }
}
