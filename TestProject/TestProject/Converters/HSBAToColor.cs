using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using TestProject.Models;

namespace TestProject.Converters
{
    class HSBAToColor : IMultiValueConverter
    {
        //HSBA to Color
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //float H = (float)values[0];
            float H = float.Parse(values[0].ToString());
            float S = float.Parse(values[1].ToString());
            float B = float.Parse(values[2].ToString());
            float A = float.Parse(values[3].ToString());

            HSBAColor hsba = new HSBAColor(H, S, B, A);
            Color c = hsba.ToColor();

            return c;
        }

        //Color to HSBA
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            Color c = (Color)value;
            HSBAColor hsba = new RGBAColor(c).ToHSBA();
            object[] splitValues = { (float)hsba.H, (float)hsba.S, (float)hsba.B, (float)hsba.A };
            return splitValues;
        }
    }
}
