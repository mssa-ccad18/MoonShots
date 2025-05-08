using AmazingCalculatorLibrary.Models;

namespace AmazingCalculatorsTest;

[TestClass]
public class BMITest
{
    [TestMethod]
    public void UpdateUserProfileWithBMI_ShouldUpdatePropertiesCorrectly()
    {
        // Arrange
        var bmi = new BMI(150, 5, 3, true); // Weight: 150 lbs, Height: 5'3", Male
        var userProfile = new UserProfiles
        {
            UserId = 1,
            UserName = "TestUser",
            WeightInPounds = null,
            HeightInInches = null,
            BMIValue = null,
            BMICategory = null
        };

        // Act
        userProfile.UpdateFromBMI(bmi);

        // Assert
        Assert.AreEqual(bmi.WeightLbs, userProfile.WeightInPounds, "WeightInPounds should match BMI.WeightLbs");
        Assert.AreEqual(bmi.HeightFeet * 12 + bmi.HeightInches, userProfile.HeightInInches, "HeightInInches should match BMI height in inches");
        Assert.AreEqual(bmi.BMIValue, userProfile.BMIValue, "BMIValue should match BMI.BMIValue");
        Assert.AreEqual(bmi.BMICategory, userProfile.BMICategory, "BMICategory should match BMI.BMICategory");
    }
}
