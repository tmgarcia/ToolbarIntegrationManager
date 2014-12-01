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
using TIM.Toolbars;

namespace TIM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool expanded;
        List<ColumnDefinition> expandedColumns;
        List<Models.Toolbar> toolbars;
        int numColsPerIcon = 4;
        int totalCols;

        int iconWidth = 150;
        int iconSpacing = 10;

        ToolbarIcon launchIcon;

        System.Windows.Forms.NotifyIcon ni;

        public MainWindow()
        {
            expanded = false;
            InitializeComponent();
            launcherWindow.Width = iconWidth + (2 * iconSpacing);//Launch icon width + spacing on left & right
            InitializeLauncherIcon();
            InitializeIcons();
            totalCols = (toolbars.Count * numColsPerIcon) + toolbars.Count;//Num toolbars & num cols per icon, + 1 column in between each icon
            InitializeColumnDefs();
            this.Deactivated += ToolbarWindow_Deactivated;
            this.Activated += ToolbarWindow_Activated;

            this.ShowInTaskbar = false;

            ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon(Constants.SolutionRoot + "Images/launcher.ico");
            ni.Visible = true;
            ni.DoubleClick +=
                delegate(object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = System.Windows.WindowState.Normal;
                };
            
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

        private void InitializeColumnDefs()
        {
            expandedColumns = new List<ColumnDefinition>();
            for (int i = 0; i < totalCols; i++)
            {
                expandedColumns.Add(new ColumnDefinition());
            }
        }
        private void InitializeLauncherIcon()
        {
            ImageStates states = new ImageStates();
            states.ActivePath = Constants.ToolbarIconPath + "launchIconActive.png";
            states.ActiveHoverPath = Constants.ToolbarIconPath + "launchIconActiveHover.png";
            states.ActivePressedPath = Constants.ToolbarIconPath + "launchIconActivePressed.png";
            states.InactivePath = Constants.ToolbarIconPath + "launchIconInactive.png";
            states.InactiveHoverPath = Constants.ToolbarIconPath + "launchIconInactiveHover.png";
            states.InactivePressedPath = Constants.ToolbarIconPath + "launchIconInactivePressed.png";
            states.DisabledPath = Constants.ToolbarIconPath + "launchIconDisabled.png";
            launchIcon = new ToolbarIcon(Constants.ToolbarIconWidth, Constants.ToolbarIconHeight, states, false);
            launchIcon.GetButtonControl().Click += new RoutedEventHandler(launcherButton_Click);
            Grid.SetRow(launchIcon.GetButtonControl(), 1);
            Grid.SetColumn(launchIcon.GetButtonControl(), 1);
            Grid.SetColumnSpan(launchIcon.GetButtonControl(), numColsPerIcon);
            launcherGrid.Children.Add(launchIcon.GetButtonControl());

            ToolbarIconCommandButtons launchCommands = new ToolbarIconCommandButtons();
            launchCommands.Close.Click += new RoutedEventHandler(closeButton_Click);
            launchCommands.Minimize.Click += new RoutedEventHandler(minimizeButton_Click);
            launchCommands.AddButtonsToGrid(launcherGrid, 3, 0);
            launchCommands.SetToActiveState();
        }
        private void InitializeIcons()
        {
            toolbars = new List<Models.Toolbar>();
            Toolbar t1 = new ColorToolbar();
            toolbars.Add(t1);
            Toolbar t2 = new DrawToolbar();
            toolbars.Add(t2);
            Toolbar t3 = new MeasureToolbar();
            toolbars.Add(t3);
            Toolbar t4 = new ConversionToolbar();
            toolbars.Add(t4);
            Toolbar t5 = new NotesToolbar();
            toolbars.Add(t5);


            int startCol = numColsPerIcon + 2;//2 = 1 col on the left of the launcher icon & 1 on the right
            int row = 1; //row 0 is for the command buttons
            for (int i = 0; i < toolbars.Count; i++)
            {
                int col = startCol + (i * numColsPerIcon) + i;
                Grid.SetRow(toolbars[i].icon.GetButtonControl(), row);
                Grid.SetColumn(toolbars[i].icon.GetButtonControl(), col);
                Grid.SetColumnSpan(toolbars[i].icon.GetButtonControl(), numColsPerIcon);
            }
        }
        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                this.Hide();
            }

            base.OnStateChanged(e);
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void launcherButton_Click(object sender, RoutedEventArgs e)
        {
            if (expanded)
            {
                Collapse();
                launchIcon.SetInactive();
                expanded = false;
            }
            else
            {
                Expand();
                launchIcon.SetActive();
                expanded = true;
            }
        }

        private void Expand()
        {
            launcherWindow.Width += ((toolbars.Count * iconWidth) + (toolbars.Count * iconSpacing));
            Grid.SetColumnSpan(backBorder, expandedColumns.Count + 6);
            foreach (ColumnDefinition c in expandedColumns)
            {
                launcherGrid.ColumnDefinitions.Add(c);
            }
            int startCol = numColsPerIcon+4;//what is this 4 for?!
            int row = 0;
            foreach (Toolbar b in toolbars)
            {
                launcherGrid.Children.Add(b.icon.GetButtonControl());
                b.commandButtons.AddButtonsToGrid(launcherGrid, startCol, row);
                startCol += numColsPerIcon + 1;
            }
        }
        private void Collapse()
        {
            launcherWindow.Width -= (toolbars.Count * (iconWidth + iconSpacing));
            Grid.SetColumnSpan(backBorder, toolbars.Count+2);//2 = one extra column at each end
            foreach (ColumnDefinition c in expandedColumns)
            {
                launcherGrid.ColumnDefinitions.Remove(c);
            }
            foreach (Toolbar b in toolbars)
            {
                launcherGrid.Children.Remove(b.icon.GetButtonControl());
                b.commandButtons.RemoveButtonsFromGrid(launcherGrid);
            }
        }

        private void launcherGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            ni.Visible = false;
            ni.Dispose();
            base.OnClosing(e);
        }
    }
}
