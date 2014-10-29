using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TestProject.UserControls
{
    /// <summary>
    /// Interaction logic for MeasuringOverlay.xaml
    /// </summary>
    public partial class MeasuringOverlay : Window
    {
        public bool isActive;

        public bool gridIsActive;
        private Image gridImage;
        private bool horizontalRulerActive;
        private bool verticalRulerActive;
        private Image horizontalRuler;
        private Image verticalRuler;

        public MeasuringOverlay()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Width = screenWidth;
            this.Height = screenHeight;
            this.Deactivated += MeasuringOverlay_Deactivated;
            this.Activated += MeasuringOverlay_Activated;
            this.ShowInTaskbar = false;

            isActive = false;
            this.Visibility = System.Windows.Visibility.Collapsed;
            this.WindowState = System.Windows.WindowState.Minimized;

            ContentGrid.Focusable = false;
            this.Focusable = false;
            horizontalRulerActive = false;
            verticalRulerActive = false;
            CreateHorizontalRuler();
            CreateVerticalRuler();
            ContentGrid.MouseMove += MeasuringOverlay_MouseMove;
        }

        void MeasuringOverlay_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(this);
            double x = p.X;
            double y = p.Y;
        }

        public void ShowHorizontalRuler()
        {
            horizontalRulerActive = true;
            horizontalRuler.Visibility = System.Windows.Visibility.Visible;
        }
        public void ShowVerticalRuler()
        {
            verticalRulerActive = true;
            verticalRuler.Visibility = System.Windows.Visibility.Visible;
        }
        public void HideHorizontalRuler()
        {
            horizontalRulerActive = false;
            horizontalRuler.Visibility = System.Windows.Visibility.Collapsed;
        }
        public void HideVerticalRuler()
        {
            verticalRulerActive = false;
            verticalRuler.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void CreateHorizontalRuler()
        {
            int majorTick = 100;
            int subTicks = 4;
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            int rulerHeight = 20;
            int majorTickHeight = 20;
            int minorTickHeight = 5;

            double fontSize = 10;

            DrawingGroup drawingGroup = new DrawingGroup();
            GuidelineSet guidelines = new GuidelineSet();

            Pen tickPen = new Pen(new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)), 1);
            SolidColorBrush rulerBack = new SolidColorBrush(Color.FromArgb(200, 50, 50, 50));
            SolidColorBrush fontColor = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

            using (DrawingContext dc = drawingGroup.Open())
            {
                guidelines.GuidelinesX.Add(0);
                guidelines.GuidelinesX.Add(screenWidth);
                dc.DrawRectangle(rulerBack, null, new Rect(new Point(0, 0), new Point(screenWidth, rulerHeight)));
                for (int x = majorTick; x < screenWidth; x += majorTick)
                {
                    for (int s = 1; s < subTicks; s++)
                    {
                        int subInterval = (int)(majorTick * ((double)s / subTicks));
                        int subX = (x - majorTick) + subInterval;
                        guidelines.GuidelinesX.Add(subX);
                        dc.DrawLine(tickPen, new Point(subX, rulerHeight), new Point(subX, rulerHeight-minorTickHeight));
                    }
                    guidelines.GuidelinesX.Add(x);
                    dc.DrawLine(tickPen, new Point(x, rulerHeight), new Point(x, rulerHeight - majorTickHeight));
                    
                    FormattedText text = new FormattedText(""+x,
                    new CultureInfo("en-us"),
                    FlowDirection.LeftToRight,
                    new Typeface(this.FontFamily, FontStyles.Normal, FontWeights.Normal, new FontStretch()),
                    fontSize,
                    fontColor);
                    dc.DrawText(text, new Point(x + 1, rulerHeight/4));
                    
                }
            }
            drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(new Point(0, 0), new Point(screenWidth, rulerHeight)));
            drawingGroup.GuidelineSet = guidelines;
            DrawingImage rulerImage = new DrawingImage(drawingGroup);
            rulerImage.Freeze();
            horizontalRuler = new Image();
            horizontalRuler.Source = rulerImage;
            horizontalRuler.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            horizontalRuler.SnapsToDevicePixels = true;
            horizontalRuler.Width = screenWidth;
            ContentGrid.Children.Add(horizontalRuler);
            horizontalRuler.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void CreateVerticalRuler()
        {
            int majorTick = 100;
            int subTicks = 4;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            int rulerWidth = 20;
            int majorTickHeight = 20;
            int minorTickHeight = 5;

            double fontSize = 10;

            DrawingGroup drawingGroup = new DrawingGroup();
            GuidelineSet guidelines = new GuidelineSet();

            Pen tickPen = new Pen(new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)), 1);
            SolidColorBrush rulerBack = new SolidColorBrush(Color.FromArgb(200, 50, 50, 50));
            SolidColorBrush fontColor = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

            using (DrawingContext dc = drawingGroup.Open())
            {
                dc.DrawRectangle(rulerBack, null, new Rect(new Point(0, 0), new Point(rulerWidth, screenHeight)));
                for (int y = majorTick; y < screenHeight+majorTick; y += majorTick)
                {
                    for (int s = 1; s < subTicks; s++)
                    {
                        int subInterval = (int)(majorTick * ((double)s / subTicks));
                        int subY = (y - majorTick) + subInterval;
                        guidelines.GuidelinesY.Add(subY);
                        dc.DrawLine(tickPen, new Point(rulerWidth, subY), new Point(rulerWidth - minorTickHeight, subY));
                    }
                    if (y < screenHeight)
                    {
                        guidelines.GuidelinesY.Add(y);
                        dc.DrawLine(tickPen, new Point(rulerWidth, y), new Point(rulerWidth - majorTickHeight, y));

                        dc.PushTransform(new RotateTransform(-90));
                        FormattedText text = new FormattedText("" + y,
                        new CultureInfo("en-us"),
                        FlowDirection.LeftToRight,
                        new Typeface(this.FontFamily, FontStyles.Normal, FontWeights.Normal, new FontStretch()),
                        fontSize,
                        fontColor);
                        dc.DrawText(text, new Point(-(y - 2), 0));
                        dc.Pop();
                    }
                }
            }
            drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(new Point(0, 0), new Point(rulerWidth, screenHeight)));
            drawingGroup.GuidelineSet = guidelines;
            DrawingImage rulerImage = new DrawingImage(drawingGroup);
            rulerImage.Freeze();
            verticalRuler = new Image();
            verticalRuler.Source = rulerImage;
            verticalRuler.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            verticalRuler.SnapsToDevicePixels = true;
            verticalRuler.Height = screenHeight;
            ContentGrid.Children.Add(verticalRuler);
            verticalRuler.Visibility = System.Windows.Visibility.Collapsed;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(hwnd);
        }

        public void ActivateOverlay()
        {
            if (!isActive)
            {
                isActive = true;
                this.Visibility = System.Windows.Visibility.Visible;
                this.WindowState = System.Windows.WindowState.Maximized;
            }
        }
        public void DeactivateOverlay()
        {
            if (isActive)
            {
                isActive = false;
                this.Visibility = System.Windows.Visibility.Collapsed;
                this.WindowState = System.Windows.WindowState.Minimized;
            }
        }
        void MeasuringOverlay_Activated(object sender, EventArgs e)
        {
            this.Topmost = true;
        }
        void MeasuringOverlay_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;
            //this.Activate();
        }

        public void AddGrid(DrawingImage di)
        {
            gridIsActive = true;
            gridImage = new Image();
            gridImage.Source = di;
            gridImage.Width = this.Width;
            gridImage.Height = this.Height;
            gridImage.SnapsToDevicePixels = true;
            gridImage.Focusable = false;
            ContentGrid.Children.Add(gridImage);
        }
        public void RemoveGrid()
        {
            if (gridIsActive)
            {
                gridIsActive = false;
                ContentGrid.Children.Remove(gridImage);
            }
        }
        public void ResetGrid(DrawingImage di)
        {
            if (gridIsActive)
            {
                gridImage.Source = di;
            }
        }
    }
}
