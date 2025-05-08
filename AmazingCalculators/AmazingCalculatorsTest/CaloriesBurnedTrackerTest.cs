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
            var tracker = new CalorieBurnedTracker(70, 175, true, "light");

            // Act
            double caloriesBurned = tracker.CaloriesBurnedPerHour;

            // Assert
            Assert.AreEqual(210.0, caloriesBurned, 0.01, "Calories burned for light activity is incorrect.");
        }

        [TestMethod]
        public void TestCaloriesBurnedPerHour_ModerateActivity()
        {
            // Arrange
            var tracker = new CalorieBurnedTracker(70, 175, true, "moderate");

            // Act
            double caloriesBurned = tracker.CaloriesBurnedPerHour;

            // Assert
            Assert.AreEqual(420.0, caloriesBurned, 0.01, "Calories burned for moderate activity is incorrect.");
        }

        [TestMethod]
        public void TestCaloriesBurnedPerHour_IntenseActivity()
        {
            // Arrange
            var tracker = new CalorieBurnedTracker(70, 175, true, "intense");

            // Act
            double caloriesBurned = tracker.CaloriesBurnedPerHour;

            // Assert
            Assert.AreEqual(630.0, caloriesBurned, 0.01, "Calories burned for intense activity is incorrect.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidActivityLevel_ThrowsException()
        {
            // Arrange
            var tracker = new CalorieBurnedTracker(70, 175, true, "invalid");

            // Act
            _ = tracker.CaloriesBurnedPerHour;

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void TestActivityCategory_LightActivity()
        {
            // Arrange
            var tracker = new CalorieBurnedTracker(70, 175, true, "light");

            // Act
            string category = tracker.ActivityCategory;

            // Assert
            Assert.AreEqual("Light Activity", category, "Activity category for light activity is incorrect.");
        }

        [TestMethod]
        public void TestActivityCategory_UnknownActivity()
        {
            // Arrange
            var tracker = new CalorieBurnedTracker(70, 175, true, "unknown");

            // Act
            string category = tracker.ActivityCategory;

            // Assert
            Assert.AreEqual("Unknown Activity", category, "Activity category for unknown activity is incorrect.");
        }
    }
}
