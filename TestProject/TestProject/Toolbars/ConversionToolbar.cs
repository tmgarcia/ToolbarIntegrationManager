using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Models;

namespace TestProject.Toolbars
{
    class ConversionToolbar : Toolbar
    {
        public ConversionToolbar() : base()
        {
            iconA   = "conversionIconActive.png";
            iconAH  = "conversionIconActiveHover.png";
            iconAP  = "conversionIconActivePressed.png";
            iconI   = "conversionIconInactive.png";
            iconIH  = "conversionIconInactiveHover.png";
            iconIP  = "conversionIconInactivePressed.png";
            iconD   = "conversionIconDisabled.png";
            SetupLaunchIcon();
            SetupCommandButtons();
        }
    }
}
