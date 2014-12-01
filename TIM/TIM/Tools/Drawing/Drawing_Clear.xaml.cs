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
using TIM.Models;

namespace TIM.Tools
{
    /// <summary>
    /// Interaction logic for Drawing_Clear.xaml
    /// </summary>
    public partial class Drawing_Clear : UserControl, IDeactivatableTool
    {
        public Drawing_Clear()
        {
            InitializeComponent();
            toggle.SetSymbol((DrawingImage)Application.Current.FindResource("SymbolDynamite"));
            toggle.Toggle.Checked += Toggle_Checked;
            toggle.Toggle.Unchecked += Toggle_Unchecked;
            toggle.Toggle.ToolTip = "Clear Drawing";
        }

        private void Toggle_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Toggle_Unchecked(object sender, RoutedEventArgs e)
        {

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
