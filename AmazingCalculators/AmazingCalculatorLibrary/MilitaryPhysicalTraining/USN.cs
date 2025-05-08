using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        }
    }
}
