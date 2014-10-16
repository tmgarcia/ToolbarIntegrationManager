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
    class ColorToDisplayHSBA : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Color c = (Color)value;
            return new RGBAColor(c).ToHSBA();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            HSBAColor h = (HSBAColor)value;
            return h.ToColor();
        }
    }
}
