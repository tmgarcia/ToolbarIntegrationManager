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

namespace TestProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool expanded;
        List<ColumnDefinition> expandedColumns;
        List<Button> icons;
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
            icons = new List<Button>();
            Button b1 = new Button();
            b1.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            b1.Click += new RoutedEventHandler(b1_Click);
            Button b2 = new Button();
            b2.Background = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
            Button b3 = new Button();
            b3.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 255));
            Button b4 = new Button();
            b4.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));
            icons.Add(b1);
            icons.Add(b2);
            icons.Add(b3);
            icons.Add(b4);

            int startCol = numColsPerIcon + 2;
            int row = 1;
            for (int i = 0; i < numToolbarIcons; i++)
            {
                int col = startCol + (i * numColsPerIcon) + i;
                Grid.SetRow(icons[i], row);
                Grid.SetColumn(icons[i], col);
                Grid.SetColumnSpan(icons[i], numColsPerIcon);
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
            foreach (ColumnDefinition c in expandedColumns)
            {
                launcherGrid.ColumnDefinitions.Add(c);
            }
            int startCol = numColsPerIcon+2;
            int row = 1;
            foreach (Button b in icons)
            {
                launcherGrid.Children.Add(b);
            }
        }
        private void Collapse()
        {
            launcherWindow.Width -= (numToolbarIcons * (iconWidth + iconSpacing));
            foreach (ColumnDefinition c in expandedColumns)
            {
                launcherGrid.ColumnDefinitions.Remove(c);
            }
            foreach (Button b in icons)
            {
                launcherGrid.Children.Remove(b);
            }
        }
    }
}
