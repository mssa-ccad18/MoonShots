using AmazingCalculatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AmazingCalculatorLibrary.MilitaryPhysicalTraining
{
    public class USAF
    {
        public readonly FitnessDbContext _context;

        public USAF(FitnessDbContext context)
        {
            _context = context;
        }

        public void DisplayWorkoutHistory(int userId)
        {
            var userProfile = _context.UserProfiles
                .FirstOrDefault(u => u.UserId == userId);

            if (userProfile == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            var age = userProfile.DateOfBirth.HasValue
                ? DateTime.Now.Year - userProfile.DateOfBirth.Value.Year
                : 0;

            var isMale = userProfile.IsMale;
            var isFemale = !isMale;
        }

        public void USAFMalePRT(bool isMale, int age, int pushupReps, int situpReps, int crunchesReps, int plankTime, int runTime, int handReleaseReps, int HAMRReps)
        {
            string jsonFilePath = Path.Combine("MilitaryPhysicalTraining", "USAFjson.json");

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"The JSON file was not found at path: {jsonFilePath}");
            }

            string jsonString = File.ReadAllText(jsonFilePath);
            var fitnessData = JsonSerializer.Deserialize<USAFFitnessStandard>(jsonString);

            if (fitnessData == null || fitnessData.Male == null)
            {
                throw new InvalidOperationException("The JSON file contains invalid or empty data.");
            }

            if (age < 17)
            {
                Console.WriteLine("Table values do not exist for ages under 17.");
                return;
            }

            var ageGroup = GetAgeGroup(age);
            if (ageGroup == null || !fitnessData.Male.ContainsKey(ageGroup))
            {
                Console.WriteLine("No data available for this age group.");
                return;
            }

            var selectedRow = fitnessData.Male[ageGroup];

            int pushupPoints = GetPoints(selectedRow.OneMinPushups, pushupReps);
            int HAMRPoints = GetPoints(selectedRow.TwentyMeterHAMRShuffle, HAMRReps);
            int runPoints = GetPoints(selectedRow.OneAndOneHalfMileRun, runTime);
            int handReleasePoints = GetPoints(selectedRow.TwoMinHandReleasePushups, handReleaseReps);
            int situpPoints = GetPoints(selectedRow.OneMinSitups, situpReps);
            int crunchesPoints = GetPoints(selectedRow.TwoMinCrossLegReverseCrunch, crunchesReps);
            int plankPoints = GetPoints(selectedRow.Plank, plankTime);

            int higherUpperBodyPoints = Math.Max(pushupPoints, handReleasePoints);
            int higherCorePoints = Math.Max(crunchesPoints, Math.Max(plankPoints, situpPoints));
            int higherRunPoints = Math.Max(runPoints, HAMRPoints);

            Console.WriteLine($"Higher Upper Body Points (Pushups or Pull-ups): {higherUpperBodyPoints}");
            Console.WriteLine($"Higher Core Points (Crunches or Plank): {higherCorePoints}");
            Console.WriteLine($"Run Points: {higherRunPoints}");

            int totalPoints = higherUpperBodyPoints + higherCorePoints + higherRunPoints;
            Console.WriteLine($"Total Points: {totalPoints}");
        }

        public string GetAgeGroup(int age)
        {
            if (age >= 17 && age < 25) return "17-24";
            if (age >= 25 && age < 30) return "25-29";
            if (age >= 30 && age < 35) return "30-34";
            if (age >= 35 && age < 40) return "35-39";
            if (age >= 40 && age < 45) return "40-44";
            if (age >= 45 && age < 50) return "45-49";
            if (age >= 50 && age < 55) return "50-54";
            if (age >= 55 && age < 60) return "55-59";
            if (age >= 60) return "60+";
            return null;
        }

        public int GetPoints(Dictionary<string, double> exercise, int input)
        {
            if (exercise == null)
            {
                throw new ArgumentNullException(nameof(exercise), "The exercise object cannot be null.");
            }

            string inputTime = TimeSpan.FromSeconds(input).ToString(@"m\:ss");
            if (exercise.ContainsKey(inputTime))
            {
                return (int)exercise[inputTime];
            }

            return 0;
        }
    }
    public class USAFFitnessStandard
    {
        public Dictionary<string, AgeGroup> Male { get; set; } = new();
        public Dictionary<string, AgeGroup> Female { get; set; } = new();
    }

    public class AgeGroup
    {
        public Dictionary<string, double> OneAndOneHalfMileRun { get; set; } = new();
        public Dictionary<string, double> OneMinPushups { get; set; } = new();
        public Dictionary<string, double> OneMinSitups { get; set; } = new();
        public Dictionary<string, double> TwoMinHandReleasePushups { get; set; } = new();
        public Dictionary<string, double> TwoMinCrossLegReverseCrunch { get; set; } = new();
        public Dictionary<string, double> Plank { get; set; } = new();
        public Dictionary<string, double> TwentyMeterHAMRShuffle { get; set; } = new();
    }

    //public class USAFFitnessStandard
    //{
    //    public List<AFFitnessStandards> FitnessStandards { get; set; }
    //}

    public class AFFitnessStandards
    {
        public string ageGroup { get; set; }
        //public AFGenderFitness Male { get; set; }
        //public AFGenderFitness Female { get; set; }
    }

    public class AFGenderFitness
    {
        public AFExercise OneMinPushups { get; set; }
        public AFExercise TwoMinHandReleasePushups { get; set; }
        public AFExercise TwoMinCrossLegReverseCrunch { get; set; }
        public AFExercise TwentyMeterHAMRShuffle { get; set; }
        public AFExercise Plank { get; set; }
        public AFExercise OneAndOneHalfMileRun { get; set; }
        public AFExercise OneMinSitups { get; set; }

    }

    public class AFExercise
    {
        public List<string> times { get; set; }
        public List<int> Reps { get; set; }
        public List<int> Points { get; set; }
    }
}

