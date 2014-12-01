using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace TIM.Models
{
    class HSBAColorWrapper : DependencyObject
    {
        public static DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(HSBAColor), typeof(HSBAColorWrapper));
        public HSBAColor Color
        {
            get { return (HSBAColor)GetValue(ColorProperty); }
            set {SetValue(ColorProperty, value); }
        }
    }
}