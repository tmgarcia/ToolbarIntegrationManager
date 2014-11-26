using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestProject.Converters
{
    class ASCIIConverter : IValueConverter
    {
        //from character
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string ret = "";
            if (!String.IsNullOrEmpty((string)value))
            {
                char c = ((string)value)[0];
                switch ((string)parameter)
                {
                    case "bin":
                        ret = System.Convert.ToString(c, 2);
                        break;
                    case "dec":
                        ret = System.Convert.ToString(c, 10);
                        break;
                    case "hex":
                        ret = System.Convert.ToString(c, 16);
                        break;
                }
            }
            return ret;
        }
        //to character
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string val = (string)value;
            string ret = "";
            int ascii = 0;
            char c = ' ';
            try
            {
                switch ((string)parameter)
                {
                    case "bin":
                        ascii = System.Convert.ToInt32(val, 2);
                        c = System.Convert.ToChar(ascii);
                        break;
                    case "dec":
                        ascii = System.Convert.ToInt32(val, 10);
                        c = System.Convert.ToChar(ascii);
                        break;
                    case "hex":
                        ascii = System.Convert.ToInt32(val, 16);
                        c = System.Convert.ToChar(ascii);
                        break;
                }
                if (char.IsControl(c))
                {
                    ret = "CTRL";
                }
                else
                {
                    ret += c;
                }
            }
            catch (System.OverflowException)
            {
                ret = "NA";
            }
            return ret;
        }
    }
}
