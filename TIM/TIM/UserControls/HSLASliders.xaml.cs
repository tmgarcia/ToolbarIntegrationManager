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
    /// Interaction logic for HSLASliders.xaml
    /// </summary>
    public partial class HSLASliders : UserControl
    {
        public static DependencyProperty CurrentColorProperty = DependencyProperty.Register("CurrentColor", typeof(Color), typeof(HSLASliders));

        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }
        LinearGradientBrush hBackground;//S-1 L-0.5

        public HSLASliders()
        {
            InitializeComponent();
            setupBrushes();
            sliders.DataContext = new { HBack = hBackground };
            hSlider.Value = 0;
            sSlider.Value = 1;
            lSlider.Value = 0.5f;
            aSlider.Value = 1;
        }
        private void setupBrushes()
        {
            Point start = new Point(0, 0.5);
            Point end = new Point(1, 0.5);

            hBackground = new LinearGradientBrush();
            hBackground.StartPoint = start;
            hBackground.EndPoint = end;
            hBackground.GradientStops.Add(new GradientStop(new HSLAColor(0, 1, 0.5f, 1).ToColor(), 0 / 6.0));
            hBackground.GradientStops.Add(new GradientStop(new HSLAColor(60, 1, 0.5f, 1).ToColor(), 1 / 6.0));
            hBackground.GradientStops.Add(new GradientStop(new HSLAColor(120, 1, 0.5f, 1).ToColor(), 2 / 6.0));
            hBackground.GradientStops.Add(new GradientStop(new HSLAColor(180, 1, 0.5f, 1).ToColor(), 3 / 6.0));
            hBackground.GradientStops.Add(new GradientStop(new HSLAColor(240, 1, 0.5f, 1).ToColor(), 4 / 6.0));
            hBackground.GradientStops.Add(new GradientStop(new HSLAColor(300, 1, 0.5f, 1).ToColor(), 5 / 6.0));
            hBackground.GradientStops.Add(new GradientStop(new HSLAColor(359, 1, 0.5f, 1).ToColor(), 1.0));
        }
        public void SetToColor(Color c)
        {
            HSLAColor hc = new RGBAColor(c).ToHSLA();

            hSlider.Value = hc.H;
            sSlider.Value = hc.S;
            lSlider.Value = hc.L;
            aSlider.Value = hc.A;
        }
    }
}
