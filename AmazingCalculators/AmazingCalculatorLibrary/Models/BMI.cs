using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingCalculatorLibrary.Models
{
    public class BMI
    {

        public double WeightLbs { get; set; }
        public double HeightFeet { get; set; }
        public double HeightInches { get; set; }

        public BMI(double weightLbs, double heightFeet, double heightInches)
        {
            WeightLbs = weightLbs;
            HeightFeet = heightFeet;
            HeightInches = heightInches;
        }

        private double ConvertHeightToInches()
        {
            return (HeightFeet * 12) + HeightInches;
        }

        public double Calculate()
        {
            double heightInTotalInches = ConvertHeightToInches();
            if (heightInTotalInches <= 0)
                throw new ArgumentException("Height must be greater than zero.");

            return (WeightLbs * 703) / (heightInTotalInches * heightInTotalInches);
        }

        public string GetBMICategory()
        {
            double bmi = Calculate();

            if (bmi < 18.5)
                return "UnderWeight";
            else if (bmi >= 18.5 && bmi <= 24.9)
                return "Normal weight";
            else if (bmi >= 25.0 && bmi <= 29.9)
                return "Overweight";
            return "obese";

        }
        public double TestConvertHeightToInches() //wrapper method to public method
        {
            return ConvertHeightToInches();
        }
    }


}

