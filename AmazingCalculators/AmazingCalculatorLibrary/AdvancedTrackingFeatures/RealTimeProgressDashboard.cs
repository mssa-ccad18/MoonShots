
//using AmazingCalculatorLibrary.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AmazingCalculatorLibrary.AdvancedTrackingFeatures
//{
//    public class RealTimeProgressDashboard
//    {
//        // Method that reads the input from the WorkoutSession SQL database
//        private readonly FitnessDbContext _context;


//        public RealTimeProgressDashboard(FitnessDbContext context)
//        {
//            _context = context;
//        }

//        public void DisplayWorkoutHistory(int userId)
//        {
//            var workouts = _context.WorkoutSessions
//                .Where(w => w.UserId == userId)
//                .OrderByDescending(w => w.WorkoutDate)
//                .ToList();

            //CoPilot suggested this. We have modified this code to use properties in BMI and BMR, and anticipate CalorieBurnedTracker to

            //Console.WriteLine("\n Workout History:");
            //foreach (var workout in workouts)
            //{
            //    Console.WriteLine($"- {workout.WorkoutDate.ToShortDateString()}: {workout.WorkoutType} for {workout.DurationInMinutes} min, burned {workout.CaloriesBurned} kcal");
            //}

            //// Main Static Method that takes user input and calculates BMI and calories burned
            //static void Main()
            //{
            //    Console.WriteLine("🏋️ Welcome to Your Real-Time Fitness Dashboard!");

            //    // Get user input
            //    Console.Write("Enter weight (kg): ");
            //    double weight = Convert.ToDouble(Console.ReadLine());

            //    Console.WriteLine("\n🔥 Stay consistent and keep moving!");
            //}
//        }
//    }
//}


