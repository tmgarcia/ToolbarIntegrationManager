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

namespace TestProject.UserControls
{
    /// <summary>
    /// Interaction logic for ToolbarWindow.xaml
    /// </summary>
    public partial class ToolbarWindow : Window
    {
        public static readonly RoutedEvent ToolbarClosedEvent = EventManager.RegisterRoutedEvent("ToolbarClosed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ToolbarWindow));

        public event RoutedEventHandler ToolbarClosed
        {
            add { AddHandler(ToolbarClosedEvent, value); }
            remove { RemoveHandler(ToolbarClosedEvent, value); }
        }

        FloatingToolbar tb;
        public ToolbarWindow()
        {
            InitializeComponent();
            tb = new FloatingToolbar();
            gridControl.Children.Add(tb);
            tb.TemplateApplied += new RoutedEventHandler(toolbarTemplateApplied);
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
            CloseToolbar();
        }
        public void CloseToolbar()
        {
            this.RaiseEvent(new RoutedEventArgs(ToolbarClosedEvent, this));
            this.Close();
        }
    }
}
