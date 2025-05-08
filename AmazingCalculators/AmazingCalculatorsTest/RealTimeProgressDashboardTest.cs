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
            var optionsMale = new DbContextOptionsBuilder<FitnessDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseMale")
                .Options;

            using var contextMale = new FitnessDbContext(optionsMale);

            // Seed data
            var userMale = new UserProfiles
            {
                UserId = 1,
                UserName = "TestUserMale",
                PasswordHash = "hashedpassword1",
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

            contextMale.UserProfiles.Add(userMale);
            contextMale.SaveChanges();

            var dashboard = new RealTimeProgressDashboard(contextMale);

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
            var optionsFemale = new DbContextOptionsBuilder<FitnessDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseFemale")
                .Options;

            using var contextFemale = new FitnessDbContext(optionsFemale);

            // Seed data
            var userFemale = new UserProfiles
            {
                UserId = 2,
                UserName = "TestUserFemale",
                PasswordHash = "hashedpassword2",
                HeightInInches = 70,
                WeightInPounds = 150,
                DateOfBirth = new DateTime(1990, 1, 1),
                IsMale = false,
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

            contextFemale.UserProfiles.Add(userFemale);
            contextFemale.SaveChanges();

            var dashboard = new RealTimeProgressDashboard(contextFemale);

            // Act
            dashboard.DisplayWorkoutHistory(2);

            // Assert
            // Since the method writes to the console, you can redirect the console output and verify it if needed.
            // For simplicity, this test ensures no exceptions are thrown and the method executes successfully.
        }

        [TestMethod]
        public void TestDataFromBMIToUserProfileInRealTimeProgressDashboard()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FitnessDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseBMIToUserProfile")
                .Options;

            using var context = new FitnessDbContext(options);

            // Create a BMI object with test data
            var bmi = new BMI(150, 5, 3, true); // Weight: 150 lbs, Height: 5'3", Male

            // Create a UserProfiles object and add it to the database
            var userProfile = new UserProfiles
            {
                UserId = 1,
                UserName = "TestUser",
                PasswordHash = "hashedpassword",
                HeightInInches = null,
                WeightInPounds = null,
                BMIValue = null,
                BMICategory = null
            };

            context.UserProfiles.Add(userProfile);
            context.SaveChanges();

            // Create the RealTimeProgressDashboard instance
            var dashboard = new RealTimeProgressDashboard(context);

            // Act
            userProfile.UpdateFromBMI(bmi); // Update the UserProfiles object with BMI data
            context.SaveChanges(); // Save changes to the database

            // Retrieve the updated UserProfiles object
            var updatedUserProfile = context.UserProfiles.FirstOrDefault(u => u.UserId == 1);

            // Assert
            Assert.IsNotNull(updatedUserProfile, "UserProfile should not be null.");
            Assert.AreEqual(bmi.WeightLbs, updatedUserProfile.WeightInPounds, "WeightInPounds should match BMI.WeightLbs.");
            Assert.AreEqual(bmi.HeightFeet * 12 + bmi.HeightInches, updatedUserProfile.HeightInInches, "HeightInInches should match BMI height in inches.");
            Assert.AreEqual(bmi.BMIValue, updatedUserProfile.BMIValue, "BMIValue should match BMI.BMIValue.");
            Assert.AreEqual(bmi.BMICategory, updatedUserProfile.BMICategory, "BMICategory should match BMI.BMICategory.");
        }


    }
}

