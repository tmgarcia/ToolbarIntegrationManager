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
    /// Interaction logic for Drawing_Highlighter.xaml
    /// </summary>
    public partial class Drawing_Highlighter : UserControl, IDeactivatableTool
    {
        public Drawing_Highlighter()
        {
            InitializeComponent();
            toggle.SetSymbol((DrawingImage)Application.Current.FindResource("SymbolMarker"));
            toggle.ToolTip = "Highlighter";
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
