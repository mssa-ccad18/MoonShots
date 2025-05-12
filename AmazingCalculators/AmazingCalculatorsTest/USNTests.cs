using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text.Json;
using System.Linq;
using AmazingCalculatorLibrary.MilitaryPhysicalTraining;
using System.Diagnostics;

namespace AmazingCalculatorLibrary.Tests
{
    [TestClass]
    public class USNTests
    {
        public USNFitnessStandard _fitnessData;

      

        [TestMethod]
        public void VerifyAllAgeGroupsWorkForUSNMale()
        {
            // Load and deserialize the JSON file
            string jsonFilePath = Path.Combine("MilitaryPhysicalTraining", "USNjson.json");

            // Ensure the file exists before attempting to read it
            Assert.IsTrue(File.Exists(jsonFilePath), $"The JSON file at {jsonFilePath} does not exist.");

            string jsonContent = File.ReadAllText(jsonFilePath);
            _fitnessData = JsonSerializer.Deserialize<USNFitnessStandard>(jsonContent);

            // Arrange
            var usn = new USN(null); // Pass null for the DbContext since it's not used in this test
            var expectedAgeGroups = new[] { "17-19", "20-24", "25-29", "30-34", "35-39", "40-44", "45-49", "50" };

            foreach (var ageGroup in expectedAgeGroups)
            {
                // Act
                var fitnessStandard = _fitnessData.USNavyFitnessStandards.FirstOrDefault(fs => fs.ageGroup == ageGroup);
                Assert.IsNotNull(fitnessStandard, $"Age group {ageGroup} not found in the JSON data.");
                Assert.IsNotNull(fitnessStandard.male, $"Male fitness standards for age group {ageGroup} are null.");

                // Ensure all required properties are not null
                Assert.IsNotNull(fitnessStandard.male.pushups, $"Pushups data for age group {ageGroup} is null.");
                Assert.IsNotNull(fitnessStandard.male.plank, $"Plank data for age group {ageGroup} is null.");
                Assert.IsNotNull(fitnessStandard.male.oneAndHalfMileRun, $"1.5-mile run data for age group {ageGroup} is null.");

                // Simulate inputs for each exercise
                int pushupReps = fitnessStandard.male.pushups?.reps?.FirstOrDefault() ?? 0;
                int expectedPlankTime = ParseTimeToSeconds(fitnessStandard.male.plank?.times?.FirstOrDefault() ?? "0:00");
                int expectedRunTime = ParseTimeToSeconds(fitnessStandard.male.oneAndHalfMileRun?.times?.FirstOrDefault() ?? "0:00");

                // Call the method
                usn.USNPRT(true, GetAgeFromAgeGroup(ageGroup), pushupReps, expectedPlankTime, expectedRunTime);

                // Assert
                Assert.IsTrue(pushupReps >= 0, $"Pushup reps for age group {ageGroup} are invalid.");
                Assert.IsTrue(expectedPlankTime >= 0, $"Plank time for age group {ageGroup} is invalid.");
                Assert.IsTrue(expectedRunTime >= 0, $"Run time for age group {ageGroup} is invalid.");
            }
        }


        [TestMethod]
        public void VerifyAllAgeGroupsWorkForUSNFemale()
        {

            // Load and deserialize the JSON file
            string jsonFilePath = Path.Combine("MilitaryPhysicalTraining", "USNjson.json");

            // Ensure the file exists before attempting to read it
            Assert.IsTrue(File.Exists(jsonFilePath), $"The JSON file at {jsonFilePath} does not exist.");

            string jsonContent = File.ReadAllText(jsonFilePath);
            _fitnessData = JsonSerializer.Deserialize<USNFitnessStandard>(jsonContent);

            // Arrange
            var usn = new USN(null); // Pass null for the DbContext since it's not used in this test
            var expectedAgeGroups = new[] { "17-19", "20-24", "25-29", "30-34", "35-39", "40-44", "45-49", "50" };

            foreach (var ageGroup in expectedAgeGroups)
            {
                // Act
                var fitnessStandard = _fitnessData.USNavyFitnessStandards.FirstOrDefault(fs => fs.ageGroup == ageGroup);
                Assert.IsNotNull(fitnessStandard, $"Age group {ageGroup} not found in the JSON data.");
                Assert.IsNotNull(fitnessStandard.female, $"Female fitness standards for age group {ageGroup} are null.");

                // Ensure all required properties are not null
                Assert.IsNotNull(fitnessStandard.female.pushups, $"Pushups data for age group {ageGroup} is null.");
                Assert.IsNotNull(fitnessStandard.female.plank, $"Plank data for age group {ageGroup} is null.");
                Assert.IsNotNull(fitnessStandard.female.oneAndHalfMileRun, $"1.5-mile run data for age group {ageGroup} is null.");

                // Simulate inputs for each exercise
                int pushupReps = fitnessStandard.female.pushups?.reps?.FirstOrDefault() ?? 0;
                int expectedPlankTime = ParseTimeToSeconds(fitnessStandard.female.plank?.times?.FirstOrDefault() ?? "0:00");
                int expectedRunTime = ParseTimeToSeconds(fitnessStandard.female.oneAndHalfMileRun?.times?.FirstOrDefault() ?? "0:00");

                // Call the method
                usn.USNPRT(false, GetAgeFromAgeGroup(ageGroup), pushupReps, expectedPlankTime, expectedRunTime);

                // Assert
                Assert.IsTrue(pushupReps >= 0, $"Pushup reps for age group {ageGroup} are invalid.");
                Assert.IsTrue(expectedPlankTime >= 0, $"Plank time for age group {ageGroup} is invalid.");
                Assert.IsTrue(expectedRunTime >= 0, $"Run time for age group {ageGroup} is invalid.");
            }
        }

        private int GetAgeFromAgeGroup(string ageGroup)
        {
            // Extract the lower bound of the age group for testing
            return int.Parse(ageGroup.Split('-')[0].Replace("+", ""));
        }

        private int ParseTimeToSeconds(string time)
        {
            if (TimeSpan.TryParseExact(time, @"m\:ss", null, out var result))
            {
                return (int)result.TotalSeconds;
            }
            return 0;
        }



    }
}
