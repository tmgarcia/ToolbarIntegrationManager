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
    /// Interaction logic for Conversion_Ascii.xaml
    /// </summary>
    public partial class Conversion_Ascii : UserControl, IDeactivatableTool
    {
        public Conversion_Ascii()
        {
            InitializeComponent();
            Height = Constants.ToolButtonHeight;
            Width = 200;
        }

        public void Activate()
        {

        }

        public void Deactivate()
        {

        }
    }
}
