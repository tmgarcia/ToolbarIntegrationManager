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
using TestProject.Enums;
using TestProject.Models;

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Drawing_Shapes.xaml
    /// </summary>
    public partial class Drawing_Shapes : UserControl, IDeactivatableTool
    {
        public static readonly RoutedEvent ShapeSelectedEvent = EventManager.RegisterRoutedEvent("ShapeSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Drawing_Shapes));
        public event RoutedEventHandler ShapeSelected
        {
            add { AddHandler(ShapeSelectedEvent, value); }
            remove { RemoveHandler(ShapeSelectedEvent, value); }
        }

        public DrawingStrokeType selectedStrokeType;
        public Drawing_Shapes()
        {
            InitializeComponent();
            expandable.setSymbol((DrawingImage)Application.Current.FindResource("SymbolShapes"));
            expandable.Toggle.toggleControl.ToolTip = "Shapes";
        }

        private void Square_Selected(object sender, RoutedEventArgs e)
        {
            selectedStrokeType = DrawingStrokeType.Shape_Rectangle;
            this.RaiseEvent(new RoutedEventArgs(ShapeSelectedEvent, this));
        }
        private void Circle_Selected(object sender, RoutedEventArgs e)
        {
            selectedStrokeType = DrawingStrokeType.Shape_Ellipse;
            this.RaiseEvent(new RoutedEventArgs(ShapeSelectedEvent, this));
        }
        private void Triangle_Selected(object sender, RoutedEventArgs e)
        {
            selectedStrokeType = DrawingStrokeType.Shape_Triangle;
            this.RaiseEvent(new RoutedEventArgs(ShapeSelectedEvent, this));
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
