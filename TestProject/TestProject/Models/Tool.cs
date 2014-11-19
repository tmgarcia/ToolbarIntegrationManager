using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestProject.Models
{
    class Tool
    {
        public Tool()
        {
            userControl = null;
        }
        public Tool(UIElement control)
        {
            userControl = control;
        }
        public UIElement userControl;
        public void Deactivate()
        {
            if (((IDeactivatableTool)userControl) != null)
            {
                ((IDeactivatableTool)userControl).Deactivate();
            }
        }
        public void Activate()
        {
            if (((IDeactivatableTool)userControl) != null)
            {
                ((IDeactivatableTool)userControl).Activate();
            }
        }
    }
}
