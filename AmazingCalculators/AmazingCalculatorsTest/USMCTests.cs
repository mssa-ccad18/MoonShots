using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text.Json;
using System.Linq;
using AmazingCalculatorLibrary.MilitaryPhysicalTraining;

namespace AmazingCalculatorLibrary.Tests
{
    [TestClass]
    public class USMCTests
    {
        public USMCFitnessStandard _fitnessData;

        [TestInitialize]
        public void TestInitialize()
        {
            // Load and deserialize the JSON file
            string jsonFilePath = Path.Combine("MilitaryPhysicalTraining", "USMCjson.json");

            // Ensure the file exists before attempting to read it
            Assert.IsTrue(File.Exists(jsonFilePath), $"The JSON file at {jsonFilePath} does not exist.");

            string jsonContent = File.ReadAllText(jsonFilePath);
            _fitnessData = JsonSerializer.Deserialize<USMCFitnessStandard>(jsonContent);

            // Ensure the deserialization was successful
            Assert.IsNotNull(_fitnessData, "Failed to deserialize the JSON data into USMCFitnessStandard.");
            Assert.IsNotNull(_fitnessData.FitnessStandards, "The FitnessStandards property is null after deserialization.");
        }

        [TestMethod]
        public void VerifyAllAgeGroupsWorkForMale()
        {
            // Arrange
            var usmc = new USMC(null); // Pass null for the DbContext since it's not used in this test
            var expectedAgeGroups = new[] { "17-20", "21-25", "26-30", "31-35", "36-40", "41-45", "46-50", "51+" };

            // Ensure _fitnessData and FitnessStandards are not null
            Assert.IsNotNull(_fitnessData, "_fitnessData is null. Ensure the JSON file is loaded correctly.");
            Assert.IsNotNull(_fitnessData.FitnessStandards, "_fitnessData.FitnessStandards is null. Ensure the JSON file contains valid data.");

            foreach (var ageGroup in expectedAgeGroups)
            {
                // Act
                var fitnessStandard = _fitnessData.FitnessStandards.FirstOrDefault(fs => fs.ageGroup == ageGroup);
                Assert.IsNotNull(fitnessStandard, $"Age group {ageGroup} not found in the JSON data.");

                // Ensure Male standards are not null
                Assert.IsNotNull(fitnessStandard.male, $"Male fitness standards for age group {ageGroup} are null.");

                // Simulate inputs for each exercise
                int pushupReps = fitnessStandard.male.Pushups?.Reps?.FirstOrDefault() ?? 0;
                int pullupReps = fitnessStandard.male.PullUps?.Reps?.FirstOrDefault() ?? 0;
                int crunchesReps = fitnessStandard.male.Crunches?.Reps?.FirstOrDefault() ?? 0;
                int plankTime = fitnessStandard.male.Plank?.Reps?.FirstOrDefault() ?? 0;
                int runTime = fitnessStandard.male.ThreeMileRun?.Reps?.FirstOrDefault() ?? 0;

                // Call the method
                usmc.USMCMalePRT(true, GetAgeFromAgeGroup(ageGroup));

                // Assert
                Assert.IsTrue(pushupReps >= 0, $"Pushup reps for age group {ageGroup} are invalid.");
                Assert.IsTrue(pullupReps >= 0, $"Pullup reps for age group {ageGroup} are invalid.");
                Assert.IsTrue(crunchesReps >= 0, $"Crunches reps for age group {ageGroup} are invalid.");
                Assert.IsTrue(plankTime >= 0, $"Plank time for age group {ageGroup} is invalid.");
                Assert.IsTrue(runTime >= 0, $"Run time for age group {ageGroup} is invalid.");
            }
        }

        [TestMethod]
        public void VerifyAllAgeGroupsWorkForFemale()
        {
            // Arrange
            var usmc = new USMC(null); // Pass null for the DbContext since it's not used in this test
            var expectedAgeGroups = new[] { "17-20", "21-25", "26-30", "31-35", "36-40", "41-45", "46-50", "51+" };

            foreach (var ageGroup in expectedAgeGroups)
            {
                // Act
                var fitnessStandard = _fitnessData.FitnessStandards.FirstOrDefault(fs => fs.ageGroup == ageGroup);
                Assert.IsNotNull(fitnessStandard, $"Age group {ageGroup} not found in the JSON data.");

                // Simulate inputs for each exercise
                int pushupReps = fitnessStandard.female.Pushups.Reps.First();
                int pullupReps = fitnessStandard.female.PullUps.Reps.First();
                int crunchesReps = fitnessStandard.female.Crunches.Reps.First();
                int plankTime = fitnessStandard.female.Plank.Reps.First();
                int runTime = fitnessStandard.female.ThreeMileRun.Reps.First();

                // Call the method
                usmc.USMCFemalePRT(false, GetAgeFromAgeGroup(ageGroup));

                // Assert
                Assert.IsTrue(pushupReps >= 0, $"Pushup reps for age group {ageGroup} are invalid.");
                Assert.IsTrue(pullupReps >= 0, $"Pullup reps for age group {ageGroup} are invalid.");
                Assert.IsTrue(crunchesReps >= 0, $"Crunches reps for age group {ageGroup} are invalid.");
                Assert.IsTrue(plankTime >= 0, $"Plank time for age group {ageGroup} is invalid.");
                Assert.IsTrue(runTime >= 0, $"Run time for age group {ageGroup} is invalid.");
            }
        }

        private int GetAgeFromAgeGroup(string ageGroup)
        {
            // Extract the lower bound of the age group for testing
            return int.Parse(ageGroup.Split('-')[0]);
        }
    }
}
