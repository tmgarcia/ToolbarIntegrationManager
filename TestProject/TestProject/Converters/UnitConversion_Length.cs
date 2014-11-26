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
                        toValue = ConvertFromCentimeters(fromNum, toUnit);
                        break;
                    case "Feet":
                        toValue = ConvertFromFeet(fromNum, toUnit);
                        break;
                    case "Inches":
                        toValue = ConvertFromInches(fromNum, toUnit);
                        break;
                    case "Kilometers":
                        toValue = ConvertFromKilometers(fromNum, toUnit);
                        break;
                    case "Meters":
                        toValue = ConvertFromMeters(fromNum, toUnit);
                        break;
                    case "Miles":
                        toValue = ConvertFromMiles(fromNum, toUnit);
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
                    toVal = feet * 12.0f;
                    break;
                case "Kilometers":
                    toVal = feet * 0.0003048f;
                    break;
                case "Meters":
                    toVal = feet * 0.3048f;
                    break;
                case "Miles":
                    toVal = feet * 0.000189394f;
                    break;
                case "Milimeters":
                    toVal = feet * 304.8f;
                    break;
                case "Nanometers":
                    toVal = feet * 304800000.0f;
                    break;
                case "PICA":
                    toVal = feet * 72.27f;
                    break;
                case "Yard":
                    toVal = feet / 3.0f;
                    break;
            }
            returnString = "" + toVal;
            return returnString;
        }

        private string ConvertFromInches(float inches, string toUnit)
        {
            string returnString = "";
            float toVal = inches;
            switch (toUnit)
            {
                case "Centimeters":
                    toVal = inches * 2.54f;
                    break;
                case "Feet":
                    toVal = inches / 12.0f;
                    break;
                case "Inches":
                    toVal = inches;
                    break;
                case "Kilometers":
                    toVal = inches * 0.0000254f;
                    break;
                case "Meters":
                    toVal = inches * 0.0254f;
                    break;
                case "Miles":
                    toVal = inches * 0.000015783f;
                    break;
                case "Milimeters":
                    toVal = inches * 25.4f;
                    break;
                case "Nanometers":
                    toVal = inches * 25400000.0f;
                    break;
                case "PICA":
                    toVal = inches * 6.0f;
                    break;
                case "Yard":
                    toVal = inches * 0.0277778f;
                    break;
            }
            returnString = "" + toVal;
            return returnString;
        }

        private string ConvertFromKilometers(float kilometers, string toUnit)
        {
            string returnString = "";
            float toVal = kilometers;
            switch (toUnit)
            {
                case "Centimeters":
                    toVal = kilometers * 100000.0f;
                    break;
                case "Feet":
                    toVal = kilometers * 3280.84f;
                    break;
                case "Inches":
                    toVal = kilometers * 39370.1f;
                    break;
                case "Kilometers":
                    toVal = kilometers;
                    break;
                case "Meters":
                    toVal = kilometers * 1000.0f;
                    break;
                case "Miles":
                    toVal = kilometers * 0.621371f;
                    break;
                case "Milimeters":
                    toVal = kilometers * 1000000.0f;
                    break;
                case "Nanometers":
                    toVal = kilometers * 1000000000000.0f;
                    break;
                case "PICA":
                    toVal = kilometers * 237106.30158f;
                    break;
                case "Yard":
                    toVal = kilometers * 1093.61f;
                    break;
            }
            returnString = "" + toVal;
            return returnString;
        }

        private string ConvertFromMeters(float meters, string toUnit)
        {
            string returnString = "";
            float toVal = meters;
            switch (toUnit)
            {
                case "Centimeters":
                    toVal = meters * 100.0f;
                    break;
                case "Feet":
                    toVal = meters * 3.28084f;
                    break;
                case "Inches":
                    toVal = meters * 39.3701f;
                    break;
                case "Kilometers":
                    toVal = meters * 0.001f;
                    break;
                case "Meters":
                    toVal = meters;
                    break;
                case "Miles":
                    toVal = meters * 0.000621371f;
                    break;
                case "Milimeters":
                    toVal = meters * 1000.0f;
                    break;
                case "Nanometers":
                    toVal = meters * 1000000000.0f;
                    break;
                case "PICA":
                    toVal = meters * 237.10630158f;
                    break;
                case "Yard":
                    toVal = meters * 1.09361f;
                    break;
            }
            returnString = "" + toVal;
            return returnString;
        }

        private string ConvertFromMiles(float miles, string toUnit)
        {
            string returnString = "";
            float toVal = miles;
            switch (toUnit)
            {
                case "Centimeters":
                    toVal = miles * 160934.0f;
                    break;
                case "Feet":
                    toVal = miles * 5280.0f;
                    break;
                case "Inches":
                    toVal = miles * 63360.0f;
                    break;
                case "Kilometers":
                    toVal = miles * 1.60934f;
                    break;
                case "Meters":
                    toVal = miles * 1609.34f;
                    break;
                case "Miles":
                    toVal = miles;
                    break;
                case "Milimeters":
                    toVal = miles * 1609000.0f;
                    break;
                case "Nanometers":
                    toVal = miles * 1609344000000.0f;
                    break;
                case "PICA":
                    toVal = miles * 381585.6038f;
                    break;
                case "Yard":
                    toVal = miles * 1760.0f;
                    break;
            }
            returnString = "" + toVal;
            return returnString;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
