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
    /// Interaction logic for ScreenClippingOverlay.xaml
    /// </summary>
    public partial class ScreenClippingOverlay : Window
    {
        public static readonly RoutedEvent ClippingEndedEvent = EventManager.RegisterRoutedEvent("ClippingEnded", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ScreenClippingOverlay));
        public event RoutedEventHandler ClippingEnded
        {
            add { AddHandler(ClippingEndedEvent, value); }
            remove { RemoveHandler(ClippingEndedEvent, value); }
        }

        ScreenClipInk_InkCanvas screenClippingInkCanvas;
        public ScreenClippingOverlay(BitmapSource screenCap)
        {
            InitializeComponent();
            clippedImage = null;
            screenClippingInkCanvas = new ScreenClipInk_InkCanvas(screenCap);
            screenClippingInkCanvas.FinishedClip += screenClippingInkCanvas_FinishedClip;
            overlayGrid.Children.Add(screenClippingInkCanvas);
        }

        public BitmapSource clippedImage;
        void screenClippingInkCanvas_FinishedClip(object sender, RoutedEventArgs e)
        {
            clippedImage = screenClippingInkCanvas.clippedImage;
            RaiseEvent(new RoutedEventArgs(ClippingEndedEvent, this));
        }
    }
}
