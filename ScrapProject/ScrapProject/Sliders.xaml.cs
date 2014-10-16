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
using ScrapProject.Converters;

namespace ScrapProject
{
    /// <summary>
    /// Interaction logic for Sliders.xaml
    /// </summary>
    public partial class Sliders : Window
    {
        MultiBinding rgbaBinding;
        MultiBinding hslaBinding;
        MultiBinding hsbaBinding;


        public Sliders()
        {
            InitializeComponent();
            //SliderChoices
            List<string> choices = new List<string>();
            choices.Add("RGBA");
            choices.Add("HSBA");
            choices.Add("HSLA");
            SliderSelector.DataContext = new { SliderChoices = choices };
            BuildMultiBinds();
            SwitchToRGBA();
        }

        private void BuildMultiBinds()
        {
            RGBAToColorConverter rgbaCon = new RGBAToColorConverter();
            rgbaBinding = new MultiBinding();
            rgbaBinding.Converter = rgbaCon;
            rgbaBinding.Bindings.Add(new Binding { Source = rgbaSliders.rSlider, Path = new PropertyPath("Value")});
            rgbaBinding.Bindings.Add(new Binding { Source = rgbaSliders.gSlider, Path = new PropertyPath("Value") });
            rgbaBinding.Bindings.Add(new Binding { Source = rgbaSliders.bSlider, Path = new PropertyPath("Value") });
            rgbaBinding.Bindings.Add(new Binding { Source = rgbaSliders.aSlider, Path = new PropertyPath("Value") });

            HSLAToColorConverter hslaCon = new HSLAToColorConverter();
            hslaBinding = new MultiBinding();
            hslaBinding.Converter = hslaCon;
            hslaBinding.Bindings.Add(new Binding { Source = hslaSliders.hSlider, Path = new PropertyPath("Value") });
            hslaBinding.Bindings.Add(new Binding { Source = hslaSliders.sSlider, Path = new PropertyPath("Value") });
            hslaBinding.Bindings.Add(new Binding { Source = hslaSliders.lSlider, Path = new PropertyPath("Value") });
            hslaBinding.Bindings.Add(new Binding { Source = hslaSliders.aSlider, Path = new PropertyPath("Value") });

            HSLAToColorConverter hsbaCon = new HSLAToColorConverter();
            hsbaBinding = new MultiBinding();
            hsbaBinding.Converter = hsbaCon;
            hsbaBinding.Bindings.Add(new Binding { Source = hsbaSliders.hSlider, Path = new PropertyPath("Value") });
            hsbaBinding.Bindings.Add(new Binding { Source = hsbaSliders.sSlider, Path = new PropertyPath("Value") });
            hsbaBinding.Bindings.Add(new Binding { Source = hsbaSliders.bSlider, Path = new PropertyPath("Value") });
            hsbaBinding.Bindings.Add(new Binding { Source = hsbaSliders.aSlider, Path = new PropertyPath("Value") });
        }

        private void SliderSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selection = (string)SliderSelector.SelectedItem;
            switch (selection)
            {
                case "RGBA":
                    SwitchToRGBA();
                    break;
                case "HSBA":
                    SwitchToHSBA();
                    break;
                case "HSLA":
                    SwitchToHSLA();
                    break;
            }
        }
        private void SwitchToRGBA()
        {
            rgbaSliders.Visibility = System.Windows.Visibility.Visible;
            rgbaSliders.SetToColor(ColorDisplayBrush.Color);
            hsbaSliders.Visibility = System.Windows.Visibility.Collapsed;
            hslaSliders.Visibility = System.Windows.Visibility.Collapsed;
            BindingOperations.SetBinding(ColorDisplayBrush, SolidColorBrush.ColorProperty, rgbaBinding);
        }
        private void SwitchToHSBA()
        {
            rgbaSliders.Visibility = System.Windows.Visibility.Collapsed;
            hsbaSliders.Visibility = System.Windows.Visibility.Visible;
            hsbaSliders.SetToColor(ColorDisplayBrush.Color);
            hslaSliders.Visibility = System.Windows.Visibility.Collapsed;
            BindingOperations.SetBinding(ColorDisplayBrush, SolidColorBrush.ColorProperty, hsbaBinding);
        }
        private void SwitchToHSLA()
        {
            rgbaSliders.Visibility = System.Windows.Visibility.Collapsed;
            hsbaSliders.Visibility = System.Windows.Visibility.Collapsed;
            hslaSliders.Visibility = System.Windows.Visibility.Visible;
            hslaSliders.SetToColor(ColorDisplayBrush.Color);
            BindingOperations.SetBinding(ColorDisplayBrush, SolidColorBrush.ColorProperty, hslaBinding);
        }

    }
}
