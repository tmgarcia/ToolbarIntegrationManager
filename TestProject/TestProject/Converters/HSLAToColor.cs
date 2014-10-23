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
    class HSLAToColor : IMultiValueConverter
    {
        //HSBA to Color
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //float H = (float)values[0];
            float H = float.Parse(values[0].ToString());
            float S = float.Parse(values[1].ToString());
            float L = float.Parse(values[2].ToString());
            float A = float.Parse(values[3].ToString());

            HSLAColor hsla = new HSLAColor(H, S, L, A);
            Color c = hsla.ToColor();

            return c;
        }

        //Color to HSBA
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            Color c = (Color)value;
            HSLAColor hsla = new RGBAColor(c).ToHSLA();
            object[] splitValues = { (double)hsla.H, (double)hsla.S, (double)hsla.L, (double)hsla.A };
            return splitValues;
        }
    }
}
