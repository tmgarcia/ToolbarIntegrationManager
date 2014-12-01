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
using System.Windows.Shapes;
using TIM.UserControls;

namespace TIM
{
    /// <summary>
    /// Interaction logic for TestWindow1.xaml
    /// </summary>
    public partial class TestWindow1 : Window
    {
        FloatingToolbar tb;
        public TestWindow1()
        {
            InitializeComponent();
            tb = new FloatingToolbar();
            gridControl.Children.Add(tb);
            tb.TemplateApplied+=new RoutedEventHandler(toolbarTemplateApplied);
            tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
        }
        private void toolbarTemplateApplied(object sender, RoutedEventArgs e)
        {
            tb.getCloseButtonControl().Click += new RoutedEventHandler(closeClicked);
            tb.getThumbControl().DragDelta += new DragDeltaEventHandler(toolbarDragged);
        }
        private void toolbarDragged(object sender, DragDeltaEventArgs e)
        {
            this.Left += e.HorizontalChange;
            this.Top += e.VerticalChange;
        }
        private void closeClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
