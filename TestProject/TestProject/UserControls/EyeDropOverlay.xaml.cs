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

namespace TestProject.UserControls
{
    /// <summary>
    /// Interaction logic for EyeDropOverlay.xaml
    /// </summary>
    public partial class EyeDropOverlay : Window
    {
        public static readonly RoutedEvent SampleColorEvent = EventManager.RegisterRoutedEvent("SampleColor", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(EyeDropOverlay));
        public event RoutedEventHandler SampleColor
        {
            add { AddHandler(SampleColorEvent, value); }
            remove { RemoveHandler(SampleColorEvent, value); }
        }
        public bool active;
        public EyeDropOverlay()
        {
            active = false;
            InitializeComponent();
            Mouse.OverrideCursor = Cursors.Pen;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            active = true;
            this.MouseMove += Window_MouseMove;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (active)
            {
                this.RaiseEvent(new RoutedEventArgs(SampleColorEvent, this));
            }
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            active = false;
            Mouse.OverrideCursor = Cursors.Arrow;
            this.Close();
        }

    }
}
