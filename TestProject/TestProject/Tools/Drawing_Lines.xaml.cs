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
    /// Interaction logic for Drawing_Lines.xaml
    /// </summary>
    public partial class Drawing_Lines : UserControl
    {
        public Drawing_Lines()
        {
            InitializeComponent();
            expandable.setSymbol((DrawingImage)Application.Current.FindResource("SymbolLineDiagonal"));
        }
        private void Line_Selected(object sender, RoutedEventArgs e)
        {

        }
        private void Arrow_Selected(object sender, RoutedEventArgs e)
        {

        }
        private void CoordQuad_Selected(object sender, RoutedEventArgs e)
        {

        }
        private void Coord2D_Selected(object sender, RoutedEventArgs e)
        {

        }
        private void Coord3D_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
