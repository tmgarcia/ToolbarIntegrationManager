using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace TestProject.Converters
{
    class ColorToRGBASliderGradients : IValueConverter
    {
        //color to s, b, or a
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Color c = (Color)value;
            LinearGradientBrush brush = new LinearGradientBrush();
            brush.StartPoint = new System.Windows.Point(0, 0.5);
            brush.EndPoint = new System.Windows.Point(1, 0.5);
            Color high = Color.FromArgb(c.A, c.R, c.G, c.B);
            high.A = 255;
            Color low = Color.FromArgb(c.A, c.R, c.G, c.B);
            low.A = 255;

            switch ((string)parameter)
            {
                case "R":
                    high.R = 255;
                    low.R = 0;
                    break;
                case "G":
                    high.G = 255;
                    low.G = 0;
                    break;
                case "B":
                    high.B = 255;
                    low.B = 0;
                    break;
                case "A":
                    low.A = 0;
                    break;
                default:

                    break;
            }
            brush.GradientStops.Add(new GradientStop(low, 0));
            brush.GradientStops.Add(new GradientStop(high, 1));

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
