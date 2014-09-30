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

namespace TestProject.UserControls
{
    /// <summary>
    /// Interaction logic for ThreeStateEnabledButton.xaml
    /// </summary>
    public partial class ThreeStateEnabledButton : Button
    {
        string normal;
        string hover;
        string pressed;
        public ThreeStateEnabledButton(string normalPath, string hoverPath, string pressedPath)
        {
            normal = normalPath;
            hover = hoverPath;
            pressed = pressedPath;
            InitializeComponent();
        }
        public void disable()
        {
            buttonControl.IsEnabled = false;
        }
        public void enable()
        {
            buttonControl.IsEnabled = true;
        }
        public override void OnApplyTemplate()
        {
            BitmapImage normalImage = new BitmapImage();
            normalImage.BeginInit();
            normalImage.UriSource = new Uri("pack://application:,,,/TestProject;component/" + normal);
            normalImage.EndInit();
            ((Image)buttonControl.Template.FindName("Normal", buttonControl)).Source = normalImage;
            BitmapImage hoverImage = new BitmapImage();
            hoverImage.BeginInit();
            hoverImage.UriSource = new Uri("pack://application:,,,/TestProject;component/" + hover);
            hoverImage.EndInit();
            ((Image)buttonControl.Template.FindName("Hover", buttonControl)).Source = hoverImage;
            BitmapImage pressedImage = new BitmapImage();
            pressedImage.BeginInit();
            pressedImage.UriSource = new Uri("pack://application:,,,/TestProject;component/" + pressed);
            pressedImage.EndInit();
            ((Image)buttonControl.Template.FindName("Pressed", buttonControl)).Source = pressedImage;
        }
    }
}
