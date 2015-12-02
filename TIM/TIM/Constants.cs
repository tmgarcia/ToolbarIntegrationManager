using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TIM
{
    public static class Constants
    {
        public static int ToolbarIconWidth = 150;
        public static int ToolbarIconHeight = 150;
        public static int ToolIconWidth = 40;
        public static int ToolIconHeight = 40;

        public static int ToolbarHorizontalExpandedHeight = 50;
        public static int ToolbarHorizontalExpandedWidth = 200;

        public static int ToolbarVerticalExpandedHeight = 200;
        public static int ToolbarVerticalExpandedWidth = 50;

        public static int ToolbarCollapsedHeight = 50;
        public static int ToolbarCollapsedWidth = 100;

        public static int ToolButtonHeight = 40;
        public static int ToolButtonWidth = 40;

        public static Point VerticalGradientStart = new Point(0, 0);
        public static Point VerticalGradientEnd = new Point(0, 1);
        public static Point HorizontalGradientStart = new Point(0, 0);
        public static Point HorizontalGradientEnd = new Point(1, 0);

        public static string ToolbarIconPath = "Images/ToolbarIcons/";
        public static string CommandButtonPath = "Images/CommandButtons/";
        public static string SolutionRoot = System.AppDomain.CurrentDomain.BaseDirectory;
        public static string QuickNoteSavePath = System.AppDomain.CurrentDomain.BaseDirectory + "QuickNotes/";
    }
}
