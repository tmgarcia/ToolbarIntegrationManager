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
    class ColorToHSLASliderGradients : IValueConverter
    {
        //color to s, b, or a
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Color c = (Color)value;
            HSLAColor hsla = new RGBAColor((Color)c).ToHSLA();
            LinearGradientBrush brush = new LinearGradientBrush();
            brush.StartPoint = new System.Windows.Point(0, 0.5);
            brush.EndPoint = new System.Windows.Point(1, 0.5);
            HSLAColor high = hsla.Copy();
            high.A = 1;
            HSLAColor low = hsla.Copy();
            low.A = 1;

            switch ((string)parameter)
            {
                case "S":
                    high.S = 1;
                    low.S = 0;
                    break;
                case "L":
                    HSLAColor mid = hsla.Copy();
                    mid.A = 1;
                    high.L = 1;
                    mid.L = 0.5f;
                    low.L = 0;
                    brush.GradientStops.Add(new GradientStop(mid.ToColor(), 0.5));
                    break;
                case "A":
                    low.A = 0;
                    break;
                default:

                    break;
            }
            brush.GradientStops.Add(new GradientStop(low.ToColor(), 0));
            brush.GradientStops.Add(new GradientStop(high.ToColor(), 1));

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
