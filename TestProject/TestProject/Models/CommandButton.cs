using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TestProject.Enums;
using TestProject.UserControls;

namespace TestProject.Models
{
    class CommandButton
    {
        CommandButtonTypes type;
        ImageSource symbol;
        EnabledSymbolButton button;

        public CommandButton(CommandButtonTypes buttonType)
        {
            type = buttonType;
            GetSymbol();
            button = new EnabledSymbolButton(symbol);
            button.ApplyTemplate();
        }

        public Button GetButtonControl()
        {
            return button.buttonControl;
        }

        public void Disable()
        {
            button.Disable();
        }
        public void Enable()
        {
            button.Enable();
        }

        private void GetSymbol()
        {
            switch (type)
            {
                case CommandButtonTypes.ToolbarIconClose:
                    symbol = (DrawingImage)Application.Current.FindResource("SymbolClose");
                    break;
                case CommandButtonTypes.ToolbarIconMinimize:
                    symbol = (DrawingImage)Application.Current.FindResource("SymbolBar");
                    break;
                case CommandButtonTypes.ToolbarIconRestore:
                    symbol = (DrawingImage)Application.Current.FindResource("SymbolWindows");
                    break;
                case CommandButtonTypes.ToolbarIconHelp:
                    symbol = (DrawingImage)Application.Current.FindResource("SymbolQuestionMark");
                    break;
                default:

                    break;
            }
        }
    }
}
