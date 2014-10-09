using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Models;

namespace TestProject.Toolbars
{
    class DrawToolbar : Toolbar
    {
        public DrawToolbar() : base()
        {
            iconA   = "drawingIconActive.png";
            iconAH  = "drawingIconActiveHover.png";
            iconAP  = "drawingIconActivePressed.png";
            iconI   = "drawingIconInactive.png";
            iconIH  = "drawingIconInactiveHover.png";
            iconIP  = "drawingIconInactivePressed.png";
            iconD   = "drawingIconDisabled.png";
            SetupLaunchIcon();
            SetupCommandButtons();
        }
    }
}
