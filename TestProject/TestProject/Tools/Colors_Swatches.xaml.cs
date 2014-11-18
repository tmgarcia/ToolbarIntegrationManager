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
using TestProject.Models;

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Colors_Swatches.xaml
    /// </summary>
    public partial class Colors_Swatches : UserControl, IDeactivatableTool
    {
        SolidColorBrush CurrentColorBrush;
        public Colors_Swatches(SolidColorBrush currentBrush)
        {
            CurrentColorBrush = currentBrush;
            InitializeComponent();
            Height = Constants.ToolButtonHeight;
            Width = 3 * Constants.ToolButtonWidth;
            Scroll.Height = Constants.ToolButtonHeight;
            Scroll.Width = 3 * Constants.ToolButtonWidth;
            //ItemsControl.Height = Constants.ToolButtonHeight;
            //ItemsControl.Width = 2.5 * Constants.ToolButtonWidth;

            //for (int i = 0; i < 50; i++)
            //{
            //    ItemsControl.Items.Add(new Rectangle{Fill = new SolidColorBrush{Color = Color.FromArgb(255,255,0,0)}, Width=15, Height=15, Margin=new Thickness(3)});
            //}
        }

        private Rectangle preview;
        private void Grid_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string dataString = (string)e.Data.GetData(DataFormats.StringFormat);
                BrushConverter converter = new BrushConverter();
                if (converter.IsValid(dataString))
                {
                    preview = new Rectangle();
                    preview.Width = 15;
                    preview.Height = 15;
                    preview.Stroke = (Brush)converter.ConvertFromString(dataString);
                    preview.Fill = new SolidColorBrush { Color = Color.FromArgb(0, 0, 0, 0) };
                    ItemsControl.Items.Add(preview);
                }
            }
        }

        private void Grid_DragLeave(object sender, DragEventArgs e)
        {
            ItemsControl.Items.Remove(preview);
        }

        private void Grid_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;

            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string dataString = (string)e.Data.GetData(DataFormats.StringFormat);

                BrushConverter converter = new BrushConverter();
                if (converter.IsValid(dataString))
                {
                    e.Effects = DragDropEffects.Copy;
                }
            }
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                e.Effects = DragDropEffects.Copy;
                string dataString = (string)e.Data.GetData(DataFormats.StringFormat);
                BrushConverter converter = new BrushConverter();
                if (converter.IsValid(dataString))
                {
                    Rectangle newRectangle = new Rectangle();
                    newRectangle.Height = 20;
                    newRectangle.Width = 20;
                    newRectangle.Margin = new Thickness(2);
                    newRectangle.Fill = (Brush)converter.ConvertFromString(dataString);
                    newRectangle.Stroke = new SolidColorBrush { Color = Color.FromArgb(255, 0, 0, 0) };
                    newRectangle.StrokeThickness = 1;
                    newRectangle.MouseDown += newRectangle_MouseDown;
                    ItemsControl.Items.Remove(preview);
                    ItemsControl.Items.Add(newRectangle);
                }
            }
        }

        void newRectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                Rectangle r = sender as Rectangle;
                Color c = (r.Fill as SolidColorBrush).Color;
                CurrentColorBrush.Color = c;
            }
        }



        public void Activate()
        {

        }

        public void Deactivate()
        {

        }
    }
}
