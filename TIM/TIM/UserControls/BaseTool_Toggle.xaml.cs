using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TIM.UserControls
{
    /// <summary>
    /// Interaction logic for BaseTool_Toggle.xaml
    /// </summary>
    public partial class BaseTool_Toggle : UserControl
    {
        ImageSource sym;
        public BaseTool_Toggle()
        {
            InitializeComponent();
            this.Loaded += BaseTool_Toggle_Loaded;
        }

        void BaseTool_Toggle_Loaded(object sender, RoutedEventArgs e)
        {
            Height = Constants.ToolButtonHeight;
            Width = Constants.ToolButtonWidth;
        }

        public void SetSymbol(ImageSource symbol)
        {
            sym = symbol;
            //((Image)toggleControl.Template.FindName("Symbol", toggleControl)).Source = symbol;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            toggleControl.ApplyTemplate();
            Image s = ((Image)toggleControl.Template.FindName("Symbol", toggleControl));
            s.Source = sym;
        }

        public ToggleButton Toggle
        {
            get { return toggleControl; }
        }
    }
}
