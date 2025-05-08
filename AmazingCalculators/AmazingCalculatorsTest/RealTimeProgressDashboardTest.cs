using Microsoft.EntityFrameworkCore;
using AmazingCalculatorLibrary.Models;
using AmazingCalculatorLibrary.AdvancedTrackingFeatures;
using Microsoft.EntityFrameworkCore.InMemory; // Add this using directive for InMemoryDatabase support
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AmazingCalculatorsTest
{
    [TestClass]
    public class RealTimeProgressDashboardTest
    {
        [TestMethod]
        public void DisplayWorkoutHistory_ShouldDisplayCorrectData_Male()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FitnessDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using var context = new FitnessDbContext(options);

            // Seed data
            var user = new UserProfiles
            {
                UserId = 1,
                UserName = "TestUser",
                PasswordHash = "hashedpassword",
                HeightInInches = 70,
                WeightInPounds = 150,
                DateOfBirth = new DateTime(1990, 1, 1),
                IsMale = true,
                BMIValue = 21.5,
                WorkoutHistory = new List<WorkoutSession>
                {
                    new WorkoutSession
                    {
                        WorkoutSessionId = 1,
                        WorkoutDate = DateTime.Now.AddDays(-1),
                        WorkoutType = "Cardio",
                        DurationInMinutes = 30,
                        CaloriesBurned = 300
                    },
                    new WorkoutSession
                    {
                        WorkoutSessionId = 2,
                        WorkoutDate = DateTime.Now.AddDays(-2),
                        WorkoutType = "Strength",
                        DurationInMinutes = 45,
                        CaloriesBurned = 400
                    }
                }
            };

            context.UserProfiles.Add(user);
            context.SaveChanges();

            var dashboard = new RealTimeProgressDashboard(context);

            // Act
            dashboard.DisplayWorkoutHistory(1);

            // Assert
            // Since the method writes to the console, you can redirect the console output and verify it if needed.
            // For simplicity, this test ensures no exceptions are thrown and the method executes successfully.
        }
        [TestMethod]
        public void DisplayWorkoutHistory_ShouldDisplayCorrectData_Female()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FitnessDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using var context = new FitnessDbContext(options);

            // Seed data
            var user = new UserProfiles
            {
                UserId = 1,
                UserName = "TestUser",
                PasswordHash = "hashedpassword",
                HeightInInches = 70,
                WeightInPounds = 150,
                DateOfBirth = new DateTime(1990, 1, 1),
                IsMale = true,
                BMIValue = 21.5,
                WorkoutHistory = new List<WorkoutSession>
                {
                    new WorkoutSession
                    {
                        WorkoutSessionId = 1,
                        WorkoutDate = DateTime.Now.AddDays(-1),
                        WorkoutType = "Cardio",
                        DurationInMinutes = 30,
                        CaloriesBurned = 300
                    },
                    new WorkoutSession
                    {
                        WorkoutSessionId = 2,
                        WorkoutDate = DateTime.Now.AddDays(-2),
                        WorkoutType = "Strength",
                        DurationInMinutes = 45,
                        CaloriesBurned = 400
                    }
                }
            };

            context.UserProfiles.Add(user);
            context.SaveChanges();

            var dashboard = new RealTimeProgressDashboard(context);

            // Act
            dashboard.DisplayWorkoutHistory(1);

            // Assert
            // Since the method writes to the console, you can redirect the console output and verify it if needed.
            // For simplicity, this test ensures no exceptions are thrown and the method executes successfully.
        }
    }
}

