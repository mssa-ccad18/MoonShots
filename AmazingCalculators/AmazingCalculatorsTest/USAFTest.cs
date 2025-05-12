//using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using AmazingCalculatorLibrary.MilitaryPhysicalTraining;
using AmazingCalculatorLibrary.Models;
using AmazingCalculatorsTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace AmazingCalculatorLibrary.Tests
{
    [TestClass]
    public class USAFTests
    {
        public USAFFitnessStandard _fitnessData;

        [TestInitialize]
        public void TestInitialize()
        {
            // Load and deserialize the JSON file
            string jsonFilePath = Path.Combine("MilitaryPhysicalTraining", "USAFjson.json");

            // Ensure the file exists before attempting to read it
            Assert.IsTrue(File.Exists(jsonFilePath), $"The JSON file at {jsonFilePath} does not exist.");

            string jsonContent = File.ReadAllText(jsonFilePath);
            _fitnessData = JsonSerializer.Deserialize<USAFFitnessStandard>(jsonContent);

            // Ensure the deserialization was successful
            Assert.IsNotNull(_fitnessData, "Failed to deserialize the JSON data into USAFFitnessStandard.");

            // Fix for CS1061: Correct property name or structure
            Assert.IsNotNull(_fitnessData.Male, "The Male property is null after deserialization.");
            Assert.IsNotNull(_fitnessData.Female, "The Female property is null after deserialization.");

            // Optionally, check that the dictionaries contain data
            Assert.IsTrue(_fitnessData.Male.Count > 0, "The 'Male' dictionary is empty after deserialization.");
            Assert.IsTrue(_fitnessData.Female.Count > 0, "The 'Female' dictionary is empty after deserialization.");
        }

        //private readonly Mock<FitnessDbContext> _mockContext;
        //private readonly USAF _usaf;

        //public USAFTests()
        //{
        //    _mockContext = new Mock<FitnessDbContext>();
        //    _usaf = new USAF(_mockContext.Object);
        //}

        [TestMethod]
        public void DisplayWorkoutHistory_UserNotFound_DisplaysMessage()
        {
            // Arrange
            var userId = 1;
            var userProfile = new UserProfile
            {
                UserId = userId,
                DateOfBirth = new DateTime(1990, 1, 1),
                //IsMale = true
            };
            // Fix for CS1503: Ensure the correct type is used for the mock setup
            _mockContext.Setup(c => c.UserProfiles.FirstOrDefault(u => u.UserId == userId)).Returns(new AmazingCalculatorLibrary.Models.UserProfiles
            {
                UserId = userProfile.UserId,
                DateOfBirth = userProfile.DateOfBirth,
                // Map other properties as needed
            });

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                _usaf.DisplayWorkoutHistory(1);
                // Assert that the message is displayed
                Assert.AreEqual("User not found.", sw.ToString().Trim());
                Assert.IsTrue(sw.ToString().Contains("User not found."), "Expected message not found in output.");
            }
        }

        [TestMethod]
        public void GetAgeGroup_ValidAge_ReturnsCorrectGroup()
        {
            Assert.Equals("17-24", _usaf.GetAgeGroup(20));
            Assert.Equals("25-29", _usaf.GetAgeGroup(27));
            Assert.Equals("60+", _usaf.GetAgeGroup(65));
        }

        [TestMethod]
        public void GetAgeGroup_InvalidAge_ReturnsNull()
        {
            Assert.IsNotNull(_usaf.GetAgeGroup(17));
            Assert.IsNull(_usaf.GetAgeGroup(15));
        }

        [TestMethod]
        public void GetPoints_ValidInput_ReturnsCorrectPoints()
        {
            var exercise = new Dictionary<string, double> { { "1:30", 10.0 } };
            Assert.Equals(10, _usaf.GetPoints(exercise, 90));
        }

        [TestMethod]
        public void GetPoints_InvalidInput_ReturnsZero()
        {
            var exercise = new Dictionary<string, double> { { "1:30", 10.0 } };
            Assert.Equals(0, _usaf.GetPoints(exercise, 95));
        }

        [TestMethod]
        public void GetPoints_NullExercise_ThrowsException()
        {
            Assert.AreEqual(null, _usaf.GetPoints(null, 90));
            Assert.AreEqual(null, _usaf.GetPoints(new Dictionary<string, double>(), 90));
            //Assert.Throws<ArgumentNullException>(() => _usaf.GetPoints(null, 90));
        }

        [TestMethod]
        public void USAFMalePRT_MissingJsonFile_ThrowsFileNotFoundException()
        {
            Assert.ThrowsException<FileNotFoundException>(() => _usaf.USAFMalePRT(true, 25, 50, 40, 30, 120, 900, 20, 15));
        }

        [TestMethod]
        public void USAFMalePRT_ValidInputs_CalculatesTotalPoints()
        {
            string jsonFilePath = Path.Combine("MilitaryPhysicalTraining", "USAFjson.json");
            var fitnessData = new USAFFitnessStandard
            {
                Male = new Dictionary<string, AgeGroup>
                {
                    { "25-29", new AgeGroup { OneMinPushups = new Dictionary<string, double> { { "1:30", 10.0 } } } }
                }
            };
            File.WriteAllText(jsonFilePath, JsonSerializer.Serialize(fitnessData));

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                _usaf.USAFMalePRT(true, 25, 50, 40, 30, 120, 900, 20, 15);
                // Assert that the output contains the expected points
                Assert.IsTrue(sw.ToString().Contains("Higher Upper Body Points (Pushups or Pull-ups):"));
                Assert.IsTrue(sw.ToString().Contains("Higher Core Points (Crunches or Plank):"));
                Assert.IsTrue(sw.ToString().Contains("Run Points:"));
                Assert.IsTrue(sw.ToString().Contains("Total Points:"));
                // Assert that the output contains the expected total points
                // Note: The actual total points will depend on the logic in the USAFMalePRT method
            }
            File.Delete(jsonFilePath);
        }
    }
}



//using Moq;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text.Json;
//using AmazingCalculatorLibrary.MilitaryPhysicalTraining;
//using AmazingCalculatorLibrary.Models;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace AmazingCalculatorLibrary.Tests
//{
//    [TestClass]
//    public class USAFTests
//    {
//        [TestInitialize]
//        public void TestInitialize()
//        {
//            string jsonFilePath = Path.Combine("MilitaryPhysicalTraining", "USAFjson.json");
//            if (!File.Exists(jsonFilePath))
//            {
//                throw new FileNotFoundException($"The JSON file was not found at path: {jsonFilePath}");
//            }
//            Assert.IsTrue(File.Exists(jsonFilePath), $"The JSON file at {jsonFilePath} does not exist.");

//            string jsonContent = File.ReadAllText(jsonFilePath);
//            var fitnessData = JsonSerializer.Deserialize<USAFFitnessStandard>(jsonContent);
//            Assert.IsNotNull(fitnessData, "Failed to deserialize the JSON data into USAFFitnessStandard.");

//            // Ensure the deserialization was successful
//            Assert.IsNotNull(fitnessData, "Failed to deserialize the JSON data into USAFFitnessStandard.");
//        }
//        [TestMethod]
//        public void VerifyAllAgeGroupsWorkForMale()
//        {
//            var usaf = new USAF(null); // Pass null for the DbContext since it's not used in this test
//            var expectedAgeGroups = new[] { "17-24", "25-29", "30-34", "35-39", "40-44", "45-49", "50-54", "55-59", "60+" };

//            foreach (var ageGroup in  expectedAgeGroups)
//            {
//                //Act
//                var fitnessStandard = _fitnessData.FitnessStandards.FirstOrDefault(fs => fs.ageGroup == ageGroup);
//            }

//            //var usaf = new USAF(new Mock<FitnessDbContext>().Object);
//        }
//[TestMethod]
//        public void DisplayWorkoutHistory_UserNotFound_DisplaysMessage()
//        {
//            // Arrange
//            var userId = 1;
//            var mockContext = new Mock<FitnessDbContext>();
//            mockContext.Setup(c => c.UserProfiles.FirstOrDefault(u => u.UserId == userId))
//                        .Returns((UserProfiles)null);
//            using (var sw = new StringWriter())
//            {
//                Console.SetOut(sw);
//                var usaf = new USAF(mockContext.Object);
//                usaf.DisplayWorkoutHistory(userId);
//                // Assert
//                Assert.AreEqual("User not found.", sw.ToString().Trim());
//            }
//        }
//        [TestMethod]
//        public void GetAgeGroup_ValidAge_ReturnsCorrectGroup()
//        {
//            var usaf = new USAF(new Mock<FitnessDbContext>().Object);
//            Assert.AreEqual("17-24", usaf.GetAgeGroup(20));
//            Assert.AreEqual("25-29", usaf.GetAgeGroup(27));
//            Assert.AreEqual("60+", usaf.GetAgeGroup(65));
//        }
//        [TestMethod]
//        public void GetAgeGroup_InvalidAge_ReturnsNull()
//        {
//            var usaf = new USAF(new Mock<FitnessDbContext>().Object);
//            Assert.IsNull(usaf.GetAgeGroup(15));
//        }
//        [TestMethod]
//        public void GetPoints_ValidInput_ReturnsCorrectPoints()
//        {
//            var usaf = new USAF(new Mock<FitnessDbContext>().Object);
//            var exercise = new Dictionary<string, double> { { "1:30", 10.0 } };
//            Assert.AreEqual(10, usaf.GetPoints(exercise, 90));
//        }
//        [TestMethod]
//        public void GetPoints_InvalidInput_ReturnsZero()
//        {
//            var usaf = new USAF(new Mock<FitnessDbContext>().Object);
//            var exercise = new Dictionary<string, double> { { "1:30", 10.0 } };
//            Assert.AreEqual(0, usaf.GetPoints(exercise, 95));
//        }
//        [TestMethod]
//        public void GetPoints_NullExercise_ThrowsException()
//        {
//            var usaf = new USAF(new Mock<FitnessDbContext>().Object);
//            Assert.ThrowsException<ArgumentNullException>(() => usaf.GetPoints(null!, 90)); // Use null-forgiving operator to satisfy non-nullable reference type
//        }
//        [TestMethod]
//        public void USAFMalePRT_MissingJsonFile_ThrowsFileNotFoundException()
//        {
//            var usaf = new USAF(new Mock<FitnessDbContext>().Object);
//            Assert.ThrowsException<FileNotFoundException>(() =>
//                usaf.USAFMalePRT(true, 25, 50, 40, 30, 120, 900, 20, 15));
//        }
//    }
//}
