using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIM.Models;
using TIM.UserControls;
using TIM.Tools;

namespace TIM.Toolbars
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
            displayIconPath = Constants.ToolbarIconPath + "colorsIconInToolbar.png";
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
            Colors_ColorDisplay color = new Colors_ColorDisplay();
            Colors_Sliders sliders = new Colors_Sliders(color.ColorDisplayBrush);
            Colors_ValuesDisplay values = new Colors_ValuesDisplay(color.ColorDisplayBrush, sliders.rgbaSliders, sliders.hsbaSliders);
            Colors_Eyedropper eye = new Colors_Eyedropper(color.ColorDisplayBrush);
            Colors_Swatches swatch = new Colors_Swatches(color.ColorDisplayBrush);
            Tool ct = new Tool(color);
            Tool v = new Tool(values);
            Tool sl = new Tool(sliders);
            Tool e = new Tool(eye);
            Tool sw = new Tool(swatch);
            tools.Add(ct);
            tools.Add(v);
            tools.Add(e);
            tools.Add(sl);
            tools.Add(sw);
        }
    }
}
