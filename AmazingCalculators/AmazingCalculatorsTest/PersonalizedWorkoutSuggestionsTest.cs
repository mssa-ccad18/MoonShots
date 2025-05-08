using AmazingCalculatorLibrary.AdvancedTrackingFeatures;
using AmazingCalculatorLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AmazingCalculatorsTest;

[TestClass]
public class PersonalizedWorkoutSuggestionsTest
{
    [TestMethod]
    public void TestUnderweightLowFitnessLevel()
    {
        // Arrange
        var userProfile = new UserProfiles
        {
            WeightInPounds = 100,
            HeightInInches = 70,
            IsMale = true,
            ActivityLevel = 1 // Low fitness level
        };
        var workoutSuggestions = new PersonalizedWorkoutSuggestions(userProfile);

        // Act
        var suggestions = workoutSuggestions.GetWorkoutSuggestions();

        // Assert
        CollectionAssert.AreEqual(
            new List<string>
            {
                "Strength training with light weights",
                "Pilates for core strength",
                "Walking with light resistance bands"
            },
            suggestions,
            "Suggestions for underweight with low fitness level are incorrect."
        );
    }

    [TestMethod]
    public void TestNormalWeightModerateFitnessLevel()
    {
        // Arrange
        var userProfile = new UserProfiles
        {
            WeightInPounds = 150,
            HeightInInches = 70,
            IsMale = true,
            ActivityLevel = 2 // Moderate fitness level
        };
        var workoutSuggestions = new PersonalizedWorkoutSuggestions(userProfile);

        // Act
        var suggestions = workoutSuggestions.GetWorkoutSuggestions();

        // Assert
        CollectionAssert.AreEqual(
            new List<string>
            {
                "Jogging (20-30 minutes, 3-4 times a week)",
                "Bodyweight exercises (e.g., push-ups, squats)",
                "Cycling at a moderate pace"
            },
            suggestions,
            "Suggestions for normal weight with moderate fitness level are incorrect."
        );
    }

    [TestMethod]
    public void TestOverweightHighFitnessLevel()
    {
        // Arrange
        var userProfile = new UserProfiles
        {
            WeightInPounds = 200,
            HeightInInches = 65,
            IsMale = true,
            ActivityLevel = 3 // High fitness level
        };
        var workoutSuggestions = new PersonalizedWorkoutSuggestions(userProfile);

        // Act
        var suggestions = workoutSuggestions.GetWorkoutSuggestions();

        // Assert
        CollectionAssert.AreEqual(
            new List<string>
            {
                "High-intensity interval training (HIIT)",
                "Strength training with moderate weights",
                "Cycling at a vigorous pace"
            },
            suggestions,
            "Suggestions for overweight with high fitness level are incorrect."
        );
    }

    [TestMethod]
    public void TestUnknownFitnessLevel()
    {
        // Arrange
        var userProfile = new UserProfiles
        {
            WeightInPounds = 150,
            HeightInInches = 70,
            IsMale = true,
            ActivityLevel = 99 // Unknown fitness level
        };
        var workoutSuggestions = new PersonalizedWorkoutSuggestions(userProfile);

        // Act
        var suggestions = workoutSuggestions.GetWorkoutSuggestions();

        // Assert
        CollectionAssert.AreEqual(
            new List<string>
            {
                "Consult a fitness professional for personalized advice."
            },
            suggestions,
            "Suggestions for unknown fitness level are incorrect."
        );
    }
}


