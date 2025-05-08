using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingCalculatorLibrary.Models
{
    public class CalorieBurnedTracker
    {
        private readonly UserProfiles userProfiles;

        public CalorieBurnedTracker(UserProfiles userProfiles)
        {
            this.userProfiles = userProfiles ?? throw new ArgumentNullException(nameof(userProfiles));
        }

        private double WeightKg => userProfiles.WeightInPounds.HasValue
            ? userProfiles.WeightInPounds.Value * 0.453592
            : throw new InvalidOperationException("Weight is not set in the user profile.");

        private string ActivityLevel
        {
            get
            {
                return userProfiles.ActivityLevel switch
                {
                    1 => "light",
                    2 => "moderate",
                    3 => "intense",
                    _ => throw new ArgumentException("Invalid activity level. Valid options are: 1 (light), 2 (moderate), 3 (intense).")
                };
            }
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


