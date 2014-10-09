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
using TestProject.Models;
using TestProject.Toolbars;

namespace TestProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool expanded;
        List<ColumnDefinition> expandedColumns;
        List<Models.Toolbar> toolbars;
        int numToolbarIcons=4;
        int numColsPerIcon = 4;
        int totalCols;

        int iconWidth = 150;
        int iconSpacing = 10;

        ToolbarIcon launchIcon;

        public MainWindow()
        {
            expanded = false;
            totalCols = (numToolbarIcons * numColsPerIcon)+numToolbarIcons;
            InitializeComponent();
            launcherWindow.Width = iconWidth + (2 * iconSpacing);
            InitializeColumnDefs();
            InitializeLauncherIcon();
            InitializeIcons();
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
            Toolbar t2 = new ColorToolbar();
            toolbars.Add(t2);
            Toolbar t3 = new ColorToolbar();
            toolbars.Add(t3);
            Toolbar t4 = new ColorToolbar();
            toolbars.Add(t4);


            int startCol = numColsPerIcon + 2;
            int row = 1;
            for (int i = 0; i < numToolbarIcons; i++)
            {
                int col = startCol + (i * numColsPerIcon) + i;
                Grid.SetRow(toolbars[i].icon.GetButtonControl(), row);
                Grid.SetColumn(toolbars[i].icon.GetButtonControl(), col);
                Grid.SetColumnSpan(toolbars[i].icon.GetButtonControl(), numColsPerIcon);
            }
        }
        void b1_Click(object sender, RoutedEventArgs e)
        {
            TestWindow1 w = new TestWindow1();
            w.Show();
        }
        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            
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
            launcherWindow.Width += ((numToolbarIcons * iconWidth) + (numToolbarIcons * iconSpacing));
            Grid.SetColumnSpan(backBorder, expandedColumns.Count + 6);
            foreach (ColumnDefinition c in expandedColumns)
            {
                launcherGrid.ColumnDefinitions.Add(c);
            }
            int startCol = numColsPerIcon+4;
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
            launcherWindow.Width -= (numToolbarIcons * (iconWidth + iconSpacing));
            Grid.SetColumnSpan(backBorder,6);
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
    }
}
