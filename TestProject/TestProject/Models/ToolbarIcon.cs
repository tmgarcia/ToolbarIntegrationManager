using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TestProject.UserControls;

namespace TestProject.Models
{
    class ToolbarIcon
    {
        ActiveInactiveImageButton button;
        bool isActive;

        public ToolbarIcon(int width, int height, ImageStates states, bool isActive)
        {
            this.isActive = isActive;
            button = new ActiveInactiveImageButton(width, height, states, isActive);
        }
        public Button GetButtonControl()
        {
            return button.buttonControl;
        }
        public void SetActive()
        {
            isActive = true;
            button.SetAsActive();
        }
        public void SetInactive()
        {
            isActive = false;
            button.SetAsInactive();
        }
    }
}
