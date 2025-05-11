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

        public void USMCMalePRT(bool isMale, int age)
        {
            string jsonString = Path.Combine("MilitaryPhysicalTraining", "USAFjson.json");
            var fitnessData = JsonSerializer.Deserialize<USAFFitnessStandard>(jsonString);

            if (age < 17)
            {
                Console.WriteLine("Table values do not exist for ages under 17.");
                return;
            }

            // Prompt user for input
            Console.Write("Enter number of pushups: ");
            int pushupReps = int.Parse(Console.ReadLine());

            Console.Write("Enter number of situps: ");
            int situpReps = int.Parse(Console.ReadLine());

            Console.Write("Enter number of crunches: ");
            int crunchesReps = int.Parse(Console.ReadLine());

            Console.Write("Enter plank time (mm:ss): ");
            int plankTime = ParseTimeToSeconds(Console.ReadLine());

            Console.Write("Enter 1.5 mile run time (mm:ss): ");
            int runTime = ParseTimeToSeconds(Console.ReadLine());

            Console.Write("Enter number of hand release pushups: ");
            int handReleaseReps = ParseTimeToSeconds(Console.ReadLine());

            // Find the appropriate age group
            var selectedRow = fitnessData.FitnessStandards
                .FirstOrDefault(fs => fs.AgeGroup == GetAgeGroup(age));

            if (selectedRow == null)
            {
                Console.WriteLine("No data available for this age group.");
                return;
            }

            // Calculate points for each exercise
            int pushupPoints = GetPoints(selectedRow.Male.OneMinPushups, pushupReps);
            int HAMRPoints = GetPoints(selectedRow.Male.TwentyMeterHAMRShuffle, runTime);
            int runPoints = GetPoints(selectedRow.Male.OneAndOneHalfMileRun, runTime);
            int handReleasePoints = GetPoints(selectedRow.Male.TwoMinHandReleasePushups, handReleaseReps);
            int situpPoints = GetPoints(selectedRow.Male.OneMinSitups, situpReps);
            int crunchesPoints = GetPoints(selectedRow.Male.TwoMinCrossLegReverseCrunch, crunchesReps);
            int plankPoints = GetPoints(selectedRow.Male.Plank, plankTime);


            // Choose the higher score between pull-ups and pushups
            int higherUpperBodyPoints = Math.Max(pushupPoints, handReleasePoints);

            // Choose the higher score between plank and crunches
            int higherCorePoints = Math.Max(crunchesPoints, Math.Max(plankPoints, situpPoints));

            // Choose the higher score between run and HAMR
            int higherRunPoints = Math.Max(runPoints, HAMRPoints);

            // Display results
            Console.WriteLine($"Higher Upper Body Points (Pushups or Pull-ups): {higherUpperBodyPoints}");
            Console.WriteLine($"Higher Core Points (Crunches or Plank): {higherCorePoints}");
            Console.WriteLine($"Run Points: {higherRunPoints}");

            int totalPoints = higherUpperBodyPoints + higherCorePoints + higherRunPoints;
            Console.WriteLine($"Total Points: {totalPoints}");
        }
        public void USMCFemalePRT(bool isFemale, int age)
        {
            string jsonString = Path.Combine("MilitaryPhysicalTraining", "USAFjson.json");
            var fitnessData = JsonSerializer.Deserialize<USAFFitnessStandard>(jsonString);

            if (age < 17)
            {
                Console.WriteLine("Table values do not exist for ages under 17.");
                return;
            }

            // Prompt user for input
            Console.Write("Enter number of pushups: ");
            int pushupReps = int.Parse(Console.ReadLine());

            Console.Write("Enter number of situps: ");
            int situpReps = int.Parse(Console.ReadLine());

            Console.Write("Enter number of crunches: ");
            int crunchesReps = int.Parse(Console.ReadLine());

            Console.Write("Enter plank time (mm:ss): ");
            int plankTime = ParseTimeToSeconds(Console.ReadLine());

            Console.Write("Enter 1.5 mile run time (mm:ss): ");
            int runTime = ParseTimeToSeconds(Console.ReadLine());

            Console.Write("Enter number of hand release pushups: ");
            int handReleaseReps = ParseTimeToSeconds(Console.ReadLine());

            // Find the appropriate age group
            var selectedRow = fitnessData.FitnessStandards
                .FirstOrDefault(fs => fs.AgeGroup == GetAgeGroup(age));

            if (selectedRow == null)
            {
                Console.WriteLine("No data available for this age group.");
                return;
            }

            // Calculate points for each exercise
            int pushupPoints = GetPoints(selectedRow.Female.OneMinPushups, pushupReps);
            int HAMRPoints = GetPoints(selectedRow.Female.TwentyMeterHAMRShuffle, runTime);
            int runPoints = GetPoints(selectedRow.Female.OneAndOneHalfMileRun, runTime);
            int handReleasePoints = GetPoints(selectedRow.Female.TwoMinHandReleasePushups, handReleaseReps);
            int situpPoints = GetPoints(selectedRow.Female.OneMinSitups, situpReps);
            int crunchesPoints = GetPoints(selectedRow.Female.TwoMinCrossLegReverseCrunch, crunchesReps);
            int plankPoints = GetPoints(selectedRow.Female.Plank, plankTime);

            // Choose the higher score between pull-ups and pushups
            int higherUpperBodyPoints = Math.Max(pushupPoints, handReleasePoints);

            // Choose the higher score between plank and crunches
            int higherCorePoints = Math.Max(crunchesPoints, Math.Max(plankPoints, situpPoints));

            // Choose the higher score between run and HAMR
            int higherRunPoints = Math.Max(runPoints, HAMRPoints);

            // Display results
            Console.WriteLine($"Higher Upper Body Points (Pushups or Pull-ups): {higherUpperBodyPoints}");
            Console.WriteLine($"Higher Core Points (Crunches or Plank): {higherCorePoints}");
            Console.WriteLine($"Run Points: {higherRunPoints}");

            int totalPoints = higherUpperBodyPoints + higherCorePoints + higherRunPoints;
            Console.WriteLine($"Total Points: {totalPoints}");
        }

        private string GetAgeGroup(int age)
        {
            if (age >= 17 && age < 25) return "17-24";
            if (age >= 25 && age < 30) return "25-29";
            if (age >= 30 && age < 35) return "30-34";
            if (age >= 35 && age < 40) return "35-39";
            if (age >= 40 && age < 45) return "40-44";
            if (age >= 45 && age < 50) return "45-49";
            if (age >= 50 && age < 55) return "50-54";
            if (age >= 55 && age < 60) return "50-59";
            if (age >= 60) return "60+";
            return null;
        }

        private int GetPoints(AFExercise exercise, int input)
        {
            int index = exercise.Reps.IndexOf(input);
            return index != -1 ? exercise.Points[index] : 0;
        }

        private int ParseTimeToSeconds(string time)
        {
            var parts = time.Split(':');
            if (parts.Length != 2 || !int.TryParse(parts[0], out int minutes) || !int.TryParse(parts[1], out int seconds))
            {
                throw new FormatException("Invalid time format. Please enter time as mm:ss.");
            }
            return (minutes * 60) + seconds;
        }

    }


    public class USAFFitnessStandard
    {
        public List<AFFitnessStandards> FitnessStandards { get; set; }
    }

    public class AFFitnessStandards
    {
        public string AgeGroup { get; set; }
        public AFGenderFitness Male { get; set; }
        public AFGenderFitness Female { get; set; }
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
        public List<int> Reps { get; set; }
        public List<int> Points { get; set; }
    }
}

