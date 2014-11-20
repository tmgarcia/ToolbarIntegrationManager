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
using TestProject.Models;
using TestProject.UserControls;

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Colors_Eyedropper.xaml
    /// </summary>
    public partial class Colors_Eyedropper : UserControl, IDeactivatableTool
    {
        SolidColorBrush ColorDisplayBrush;
        public Colors_Eyedropper(SolidColorBrush currentBrush)
        {
            InitializeComponent();
            ColorDisplayBrush = currentBrush;
            toggle.SetSymbol((DrawingImage)Application.Current.FindResource("SymbolEyedropper"));
            toggle.Toggle.Checked += Toggle_Checked;
            toggle.Toggle.Unchecked += Toggle_Unchecked;
        }

        void Toggle_Unchecked(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        BitmapSource image;
        void Toggle_Checked(object sender, RoutedEventArgs e)
        {
            image = ScreenCapturer.CaptureFullScreen();

            EyeDropOverlay ov = new EyeDropOverlay();
            ov.Show();
            ov.SampleColor += ov_SampleColor;
            ov.Closed += ov_Closed;
        }

        void ov_Closed(object sender, EventArgs e)
        {
            toggle.Toggle.IsChecked = false;
        }

        void ov_SampleColor(object sender, RoutedEventArgs e)
        {
            Color c = SamplePixelForColor();
            ColorDisplayBrush.SetCurrentValue(SolidColorBrush.ColorProperty, c);
        }

        private Color SamplePixelForColor()
        {
            Color selectedColor = Color.FromRgb(250, 0, 0);
            var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
            System.Drawing.Point pnt = System.Windows.Forms.Control.MousePosition;
            System.Windows.Point p = new System.Windows.Point(pnt.X, pnt.Y);
            System.Windows.Point point = transform.Transform(p);

            if ((point.X <= image.PixelWidth) && (point.Y <= image.PixelHeight))
            {
                var croppedBitmap = new CroppedBitmap(image, new Int32Rect((int)point.X, (int)point.Y, 1, 1));

                var pixels = new byte[4];
                croppedBitmap.CopyPixels(pixels, 4, 0);
                selectedColor = Color.FromArgb(255, pixels[2], pixels[1], pixels[0]);
            }
            return selectedColor;
        }

        public void Activate()
        {

        }

        public void Deactivate()
        {

        }
    }
}
