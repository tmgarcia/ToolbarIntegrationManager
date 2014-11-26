using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestProject.Models;
using TestProject.UserControls;

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Measure_GridDisplay.xaml
    /// </summary>
    public partial class Measure_GridDisplay : UserControl, IDeactivatableTool
    {
        public bool gridVisible;
        private Button showButton;
        private Button resetButton;
        private MeasuringOverlay overlay;
        private TextBox divideEveryBox;
        private TextBox subdivideBox;

        public Measure_GridDisplay(MeasuringOverlay overlay)
        {
            gridVisible = false;
            InitializeComponent();
            expandable.setSymbol((DrawingImage)Application.Current.FindResource("SymbolGridSquare"));
            expandable.Toggle.toggleControl.ToolTip = "Grid Screen Overlay";
            StackPanel content = expandable.PopupContent as StackPanel;
            Grid textboxGrid = content.Children[0] as Grid;
            divideEveryBox = textboxGrid.Children[1] as TextBox;
            subdivideBox = textboxGrid.Children[4] as TextBox;
            Grid buttonGrid = content.Children[1] as Grid;
            showButton = buttonGrid.Children[0] as Button;
            resetButton = buttonGrid.Children[1] as Button;
            showButton.Content = "Show";
            resetButton.IsEnabled = false;
            this.overlay = overlay;
        }


        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (gridVisible)
            {
                //disable grid
                gridVisible = false;
                showButton.Content = "Show";
                resetButton.IsEnabled = false;
                HideGrid();
            }
            else
            {
                //enable grid
                gridVisible = true;
                showButton.Content = "Hide";
                resetButton.IsEnabled = true;
                DisplayGrid();
            }
        }

        private void DisplayGrid()
        {
            if (!overlay.isActive)
            {
                overlay.ActivateOverlay();
            }
            overlay.AddGrid(MakeGridLines());

        }

        private void previewTextInput_NumsOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumOnly(e.Text);//handled (stops the text) if not all nums
        }
        private bool IsNumOnly(string text)
        {
            Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void HideGrid()
        {
            overlay.RemoveGrid();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            overlay.ResetGrid(MakeGridLines());
        }

        private DrawingImage MakeGridLines()
        {
            int interval = int.Parse(divideEveryBox.Text);
            int subDivides = int.Parse(subdivideBox.Text);
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            DrawingGroup lineGroup = new DrawingGroup();
            GuidelineSet guidelines = new GuidelineSet();

            Pen dividePen = new Pen(new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)), 1);
            Pen subDividePen = new Pen(new SolidColorBrush(Color.FromArgb(128, 100, 100, 100)), 1);
            using (DrawingContext dc = lineGroup.Open())
            {
                for (int x = interval; x < screenWidth; x += interval)
                {
                    if (subDivides > 1)
                    {
                        for (int i = 1; i < subDivides; i++)
                        {
                            int subInterval = (int)(interval * ((double)i / subDivides));
                            int subX = (x - interval) + subInterval;
                            guidelines.GuidelinesX.Add(subX);
                            dc.DrawLine(subDividePen, new Point(subX, 0), new Point(subX, screenHeight));
                        }
                    }
                    guidelines.GuidelinesX.Add(x);
                    dc.DrawLine(dividePen, new Point(x, 0), new Point(x, screenHeight));
                }
                for (int y = interval; y < screenHeight; y += interval)
                {
                    if (subDivides > 1)
                    {
                        for (int i = 1; i < subDivides; i++)
                        {
                            int subInterval = (int)(interval * ((double)i / subDivides));
                            int subY = (y - interval) + subInterval;
                            guidelines.GuidelinesY.Add(subY);
                            dc.DrawLine(subDividePen, new Point(0, subY), new Point(screenWidth, subY));
                        }
                    }
                    guidelines.GuidelinesY.Add(y);
                    dc.DrawLine(dividePen, new Point(0, y), new Point(screenWidth, y));
                }
            }
            lineGroup.GuidelineSet = guidelines;
            DrawingImage dImage = new DrawingImage(lineGroup);
            dImage.Freeze();

            return dImage;
        }

        private void Divisions_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (divideEveryBox != null)
            {
                if (String.IsNullOrWhiteSpace(divideEveryBox.Text))
                {
                    divideEveryBox.Text = "100";
                }
            }
        }
        private void Subdivisions_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (subdivideBox != null)
            {
                if (String.IsNullOrWhiteSpace(subdivideBox.Text))
                {
                    subdivideBox.Text = "2";
                }
            }
        }


        public void Activate()
        {

        }

        public void Deactivate()
        {
            expandable.Collapse();
            gridVisible = false;
            showButton.Content = "Show";
            resetButton.IsEnabled = false;
            HideGrid();
        }


        public void ReorientHorizontal()
        {
            expandable.SetPlacementModeBottom();
            expandable.setAlignmentPointTopLeft();
        }

        public void ReorientVertical()
        {
            expandable.SetPlacementModeLeft();
            expandable.setAlignmentPointTopRight();
        }

        public void Collapse()
        {
            expandable.Collapse();
        }
    }
}
