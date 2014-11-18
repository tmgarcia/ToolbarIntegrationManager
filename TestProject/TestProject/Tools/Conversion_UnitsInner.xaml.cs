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

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Conversion_UnitsInner.xaml
    /// </summary>
    public partial class Conversion_UnitsInner : UserControl, IDeactivatableTool
    {
        List<string> angleUnits;
        List<string> lengthUnits;
        public Conversion_UnitsInner()
        {
            InitializeComponent();
            buildLists();
        }

        private void buildLists()
        {
            angleUnits = new List<string>() { "Degrees", "Radians", "Gradians" };
            List<string> unitTypes = new List<string>() { "Angles" };
            UnitSelect.ItemsSource = unitTypes;
            UnitSelect.SelectedIndex = 0;
        }

        private void FromValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumOnly(e.Text);
            if (e.Text == ".")
            {
                if(ToValue.Text.Contains("."))
                {
                    e.Handled = true;
                }
                else if (string.IsNullOrWhiteSpace(ToValue.Text))
                {
                    FromValue.Text = "0";
                }
            }
        }
        private bool IsNumOnly(string text)
        {
            Regex regex = new Regex("[^0-9.]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }
        private void UnitSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FromValue.Text = "0";
            switch ((string)UnitSelect.SelectedItem)
            {
                case "Angles":
                    FromSelect.ItemsSource = angleUnits;
                    ToSelect.ItemsSource = angleUnits;
                    FromSelect.SelectedIndex = 0;
                    ToSelect.SelectedIndex = 0;
                    ResetBinding(new UnitConversion_Angles());
                    break;
            }
        }

        private void ResetBinding(IMultiValueConverter converter)
        {
            MultiBinding toValueBinding = new MultiBinding();
            toValueBinding.Mode = BindingMode.OneWay;
            toValueBinding.Converter = converter;
            toValueBinding.Bindings.Add(new Binding { Source = FromSelect, Path = new PropertyPath("SelectedItem") });
            toValueBinding.Bindings.Add(new Binding { Source = ToSelect, Path = new PropertyPath("SelectedItem") });
            toValueBinding.Bindings.Add(new Binding { Source = FromValue, Path = new PropertyPath("Text") });
            BindingOperations.SetBinding(ToValue, TextBox.TextProperty, toValueBinding);
        }

        public void Activate()
        {

        }

        public void Deactivate()
        {

        }
    }
}
