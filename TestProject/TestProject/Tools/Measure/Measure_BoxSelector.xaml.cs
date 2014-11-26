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
using TestProject.Models;
using TestProject.UserControls;

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Measure_BoxSelector.xaml
    /// </summary>
    public partial class Measure_BoxSelector : UserControl, IDeactivatableTool
    {
        private FloatingSelectionBox box;
        bool boxActive;
        public Measure_BoxSelector()
        {
            InitializeComponent();
            box = new FloatingSelectionBox();
            expandable.setSymbol((DrawingImage)Application.Current.FindResource("SymbolSelectionBox"));
            expandable.Toggle.toggleControl.Checked += toggleControl_Checked;
            expandable.Toggle.toggleControl.Unchecked += toggleControl_Unchecked;
            boxActive = false;
            expandable.Toggle.toggleControl.ToolTip = "Screen Dimensions Selector";
            Grid grid = expandable.PopupContent as Grid;

            Binding bx = new Binding { Source = box.CenterPosition, Path = new PropertyPath("X"), Mode = BindingMode.OneWay };
            Binding by = new Binding { Source = box.CenterPosition, Path = new PropertyPath("Y"), Mode = BindingMode.OneWay };
            Binding bw = new Binding { Source = box.WinGrid, Path = new PropertyPath("ActualWidth"), Mode = BindingMode.OneWay };
            Binding bh = new Binding { Source = box.WinGrid, Path = new PropertyPath("ActualHeight"), Mode = BindingMode.OneWay };

            TextBox x = grid.Children[1] as TextBox;
            BindingOperations.SetBinding(x, TextBox.TextProperty, bx);
            TextBox y = grid.Children[3] as TextBox;
            BindingOperations.SetBinding(y, TextBox.TextProperty, by);
            TextBox w = grid.Children[5] as TextBox;
            BindingOperations.SetBinding(w, TextBox.TextProperty, bw);
            TextBox h = grid.Children[7] as TextBox;
            BindingOperations.SetBinding(h, TextBox.TextProperty, bh);
        }

        void toggleControl_Unchecked(object sender, RoutedEventArgs e)
        {
            box.Hide();
        }

        void toggleControl_Checked(object sender, RoutedEventArgs e)
        {
            box.Show();
        }

        public void Activate()
        {

        }

        public void Deactivate()
        {
            expandable.Toggle.toggleControl.IsChecked = false;
        }


        public void ReorientHorizontal()
        {
            expandable.SetPlacementModeBottom();
            expandable.setAlignmentPointTopLeft();
        }

        public void ReorientVertical()
        {
            expandable.SetPlacementModeLeft();
            expandable.setAlignmentPointTopRight();
        }

        public void Collapse()
        {
            this.expandable.Collapse();
        }
    }
}
