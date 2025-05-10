using AmazingCalculatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AmazingCalculatorLibrary.MilitaryPhysicalTraining
{
    public class USMC
    {
        public readonly FitnessDbContext _context;


        public USMC(FitnessDbContext context)
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
            string jsonString = File.ReadAllText("USMCJson.json");
            var fitnessData = JsonSerializer.Deserialize<USMCFitnessStandard>(jsonString);

            if (age < 17)
            {
                Console.WriteLine("Table values do not exist for ages under 17.");
                return;
            }

            // Prompt user for input
            Console.Write("Enter number of pushups: ");
            int pushupReps = int.Parse(Console.ReadLine());

            Console.Write("Enter number of pull-ups: ");
            int pullupReps = int.Parse(Console.ReadLine());

            Console.Write("Enter number of crunches: ");
            int crunchesReps = int.Parse(Console.ReadLine());

            Console.Write("Enter plank time (mm:ss): ");
            int plankTime = ParseTimeToSeconds(Console.ReadLine());

            Console.Write("Enter 3-mile run time (mm:ss): ");
            int runTime = ParseTimeToSeconds(Console.ReadLine());

            // Find the appropriate age group
            var selectedRow = fitnessData.FitnessStandards
                .FirstOrDefault(fs => fs.AgeGroup == GetAgeGroup(age));

            if (selectedRow == null)
            {
                Console.WriteLine("No data available for this age group.");
                return;
            }

            // Calculate points for each exercise
            int pushupPoints = GetPoints(selectedRow.Male.Pushups, pushupReps);
            int pullupPoints = GetPoints(selectedRow.Male.PullUps, pullupReps);
            int crunchesPoints = GetPoints(selectedRow.Male.Crunches, crunchesReps);
            int plankPoints = GetPoints(selectedRow.Male.Plank, plankTime);
            int runPoints = GetPoints(selectedRow.Male.ThreeMileRun, runTime);

            // Choose the higher score between pull-ups and pushups
            int higherUpperBodyPoints = Math.Max(pushupPoints, pullupPoints);

            // Choose the higher score between plank and crunches
            int higherCorePoints = Math.Max(crunchesPoints, plankPoints);

            // Display results
            Console.WriteLine($"Higher Upper Body Points (Pushups or Pull-ups): {higherUpperBodyPoints}");
            Console.WriteLine($"Higher Core Points (Crunches or Plank): {higherCorePoints}");
            Console.WriteLine($"Run Points: {runPoints}");

            int totalPoints = higherUpperBodyPoints + higherCorePoints + runPoints;
            Console.WriteLine($"Total Points: {totalPoints}");
        }
        public void USMCFemalePRT(bool isFemale, int age)
        {
            string jsonString = File.ReadAllText("USMCJson.json");
            var fitnessData = JsonSerializer.Deserialize<USMCFitnessStandard>(jsonString);

            if (age < 17)
            {
                Console.WriteLine("Table values do not exist for ages under 17.");
                return;
            }

            // Prompt user for input
            Console.Write("Enter number of pushups: ");
            int pushupReps = int.Parse(Console.ReadLine());

            Console.Write("Enter number of pull-ups: ");
            int pullupReps = int.Parse(Console.ReadLine());

            Console.Write("Enter number of crunches: ");
            int crunchesReps = int.Parse(Console.ReadLine());

            Console.Write("Enter plank time (mm:ss): ");
            int plankTime = ParseTimeToSeconds(Console.ReadLine());

            Console.Write("Enter 3-mile run time (mm:ss): ");
            int runTime = ParseTimeToSeconds(Console.ReadLine());

            // Find the appropriate age group
            var selectedRow = fitnessData.FitnessStandards
                .FirstOrDefault(fs => fs.AgeGroup == GetAgeGroup(age));

            if (selectedRow == null)
            {
                Console.WriteLine("No data available for this age group.");
                return;
            }

            // Calculate points for each exercise
            int pushupPoints = GetPoints(selectedRow.Female.Pushups, pushupReps);
            int pullupPoints = GetPoints(selectedRow.Female.PullUps, pullupReps);
            int crunchesPoints = GetPoints(selectedRow.Female.Crunches, crunchesReps);
            int plankPoints = GetPoints(selectedRow.Female.Plank, plankTime);
            int runPoints = GetPoints(selectedRow.Female.ThreeMileRun, runTime);

            // Choose the higher score between pull-ups and pushups
            int higherUpperBodyPoints = Math.Max(pushupPoints, pullupPoints);

            // Choose the higher score between plank and crunches
            int higherCorePoints = Math.Max(crunchesPoints, plankPoints);

            // Display results
            Console.WriteLine($"Higher Upper Body Points (Pushups or Pull-ups): {higherUpperBodyPoints}");
            Console.WriteLine($"Higher Core Points (Crunches or Plank): {higherCorePoints}");
            Console.WriteLine($"Run Points: {runPoints}");

            int totalPoints = higherUpperBodyPoints + higherCorePoints + runPoints;
            Console.WriteLine($"Total Points: {totalPoints}");
        }

        private string GetAgeGroup(int age)
        {
            if (age >= 17 && age < 21) return "17-20";
            if (age >= 21 && age < 26) return "21-25";
            if (age >= 26 && age < 31) return "26-30";
            if (age >= 31 && age < 36) return "31-35";
            if (age >= 36 && age < 41) return "36-40";
            if (age >= 41 && age < 46) return "41-45";
            if (age >= 46 && age < 51) return "46-50";
            if (age >= 51) return "51+";
            return null;
        }

        private int GetPoints(Exercise exercise, int input)
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


    public class USMCFitnessStandard
    {
        public List<FitnessStandard> FitnessStandards { get; set; }
    }

    public class FitnessStandard
    {
        public string AgeGroup { get; set; }
        public GenderFitness Male { get; set; }
        public GenderFitness Female { get; set; }
    }

    public class GenderFitness
    {
        public Exercise Pushups { get; set; }
        public Exercise PullUps { get; set; }
        public Exercise Crunches { get; set; }
        public Exercise ThreeMileRun { get; set; }
        public Exercise Plank { get; set; }

    }

    public class Exercise
    {
        public List<int> Reps { get; set; }
        public List<int> Points { get; set; }
    }
}

