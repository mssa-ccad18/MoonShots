using AmazingCalculatorLibrary.Models;
using AmazingCalculatorLibrary.MilitaryPhysicalTraining;

namespace AmazingCalculatorsTest;

[TestClass]
public class USATest
{
    [TestMethod]
    public void CalculateTotalScore_ShouldReturnCorrectScore()
    {
        // Arrange
        var userProfile = new UserProfiles
        {
            DateOfBirth = new DateTime(1990, 1, 1),
            UserName = "TestUser",
            PasswordHash = "hashedpassword"
        };
        var usa = new USA(userProfile);

        // Act
        int score = usa.CalculateTotalScore(180, 50, 90.0, 150, 14.0);

        // Assert
        Assert.AreEqual(500, score); // Maximum score
    }

    [TestMethod]
    public void GetFitnessResult_ShouldReturnPassForCombatRole()
    {
        // Arrange
        var userProfile = new UserProfiles { DateOfBirth = new DateTime(1990, 1, 1) };
        var usa = new USA(userProfile);

        // Act
        string result = usa.GetFitnessResult(180, 50, 90.0, 150, 14.0, "combat");

        // Assert
        Assert.AreEqual("Pass", result);
    }

    [TestMethod]
    public void GetFitnessResult_ShouldReturnFailForNonCombatRole()
    {
        // Arrange
        var userProfile = new UserProfiles { DateOfBirth = new DateTime(1990, 1, 1) };
        var usa = new USA(userProfile);

        // Act
        string result = usa.GetFitnessResult(120, 20, 160.0, 60, 20.0, "non-combat");

        // Assert
        Assert.AreEqual("Fail", result);
    }

    [TestMethod]
    public void GetAge_ShouldReturnCorrectAge()
    {
        // Arrange
        var userProfile = new UserProfiles { DateOfBirth = new DateTime(1990, 1, 1) };
        var usa = new USA(userProfile);

        // Act
        int age = usa.GetAge();

        // Assert
        Assert.AreEqual(DateTime.Today.Year - 1990, age);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void GetAge_ShouldThrowExceptionWhenDateOfBirthIsNull()
    {
        // Arrange
        var userProfile = new UserProfiles { DateOfBirth = null };
        var usa = new USA(userProfile);

        // Act
        usa.GetAge();
    }
}
