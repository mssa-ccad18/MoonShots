using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazingCalculatorLibrary;
using AmazingCalculatorLibrary.Models;
using static System.Net.Mime.MediaTypeNames;

namespace AmazingCalculatorLibrary.Models
{
    public class BMRCalculator
    {
        public double WeightLbs { get; set; }
        public double HeightInches { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public BMRCalculator() { }
        public BMRCalculator(double weightLbs, double heightInches, int age)
        {
            WeightLbs = weightLbs;
            HeightInches = heightInches;
            Age = age;
        }


        public static double GetCalorieBurned(BMI bmi, int age)
        {
            return bmi.IsMale
            ? 88.36 + (13.4 * bmi.WeightKGs) + (4.8 * bmi.HeightInCm) - (5.7 * age)
            : 447.6 + (9.25 * bmi.WeightKGs) + (3.1 * bmi.HeightInCm) - (4.3 * age);

        }   
        
    }

}

//consider updating to 

//public static class BMRCalculator
//{
//    public static double? CalculateBMR(UserProfiles user)
//    {
//        if (!user.WeightInPounds.HasValue || !user.HeightInInches.HasValue || !user.DateOfBirth.HasValue)
//            return null;

//        var age = DateTime.Now.Year - user.DateOfBirth.Value.Year;

//        return user.IsMale
//            ? 66 + (6.23 * user.WeightInPounds.Value) + (12.7 * user.HeightInInches.Value) - (6.8 * age)
//            : 655 + (4.35 * user.WeightInPounds.Value) + (4.7 * user.HeightInInches.Value) - (4.7 * age);
//    }
//}
//Then just call:


//BMR = BMRCalculator.CalculateBMR(UserProfile);