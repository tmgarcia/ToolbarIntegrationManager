using System;
using System.Collections.Generic;
using System.IO;
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
using TestProject.Models;
using TestProject.UserControls;

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Notes_ScreenClipping.xaml
    /// </summary>
    public partial class Notes_ScreenClipping : UserControl, IDeactivatableTool
    {
        public Notes_ScreenClipping()
        {
            InitializeComponent();
            toggle.SetSymbol((DrawingImage)Application.Current.FindResource("SymbolEyedropper"));
            toggle.Toggle.Checked += Toggle_Checked;
            toggle.Toggle.Unchecked += Toggle_Unchecked;
        }

        void Toggle_Unchecked(object sender, RoutedEventArgs e)
        {
            //Mouse.OverrideCursor = Cursors.Arrow;
        }

        BitmapSource image;
        ScreenClippingOverlay overlay;
        void Toggle_Checked(object sender, RoutedEventArgs e)
        {
            image = ScreenCapturer.CaptureFullScreen();
            overlay = new ScreenClippingOverlay(image);
            overlay.Show();
            Mouse.OverrideCursor = Cursors.Cross;
            overlay.ClippingEnded += overlay_ClippingEnded;
        }

        void overlay_ClippingEnded(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
            BitmapSource clipping = overlay.clippedImage;
            overlay.Close();
            toggle.Toggle.IsChecked = false;
            if (clipping != null)
            {
                string messageBoxText = "Would you like to save the screen clipping?\n\n" +
                    "Yes: Opens Save dialog\n"+
                    "No: Clipping will be placed onto clipboard\n"+
                    "Cancel: Clipping will be discarded";
                MessageBoxResult result = MessageBox.Show(messageBoxText, "Save Clipping?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        SaveImage(clipping);
                        break;
                    case MessageBoxResult.No:
                        Clipboard.SetImage(clipping);
                        break;
                    case MessageBoxResult.Cancel:
                        clipping = null;
                        break;
                }
            }
        }
        private void SaveImage(BitmapSource clipping)
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.DefaultExt = ".bmp";
            sfd.Filter = "Bitmap Image (.bmp)|*.bmp";
            bool? result = sfd.ShowDialog();

            if (result == true)
            {
                string fileName = sfd.FileName;
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(clipping));
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                encoder.Save(fs);
                fs.Close();
            }
        }

        public void Activate()
        {

        }

        public void Deactivate()
        {

        }
    }
}
