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
        public static readonly RoutedEvent ToolExpandedEvent = EventManager.RegisterRoutedEvent("ToolExpanded", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BaseTool_Expandable));
        public event RoutedEventHandler ToolExpanded
        {
            add { AddHandler(ToolExpandedEvent, value); }
            remove { RemoveHandler(ToolExpandedEvent, value); }
        }
        public static readonly DependencyProperty PopupContentProperty =
            DependencyProperty.Register("PopupContent", typeof(object), typeof(BaseTool_Expandable),
            new UIPropertyMetadata(null));
        public object PopupContent
        {
            get { return (object)GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }

        private enum PopupPlacement
        {
            Top, Bottom, Left, Right
        }
        private enum PopupAlignment
        {
            TopRight, TopLeft, BottomRight, BottomLeft
        }

        private PopupPlacement currentPlacement;
        private PopupAlignment currentAlignment;
        public BaseTool_Expandable()
        {
            InitializeComponent();
            this.Loaded += BaseTool_Expandable_Loaded;
            SetPlacementModeBottom();
            Toggle.toggleControl.Checked += toggleControl_Checked;
            Toggle.toggleControl.Unchecked += toggleControl_Unchecked;
        }

        public void Expand()
        {
            Toggle.toggleControl.IsChecked = true;
        }
        public void Collapse()
        {
            Toggle.toggleControl.IsChecked = false;
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
                    RefreshPopupLocation();
                };
            }
        }

        public void RefreshPopupLocation()
        {
            var offset = popupControl.HorizontalOffset;
            popupControl.HorizontalOffset = offset + 1;
            popupControl.HorizontalOffset = offset;
        }
        void toggleControl_Checked(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(ToolExpandedEvent, this));
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
        public void SetPlacementModeBottom()
        {
            popupControl.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            currentPlacement = PopupPlacement.Bottom;
            this.LayoutUpdated += BaseTool_Expandable_LayoutUpdated;
        }
        public void SetPlacementModeTop()
        {
            popupControl.Placement = System.Windows.Controls.Primitives.PlacementMode.Top;
            currentPlacement = PopupPlacement.Top;
            this.LayoutUpdated += BaseTool_Expandable_LayoutUpdated;
        }
        public void SetPlacementModeLeft()
        {
            popupControl.Placement = System.Windows.Controls.Primitives.PlacementMode.Left;
            currentPlacement = PopupPlacement.Left;
            this.LayoutUpdated += BaseTool_Expandable_LayoutUpdated;
        }
        public void SetPlacementModeRight()
        {
            popupControl.Placement = System.Windows.Controls.Primitives.PlacementMode.Right;
            currentPlacement = PopupPlacement.Right;
            this.LayoutUpdated += BaseTool_Expandable_LayoutUpdated;
        }
        //HorizontalOffset="20" VerticalOffset="20">
        public void setAlignmentPointTopRight()
        {
            currentAlignment = PopupAlignment.TopRight;
            double popupWidth = (((FrameworkElement)popupControl.Child) == null) ? 0 : ((FrameworkElement)popupControl.Child).ActualWidth;
            double popupHeight = (((FrameworkElement)popupControl.Child) == null) ? 0 : ((FrameworkElement)popupControl.Child).ActualHeight;
            switch (currentPlacement)
            {
                case PopupPlacement.Top:
                    //left popup width- control width
                    //down popup height
                    popupControl.HorizontalOffset = -(Math.Abs(Toggle.toggleControl.ActualWidth - popupWidth));
                    popupControl.VerticalOffset = (popupHeight);
                    break;
                case PopupPlacement.Bottom:
                    //left popup width - target width
                    //no change

                    popupControl.HorizontalOffset = -(Math.Abs(Toggle.toggleControl.ActualWidth - popupWidth));
                    popupControl.VerticalOffset = 0;
                    break;
                case PopupPlacement.Left:
                    //no change
                    //no change

                    popupControl.HorizontalOffset = 0;
                    popupControl.VerticalOffset = 0;
                    break;
                case PopupPlacement.Right:
                    //left popup width 
                    //no change

                    popupControl.HorizontalOffset = -(popupWidth);
                    popupControl.VerticalOffset = 0;
                    break;
            }
            this.LayoutUpdated += BaseTool_Expandable_LayoutUpdated;
        }
        public void setAlignmentPointTopLeft()
        {
            currentAlignment = PopupAlignment.TopLeft;
            double popupWidth = (((FrameworkElement)popupControl.Child) == null) ? 0 : ((FrameworkElement)popupControl.Child).ActualWidth;
            double popupHeight = (((FrameworkElement)popupControl.Child) == null) ? 0 : ((FrameworkElement)popupControl.Child).ActualHeight;
            switch (currentPlacement)
            {
                case PopupPlacement.Top:
                    //no change
                    //down popup height

                    popupControl.HorizontalOffset = 0;
                    popupControl.VerticalOffset = popupHeight;
                    break;
                case PopupPlacement.Bottom:
                    //no change
                    //no change

                    popupControl.HorizontalOffset = 0;
                    popupControl.VerticalOffset = 0;
                    break;
                case PopupPlacement.Left:
                    //right popup width
                    //no change

                    popupControl.HorizontalOffset = popupWidth;
                    popupControl.VerticalOffset = 0;
                    break;
                case PopupPlacement.Right:
                    //no change
                    //no change

                    popupControl.HorizontalOffset = 0;
                    popupControl.VerticalOffset = 0;
                    break;
            }
            this.LayoutUpdated += BaseTool_Expandable_LayoutUpdated;
        }
        public void setAlignmentPointBottomRight()
        {
            currentAlignment = PopupAlignment.BottomRight;
            double popupWidth = (((FrameworkElement)popupControl.Child) == null) ? 0 : ((FrameworkElement)popupControl.Child).ActualWidth;
            double popupHeight = (((FrameworkElement)popupControl.Child) == null) ? 0 : ((FrameworkElement)popupControl.Child).ActualHeight;
            switch (currentPlacement)
            {
                case PopupPlacement.Top:
                    //left popup width - target width
                    //no change

                    popupControl.HorizontalOffset = -(Math.Abs(Toggle.toggleControl.ActualWidth - popupWidth));
                    popupControl.VerticalOffset = 0;
                    break;
                case PopupPlacement.Bottom:
                    //left width difference
                    //up popup height

                    popupControl.HorizontalOffset = -(Math.Abs(Toggle.toggleControl.ActualWidth - popupWidth));
                    popupControl.VerticalOffset = -(popupControl.ActualHeight);
                    break;
                case PopupPlacement.Left:
                    //no change
                    //up height difference

                    popupControl.HorizontalOffset = 0;
                    popupControl.VerticalOffset = -(Math.Abs(Toggle.toggleControl.ActualHeight - popupHeight));
                    break;
                case PopupPlacement.Right:
                    //left popup width
                    //up height difference

                    popupControl.HorizontalOffset = -(popupControl.ActualWidth);
                    popupControl.VerticalOffset = -(Math.Abs(Toggle.toggleControl.ActualHeight - popupHeight));
                    break;
            }
            this.LayoutUpdated += BaseTool_Expandable_LayoutUpdated;
        }
        public void setAlignmentPointBottomLeft()
        {
            currentAlignment = PopupAlignment.BottomLeft;
            double popupWidth = (((FrameworkElement)popupControl.Child) == null) ? 0 : ((FrameworkElement)popupControl.Child).ActualWidth;
            double popupHeight = (((FrameworkElement)popupControl.Child) == null) ? 0 : ((FrameworkElement)popupControl.Child).ActualHeight;
            switch (currentPlacement)
            {
                case PopupPlacement.Top:
                    //no change
                    //no change

                    popupControl.HorizontalOffset = 0;
                    popupControl.VerticalOffset = 0;
                    break;
                case PopupPlacement.Bottom:
                    //no change
                    //up popup height

                    popupControl.HorizontalOffset = 0;
                    popupControl.VerticalOffset = -(popupHeight);
                    break;
                case PopupPlacement.Left:
                    //right width difference
                    //up height difference

                    popupControl.HorizontalOffset = (Math.Abs(popupWidth - Toggle.toggleControl.ActualWidth));
                    popupControl.VerticalOffset = -(Math.Abs(Toggle.toggleControl.ActualHeight - popupHeight));
                    break;
                case PopupPlacement.Right:
                    //no change
                    //up height difference

                    popupControl.HorizontalOffset = 0;
                    popupControl.VerticalOffset = -(Math.Abs(Toggle.toggleControl.ActualHeight - popupHeight));
                    break;
            }
            this.LayoutUpdated += BaseTool_Expandable_LayoutUpdated;
        }

        void BaseTool_Expandable_LayoutUpdated(object sender, EventArgs e)
        {
            this.LayoutUpdated -= BaseTool_Expandable_LayoutUpdated;
            RefreshPopupLocation();
        }
    }
}
