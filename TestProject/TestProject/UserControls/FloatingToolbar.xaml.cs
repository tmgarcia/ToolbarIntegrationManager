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
    /// Interaction logic for FloatingToolbar.xaml
    /// </summary>
    public partial class FloatingToolbar : ToolBar
    {
        public static readonly RoutedEvent TemplateAppliedEvent = EventManager.RegisterRoutedEvent("TemplateApplied", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FloatingToolbar));

        public FloatingToolbar()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler TemplateApplied
        {
            add { AddHandler(TemplateAppliedEvent, value); }
            remove { RemoveHandler(TemplateAppliedEvent, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.RaiseEvent(new RoutedEventArgs(TemplateAppliedEvent, this));
        }

        public Button getCloseButtonControl()
        {
            Button bc =  (Button)(toolbarControl.Template.FindName("CloseButton", toolbarControl));

            return bc;
        }
        public Thumb getThumbControl()
        {
            Thumb th = (Thumb)(toolbarControl.Template.FindName("ToolBarThumb", toolbarControl));
            return th;
        }
    }
}
