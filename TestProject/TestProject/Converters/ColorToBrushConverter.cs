using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace TestProject.Converters
{
    class ColorToBrushConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Brush brush = new SolidColorBrush(Colors.Black);
            if (value != null)
            {
                Color c = (Color)value;
                brush = new SolidColorBrush(c);
                if (c.A == 0)
                {
                    brush = new LinearGradientBrush();
                    ((LinearGradientBrush)brush).StartPoint = new System.Windows.Point(0.48, 0);
                    ((LinearGradientBrush)brush).EndPoint = new System.Windows.Point(0.52, 1);
                    ((LinearGradientBrush)brush).GradientStops.Add(new GradientStop(Colors.Gray, 0));
                    ((LinearGradientBrush)brush).GradientStops.Add(new GradientStop(Colors.Gray, 0.46));
                    ((LinearGradientBrush)brush).GradientStops.Add(new GradientStop(Colors.Red, 0.46));
                    ((LinearGradientBrush)brush).GradientStops.Add(new GradientStop(Colors.Red, 0.54));
                    ((LinearGradientBrush)brush).GradientStops.Add(new GradientStop(Colors.Gray, 0.54));
                    ((LinearGradientBrush)brush).GradientStops.Add(new GradientStop(Colors.Gray, 1));
                }
            }
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
