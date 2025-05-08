using Microsoft.VisualStudio.TestTools.UnitTesting;
using AmazingCalculatorLibrary.Models;
using System;

namespace AmazingCalculatorLibrary.Tests
{
    [TestClass]
    public class CalorieBurnedTrackerTest
    {
        [TestMethod]
        public void TestCaloriesBurnedPerHour_LightActivity()
        {
            // Arrange
            var userProfile = new UserProfiles
            {
                WeightInPounds = 154,
                ActivityLevel = 1 // Light activity
            };
            var tracker = new CalorieBurnedTracker(userProfile);

            // Act
            double caloriesBurned = tracker.CaloriesBurnedPerHour;

            // Assert
            Assert.AreEqual(462.0, caloriesBurned, 0.01, "Calories burned for light activity is incorrect.");
        }

        [TestMethod]
        public void TestCaloriesBurnedPerHour_ModerateActivity()
        {
            // Arrange
            var userProfile = new UserProfiles
            {
                WeightInPounds = 154,
                ActivityLevel = 2 // Moderate activity
            };
            var tracker = new CalorieBurnedTracker(userProfile);

            // Act
            double caloriesBurned = tracker.CaloriesBurnedPerHour;

            // Assert
            Assert.AreEqual(924.0, caloriesBurned, 0.01, "Calories burned for moderate activity is incorrect.");
        }

        [TestMethod]
        public void TestCaloriesBurnedPerHour_IntenseActivity()
        {
            // Arrange
            var userProfile = new UserProfiles
            {
                WeightInPounds = 154,
                ActivityLevel = 3 // Intense activity
            };
            var tracker = new CalorieBurnedTracker(userProfile);

            // Act
            double caloriesBurned = tracker.CaloriesBurnedPerHour;

            // Assert
            Assert.AreEqual(1386.0, caloriesBurned, 0.01, "Calories burned for intense activity is incorrect.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidActivityLevel_ThrowsException()
        {
            // Arrange
            var userProfile = new UserProfiles
            {
                WeightInPounds = 154,
                ActivityLevel = 99 // Invalid activity level
            };
            var tracker = new CalorieBurnedTracker(userProfile);

            // Act
            _ = tracker.CaloriesBurnedPerHour;

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void TestActivityCategory_LightActivity()
        {
            // Arrange
            var userProfile = new UserProfiles
            {
                WeightInPounds = 154,
                ActivityLevel = 1 // Light activity
            };
            var tracker = new CalorieBurnedTracker(userProfile);

            // Act
            string category = tracker.ActivityCategory;

            // Assert
            Assert.AreEqual("Light Activity", category, "Activity category for light activity is incorrect.");
        }

        [TestMethod]
        public void TestActivityCategory_UnknownActivity()
        {
            // Arrange
            var userProfile = new UserProfiles
            {
                WeightInPounds = 154,
                ActivityLevel = 99 // Invalid activity level
            };
            var tracker = new CalorieBurnedTracker(userProfile);

            // Act
            string category = tracker.ActivityCategory;

            // Assert
            Assert.AreEqual("Unknown Activity", category, "Activity category for unknown activity is incorrect.");
        }
    }
}
