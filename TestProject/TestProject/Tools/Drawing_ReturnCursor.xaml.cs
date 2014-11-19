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
    /// Interaction logic for Drawing_ReturnCursor.xaml
    /// </summary>
    public partial class Drawing_ReturnCursor : UserControl, IDeactivatableTool
    {
        public Drawing_ReturnCursor()
        {
            InitializeComponent();
            toggle.SetSymbol((DrawingImage)Application.Current.FindResource("SymbolPointer"));
        }

        public void Activate()
        {

        }

        public void Deactivate()
        {

        }
    }
}
