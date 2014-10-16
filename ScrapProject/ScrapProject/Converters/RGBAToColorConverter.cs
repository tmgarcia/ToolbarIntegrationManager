using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ScrapProject.Converters
{
    class RGBAToColorConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(values.Any(v => v == DependencyProperty.UnsetValue))
            {
                return Color.FromArgb(255, 0, 0, 0);
            }
            byte R = byte.Parse(values[0].ToString());
            byte G = byte.Parse(values[1].ToString());
            byte B = byte.Parse(values[2].ToString());
            byte A = byte.Parse(values[3].ToString());

            Color c = Color.FromArgb(A, R, G, B);

            return c;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            Color c = (Color)value;
            object[] splitValues = { c.R, c.G, c.B, c.A };
            return splitValues;
        }
    }
}
