﻿using System;
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
        private Drawing_Overlay overlay;
        public DrawToolbar() : base()
        {
            iconA   = "drawingIconActive.png";
            iconAH  = "drawingIconActiveHover.png";
            iconAP  = "drawingIconActivePressed.png";
            iconI   = "drawingIconInactive.png";
            iconIH  = "drawingIconInactiveHover.png";
            iconIP  = "drawingIconInactivePressed.png";
            iconD   = "drawingIconDisabled.png";
            displayIconPath = "../Images/ToolbarIcons/drawingIconInToolbar.png";
            SetupLaunchIcon();
            SetupCommandButtons();
            SetupTools();
        }
        //adds tools to toolbar
        protected override void PreDisplaySetup()
        {
            base.PreDisplaySetup();
            overlay.parentToolbar = this.display;
            overlay.Show();
        }
        protected override void ToolbarClosed(object sender, System.Windows.RoutedEventArgs e)
        {
            base.ToolbarClosed(sender, e);
            overlay.Hide();
        }
        //create and add tools
        private void SetupTools()
        {
            Drawing_FillSelect fill = new Drawing_FillSelect();
            Drawing_StrokeSelect stroke = new Drawing_StrokeSelect();
            Drawing_StrokeWeight weight = new Drawing_StrokeWeight();
            Drawing_Shapes shapes = new Drawing_Shapes();
            Drawing_Lines lines = new Drawing_Lines();
            Drawing_Text text = new Drawing_Text();
            Drawing_Pen pen = new Drawing_Pen();
            Drawing_Eraser eraser = new Drawing_Eraser();
            Drawing_ReturnCursor ret = new Drawing_ReturnCursor();
            Drawing_Clear clear = new Drawing_Clear();
            Drawing_Save save = new Drawing_Save();

            overlay = new Drawing_Overlay(this.display, fill, stroke, weight, shapes, lines, text, pen, eraser, clear, save, ret);
            overlay.Hide();
            tools.Add(new Tool(fill));
            tools.Add(new Tool(stroke));
            tools.Add(new Tool(weight));
            tools.Add(new Tool(shapes));
            tools.Add(new Tool(lines));
            tools.Add(new Tool(text));
            tools.Add(new Tool(pen));
            tools.Add(new Tool(eraser));
            tools.Add(new Tool(ret));
            tools.Add(new Tool(clear));
            tools.Add(new Tool(save));
        }
    }
}
