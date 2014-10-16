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
    }
}
