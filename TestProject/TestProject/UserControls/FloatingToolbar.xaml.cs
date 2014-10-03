using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestProject.Enums;

namespace TestProject.UserControls
{
    /// <summary>
    /// Interaction logic for FloatingToolbar.xaml
    /// </summary>
    public partial class FloatingToolbar : ToolBar
    {
        public static readonly RoutedEvent TemplateAppliedEvent = EventManager.RegisterRoutedEvent("TemplateApplied", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FloatingToolbar));

        public bool isCollapsed = false;
        public DisplayOrientations orientation = DisplayOrientations.Horizontal;

        public FloatingToolbar()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler TemplateApplied
        {
            add { AddHandler(TemplateAppliedEvent, value); }
            remove { RemoveHandler(TemplateAppliedEvent, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.RaiseEvent(new RoutedEventArgs(TemplateAppliedEvent, this));
        }

        public Button getCloseButtonControl()
        {
            Button bc =  (Button)(toolbarControl.Template.FindName("CloseButton", toolbarControl));

            return bc;
        }
        public Thumb getThumbControl()
        {
            Thumb th = (Thumb)(toolbarControl.Template.FindName("ToolBarThumb", toolbarControl));
            return th;
        }

        private void CollapseButton_Checked(object sender, RoutedEventArgs e)
        {
            isCollapsed = true;
            ToggleButton btn = (ToggleButton)(toolbarControl.Template.FindName("CollapseButton", toolbarControl));
            ScaleTransform scale = (ScaleTransform)(btn.Template.FindName("FlipScale", btn));
            scale.ScaleX = -1;
            //RotateTransform r = (RotateTransform)(btn.Template.FindName("BorderRotate", btn));
            //r.Angle = 90;
        }

        private void CollapseButton_Unchecked(object sender, RoutedEventArgs e)
        {
            isCollapsed = false;
            ToggleButton btn = (ToggleButton)(toolbarControl.Template.FindName("CollapseButton", toolbarControl));
            ScaleTransform scale = (ScaleTransform)(btn.Template.FindName("FlipScale", btn));
            scale.ScaleX = 1;
        }

        private void OrientationButton_Click(object sender, RoutedEventArgs e)
        {
            if (orientation == DisplayOrientations.Horizontal)
            {
                orientation = DisplayOrientations.Vertical;
                //ToggleButton btn = (ToggleButton)(toolbarControl.Template.FindName("CollapseButton", toolbarControl));
                //RotateTransform r = (RotateTransform)(btn.Template.FindName("BorderRotate", btn));
                //r.Angle = 90;
            }
            else
            {
                orientation = DisplayOrientations.Horizontal;
                //ToggleButton btn = (ToggleButton)(toolbarControl.Template.FindName("CollapseButton", toolbarControl));
                //RotateTransform r = (RotateTransform)(btn.Template.FindName("BorderRotate", btn));
                //r.Angle = 0;
            }
        }
    }
}
