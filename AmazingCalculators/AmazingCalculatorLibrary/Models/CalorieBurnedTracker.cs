using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingCalculatorLibrary.Models
{
    public class CalorieBurnedTracker
    {
        UserProfiles userProfiles = new UserProfiles();

        /*public double WeightKg { get; set; }
        public double HeightCm { get; set; }
        public bool IsMale { get; set; }
        public string ActivityLevel { get; set; }*/

        public CalorieBurnedTracker(double weightInPounds, double heightInInches, bool isMale, string activityLevel)
        {
            userProfiles.WeightInPounds = weightInPounds;
            userProfiles.HeightInInches = heightInInches;
            IsMale = isMale;
            userProfiles.ActivityLevel = activityLevel.ToLower();
        }

        private double GetMetabolicEquivalent()
        {
            return ActivityLevel switch
            {
                "light" => 3.0,
                "moderate" => 6.0,
                "intense" => 9.0,
                _ => throw new ArgumentException("Invalid activity level. Valid options are: light, moderate, intense.")
            };
        }

        public double CaloriesBurnedPerHour
        {
            get
            {
                double metabolicEquivalent = GetMetabolicEquivalent();
                return metabolicEquivalent * WeightKg;
            }
        }

        public string ActivityCategory
        {
            get
            {
                return ActivityLevel switch
                {
                    "light" => "Light Activity",
                    "moderate" => "Moderate Activity",
                    "intense" => "Intense Activity",
                    _ => "Unknown Activity"
                };
            }
        }
    }
}


