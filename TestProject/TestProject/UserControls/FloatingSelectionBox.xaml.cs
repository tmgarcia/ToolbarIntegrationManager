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
using System.Windows.Shapes;
using TestProject.Models;

namespace TestProject.UserControls
{
    /// <summary>
    /// Interaction logic for FloatingSelectionBox.xaml
    /// </summary>
    public class PositionPoint : DependencyObject
    {
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(PositionPoint),
            new UIPropertyMetadata(null));
        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(PositionPoint),
            new UIPropertyMetadata(null));
        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
    }

    public partial class FloatingSelectionBox : Window
    {
    
        public static readonly DependencyProperty CenterPositionProperty =
            DependencyProperty.Register("CenterPosition", typeof(PositionPoint), typeof(FloatingSelectionBox),
            new UIPropertyMetadata(null));
        public PositionPoint CenterPosition
        {
            get { return (PositionPoint)GetValue(CenterPositionProperty); }
            set { SetValue(CenterPositionProperty, value); }
        }

        WindowResizer ob;
        public FloatingSelectionBox()
        {
            CenterPosition = new PositionPoint();
            ob = new WindowResizer(this);
            InitializeComponent();
            this.Deactivated += ToolbarWindow_Deactivated;
            this.Activated += ToolbarWindow_Activated;
            this.ShowInTaskbar = false;

        }


        private void SetCenterValue()
        {
            Point locationFromScreen = this.PointToScreen(new Point(0, 0));
            PresentationSource source = PresentationSource.FromVisual(this);
            Point corner = source.CompositionTarget.TransformFromDevice.Transform(locationFromScreen);
            double cX = corner.X + (this.ActualWidth/2);
            
            double cY = corner.Y + (this.ActualHeight/2);
            CenterPosition.X = cX;
            CenterPosition.Y = cY;
            
        }

        void ToolbarWindow_Activated(object sender, EventArgs e)
        {
            this.Topmost = true;
        }

        void ToolbarWindow_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;
            this.Activate();
        }

        private void DisplayResizeCursor(object sender, MouseEventArgs e)
        {
            ob.displayResizeCursor(sender);
        }
        // for each rectangle, assign the following method to its MouseLeave event.
        private void ResetCursor(object sender, MouseEventArgs e)
        {
            ob.resetCursor();
            SetCenterValue();
        }
        // for each rectangle, assign the following method to its PreviewMouseDown event.
        private void Resize(object sender, MouseButtonEventArgs e)
        {
            ob.resizeWindow(sender);
            SetCenterValue();
        }
        // finally, you may use the following method to enable dragging!
        private void Drag(object sender, MouseButtonEventArgs e)
        {
            ob.dragWindow();
        }
    }
}
