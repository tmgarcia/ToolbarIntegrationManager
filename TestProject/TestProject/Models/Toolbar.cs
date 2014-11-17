using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestProject.Enums;
using TestProject.UserControls;

namespace TestProject.Models
{
    class Toolbar
    {
        public ToolbarIcon icon;
        public string displayIconPath;
        public ToolbarIconCommandButtons commandButtons;
        DisplayStates displayState;
        DisplayOrientations displayOrientation;
        bool isActive;
        protected ToolbarWindow display;

        protected string iconA;
        protected string iconAH;
        protected string iconAP;
        protected string iconI;
        protected string iconIH;
        protected string iconIP;
        protected string iconD;

        protected List<Tool> tools;

        public Toolbar()
        {
            isActive = false;
            tools = new List<Tool>();
            displayIconPath = Constants.SolutionRoot + "Images/ToolbarIcons/toolbarIconTest.png";
        }
        protected void SetupLaunchIcon()
        {
            ImageStates states = new ImageStates();
            states.ActivePath = Constants.ToolbarIconPath + iconA;
            states.ActiveHoverPath = Constants.ToolbarIconPath + iconAH;
            states.ActivePressedPath = Constants.ToolbarIconPath + iconAP;
            states.InactivePath = Constants.ToolbarIconPath + iconI;
            states.InactiveHoverPath = Constants.ToolbarIconPath + iconIH;
            states.InactivePressedPath = Constants.ToolbarIconPath + iconIP;
            states.DisabledPath = Constants.ToolbarIconPath + iconD;
            icon = new ToolbarIcon(Constants.ToolbarIconWidth, Constants.ToolbarIconHeight, states, false);
            icon.GetButtonControl().Click += new RoutedEventHandler(launchIcon_Click);
        }
        protected void SetupCommandButtons()
        {
            commandButtons = new ToolbarIconCommandButtons();
            commandButtons.Close.Click += new RoutedEventHandler(launchIconClose_Click);
            commandButtons.Minimize.Click += new RoutedEventHandler(launchIconMinimize_Click);
            commandButtons.SetToInactiveState();
        }

        protected void launchIconClose_Click(object sender, RoutedEventArgs e)
        {
            display.CloseToolbar();
        }
        protected void launchIconMinimize_Click(object sender, RoutedEventArgs e)
        {

        }
        protected virtual void PreDisplaySetup()
        {
            foreach (Tool t in tools)
            {
                display.toolbar.Items.Add(t.userControl);
            }
        }
        protected virtual void PostDisplaySetup()
        {

        }
        protected void launchIcon_Click(object sender, RoutedEventArgs e)
        {
            if (!isActive)
            {
                display = new ToolbarWindow(displayIconPath);
                display.ToolbarClosed += ToolbarClosed;
                PreDisplaySetup();
                display.Show();
                PostDisplaySetup();
                isActive = true;
                commandButtons.SetToActiveState();
                icon.SetActive();
            }
        }
        protected virtual void ToolbarClosed(object sender, System.Windows.RoutedEventArgs e)
        {
            isActive = false;
            commandButtons.SetToInactiveState();
            icon.SetInactive();
        }

    }
}
