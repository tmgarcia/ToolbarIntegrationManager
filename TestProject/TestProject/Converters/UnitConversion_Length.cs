using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestProject.Converters
{
    class UnitConversion_Length : IMultiValueConverter
    {
            //Centimeters
            //Feet
            //Inches
            //Kilometers
            //Meters
            //Miles
            //Milimeters
            //Nanometers
            //PICA
            //Yard

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //object[0] = from unit string
            //object[1] = to unit string
            //object[2] = from value
            string fromUnit = values[0] as string;
            string toUnit = values[1] as string;
            string fromValue = values[2] as string;
            string toValue = "X";
            if (!string.IsNullOrWhiteSpace(fromValue))
            {
                float fromNum = float.Parse(fromValue);
                switch (fromUnit)
                {
                    case "Centimeters":
                        //toValue = ConvertFromDegrees(fromNum, toUnit);
                        break;
                    case "Feet":
                        //toValue = ConvertFromRadians(fromNum, toUnit);
                        break;
                    case "Inches":
                        //toValue = ConvertFromGradians(fromNum, toUnit);
                        break;
                    case "Kilometers":
                        //toValue = ConvertFromGradians(fromNum, toUnit);
                        break;
                    case "Meters":
                        //toValue = ConvertFromGradians(fromNum, toUnit);
                        break;
                    case "Miles":
                        //toValue = ConvertFromGradians(fromNum, toUnit);
                        break;
                    case "Milimeters":
                        //toValue = ConvertFromGradians(fromNum, toUnit);
                        break;
                    case "Nanometers":
                        //toValue = ConvertFromGradians(fromNum, toUnit);
                        break;
                    case "PICA":
                        //toValue = ConvertFromGradians(fromNum, toUnit);
                        break;
                    case "Yard":
                        //toValue = ConvertFromGradians(fromNum, toUnit);
                        break;
                }
            }
            return toValue;
        }


        private string ConvertFromCentimeters(float centimeters, string toUnit)
        {
            string returnString = "";
            switch (toUnit)
            {
                case "Centimeters":
                    returnString = "" + centimeters;
                    break;
                case "Feet":
                    float feet = centimeters * 0.0328084f;
                    returnString += feet;
                    break;
                case "Inches":
                    float inches = centimeters * 0.393701f;
                    returnString += inches;
                    break;
                case "Kilometers":
                    float kilometers = centimeters * 0.00001f;
                    returnString += kilometers;
                    break;
                case "Meters":
                    float meters = centimeters * 0.01f;
                    returnString += meters;
                    break;
                case "Miles":
                    float miles = centimeters * 0.0000062137119f;
                    returnString += miles;
                    break;
                case "Milimeters":
                    float milimeters = centimeters * 10;
                    returnString += milimeters;
                    break;
                case "Nanometers":
                    float nanometers = centimeters * 10000000.0f;
                    returnString += nanometers;
                    break;
                case "PICA":
                    float picas = centimeters * 2.362204724f;
                    returnString += picas;
                    break;
                case "Yard":
                    float yards = centimeters * 0.0109361f;
                    returnString += yards;
                    break;
            }
            return returnString;
        }
        private string ConvertFromFeet(float feet, string toUnit)
        {
            string returnString = "";
            float toVal = feet;
            switch (toUnit)
            {
                case "Centimeters":
                    toVal = feet * 30.48f;
                    break;
                case "Feet":
                    toVal = feet;
                    break;
                case "Inches":

                    break;
                case "Kilometers":

                    break;
                case "Meters":

                    break;
                case "Miles":

                    break;
                case "Milimeters":

                    break;
                case "Nanometers":

                    break;
                case "PICA":

                    break;
                case "Yard":

                    break;
            }
            return returnString;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
