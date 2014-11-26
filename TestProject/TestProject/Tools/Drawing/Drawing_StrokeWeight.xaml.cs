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

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Drawing_StrokeWeight.xaml
    /// </summary>
    public partial class Drawing_StrokeWeight : UserControl, IDeactivatableTool
    {
        public static readonly RoutedEvent WeightChangedEvent = EventManager.RegisterRoutedEvent("WeightChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Drawing_StrokeWeight));
        public event RoutedEventHandler WeightChanged
        {
            add { AddHandler(WeightChangedEvent, value); }
            remove { RemoveHandler(WeightChangedEvent, value); }
        }
        public double currentWeight=1;
        public Drawing_StrokeWeight()
        {
            InitializeComponent();
            expandable.setSymbol((DrawingImage)Application.Current.FindResource("SymbolStrokes"));
            expandable.Toggle.toggleControl.ToolTip = "Stroke Thickness";
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            currentWeight = e.NewValue;
            this.RaiseEvent(new RoutedEventArgs(WeightChangedEvent, this));
        }

        public void Activate()
        {

        }

        public void Deactivate()
        {

        }


        public void ReorientHorizontal()
        {

        }

        public void ReorientVertical()
        {

        }

        public void Collapse()
        {

        }
    }
}
