using AmazingCalculatorLibrary.Models;

namespace AmazingCalculatorsTest;

[TestClass]
public class BMRTest
{
    [TestMethod]
    public void GetCalorieBurned_Male_ReturnsExpectedValue()
    {
        // Arrange
        var bmi = new BMI(weightLbs: 180, heightFeet: 5, heightInches: 10, ismale: true);
        int age = 30;
        // Calculation: 88.36 + (13.4 * 81) + (4.8 * 177.8) - (5.7 * 30)
        double expected = 88.36 + (13.4 * bmi.WeightKGs) + (4.8 * bmi.HeightInCm) - (5.7 * age);

        // Act
        double actual = BMRCalculator.GetCalorieBurned(bmi, age);

        // Assert
        Assert.AreEqual(expected, actual, 0.01, "BMR calculation for male is incorrect.");
    }

    [TestMethod]
    public void GetCalorieBurned_Female_ReturnsExpectedValue()
    {
        // Arrange
        var bmi = new BMI(weightLbs: 150, heightFeet: 5, heightInches: 5, ismale: false);
        int age = 28;
        // Calculation: 447.6 + (9.25 * 67.5) + (3.1 * 165.1) - (4.3 * 28)
        double expected = 447.6 + (9.25 * bmi.WeightKGs) + (3.1 * bmi.HeightInCm) - (4.3 * age);

        // Act
        double actual = BMRCalculator.GetCalorieBurned(bmi, age);

        // Assert
        Assert.AreEqual(expected, actual, 0.01, "BMR calculation for female is incorrect.");
    }
}
