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

namespace TestProject.UserControls
{
    /// <summary>
    /// Interaction logic for BacklessSymbolButton.xaml
    /// </summary>
    public partial class EnabledSymbolButton : Button
    {
        ImageSource symbol;
        public EnabledSymbolButton(ImageSource symbol)
        {
            this.symbol = symbol;
            InitializeComponent();
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ((Image)buttonControl.Template.FindName("Symbol", buttonControl)).Source = symbol;
        }
        public void Disable()
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
        }
        public void Enable()
        {
            this.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
