using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Colors_ColorDisplay.xaml
    /// </summary>
    public partial class Colors_ColorDisplay : UserControl
    {
        public Colors_ColorDisplay()
        {
            InitializeComponent();
            Height = Constants.ToolButtonHeight;
            Width = Constants.ToolButtonWidth;
            grid.Height = Height;
            grid.Width = Width;
            ColorDisplay.Height = Height - 10;
            ColorDisplay.Width = Width - 10;
            AlphaBackground.Height = Height - 10;
            AlphaBackground.Width = Width - 10;
        }

        private void ColorDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //DrdagDrop.DoDragDrop(dragSource-source of data, data, allowedEffects DragDropEffects.Copy);
                DragDrop.DoDragDrop(rectangle, rectangle.Fill.ToString(), DragDropEffects.Copy);
            }
        }
    }
}
