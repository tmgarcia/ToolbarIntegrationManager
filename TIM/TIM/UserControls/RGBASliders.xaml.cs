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

namespace TIM.UserControls
{
    /// <summary>
    /// Interaction logic for RGBASliders.xaml
    /// </summary>
    public partial class RGBASliders : UserControl
    {
        public static DependencyProperty CurrentColorProperty = DependencyProperty.Register("CurrentColor", typeof(Color), typeof(RGBASliders));

        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }

        public RGBASliders()
        {
            InitializeComponent();
        }

        public void SetToColor(Color c)
        {
            rSlider.Value = c.R;
            gSlider.Value = c.G;
            bSlider.Value = c.B;
            aSlider.Value = c.A;
        }

    }
}
