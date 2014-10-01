using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestProject.Enums;
using TestProject.UserControls;

namespace TestProject.Models
{
    class CommandButton
    {
        CommandButtonTypes type;
        string normalPath;
        string hoverPath;
        string pressedPath;
        ThreeStateEnabledButton button;

        public CommandButton(CommandButtonTypes buttonType)
        {
            type = buttonType;
            SetupTypePaths();
            button = new ThreeStateEnabledButton(normalPath, hoverPath, pressedPath);
        }

        public Button GetButtonControl()
        {
            return button.buttonControl;
        }

        public void Disable()
        {
            button.disable();
        }
        public void Enable()
        {
            button.enable();
        }

        private void SetupTypePaths()
        {
            switch (type)
            {
                case CommandButtonTypes.ToolbarIconClose:
                    normalPath = Constants.CommandButtonPath + "commandButtonToolbarIconCloseN.png";
                    hoverPath = Constants.CommandButtonPath + "commandButtonToolbarIconCloseH.png";
                    pressedPath = Constants.CommandButtonPath + "commandButtonToolbarIconCloseP.png";
                    break;
                case CommandButtonTypes.ToolbarIconMinimize:
                    normalPath = Constants.CommandButtonPath + "commandButtonToolbarIconMinimizeN.png";
                    hoverPath = Constants.CommandButtonPath + "commandButtonToolbarIconMinimizeH.png";
                    pressedPath = Constants.CommandButtonPath + "commandButtonToolbarIconMinimizeP.png";
                    break;
                case CommandButtonTypes.ToolbarIconRestore:
                    normalPath = Constants.CommandButtonPath + "commandButtonToolbarIconRestoreN.png";
                    hoverPath = Constants.CommandButtonPath + "commandButtonToolbarIconRestoreH.png";
                    pressedPath = Constants.CommandButtonPath + "commandButtonToolbarIconRestoreP.png";
                    break;
                case CommandButtonTypes.ToolbarIconHelp:
                    normalPath = Constants.CommandButtonPath + "commandButtonToolbarIconHelpN.png";
                    hoverPath = Constants.CommandButtonPath + "commandButtonToolbarIconHelpH.png";
                    pressedPath = Constants.CommandButtonPath + "commandButtonToolbarIconHelpP.png";
                    break;
                default:

                    break;
            }
        }
    }
}
