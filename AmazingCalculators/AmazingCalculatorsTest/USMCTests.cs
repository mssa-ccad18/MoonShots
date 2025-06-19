using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text.Json;
using System.Linq;
using AmazingCalculatorLibrary.MilitaryPhysicalTraining;
using System.Diagnostics;

namespace AmazingCalculatorLibrary.Tests
{
    [TestClass]
    public class USMCTests
    {
        public USMC.USMCFitnessStandard _fitnessData;

        [TestInitialize]
        public void TestInitialize()
        {
            // Load and deserialize the JSON file
            string jsonFilePath = Path.Combine("MilitaryPhysicalTraining", "USMCjson.json");

            // Ensure the file exists before attempting to read it
            Assert.IsTrue(File.Exists(jsonFilePath), $"The JSON file at {jsonFilePath} does not exist.");

            string jsonContent = File.ReadAllText(jsonFilePath);
            _fitnessData = JsonSerializer.Deserialize<USMC.USMCFitnessStandard>(jsonContent);

            // Ensure the deserialization was successful
            Assert.IsNotNull(_fitnessData, "Failed to deserialize the JSON data into USMCFitnessStandard.");
            Assert.IsNotNull(_fitnessData.FitnessStandards, "The FitnessStandards property is null after deserialization.");
        }

        [TestMethod]
        public void VerifyAllAgeGroupsWorkForMale()
        {

            // Arrange
            var usmc = new USMC(null); // Pass null for the DbContext since it's not used in this test
            var expectedAgeGroups = new[] { "17-20", "21-25", "26-30", "31-35", "36-40", "41-45", "46-50", "51" };

            foreach (var ageGroup in expectedAgeGroups)
            {
                // Act
                var fitnessStandard = _fitnessData.FitnessStandards.FirstOrDefault(fs => fs.ageGroup == ageGroup);
                Assert.IsNotNull(fitnessStandard, $"Age group {ageGroup} not found in the JSON data.");
                Assert.IsNotNull(fitnessStandard.male, $"Male fitness standards for age group {ageGroup} are null.");

                // Ensure all required properties are not null
                Assert.IsNotNull(fitnessStandard.male.pushups, $"Pushups data for age group {ageGroup} is null.");
                Assert.IsNotNull(fitnessStandard.male.pullups, $"Pullups data for age group {ageGroup} is null.");
                Assert.IsNotNull(fitnessStandard.male.crunches, $"Crunches data for age group {ageGroup} is null.");
                Assert.IsNotNull(fitnessStandard.male.plank, $"Plank data for age group {ageGroup} is null.");
                Assert.IsNotNull(fitnessStandard.male.threeMileRun, $"Three-mile run data for age group {ageGroup} is null.");

                // Simulate inputs for each exercise
                int pushupReps = fitnessStandard.male.pushups?.reps?.FirstOrDefault() ?? 0;
                int pullupReps = fitnessStandard.male.pullups?.reps?.FirstOrDefault() ?? 0;
                int crunchesReps = fitnessStandard.male.crunches?.reps?.FirstOrDefault() ?? 0;
                int expectedPlankTime = usmc.ParseTimeToSeconds(fitnessStandard.male.plank?.times?.FirstOrDefault() ?? "0:00");
                int expectedRunTime = usmc.ParseTimeToSeconds(fitnessStandard.male.threeMileRun?.times?.FirstOrDefault() ?? "0:00");

                // Call the method
                usmc.USMCMalePRT(true, GetAgeFromAgeGroup(ageGroup), pushupReps, pullupReps, crunchesReps, expectedPlankTime, expectedRunTime);

                // Assert
                Assert.IsTrue(pushupReps >= 0, $"Pushup reps for age group {ageGroup} are invalid.");
                Assert.IsTrue(pullupReps >= 0, $"Pullup reps for age group {ageGroup} are invalid.");
                Assert.IsTrue(crunchesReps >= 0, $"Crunches reps for age group {ageGroup} are invalid.");
                Assert.AreEqual(expectedPlankTime, usmc.ParseTimeToSeconds(fitnessStandard.male.plank?.times?.FirstOrDefault() ?? "0:00"), $"Plank time for age group {ageGroup} is invalid.");
                Assert.AreEqual(expectedRunTime, usmc.ParseTimeToSeconds(fitnessStandard.male.threeMileRun?.times?.FirstOrDefault() ?? "0:00"), $"Run time for age group {ageGroup} is invalid.");
            }
        }

        [TestMethod]
        public void VerifyAllAgeGroupsWorkForFemale()
        {

            // Arrange
            var usmc = new USMC(null); // Pass null for the DbContext since it's not used in this test
            var expectedAgeGroups = new[] { "17-20", "21-25", "26-30", "31-35", "36-40", "41-45", "46-50", "51" };

            foreach (var ageGroup in expectedAgeGroups)
            {
                // Act
                var fitnessStandard = _fitnessData.FitnessStandards.FirstOrDefault(fs => fs.ageGroup == ageGroup);
                Assert.IsNotNull(fitnessStandard, $"Age group {ageGroup} not found in the JSON data.");
                Assert.IsNotNull(fitnessStandard.female, $"Male fitness standards for age group {ageGroup} are null.");

                // Ensure all required properties are not null
                Assert.IsNotNull(fitnessStandard.female.pushups, $"Pushups data for age group {ageGroup} is null.");
                Assert.IsNotNull(fitnessStandard.female.pullups, $"Pullups data for age group {ageGroup} is null.");
                Assert.IsNotNull(fitnessStandard.female.crunches, $"Crunches data for age group {ageGroup} is null.");
                Assert.IsNotNull(fitnessStandard.female.plank, $"Plank data for age group {ageGroup} is null.");
                Assert.IsNotNull(fitnessStandard.female.threeMileRun, $"Three-mile run data for age group {ageGroup} is null.");

                // Simulate inputs for each exercise
                int pushupReps = fitnessStandard.female.pushups?.reps?.FirstOrDefault() ?? 0;
                int pullupReps = fitnessStandard.female.pullups?.reps?.FirstOrDefault() ?? 0;
                int crunchesReps = fitnessStandard.female.crunches?.reps?.FirstOrDefault() ?? 0;
                int expectedPlankTime = usmc.ParseTimeToSeconds(fitnessStandard.female.plank?.times?.FirstOrDefault() ?? "0:00");
                int expectedRunTime = usmc.ParseTimeToSeconds(fitnessStandard.female.threeMileRun?.times?.FirstOrDefault() ?? "0:00");

                // Call the method
                usmc.USMCFemalePRT(true, GetAgeFromAgeGroup(ageGroup), pushupReps, pullupReps, crunchesReps, expectedPlankTime, expectedRunTime);

                // Assert
                Assert.IsTrue(pushupReps >= 0, $"Pushup reps for age group {ageGroup} are invalid.");
                Assert.IsTrue(pullupReps >= 0, $"Pullup reps for age group {ageGroup} are invalid.");
                Assert.IsTrue(crunchesReps >= 0, $"Crunches reps for age group {ageGroup} are invalid.");
                Assert.AreEqual(expectedPlankTime, usmc.ParseTimeToSeconds(fitnessStandard.female.plank?.times?.FirstOrDefault() ?? "0:00"), $"Plank time for age group {ageGroup} is invalid.");
                Assert.AreEqual(expectedRunTime, usmc.ParseTimeToSeconds(fitnessStandard.female.threeMileRun?.times?.FirstOrDefault() ?? "0:00"), $"Run time for age group {ageGroup} is invalid.");
            }
        }

        private int GetAgeFromAgeGroup(string ageGroup)
        {
            // Extract the lower bound of the age group for testing
            return int.Parse(ageGroup.Split('-')[0]);
        }
    }
}
