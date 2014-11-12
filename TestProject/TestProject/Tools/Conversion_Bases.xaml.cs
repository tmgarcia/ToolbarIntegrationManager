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

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Conversion_Bases.xaml
    /// </summary>
    public partial class Conversion_Bases : UserControl
    {
        private enum BaseMode { Binary=2, Octal=8, Decimal=10, Hexidecimal=16 }
        private BaseMode currentBase;
        private TextBox NumberDisplay;
        public Conversion_Bases()
        {
            currentBase = BaseMode.Decimal;
            InitializeComponent();
            expandable.setSymbol((DrawingImage)Application.Current.FindResource("SymbolBaseConversion"));
            StackPanel content = expandable.PopupContent as StackPanel;
            Grid inner = content.Children[0] as Grid;
            NumberDisplay = inner.Children[0] as TextBox;
        }

        private void BinSelect_Checked(object sender, RoutedEventArgs e)
        {
            NumberDisplay.Text = ConvertBase(NumberDisplay.Text, (int)currentBase, 2);
            currentBase = BaseMode.Binary;
        }

        private void OctSelect_Checked(object sender, RoutedEventArgs e)
        {
            NumberDisplay.Text = ConvertBase(NumberDisplay.Text, (int)currentBase, 8);
            currentBase = BaseMode.Octal;
        }

        private void DecSelect_Checked(object sender, RoutedEventArgs e)
        {
            NumberDisplay.Text = ConvertBase(NumberDisplay.Text, (int)currentBase, 10);
            currentBase = BaseMode.Decimal;
        }

        private void HexSelect_Checked(object sender, RoutedEventArgs e)
        {
            NumberDisplay.Text = ConvertBase(NumberDisplay.Text, (int)currentBase, 16);
            currentBase = BaseMode.Hexidecimal;
        }

        private string ConvertBase(string value, int fromBase, int toBase)
        {
            String result = "";
            if(!String.IsNullOrWhiteSpace(value))
            {
                result = Convert.ToString(Convert.ToInt64(value, fromBase), toBase);
            }
            return result;
        }

        private void NumberDisplay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            switch (currentBase)
            {
                case BaseMode.Binary:
                    e.Handled = !IsNumOnlyBin(e.Text);
                    if (!e.Handled)
                    {
                        e.Handled = !ValidBin(e.Text);
                    }
                    break;
                case BaseMode.Octal:
                    e.Handled = !IsNumOnlyOct(e.Text);
                    if (!e.Handled)
                    {
                        e.Handled = !ValidOct(e.Text);
                    }
                    break;
                case BaseMode.Decimal:
                    e.Handled = !IsNumOnlyDec(e.Text);
                    if (!e.Handled)
                    {
                        e.Handled = !ValidDec(e.Text);
                    }
                    break;
                case BaseMode.Hexidecimal:
                    e.Handled = !IsNumOnlyHex(e.Text);
                    if (!e.Handled)
                    {
                        e.Handled = !VerifyHex(e.Text);
                    }
                    break;
            }
        }

        private bool IsNumOnlyBin(string text)
        {
            Regex regex = new Regex("[^0-1]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }
        private bool ValidBin(string newText)
        {
            bool valid = true;
            string text = NumberDisplay.Text + newText;
            valid = IsNumOnlyBin(text);
            if (valid && !String.IsNullOrEmpty(text))
            {
                try
                {
                    long number = Convert.ToInt64(text, 2);
                }
                catch (FormatException)
                {
                    valid = false;
                }
                catch (OverflowException)
                {
                    valid = false;
                }
                catch (ArgumentException)
                {
                    valid = false;
                }
            }
            return valid;
        }
        private bool IsNumOnlyOct(string text)
        {
            Regex regex = new Regex("[^0-7]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }
        private bool ValidOct(string newText)
        {
            bool valid = true;
            string text = NumberDisplay.Text + newText;
            valid = IsNumOnlyOct(text);
            if (valid && !String.IsNullOrEmpty(text))
            {
                try
                {
                    long number = Convert.ToInt64(text, 8);
                }
                catch (FormatException)
                {
                    valid = false;
                }
                catch (OverflowException) 
                {
                    valid = false;
                }
                catch (ArgumentException)
                {
                    valid = false;
                }
            }
            return valid;
        }
        private bool IsNumOnlyDec(string text)
        {
            Regex regex = new Regex("[^0-9-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }
        private bool ValidDec(string newText)
        {
            bool valid = true;
            string text = NumberDisplay.Text + newText;
            Regex regex = new Regex("(?!^-)[^0-9]+");
            valid = !regex.IsMatch(text);
            if (valid && !String.IsNullOrEmpty(text) && !(text.Contains("-") && text.Length==1))
            {
                try
                {
                    long number = Convert.ToInt64(text, 10);
                }
                catch (FormatException)
                {
                    valid = false;
                }
                catch (OverflowException)
                {
                    if (text.Contains('-'))
                    {
                        text = Convert.ToString(Int64.MinValue, 10);
                    }
                    else
                    {
                        text = Convert.ToString(Int64.MaxValue, 10);
                    }
                    valid = false;
                }
                catch (ArgumentException)
                {
                    valid = false;
                }
            }
            return valid;
        }
        private bool IsNumOnlyHex(string text)
        {
            Regex regex = new Regex("[^[0-9][a-f][A-F]]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }
        private bool VerifyHex(string newText)
        {
            bool valid = true;
            string text = NumberDisplay.Text + newText;
            valid = IsNumOnlyBin(text);
            if (valid && !String.IsNullOrEmpty(text))
            {
                try
                {
                    long number = Convert.ToInt64(text, 16);
                }
                catch (FormatException)
                {
                    valid = false;
                }
                catch (OverflowException)
                {
                    valid = false;
                }
                catch (ArgumentException)
                {
                    valid = false;
                }
            }
            return valid;
        }
    }
}
