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
using TIM.Models;

namespace TIM.UserControls
{
    /// <summary>
    /// Interaction logic for ActiveInactiveImageButton.xaml
    /// </summary>
    public partial class ActiveInactiveImageButton : Button
    {
        ImageStates states;
        bool isActive;

        int width;
        int height;

        public ActiveInactiveImageButton(int width, int height, ImageStates states, bool isActive)
        {
            this.states = states;
            this.isActive = isActive;
            this.width = width;
            this.height = height;
            InitializeComponent();
        }
        public void SetAsActive()
        {
            isActive = true;
            ResetImages();
        }
        public void SetAsInactive()
        {
            isActive = false;
            ResetImages();
        }
        private void ResetImages()
        {
            if (isActive)
            {
                ((Image)buttonControl.Template.FindName("Normal", buttonControl)).Source = states.activeImage;
                ((Image)buttonControl.Template.FindName("Hover", buttonControl)).Source = states.activeHoverImage;
                ((Image)buttonControl.Template.FindName("Pressed", buttonControl)).Source = states.activePressedImage;
            }
            else
            {
                ((Image)buttonControl.Template.FindName("Normal", buttonControl)).Source = states.inactiveImage;
                ((Image)buttonControl.Template.FindName("Hover", buttonControl)).Source = states.inactiveHoverImage;
                ((Image)buttonControl.Template.FindName("Pressed", buttonControl)).Source = states.inactivePressedImage;
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ResetImages();
            Image n = ((Image)buttonControl.Template.FindName("Normal", buttonControl));
            //n.Width = width;
            //n.Height = height;
            //Image h = ((Image)buttonControl.Template.FindName("Hover", buttonControl));
            //h.Width = width;
            //h.Height = height;
            //Image p = ((Image)buttonControl.Template.FindName("Pressed", buttonControl));
            //p.Width = width;
            //p.Height = height;
            Image d = ((Image)buttonControl.Template.FindName("Disabled", buttonControl));
            d.Source = states.disabledImage;

            //Rectangle shadow = ((Rectangle)buttonControl.Template.FindName("Shadow", buttonControl));
            //shadow.Width = n.ActualWidth;
            //d.Width = width;
            //d.Height = height;
        }
    }
}
