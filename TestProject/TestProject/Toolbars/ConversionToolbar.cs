using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Models;
using TestProject.UserControls;
using TestProject.Tools;

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
            displayIconPath =Constants.ToolbarIconPath + "conversionIconInToolbar.png";
            SetupLaunchIcon();
            SetupCommandButtons();
            SetupTools();
        }
        protected override void PreDisplaySetup()
        {
            base.PreDisplaySetup();
        }

        private void SetupTools()
        {
            Conversion_Ascii ascii = new Conversion_Ascii();
            Conversion_Unicode uni = new Conversion_Unicode();
            Conversion_Bases bases = new Conversion_Bases();
            Conversion_Units units = new Conversion_Units();
            Conversion_Calculator calc = new Conversion_Calculator();
            Tool a = new Tool(ascii);
            Tool u = new Tool(uni);
            Tool b = new Tool(bases);
            Tool un = new Tool(units);
            Tool c = new Tool(calc);
            tools.Add(a);
            tools.Add(u);
            tools.Add(b);
            tools.Add(un);
            tools.Add(c);
        }
    }
}
