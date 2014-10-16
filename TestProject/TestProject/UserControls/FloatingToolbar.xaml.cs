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
using TestProject.Enums;

namespace TestProject.UserControls
{
    /// <summary>
    /// Interaction logic for FloatingToolbar.xaml
    /// </summary>
    public partial class FloatingToolbar : ToolBar
    {
        public static readonly RoutedEvent TemplateAppliedEvent = EventManager.RegisterRoutedEvent("TemplateApplied", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FloatingToolbar));

        public bool isCollapsed = false;
        public DisplayOrientations orientation = DisplayOrientations.Horizontal;
        public double width;
        public double height;

        public FloatingToolbar()
        {
            width = Constants.ToolbarHorizontalExpandedWidth;
            height = Constants.ToolbarHorizontalExpandedHeight;

            InitializeComponent();
            toolbarControl.Width = Double.NaN;
            toolbarControl.Height = height;
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

        private void CollapseButton_Checked(object sender, RoutedEventArgs e)
        {
            isCollapsed = true;
            collapse();
        }

        private void CollapseButton_Unchecked(object sender, RoutedEventArgs e)
        {
            isCollapsed = false;
            expand();
        }

        private void OrientationButton_Click(object sender, RoutedEventArgs e)
        {
            if (orientation == DisplayOrientations.Horizontal)
            {
                orientation = DisplayOrientations.Vertical;
                orientVertical();
            }
            else
            {
                orientation = DisplayOrientations.Horizontal;
                orientHorizontal();
            }
        }

        private void collapse()
        {
            ToggleButton btn = (ToggleButton)(toolbarControl.Template.FindName("CollapseButton", toolbarControl));
            ScaleTransform scale = (ScaleTransform)(btn.Template.FindName("FlipScale", btn));
            scale.ScaleX = -1;

            ToolBarPanel panel = (ToolBarPanel)(toolbarControl.Template.FindName("PART_ToolBarPanel", toolbarControl));
            panel.Visibility = System.Windows.Visibility.Collapsed;
            if (orientation == DisplayOrientations.Horizontal)
            {
                toolbarControl.Width = Double.NaN;
            }
            else
            {
                toolbarControl.Height = Double.NaN;
            }
        }
        private void expand()
        {
            ToggleButton btn = (ToggleButton)(toolbarControl.Template.FindName("CollapseButton", toolbarControl));
            ScaleTransform scale = (ScaleTransform)(btn.Template.FindName("FlipScale", btn));
            scale.ScaleX = 1;

            ToolBarPanel panel = (ToolBarPanel)(toolbarControl.Template.FindName("PART_ToolBarPanel", toolbarControl));
            panel.Visibility = System.Windows.Visibility.Visible;

            if (orientation == DisplayOrientations.Horizontal)
            {
                toolbarControl.Width = Double.NaN;
            }
            else
            {
                toolbarControl.Height = Double.NaN;
            }
        }

        private void orientHorizontal()
        {
            double actualWidth = toolbarControl.RenderSize.Width;
            width = toolbarControl.Width = toolbarControl.RenderSize.Height;
            height = toolbarControl.Height = actualWidth;

            DockPanel dock = (DockPanel)(toolbarControl.Template.FindName("Dock", toolbarControl));

            StackPanel CommandButtonsPanel = (StackPanel)(toolbarControl.Template.FindName("CommandButtonsPanel", toolbarControl));
            CommandButtonsPanel.Orientation = System.Windows.Controls.Orientation.Vertical;
            DockPanel.SetDock(CommandButtonsPanel, Dock.Right);
            dock.Children.Remove(CommandButtonsPanel);
            dock.Children.Insert(dock.Children.Count-2, CommandButtonsPanel);

            StackPanel CollapseAndIconPanel = (StackPanel)(toolbarControl.Template.FindName("CollapseAndIconPanel", toolbarControl));
            double cipW = CollapseAndIconPanel.ActualWidth;
            DockPanel.SetDock(CollapseAndIconPanel, Dock.Left);

            ToggleButton btn = (ToggleButton)(toolbarControl.Template.FindName("CollapseButton", toolbarControl));
            btn.Width = 20;
            ((RotateTransform)(btn.Template.FindName("BorderRotateL", btn))).Angle = 0;
            ((ScaleTransform)(btn.Template.FindName("BorderScale", btn))).ScaleX = 1;
            CollapseAndIconPanel.Width = cipW;
            CollapseAndIconPanel.Width = Double.NaN;
            CollapseAndIconPanel.Height = Double.NaN;
            CollapseAndIconPanel.Orientation = System.Windows.Controls.Orientation.Horizontal;

            Thumb ToolBarThumb = (Thumb)(toolbarControl.Template.FindName("ToolBarThumb", toolbarControl));
            DockPanel.SetDock(ToolBarThumb, Dock.Left);

            ToolBarThumb.Width = ToolBarThumb.ActualHeight;
            ToolBarThumb.Height = Double.NaN;

            ToolBarPanel panel = (ToolBarPanel)(toolbarControl.Template.FindName("PART_ToolBarPanel", toolbarControl));
            panel.Orientation = System.Windows.Controls.Orientation.Horizontal;

            //00 10
            LinearGradientBrush BorderBackgroundBrush = (LinearGradientBrush)(toolbarControl.Template.FindName("BorderBackgroundBrush", toolbarControl));
            BorderBackgroundBrush.StartPoint = Constants.VerticalGradientStart;
            BorderBackgroundBrush.EndPoint = Constants.VerticalGradientEnd;
        }
        private void orientVertical()
        {
            double actualWidth = toolbarControl.RenderSize.Width;
            width = toolbarControl.Width = toolbarControl.RenderSize.Height;
            height = toolbarControl.Height = actualWidth;

            DockPanel dock = (DockPanel)(toolbarControl.Template.FindName("Dock", toolbarControl));

            StackPanel CommandButtonsPanel = (StackPanel)(toolbarControl.Template.FindName("CommandButtonsPanel", toolbarControl));
            CommandButtonsPanel.Orientation = System.Windows.Controls.Orientation.Horizontal;
            DockPanel.SetDock(CommandButtonsPanel, Dock.Top);
            dock.Children.Remove(CommandButtonsPanel);
            dock.Children.Insert(0,CommandButtonsPanel);

            StackPanel CollapseAndIconPanel = (StackPanel)(toolbarControl.Template.FindName("CollapseAndIconPanel", toolbarControl));
            double cipH = CollapseAndIconPanel.ActualHeight;
            CollapseAndIconPanel.Orientation = System.Windows.Controls.Orientation.Vertical;
            DockPanel.SetDock(CollapseAndIconPanel, Dock.Top);

            ToggleButton btn = (ToggleButton)(toolbarControl.Template.FindName("CollapseButton", toolbarControl));
            //btn.Width = width / 5;
            ((RotateTransform)(btn.Template.FindName("BorderRotateL", btn))).Angle = 90;
            ((ScaleTransform)(btn.Template.FindName("BorderScale", btn))).ScaleX = 1.5;
            CollapseAndIconPanel.Height = Double.NaN;
            CollapseAndIconPanel.Width = cipH;

            Thumb ToolBarThumb = (Thumb)(toolbarControl.Template.FindName("ToolBarThumb", toolbarControl));
            DockPanel.SetDock(ToolBarThumb, Dock.Top);

            ToolBarThumb.Height = ToolBarThumb.RenderSize.Width;
            ToolBarThumb.Width = Double.NaN;

            ToolBarPanel panel = (ToolBarPanel)(toolbarControl.Template.FindName("PART_ToolBarPanel", toolbarControl));
            panel.Orientation = System.Windows.Controls.Orientation.Vertical;

            //00 10
            LinearGradientBrush BorderBackgroundBrush = (LinearGradientBrush)(toolbarControl.Template.FindName("BorderBackgroundBrush", toolbarControl));
            BorderBackgroundBrush.StartPoint = Constants.HorizontalGradientStart;
            BorderBackgroundBrush.EndPoint = Constants.HorizontalGradientEnd;
        }
    }
}
