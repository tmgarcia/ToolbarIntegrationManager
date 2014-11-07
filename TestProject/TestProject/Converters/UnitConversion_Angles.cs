using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestProject.Converters
{
    class UnitConversion_Angles : IMultiValueConverter
    {
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
                    case "Degrees":
                        toValue = ConvertFromDegrees(fromNum, toUnit);
                        break;
                    case "Radians":
                        toValue = ConvertFromRadians(fromNum, toUnit);
                        break;
                    case "Gradians":
                        toValue = ConvertFromGradians(fromNum, toUnit);
                        break;
                }
            }
            return toValue;
        }


        private string ConvertFromDegrees(float degrees, string toUnit)
        {
            string returnValue = "";
            switch (toUnit)
            {
                case "Degrees":
                    returnValue = "" + degrees;
                    break;
                case "Radians":
                    //d * (pi/180)
                    float radians = degrees * (float)(Math.PI / 180.0);
                    returnValue = "" + radians;
                    break;
                case "Gradians":
                    float grad = degrees / 0.9f;
                    returnValue = "" + grad;
                    break;
            }
            return returnValue;
        }
        private string ConvertFromRadians(float radians, string toUnit)
        {
            string returnValue = "";
            switch (toUnit)
            {
                case "Degrees":
                    //r * (180/pi)
                    float degrees = radians * (float)(180.0/ Math.PI);
                    returnValue = "" + degrees;
                    break;
                case "Radians":
                    returnValue = "" + radians;
                    break;
                case "Gradians":
                    float grad = radians / (float)(Math.PI/200.0);
                    returnValue = "" + grad;
                    break;
            }
            return returnValue;
        }
        private string ConvertFromGradians(float grads, string toUnit)
        {
            string returnValue = "";
            switch (toUnit)
            {
                case "Degrees":
                    //r * (180/pi)
                    float degrees = grads * 0.9f;
                    returnValue = "" + degrees;
                    break;
                case "Radians":
                    float radians = grads * (float)(Math.PI / 200.0);
                    returnValue = "" + radians;
                    break;
                case "Gradians":
                    returnValue = "" + grads;
                    break;
            }
            return returnValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
