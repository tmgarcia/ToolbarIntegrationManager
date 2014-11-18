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
using TestProject.Converters;
using TestProject.Models;
using TestProject.UserControls;

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Colors_Sliders.xaml
    /// </summary>
    public partial class Colors_Sliders : UserControl, IDeactivatableTool
    {
        MultiBinding rgbaBinding;
        MultiBinding hslaBinding;
        MultiBinding hsbaBinding;

        SolidColorBrush ColorDisplayBrush;
        ComboBox SliderSelector;
        public RGBASliders rgbaSliders;
        public HSLASliders hslaSliders;
        public HSBASliders hsbaSliders;

        public Colors_Sliders(SolidColorBrush colorBrush)
        {
            ColorDisplayBrush = colorBrush;
            InitializeComponent();

            expandable.setSymbol((DrawingImage)Application.Current.FindResource("SymbolSliders"));
            //expandable.Height = 20;

            StackPanel content = expandable.PopupContent as StackPanel;
            SliderSelector = content.Children[0] as ComboBox;
            rgbaSliders = content.Children[1] as RGBASliders;
            hslaSliders = content.Children[2] as HSLASliders;
            hsbaSliders = content.Children[3] as HSBASliders;

            List<string> choices = new List<string>();
            choices.Add("RGBA");
            choices.Add("HSBA");
            choices.Add("HSLA");
            SliderSelector.DataContext = new { SliderChoices = choices };
            expandable.DataContext = new { ColorBrush = ColorDisplayBrush };
            BuildMultiBinds();
            ColorDisplayBrush.Color = Color.FromArgb(255, 255, 255, 255);

            SliderSelector.SelectedIndex = 0;
        }

        private void BuildMultiBinds()
        {
            RGBAToColor rgbaCon = new RGBAToColor();
            rgbaBinding = new MultiBinding();
            rgbaBinding.Mode = BindingMode.TwoWay;
            rgbaBinding.Converter = rgbaCon;
            rgbaBinding.Bindings.Add(new Binding { Source = rgbaSliders.rSlider, Path = new PropertyPath("Value"), Mode = BindingMode.TwoWay });
            rgbaBinding.Bindings.Add(new Binding { Source = rgbaSliders.gSlider, Path = new PropertyPath("Value"), Mode = BindingMode.TwoWay });
            rgbaBinding.Bindings.Add(new Binding { Source = rgbaSliders.bSlider, Path = new PropertyPath("Value"), Mode = BindingMode.TwoWay });
            rgbaBinding.Bindings.Add(new Binding { Source = rgbaSliders.aSlider, Path = new PropertyPath("Value"), Mode = BindingMode.TwoWay });

            HSLAToColor hslaCon = new HSLAToColor();
            hslaBinding = new MultiBinding();
            hslaBinding.Mode = BindingMode.TwoWay;
            hslaBinding.Converter = hslaCon;
            hslaBinding.Bindings.Add(new Binding { Source = hslaSliders.hSlider, Path = new PropertyPath("Value"), Mode = BindingMode.TwoWay });
            hslaBinding.Bindings.Add(new Binding { Source = hslaSliders.sSlider, Path = new PropertyPath("Value"), Mode = BindingMode.TwoWay });
            hslaBinding.Bindings.Add(new Binding { Source = hslaSliders.lSlider, Path = new PropertyPath("Value"), Mode = BindingMode.TwoWay });
            hslaBinding.Bindings.Add(new Binding { Source = hslaSliders.aSlider, Path = new PropertyPath("Value"), Mode = BindingMode.TwoWay });

            HSLAToColor hsbaCon = new HSLAToColor();
            hsbaBinding = new MultiBinding();
            hsbaBinding.Mode = BindingMode.TwoWay;
            hsbaBinding.Converter = hsbaCon;
            hsbaBinding.Bindings.Add(new Binding { Source = hsbaSliders.hSlider, Path = new PropertyPath("Value"), Mode = BindingMode.TwoWay });
            hsbaBinding.Bindings.Add(new Binding { Source = hsbaSliders.sSlider, Path = new PropertyPath("Value"), Mode = BindingMode.TwoWay });
            hsbaBinding.Bindings.Add(new Binding { Source = hsbaSliders.bSlider, Path = new PropertyPath("Value"), Mode = BindingMode.TwoWay });
            hsbaBinding.Bindings.Add(new Binding { Source = hsbaSliders.aSlider, Path = new PropertyPath("Value"), Mode = BindingMode.TwoWay });
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

        public void Activate()
        {

        }

        public void Deactivate()
        {

        }
    }
}
