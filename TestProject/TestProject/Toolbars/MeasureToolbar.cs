using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Models;
using TestProject.Tools;
using TestProject.UserControls;

namespace TestProject.Toolbars
{
    class MeasureToolbar : Toolbar
    {
        private MeasuringOverlay overlayWindow;
        public MeasureToolbar() : base()
        {
            iconA   = "measureIconActive.png";
            iconAH  = "measureIconActiveHover.png";
            iconAP  = "measureIconActivePressed.png";
            iconI   = "measureIconInactive.png";
            iconIH  = "measureIconInactiveHover.png";
            iconIP  = "measureIconInactivePressed.png";
            iconD   = "measureIconDisabled.png";
            displayIconPath = "../Images/ToolbarIcons/measureIconInToolbar.png";
            overlayWindow = new MeasuringOverlay();
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
            Measure_GridDisplay g = new Measure_GridDisplay(overlayWindow);
            Measure_VerticalRuler v = new Measure_VerticalRuler(overlayWindow);
            Measure_HorizontalRuler h = new Measure_HorizontalRuler(overlayWindow);
            Measure_MouseCoords c = new Measure_MouseCoords();
            Measure_BoxSelector b = new Measure_BoxSelector();
            Tool grid = new Tool(g);
            Tool vert = new Tool(v);
            Tool hor = new Tool(h);
            Tool mc = new Tool(c);
            Tool box = new Tool(b);
            tools.Add(grid);
            tools.Add(vert);
            tools.Add(hor);
            tools.Add(mc);
            tools.Add(box);
        }
    }
}
