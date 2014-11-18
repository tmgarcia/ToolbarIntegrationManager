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
    /// Interaction logic for Conversion_Units.xaml
    /// </summary>
    public partial class Conversion_Units : UserControl, IDeactivatableTool
    {
        public Conversion_Units()
        {
            InitializeComponent();
            expandable.setSymbol((DrawingImage)Application.Current.FindResource("SymbolSliders"));

        }

        public void Activate()
        {

        }

        public void Deactivate()
        {

        }
    }
}
