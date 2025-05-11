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

        public void USMCMalePRT(bool isMale, int age, int pushupReps, int pullupReps, int crunchesReps, int plankTime, int runTime)
        {
            // Resolve the full path to the JSON file
            string jsonFilePath = Path.Combine("MilitaryPhysicalTraining", "USMCjson.json");

            // Check if the file exists
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"The JSON file was not found at path: {jsonFilePath}");
            }

            // Read and deserialize the JSON file
            string jsonString = File.ReadAllText(jsonFilePath);
            var fitnessData = JsonSerializer.Deserialize<USMCFitnessStandard>(jsonString);

            if (fitnessData == null || fitnessData.FitnessStandards == null)
            {
                throw new InvalidOperationException("The JSON file contains invalid or empty data.");
            }

            if (age < 17)
            {
                Console.WriteLine("Table values do not exist for ages under 17.");
                return;
            }

            // Find the appropriate age group
            var selectedRow = fitnessData.FitnessStandards
                .FirstOrDefault(fs => fs.ageGroup == GetAgeGroup(age));

            if (selectedRow == null)
            {
                Console.WriteLine("No data available for this age group.");
                return;
            }

            // Calculate points for each exercise
            int pushupPoints = GetPoints(selectedRow.male.pushups, pushupReps);
            int pullupPoints = GetPoints(selectedRow.male.pullups, pullupReps);
            int crunchesPoints = GetPoints(selectedRow.male.crunches, crunchesReps);
            int plankPoints = GetPoints(selectedRow.male.plank, plankTime, isTime: true);
            int runPoints = GetPoints(selectedRow.male.threeMileRun, runTime, isTime: true);


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

        public void USMCFemalePRT(bool isFemale, int age, int pushupReps, int pullupReps, int crunchesReps, int plankTime, int runTime)
        {
            // Resolve the full path to the JSON file
            string jsonFilePath = Path.Combine("MilitaryPhysicalTraining", "USMCjson.json");

            // Check if the file exists
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"The JSON file was not found at path: {jsonFilePath}");
            }

            // Read and deserialize the JSON file
            string jsonString = File.ReadAllText(jsonFilePath);
            var fitnessData = JsonSerializer.Deserialize<USMCFitnessStandard>(jsonString);

            if (fitnessData == null || fitnessData.FitnessStandards == null)
            {
                throw new InvalidOperationException("The JSON file contains invalid or empty data.");
            }

            if (age < 17)
            {
                Console.WriteLine("Table values do not exist for ages under 17.");
                return;
            }

            // Find the appropriate age group
            var selectedRow = fitnessData.FitnessStandards
                .FirstOrDefault(fs => fs.ageGroup == GetAgeGroup(age));

            if (selectedRow == null)
            {
                Console.WriteLine("No data available for this age group.");
                return;
            }

            // Calculate points for each exercise
            int pushupPoints = GetPoints(selectedRow.female.pushups, pushupReps);
            int pullupPoints = GetPoints(selectedRow.female.pullups, pullupReps);
            int crunchesPoints = GetPoints(selectedRow.female.crunches, crunchesReps);
            int plankPoints = GetPoints(selectedRow.female.plank, plankTime, isTime: true);
            int runPoints = GetPoints(selectedRow.female.threeMileRun, runTime, isTime: true);


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

        private int GetPoints(Exercise exercise, int input, bool isTime = false)
        {
            if (exercise == null)
            {
                throw new ArgumentNullException(nameof(exercise), "The exercise object cannot be null.");
            }

            if (isTime)
            {
                if (exercise.times == null || exercise.points == null)
                {
                    throw new InvalidOperationException("The 'times' or 'points' property of the exercise object is null.");
                }

                // Convert input (seconds) to a time string (e.g., "1:30")
                string inputTime = TimeSpan.FromSeconds(input).ToString(@"m\:ss");
                int index = exercise.times.IndexOf(inputTime);
                return index != -1 && index < exercise.points.Count ? exercise.points[index] : 0;
            }
            else
            {
                if (exercise.reps == null || exercise.points == null)
                {
                    throw new InvalidOperationException("The 'reps' or 'points' property of the exercise object is null.");
                }

                int index = exercise.reps.IndexOf(input);
                return index != -1 && index < exercise.points.Count ? exercise.points[index] : 0;
            }
        }

        public int ParseTimeToSeconds(string time)
        {
            if (string.IsNullOrWhiteSpace(time))
            {
                throw new ArgumentException("Time string cannot be null or empty.");
            }

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
        public List<FitnessStandards> FitnessStandards { get; set; }
    }

    public class FitnessStandards
    {
        public string ageGroup { get; set; }
        public GenderFitness male { get; set; }
        public GenderFitness female { get; set; }
    }

    public class GenderFitness
    {
        public Exercise pushups { get; set; }
        public Exercise pullups { get; set; }
        public Exercise crunches { get; set; }
        public Exercise threeMileRun { get; set; }
        public Exercise plank { get; set; }
    }

    public class Exercise
    {
        public List<int> reps { get; set; }
        public List<string> times { get; set; }
        public List<int> points { get; set; }
    }
}

