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

namespace TIM.UserControls
{
    /// <summary>
    /// Interaction logic for HSBASliders.xaml
    /// </summary>
    public partial class HSBASliders : UserControl
    {
        public static DependencyProperty CurrentColorProperty = DependencyProperty.Register("CurrentColor", typeof(Color), typeof(HSBASliders));

        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }

        LinearGradientBrush hBackground;//S-1 L-0.5

        public HSBASliders()
        {
            InitializeComponent();
            setupBrushes();
            sliders.DataContext = new { HBack = hBackground };
            hSlider.Value = 0;
            sSlider.Value = 1;
            bSlider.Value = 1;
            aSlider.Value = 1;
        }
        private void setupBrushes()
        {
            Point start = new Point(0, 0.5);
            Point end = new Point(1, 0.5);

            hBackground = new LinearGradientBrush();
            hBackground.StartPoint = start;
            hBackground.EndPoint = end;
            hBackground.GradientStops.Add(new GradientStop(new HSBAColor(0, 1, 1, 1).ToColor(), 0 / 6.0));
            hBackground.GradientStops.Add(new GradientStop(new HSBAColor(60, 1, 1, 1).ToColor(), 1 / 6.0));
            hBackground.GradientStops.Add(new GradientStop(new HSBAColor(120, 1, 1, 1).ToColor(), 2 / 6.0));
            hBackground.GradientStops.Add(new GradientStop(new HSBAColor(180, 1, 1, 1).ToColor(), 3 / 6.0));
            hBackground.GradientStops.Add(new GradientStop(new HSBAColor(240, 1, 1, 1).ToColor(), 4 / 6.0));
            hBackground.GradientStops.Add(new GradientStop(new HSBAColor(300, 1, 1, 1).ToColor(), 5 / 6.0));
            hBackground.GradientStops.Add(new GradientStop(new HSBAColor(359, 1, 1, 1).ToColor(), 1.0));
        }
        public void SetToColor(Color c)
        {
            HSBAColor hc = new RGBAColor(c).ToHSBA();

            hSlider.Value = hc.H;
            sSlider.Value = hc.S;
            bSlider.Value = hc.B;
            aSlider.Value = hc.A;
        }

    }
}
