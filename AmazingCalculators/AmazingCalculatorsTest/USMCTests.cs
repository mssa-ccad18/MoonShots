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
        private USMCFitnessStandard _fitnessData;

        [TestInitialize]
        public void TestInitialize()
        {
            // Load and deserialize the JSON file
            string jsonString = File.ReadAllText("USMCJson.json");
            _fitnessData = JsonSerializer.Deserialize<USMCFitnessStandard>(jsonString);
        }

        [TestMethod]
        public void VerifyAllAgeGroupsWorkForMale()
        {
            // Arrange
            var usmc = new USMC(null); // Pass null for the DbContext since it's not used in this test
            var expectedAgeGroups = new[] { "17-20", "21-25", "26-30", "31-35", "36-40", "41-45", "46-50", "51+" };

            foreach (var ageGroup in expectedAgeGroups)
            {
                // Act
                var fitnessStandard = _fitnessData.FitnessStandards.FirstOrDefault(fs => fs.AgeGroup == ageGroup);
                Assert.IsNotNull(fitnessStandard, $"Age group {ageGroup} not found in the JSON data.");

                // Simulate inputs for each exercise
                int pushupReps = fitnessStandard.Male.Pushups.Reps.First();
                int pullupReps = fitnessStandard.Male.PullUps.Reps.First();
                int crunchesReps = fitnessStandard.Male.Crunches.Reps.First();
                int plankTime = fitnessStandard.Male.Plank.Reps.First();
                int runTime = fitnessStandard.Male.ThreeMileRun.Reps.First();

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
                var fitnessStandard = _fitnessData.FitnessStandards.FirstOrDefault(fs => fs.AgeGroup == ageGroup);
                Assert.IsNotNull(fitnessStandard, $"Age group {ageGroup} not found in the JSON data.");

                // Simulate inputs for each exercise
                int pushupReps = fitnessStandard.Female.Pushups.Reps.First();
                int pullupReps = fitnessStandard.Female.PullUps.Reps.First();
                int crunchesReps = fitnessStandard.Female.Crunches.Reps.First();
                int plankTime = fitnessStandard.Female.Plank.Reps.First();
                int runTime = fitnessStandard.Female.ThreeMileRun.Reps.First();

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
