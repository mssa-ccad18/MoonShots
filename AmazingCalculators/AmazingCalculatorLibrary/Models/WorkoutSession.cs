using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingCalculatorLibrary.Models
{
    public class WorkoutSession
    {
        [Key]
        public int WorkoutSessionId { get; set; }
        public DateTime WorkoutDate { get; set; }
        public string WorkoutType { get; set; } // e.g., Cardio, Strength, Flexibility
        public double DurationInMinutes { get; set; } // Duration of the workout session
        public int CaloriesBurned { get; set; } // Calories burned during the workout

    }
}
