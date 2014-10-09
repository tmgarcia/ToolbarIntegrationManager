using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Models;
using TestProject.UserControls;

namespace TestProject.Toolbars
{
    class ColorToolbar : Toolbar
    {
        public ColorToolbar() : base()
        {
            iconA   = "colorsIconActive.png";
            iconAH  = "colorsIconActiveHover.png";
            iconAP  = "colorsIconActivePressed.png";
            iconI   = "colorsIconInactive.png";
            iconIH  = "colorsIconInactiveHover.png";
            iconIP  = "colorsIconInactivePressed.png";
            iconD   = "colorsIconDisabled.png";
            SetupLaunchIcon();
            SetupCommandButtons();
        }
    }
}
