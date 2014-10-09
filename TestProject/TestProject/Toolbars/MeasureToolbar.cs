using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Models;

namespace TestProject.Toolbars
{
    class MeasureToolbar : Toolbar
    {
        public MeasureToolbar() : base()
        {
            iconA   = "measureIconActive.png";
            iconAH  = "measureIconActiveHover.png";
            iconAP  = "measureIconActivePressed.png";
            iconI   = "measureIconInactive.png";
            iconIH  = "measureIconInactiveHover.png";
            iconIP  = "measureIconInactivePressed.png";
            iconD   = "measureIconDisabled.png";
            SetupLaunchIcon();
            SetupCommandButtons();
        }
    }
}
