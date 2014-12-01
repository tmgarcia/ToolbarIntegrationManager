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
    public class OrientableUniformGrid : ItemsControl
    {
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(OrientableUniformGrid),
            new UIPropertyMetadata(null));
        public System.Windows.Controls.Orientation Orientation
        {
            get { return (System.Windows.Controls.Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty VisibleItemsCountProperty =
            DependencyProperty.Register("VisibleItemsCount", typeof(int), typeof(OrientableUniformGrid),
            new UIPropertyMetadata(null));
        public int VisibleItemsCount
        {
            get { return (int)GetValue(VisibleItemsCountProperty); }
            set { SetValue(VisibleItemsCountProperty, value); }
        }
        static OrientableUniformGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OrientableUniformGrid), new FrameworkPropertyMetadata(typeof(OrientableUniformGrid)));
        }
        public OrientableUniformGrid()
        {
            Orientation = System.Windows.Controls.Orientation.Vertical;
            VisibleItemsCount = 4;
        }
    }
}
