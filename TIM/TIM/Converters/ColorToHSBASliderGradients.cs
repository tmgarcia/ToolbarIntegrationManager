using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using TIM.Models;

namespace TIM.Converters
{
    class ColorToHSBASliderGradients : IValueConverter
    {
        //color to s, b, or a
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Color c = (Color)value;
            HSBAColor hsba = new RGBAColor((Color)c).ToHSBA();
            LinearGradientBrush brush = new LinearGradientBrush();
            brush.StartPoint = new System.Windows.Point(0, 0.5);
            brush.EndPoint = new System.Windows.Point(1, 0.5);
            HSBAColor high = hsba.Copy();
            high.A = 1;
            HSBAColor low = hsba.Copy();
            low.A = 1;

            switch ((string)parameter)
            {
                case "S":
                    high.S = 1;
                    low.S = 0;
                    break;
                case "B":
                    high.B = 1;
                    low.B = 0;
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
