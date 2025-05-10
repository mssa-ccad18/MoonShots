using System;
using AmazingCalculatorLibrary.Models;
using System.Collections.Generic;

namespace AmazingCalculatorLibrary.MilitaryPhysicalTraining
{
    public class USA
    {
        private readonly UserProfiles userProfile;

        public USA(UserProfiles userProfile)
        {
            this.userProfile = userProfile ?? throw new ArgumentNullException(nameof(userProfile));
        }

        public int CalculateTotalScore(int deadliftWeight, int pushUps, double sprintTime, int plankTime, double runTime)
        {
            int score = 0;
            score += DeadliftPoints(deadliftWeight);
            score += PushUpPoints(pushUps);
            score += SprintPoints(sprintTime);
            score += PlankPoints(plankTime);
            score += RunPoints(runTime);

            return score;
        }

        public string GetFitnessResult(int deadliftWeight, int pushUps, double sprintTime, int plankTime, double runTime, string role)
        {
            int totalScore = CalculateTotalScore(deadliftWeight, pushUps, sprintTime, plankTime, runTime);
            int passingScore = role == "combat" ? 350 : 300;

            return totalScore >= passingScore ? "Pass" : "Fail";
        }

        private int DeadliftPoints(int weight) => weight switch
        {
            >= 180 => 100,
            >= 160 => 85,
            >= 140 => 70,
            >= 120 => 60,
            _ => 0
        };

        private int PushUpPoints(int count) => count switch
        {
            >= 50 => 100,
            >= 40 => 85,
            >= 30 => 70,
            >= 20 => 60,
            _ => 0
        };

        private int SprintPoints(double time) => time switch
        {
            <= 100.0 => 100,
            <= 120.0 => 85,
            <= 140.0 => 70,
            <= 160.0 => 60,
            _ => 0
        };

        private int PlankPoints(int seconds) => seconds switch
        {
            >= 150 => 100,
            >= 120 => 85,
            >= 90 => 70,
            >= 60 => 60,
            _ => 0
        };

        private int RunPoints(double time) => time switch
        {
            <= 15.0 => 100,
            <= 16.5 => 85,
            <= 18.0 => 70,
            <= 20.0 => 60,
            _ => 0
        };

        public int GetAge()
        {
            if (userProfile.DateOfBirth == null)
                throw new InvalidOperationException("Date of birth is not set.");

            var today = DateTime.Today;
            var age = today.Year - userProfile.DateOfBirth.Value.Year;

            if (userProfile.DateOfBirth.Value.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
