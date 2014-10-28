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

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Drawing_FillSelect.xaml
    /// </summary>
    public partial class Drawing_FillSelect : UserControl
    {
        ListBox colorChoiceDisplay;
        public Drawing_FillSelect()
        {
            InitializeComponent();

            colorChoiceDisplay = expandable.PopupContent as ListBox;
            expandable.setSymbol((DrawingImage)Application.Current.FindResource("SymbolNestedEmptySquare"));
            Width = expandable.Width;
            Height = expandable.Height;
            outerBorder.Width = expandable.Width;
            outerBorder.Height = expandable.Height;

            SetupColorChoices();
        }

        private void SetupColorChoices()
        {
            List<Color> colors = new List<Color>();
            colors.Add(Colors.Transparent);
            colors.Add(Colors.Black);
            colors.Add(Colors.White);
            colors.Add(Colors.Red);
            colors.Add(Colors.Orange);
            colors.Add(Colors.Yellow);
            colors.Add(Colors.Green);
            colors.Add(Colors.Blue);
            colors.Add(Colors.Purple);
            colorChoiceDisplay.ItemsSource = colors;
            colorChoiceDisplay.SelectedIndex = 2;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            outerBorderBackground.Color = (Color)colorChoiceDisplay.SelectedItem;
        }
    }
}
