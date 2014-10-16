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

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Colors_ValuesDisplay.xaml
    /// </summary>
    public partial class Colors_ValuesDisplay : UserControl
    {
        HSBAColorWrapper currentHSBA;
        SolidColorBrush currentBrush;
        TextBox r;
        TextBox g;
        TextBox b;
        TextBox a;
        TextBox h;
        TextBox s;
        TextBox b1;
        public Colors_ValuesDisplay(SolidColorBrush colorBrush)
        {
            currentBrush = colorBrush;
            currentHSBA = new HSBAColorWrapper();
            InitializeComponent();
            expandable.setSymbol((DrawingImage)Application.Current.FindResource("SymbolColorInfo"));

            Grid content = expandable.PopupContent as Grid;
            r = content.Children[0] as TextBox;
            g = content.Children[1] as TextBox;
            b = content.Children[2] as TextBox;
            a = content.Children[3] as TextBox;
            h = content.Children[4] as TextBox;
            s = content.Children[5] as TextBox;
            b1 = content.Children[6] as TextBox;

            //currentBrush.Changed += currentBrush_Changed;

            ColorToDisplayHSBA con = new ColorToDisplayHSBA();
            Binding colorBinding = new Binding();
            colorBinding.Source = currentBrush;
            colorBinding.Mode = BindingMode.TwoWay;
            colorBinding.Converter = con;
            colorBinding.Path = new PropertyPath("Color");
            BindingOperations.SetBinding(currentHSBA, HSBAColorWrapper.ColorProperty, colorBinding);

            expandable.DataContext = new { CurrentColor = currentBrush, CurrentHColor = this.currentHSBA };

        }

    }
}
