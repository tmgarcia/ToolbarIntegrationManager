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
    /// Interaction logic for Drawing_Lines.xaml
    /// </summary>
    public partial class Drawing_Lines : UserControl, IDeactivatableTool
    {
        public static readonly RoutedEvent LineSelectedEvent = EventManager.RegisterRoutedEvent("LineSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Drawing_Lines));
        public event RoutedEventHandler LineSelected
        {
            add { AddHandler(LineSelectedEvent, value); }
            remove { RemoveHandler(LineSelectedEvent, value); }
        }

        public DrawingStrokeType selectedStrokeType;
        public Drawing_Lines()
        {
            InitializeComponent();
            expandable.setSymbol((DrawingImage)Application.Current.FindResource("SymbolLineDiagonal"));
        }
        private void Line_Selected(object sender, RoutedEventArgs e)
        {
            selectedStrokeType = DrawingStrokeType.Line_Line;
            this.RaiseEvent(new RoutedEventArgs(LineSelectedEvent, this));
        }
        private void Arrow_Selected(object sender, RoutedEventArgs e)
        {
            selectedStrokeType = DrawingStrokeType.Line_Arrow;
            this.RaiseEvent(new RoutedEventArgs(LineSelectedEvent, this));
        }
        private void CoordQuad_Selected(object sender, RoutedEventArgs e)
        {
            selectedStrokeType = DrawingStrokeType.Line_CoordQuad;
            this.RaiseEvent(new RoutedEventArgs(LineSelectedEvent, this));
        }
        private void Coord2D_Selected(object sender, RoutedEventArgs e)
        {
            selectedStrokeType = DrawingStrokeType.Line_Coord2D;
            this.RaiseEvent(new RoutedEventArgs(LineSelectedEvent, this));
        }
        private void Coord3D_Selected(object sender, RoutedEventArgs e)
        {
            selectedStrokeType = DrawingStrokeType.Line_Coord3D;
            this.RaiseEvent(new RoutedEventArgs(LineSelectedEvent, this));
        }

        public void Activate()
        {

        }

        public void Deactivate()
        {

        }
    }
}
