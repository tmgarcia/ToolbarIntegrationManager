using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestProject.Converters
{
    class NumberFormatToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string ret = "";
            switch ((string)parameter)
            {
                case "byte":
                    ret = ((byte)value).ToString();
                    break;
                case "int":
                    ret = ((int)value).ToString();
                    break;
                case "float":
                    ret = ((float)value).ToString();
                    break;
                case "double":
                    ret = ((double)value).ToString();
                    break;
                default:
                    ret = ((int)value).ToString();
                    break;
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((string)parameter)
            {
                case "byte":
                    return byte.Parse((string)value);
                    //break;
                case "int":
                    return int.Parse((string)value);
                    //break;
                case "float":
                    return float.Parse((string)value);
                    //break;
                case "double":
                    return double.Parse((string)value);
                    //break;
                default:
                    return int.Parse((string)value);
                    //break;
            }

        }
    }
}
