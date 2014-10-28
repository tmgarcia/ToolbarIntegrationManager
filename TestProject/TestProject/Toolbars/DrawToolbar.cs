using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Models;
using TestProject.Tools;

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
            SetupTools();
        }
        //adds tools to toolbar
        protected override void PreDisplaySetup()
        {
            base.PreDisplaySetup();
        }
        //create and add tools
        private void SetupTools()
        {
            Drawing_FillSelect fill = new Drawing_FillSelect();
            Drawing_StrokeSelect stroke = new Drawing_StrokeSelect();
            Drawing_StrokeWeight weight = new Drawing_StrokeWeight();
            Tool f = new Tool(fill);
            Tool s = new Tool(stroke);
            Tool w = new Tool(weight);
            tools.Add(f);
            tools.Add(s);
            tools.Add(w);
        }
    }
}
