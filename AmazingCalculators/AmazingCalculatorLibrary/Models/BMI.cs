using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazingCalculatorLibrary.Models;



namespace AmazingCalculatorLibrary.Models
{
    public class BMI 
    {

        public double WeightLbs { get; set; }
        public double HeightFeet { get; set; }
        public double HeightInches { get; set; }
        public double HeightInCm => (HeightFeet * 30.48) + (HeightInches * 2.54);
        public double WeightKGs => WeightLbs * 0.45;
        public bool IsMale { get; set; }

        /*public BMI(UserProfiles user)
        {
            double weighinKG = user.WeightInPounds * 0.45;
            double heightInCM = user.HeightInInches;
            bool ismale = user.IsMale;

        }*/
        public BMI(double weightLbs, double heightFeet, double heightInches, bool ismale)
        {
            WeightLbs = weightLbs;
            HeightFeet = heightFeet;
            HeightInches = heightInches;
            IsMale = ismale;
        }

        private double ConvertHeightToInches()
        {
            return (HeightFeet * 12) + HeightInches;
        }

        public double BMIValue
        {
            get
            {
                double heightInTotalInches = ConvertHeightToInches();
                if (heightInTotalInches <= 0)
                    throw new ArgumentException("Height must be greater than zero.");

                return (WeightLbs * 703) / (heightInTotalInches * heightInTotalInches);
            }
        }

        public string BMICategory
        {
            get
            {

                if (BMIValue < 18.5)
                    return "UnderWeight";
                else if (BMIValue >= 18.5 && BMIValue <= 24.9)
                    return "Normal weight";
                else if (BMIValue >= 25.0 && BMIValue <= 29.9)
                    return "Overweight";
                return "obese";

            }
        }

    }


}

