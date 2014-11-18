using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Colors_ValuesDisplay.xaml
    /// </summary>
    public partial class Colors_ValuesDisplay : UserControl, IDeactivatableTool
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
        public Colors_ValuesDisplay(SolidColorBrush colorBrush, RGBASliders rgbaSliders, HSBASliders hsbaSliders)
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

            ColorToDisplayHSBA con = new ColorToDisplayHSBA();
            Binding colorBinding = new Binding();
            colorBinding.Source = currentBrush;
            colorBinding.Mode = BindingMode.TwoWay;
            colorBinding.Converter = con;
            colorBinding.Path = new PropertyPath("Color");
            BindingOperations.SetBinding(currentHSBA, HSBAColorWrapper.ColorProperty, colorBinding);
            expandable.DataContext = new { CurrentColor = currentBrush, CurrentHColor = this.currentHSBA };
        }
        bool userChangingRGBA = false;
        private void previewTextInput_NumsOnlyInt(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumOnlyInt(e.Text);//handled (stops the text) if not all nums
            if (!e.Handled)
            {
                userChangingRGBA = true;
            }
        }
        bool userChangingHSBA = false;
        private void previewTextInput_NumsOnlyDec(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumOnlyDec(e.Text);//handled (stops the text) if not all nums
            if (!e.Handled)
            {
                userChangingHSBA = true;
            }
        }

        private bool IsNumOnlyDec(string text)
        {
            Regex regex = new Regex("[^0-9.]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }
        private bool IsNumOnlyInt(string text)
        {
            Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private bool IsProperIntInput(string text)
        {
            byte i=0;
            return !(!IsNumOnlyInt(text) || String.IsNullOrWhiteSpace(text) || !byte.TryParse(text, out i) || i > 255);
        }
        private bool IsProperDecInput(string text, float max)
        {
            float i = 0;
            return !(!IsNumOnlyDec(text) || String.IsNullOrWhiteSpace(text) || !float.TryParse(text, out i) || i > max);
        }
        private void R_TextInput(object sender, TextChangedEventArgs e)
        {
            if (userChangingRGBA)
            {
                userChangingRGBA = false;
                TextBox red = sender as TextBox;
                if (!IsProperIntInput(red.Text))
                {
                    red.Text = "0";
                }
                byte i = byte.Parse(red.Text);
                Color c = currentBrush.Color;
                c.R = i;
                currentBrush.Color = c;
            }
        }
        private void G_TextInput(object sender, TextChangedEventArgs e)
        {
            if (userChangingRGBA)
            {
                userChangingRGBA = false;
                TextBox green = sender as TextBox;
                if (!IsProperIntInput(green.Text))
                {
                    green.Text = "0";
                }
                byte i = byte.Parse(green.Text);
                Color c = currentBrush.Color;
                c.G = i;
                currentBrush.Color = c;
            }
        }
        private void B_TextInput(object sender, TextChangedEventArgs e)
        {
            if (userChangingRGBA)
            {
                userChangingRGBA = false;
                TextBox blue = sender as TextBox;
                if (!IsProperIntInput(blue.Text))
                {
                    blue.Text = "0";
                }
                byte i = byte.Parse(blue.Text);
                Color c = currentBrush.Color;
                c.B = i;
                currentBrush.Color = c;
            }
        }
        private void A_TextInput(object sender, TextChangedEventArgs e)
        {
            if (userChangingRGBA)
            {
                userChangingRGBA = false;
                TextBox alpha = sender as TextBox;
                if (!IsProperIntInput(alpha.Text))
                {
                    alpha.Text = "255";
                }
                byte i = byte.Parse(alpha.Text);
                Color c = currentBrush.Color;
                c.A = i;
                currentBrush.Color = c;
            }
        }

        private void H_TextInput(object sender, TextChangedEventArgs e)
        {
            if (userChangingHSBA)
            {
                userChangingHSBA = false;
                TextBox hue = sender as TextBox;
                if (!IsProperDecInput(hue.Text,360))
                {
                    hue.Text = "0";
                }
                float i = float.Parse(hue.Text);
                HSBAColor c = currentHSBA.Color;
                c.H = i;
                currentHSBA.Color = c;
            }
        }
        private void S_TextInput(object sender, TextChangedEventArgs e)
        {
            if (userChangingHSBA)
            {
                userChangingHSBA = false;
                TextBox sat = sender as TextBox;
                if (!IsProperDecInput(sat.Text, 1))
                {
                    sat.Text = "0";
                }
                float i = float.Parse(sat.Text);
                HSBAColor c = currentHSBA.Color;
                c.S = i;
                currentHSBA.Color = c;
            }
        }
        private void B1_TextInput(object sender, TextChangedEventArgs e)
        {
            if (userChangingHSBA)
            {
                userChangingHSBA = false;
                TextBox br = sender as TextBox;
                if (!IsProperDecInput(br.Text, 1))
                {
                    br.Text = "0";
                }
                float i = float.Parse(br.Text);
                HSBAColor c = currentHSBA.Color;
                c.B = i;
                currentHSBA.Color = c;
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
