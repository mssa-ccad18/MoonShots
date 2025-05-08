using AmazingCalculatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingCalculatorLibrary.MilitaryPhysicalTraining
{
    internal class USMC
    {
        private readonly FitnessDbContext _context;


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

        }

        protected int _pullUps;
        protected int _crunches;
        protected int _runTime;
        protected int _pushups;
        public void USMCMalePRT(bool IsMale, int age)
        {
            if (age < 17)
            {



            }
            else if (age >= 17 && age < 21)
            {


            }
            else if (age >= 21 && age < 26)
            {

            }
            else if (age >= 26 && age < 30)
            { 
            

            }


        }

    }
}

