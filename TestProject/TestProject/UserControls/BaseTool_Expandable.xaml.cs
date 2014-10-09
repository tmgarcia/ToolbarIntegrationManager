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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestProject.UserControls
{
    /// <summary>
    /// Interaction logic for BaseTool_Expandable.xaml
    /// </summary>
    public partial class BaseTool_Expandable : UserControl
    {
        public static readonly DependencyProperty PopupContentProperty =
            DependencyProperty.Register("PopupContent", typeof(object), typeof(BaseTool_Expandable),
            new UIPropertyMetadata(null));
        public object PopupContent
        {
            get { return (object)GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }

        public BaseTool_Expandable()
        {
            InitializeComponent();
            this.Loaded += BaseTool_Expandable_Loaded;
            setExpandDirectionDown();
            Toggle.toggleControl.Checked += toggleControl_Checked;
            Toggle.toggleControl.Unchecked += toggleControl_Unchecked;
        }

        void BaseTool_Expandable_Loaded(object sender, RoutedEventArgs e)
        {
            Height = Constants.ToolButtonHeight;
            Width = Constants.ToolButtonWidth;

            Window w = Window.GetWindow(Toggle);
            if (null != w)
            {
                w.LocationChanged += delegate(object sender2, EventArgs args)
                {
                    var offset = popupControl.HorizontalOffset;
                    popupControl.HorizontalOffset = offset + 1;
                    popupControl.HorizontalOffset = offset;
                };
            }
        }

        void toggleControl_Checked(object sender, RoutedEventArgs e)
        {
            popupControl.IsOpen = true;
        }
        void toggleControl_Unchecked(object sender, RoutedEventArgs e)
        {
            popupControl.IsOpen = false;
        }
        

        public void setSymbol(ImageSource symbol)
        {
            Toggle.SetSymbol(symbol);
        }
        public void setExpandDirectionDown()
        {
            popupControl.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
        }
        public void setExpandDirectionUp()
        {
            popupControl.Placement = System.Windows.Controls.Primitives.PlacementMode.Top;
        }
        public void setExpandDirectionLeft()
        {
            popupControl.Placement = System.Windows.Controls.Primitives.PlacementMode.Left;
        }
        public void setExpandDirectionRight()
        {
            popupControl.Placement = System.Windows.Controls.Primitives.PlacementMode.Right;
        }
    }
}
