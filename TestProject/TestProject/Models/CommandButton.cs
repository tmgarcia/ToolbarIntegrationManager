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
                    normalPath = "Images/commandButtonToolbarIconCloseN.png";
                    hoverPath = "Images/commandButtonToolbarIconCloseH.png";
                    pressedPath = "Images/commandButtonToolbarIconCloseP.png";
                    break;
                case CommandButtonTypes.ToolbarIconMinimize:
                    normalPath = "Images/commandButtonToolbarIconMinimizeN.png";
                    hoverPath = "Images/commandButtonToolbarIconMinimizeH.png";
                    pressedPath = "Images/commandButtonToolbarIconMinimizeP.png";
                    break;
                case CommandButtonTypes.ToolbarIconMaximize:

                    break;
                case CommandButtonTypes.ToolbarIconHelp:

                    break;
                default:

                    break;
            }
        }
    }
}
