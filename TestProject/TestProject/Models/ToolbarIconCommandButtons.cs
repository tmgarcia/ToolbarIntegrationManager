using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TestProject.Models
{
    class ToolbarIconCommandButtons
    {
        CommandButton _close;
        CommandButton _minimize;
        CommandButton _restore;
        CommandButton _help;

        public ToolbarIconCommandButtons()
        {
            _close = new CommandButton(Enums.CommandButtonTypes.ToolbarIconClose);
            _minimize = new CommandButton(Enums.CommandButtonTypes.ToolbarIconMinimize);
            _restore = new CommandButton(Enums.CommandButtonTypes.ToolbarIconRestore);
            _help = new CommandButton(Enums.CommandButtonTypes.ToolbarIconHelp);

            SetToInactiveState();
        }
        public void AddButtonsToGrid(Grid grid, int startCol, int startRow)
        {
            Grid.SetRow(Minimize, startRow);
            Grid.SetColumn(Minimize, startCol);
            grid.Children.Add(Minimize);
            Grid.SetRow(Restore, startRow);
            Grid.SetColumn(Restore, startCol);
            grid.Children.Add(Restore);
            Grid.SetRow(Close, startRow);
            Grid.SetColumn(Close, startCol + 1);
            grid.Children.Add(Close);
            Grid.SetRow(Help, startRow);
            Grid.SetColumn(Help, startCol + 1);
            grid.Children.Add(Help);
        }
        public void SetToInactiveState()
        {
            _close.Disable();
            _minimize.Disable();
            _restore.Disable();
            _help.Enable();
        }
        public void SetToActiveState()
        {
            _close.Enable();
            _minimize.Enable();
            _restore.Disable();
            _help.Disable();
        }
        public void SetToMinimizedState()
        {
            _close.Enable();
            _minimize.Disable();
            _restore.Enable();
            _help.Disable();
        }
        public void SetToMaximizedState()
        {
            _close.Enable();
            _minimize.Enable();
            _restore.Disable();
            _help.Disable();
        }

        public Button Close
        {
            get { return _close.GetButtonControl(); }
        }
        public Button Minimize
        {
            get { return _minimize.GetButtonControl(); }
        }
        public Button Restore
        {
            get { return _restore.GetButtonControl(); }
        }
        public Button Help
        {
            get { return _help.GetButtonControl(); }
        }

    }
}
