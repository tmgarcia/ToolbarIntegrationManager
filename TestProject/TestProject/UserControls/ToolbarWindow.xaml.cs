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

        public FloatingToolbar toolbar;
        public ToolbarWindow()
        {
            InitializeComponent();
            toolbar = new FloatingToolbar();
            gridControl.Children.Add(toolbar);
            toolbar.TemplateApplied += new RoutedEventHandler(toolbarTemplateApplied);
            toolbar.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

            this.Deactivated += ToolbarWindow_Deactivated;
            this.Activated += ToolbarWindow_Activated;
            this.ShowInTaskbar = false;
        }

        void ToolbarWindow_Activated(object sender, EventArgs e)
        {
            this.Topmost = true;
        }

        void ToolbarWindow_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;
            this.Activate();
        }
        private void toolbarTemplateApplied(object sender, RoutedEventArgs e)
        {
            toolbar.getCloseButtonControl().Click += new RoutedEventHandler(closeClicked);
            toolbar.getThumbControl().DragDelta += new DragDeltaEventHandler(toolbarDragged);
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
            toolbar.Items.Clear();
            this.Close();
        }
        
    }
}
