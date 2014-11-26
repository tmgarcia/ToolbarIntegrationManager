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
using TestProject.UserControls;

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Measure_VerticalRuler.xaml
    /// </summary>
    public partial class Measure_VerticalRuler : UserControl, IDeactivatableTool
    {
        private MeasuringOverlay overlay;
        public Measure_VerticalRuler(MeasuringOverlay overlay)
        {
            InitializeComponent();
            Toggle.toggleControl.Checked += toggleControl_Checked;
            Toggle.toggleControl.Unchecked += toggleControl_Unchecked;
            Toggle.toggleControl.ToolTip = "Vertical Screen Ruler";
            Height = Constants.ToolButtonHeight;
            Width = Constants.ToolButtonWidth;
            Toggle.SetSymbol((DrawingImage)Application.Current.FindResource("SymbolRulerVertical"));
            this.overlay = overlay;
        }

        private void toggleControl_Unchecked(object sender, RoutedEventArgs e)
        {
            overlay.HideVerticalRuler();
        }

        private void toggleControl_Checked(object sender, RoutedEventArgs e)
        {
            if (!overlay.isActive)
            {
                overlay.ActivateOverlay();
            }
            overlay.ShowVerticalRuler();
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
