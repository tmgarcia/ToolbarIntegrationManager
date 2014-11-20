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
    /// Interaction logic for Notes_Clipboard.xaml
    /// </summary>
    public partial class Notes_Clipboard : UserControl, IDeactivatableTool
    {
        Notes_ClipboardInner innerUC;
        public Notes_Clipboard()
        {
            InitializeComponent();
            expandable.setSymbol((DrawingImage)Application.Current.FindResource("SymbolLineDiagonal"));
            innerUC = expandable.PopupContent as Notes_ClipboardInner;
        }

        public void Activate()
        {
            innerUC.Activate();
        }

        public void Deactivate()
        {
            innerUC.Deactivate();
        }
    }
}
