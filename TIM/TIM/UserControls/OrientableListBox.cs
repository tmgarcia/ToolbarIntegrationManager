using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TIM.UserControls
{
    public class OrientableListBox : ListBox
    {
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(OrientableListBox),
            new UIPropertyMetadata(null));
        public System.Windows.Controls.Orientation Orientation
        {
            get { return (System.Windows.Controls.Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        static OrientableListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OrientableListBox), new FrameworkPropertyMetadata(typeof(OrientableListBox)));
        }

        public OrientableListBox()
        {
            Orientation = System.Windows.Controls.Orientation.Vertical;
        }

    }
}
