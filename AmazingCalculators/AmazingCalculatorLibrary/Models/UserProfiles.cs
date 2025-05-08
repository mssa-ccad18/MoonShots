using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AmazingCalculatorLibrary.Models
{
    public class UserProfiles
    {
        [Key]
        public int UserId { get; set; }

        // Basic login + identity info
        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        // Optional but useful metadata
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public bool IsMale { get; set; }


        // Health and fitness data (optional at registration)
        public double? HeightInInches { get; set; }
        public double? WeightInPounds { get; set; }
        public double? BMIValue { get; set; } // Calculated from height and weight
        public string? BMICategory { get; set; } // e.g., Underweight, Normal weight, Overweight, Obesity


        public int? ActivityLevel { get; set; } // 1: Sedentary, etc.

        // Fitness Goals
        public string? GoalType { get; set; }
        public DateTime? GoalTargetDate { get; set; }

        // Nutrition Preferences
        public bool IsVegan { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsDairyFree { get; set; }

        // Workout Preferences
        public List<string> FavoriteWorkoutTypes { get; set; } = new();

        // Progress Tracking
        public List<WorkoutSession> WorkoutHistory { get; set; } = new();
    }
}
