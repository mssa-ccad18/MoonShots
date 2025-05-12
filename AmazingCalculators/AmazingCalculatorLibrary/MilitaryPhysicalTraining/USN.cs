using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AmazingCalculatorLibrary.Models;

namespace AmazingCalculatorLibrary.MilitaryPhysicalTraining
{
    public class USN
    {
        // Method that reads the input from the WorkoutSession SQL database
        private readonly FitnessDbContext _context;

        public USN(FitnessDbContext context)
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
        public void USNPRT(bool isMale, int age, int pushupReps, int plankTime, int runTime)
        {
            // Resolve the full path to the JSON file
            string jsonFilePath = Path.Combine("MilitaryPhysicalTraining", "USNjson.json");

            // Check if the file exists
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"The JSON file was not found at path: {jsonFilePath}");
            }
            // Read and deserialize the JSON file
            string jsonString = File.ReadAllText(jsonFilePath);
            var fitnessData = JsonSerializer.Deserialize<USNFitnessStandard>(jsonString);

            if (fitnessData == null || fitnessData.USNavyFitnessStandards == null)
            {
                throw new InvalidOperationException("The JSON file contains invalid or empty data.");
            }

            if (age < 17)
            {
                Console.WriteLine("Table values do not exist for ages under 17.");
                return;
            }
            // Find the appropriate age group
            var selectedRow = fitnessData.USNavyFitnessStandards
                .FirstOrDefault(fs => fs.ageGroup == GetAgeGroup(age));

            if (selectedRow == null)
            {
                Console.WriteLine("No data available for this age group.");
                return;
            }

            // Calculate points for each exercise
            var genderFitness = isMale ? selectedRow.male : selectedRow.female;
            int pushupPoints = GetPoints(genderFitness.pushups, pushupReps);
            int plankPoints = GetPoints(genderFitness.plank, plankTime, isTime: true);
            int runPoints = GetPoints(genderFitness.oneAndHalfMileRun, runTime, isTime: true);

            // Display results
            Console.WriteLine($"Pushup Points: {pushupPoints}");
            Console.WriteLine($"Plank Points: {plankPoints}");
            Console.WriteLine($"Run Points: {runPoints}");

            int totalPoints = pushupPoints + plankPoints + runPoints;
            Console.WriteLine($"Total Points: {totalPoints}");
        }

        private string GetAgeGroup(int age)
        {
            if (age >= 17 && age < 20) return "17-19";
            if (age >= 20 && age < 25) return "20-24";
            if (age >= 25 && age < 30) return "25-29";
            if (age >= 30 && age < 35) return "30-34";
            if (age >= 35 && age < 40) return "35-39";
            if (age >= 40 && age < 45) return "40-44";
            if (age >= 45 && age < 50) return "45-49";
            if (age >= 50) return "50+";
            return null;
        }

        private int GetPoints(ExerciseUSN exercise, int input, bool isTime = false)
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
    }

}
        public class USNFitnessStandard
        {
            public List<USNavyFitnessStandards> USNavyFitnessStandards { get; set; }
        }

        public class USNavyFitnessStandards
{
            public string ageGroup { get; set; }
            public GenderFitness male { get; set; }
            public GenderFitness female { get; set; }
        }

        public class GenderFitness
        {
            public ExerciseUSN pushups { get; set; }
            public ExerciseUSN plank { get; set; }
            public ExerciseUSN oneAndHalfMileRun { get; set; }
        }

        public class ExerciseUSN
        {
            public List<int> reps { get; set; }
            public List<string> times { get; set; }
            public List<int> points { get; set; }
        }
    

