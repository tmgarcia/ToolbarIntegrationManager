using System;
using System.Collections.Generic;
using System.IO;
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
using TestProject.Enums;
using TestProject.UserControls;
using TestProject.Models;

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Drawing_Overlay.xaml
    /// </summary>
    public partial class Drawing_Overlay : Window
    {
        Drawing_FillSelect fillSelect;
        Drawing_StrokeSelect strokeSelect;
        Drawing_StrokeWeight weightSlider;
        Drawing_Shapes shapeSelect;
        Drawing_Lines lineSelect;
        Drawing_Text textTool;
        Drawing_Pen penTool;
        Drawing_Eraser eraserTool;
        Drawing_Clear clearButton;
        Drawing_Save saveButton;
        Drawing_ReturnCursor returnCursorButton;

        Color fillColor;
        Color strokeColor;
        double strokeWeight;
        DrawingStrokeType currentStrokeType = DrawingStrokeType.Pen;
        bool activelyDrawing = true;

        public ToolbarWindow parentToolbar;

        private int originalExtendedStyle;
        public Drawing_Overlay(ToolbarWindow parent,
            Drawing_FillSelect fillSelect,Drawing_StrokeSelect strokeSelect,
            Drawing_StrokeWeight weightSlider,Drawing_Shapes shapeSelect,Drawing_Lines lineSelect,
            Drawing_Text textTool,Drawing_Pen penTool,Drawing_Eraser eraserTool,
            Drawing_Clear clearButton,Drawing_Save saveButton, Drawing_ReturnCursor returnCursorButton)
        {
            InitializeComponent();
            this.WindowState = System.Windows.WindowState.Maximized;
            inkCanvas.DefaultDrawingAttributes.FitToCurve = true;
            parentToolbar = parent;
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Width = screenWidth;
            this.Height = screenHeight;
            this.Deactivated += DrawingOverlay_Deactivated;
            this.Activated += DrawingOverlay_Activated;
            this.ShowInTaskbar = false;

            this.fillSelect = fillSelect;
            fillColor = fillSelect.currentColor;
            fillSelect.expandable.ToolExpanded += FillSelectExpanded;
            fillSelect.FillColorSelected += fillSelect_FillColorSelected;

            this.strokeSelect = strokeSelect;
            strokeColor = strokeSelect.currentColor;
            strokeSelect.expandable.ToolExpanded += StrokeSelectExpanded;
            strokeSelect.StrokeColorSelected += strokeSelect_StrokeColorSelected;

            this.weightSlider = weightSlider;
            strokeWeight = weightSlider.currentWeight;
            weightSlider.expandable.ToolExpanded += WeightSliderExpanded;
            weightSlider.WeightChanged += weightSlider_WeightChanged;


            this.shapeSelect = shapeSelect;
            shapeSelect.expandable.ToolExpanded += ShapeSelectExpanded;
            shapeSelect.ShapeSelected += shapeSelect_ShapeSelected;

            this.lineSelect = lineSelect;
            lineSelect.expandable.ToolExpanded += LineSelectExpanded;
            lineSelect.LineSelected += lineSelect_LineSelected;

            this.textTool = textTool;
            textTool.toggle.toggleControl.Checked += TextToolActivated;
            textTool.toggle.toggleControl.Unchecked += TextToolDeactivated;

            this.penTool = penTool;
            penTool.toggle.toggleControl.Checked += PenToolActivated;
            penTool.toggle.toggleControl.Unchecked += PenToolDeactivated;

            this.eraserTool = eraserTool;
            eraserTool.toggle.toggleControl.Checked += EraserToolActivated;
            eraserTool.toggle.toggleControl.Unchecked += EraserToolDeactivated;

            this.clearButton = clearButton;
            clearButton.toggle.toggleControl.Checked += ClearButtonClicked;
            this.saveButton = saveButton;
            saveButton.toggle.toggleControl.Checked += SaveButtonClicked;

            this.returnCursorButton = returnCursorButton;
            returnCursorButton.toggle.toggleControl.Checked += ReturnCursorActivated;
        }

        void ReturnCursorActivated(object sender, RoutedEventArgs e)
        {
            textTool.toggle.toggleControl.IsChecked = false;
            penTool.toggle.toggleControl.IsChecked = false;
            eraserTool.toggle.toggleControl.IsChecked = false;
            StopDrawing();
        }

        void ClearButtonClicked(object sender, RoutedEventArgs e)
        {
            clearButton.toggle.toggleControl.IsChecked = false;
            inkCanvas.Strokes.Clear();
        }

        void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            saveButton.toggle.toggleControl.IsChecked = false;
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.DefaultExt = ".bmp";
            sfd.Filter = "Bitmap Image (.bmp)|*.bmp";
            bool? result = sfd.ShowDialog();

            if (result == true)
            {
                string fileName = sfd.FileName;
                inkCanvas.Background = new SolidColorBrush(Colors.White);
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)inkCanvas.ActualWidth, (int)inkCanvas.ActualHeight, 96d, 96d, PixelFormats.Default);
                rtb.Render(inkCanvas);
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                encoder.Save(fs);
                fs.Close();
                inkCanvas.Background = new SolidColorBrush(Color.FromArgb(1,255,255,255));
            }
        }

        void EraserToolActivated(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = CustomCursors.Cursor_Eraser();
            textTool.toggle.toggleControl.IsChecked = false;
            penTool.toggle.toggleControl.IsChecked = false;
            returnCursorButton.toggle.toggleControl.IsChecked = false;
            currentStrokeType = DrawingStrokeType.Pen;
            CollapseAll();

            inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
            StartDrawing();
        }
        void EraserToolDeactivated(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            StopDrawing();
        }

        void PenToolActivated(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = CustomCursors.Cursor_EmptyCircle((int)strokeWeight+4);
            textTool.toggle.toggleControl.IsChecked = false;
            eraserTool.toggle.toggleControl.IsChecked = false;
            returnCursorButton.toggle.toggleControl.IsChecked = false;
            currentStrokeType = DrawingStrokeType.Pen;
            CollapseAll();
            StartDrawing();
        }
        void PenToolDeactivated(object sender, RoutedEventArgs e)
        {
            StopDrawing();
        }

        void TextToolActivated(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.IBeam;
            penTool.toggle.toggleControl.IsChecked = false;
            eraserTool.toggle.toggleControl.IsChecked = false;
            returnCursorButton.toggle.toggleControl.IsChecked = false;
            currentStrokeType = DrawingStrokeType.Text;
            CollapseAll();
            StartDrawing();
        }
        void TextToolDeactivated(object sender, RoutedEventArgs e)
        {
            StopDrawing();
        }

        void lineSelect_LineSelected(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Cross;
            currentStrokeType = lineSelect.selectedStrokeType;
            CollapseAll();
            UnToggleAll();
            StartDrawing();
        }
        void shapeSelect_ShapeSelected(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Cross;
            currentStrokeType = shapeSelect.selectedStrokeType;
            CollapseAll();
            UnToggleAll();
            StartDrawing();
        }

        void weightSlider_WeightChanged(object sender, RoutedEventArgs e)
        {
            strokeWeight = weightSlider.currentWeight;
            inkCanvas.DefaultDrawingAttributes.Width = strokeWeight;
            inkCanvas.DefaultDrawingAttributes.Height = strokeWeight;

            if ((bool)penTool.toggle.toggleControl.IsChecked)
            {
                Mouse.OverrideCursor = CustomCursors.Cursor_EmptyCircle((int)strokeWeight + 4);
            }
        }

        void strokeSelect_StrokeColorSelected(object sender, RoutedEventArgs e)
        {
            strokeColor = strokeSelect.currentColor;
            inkCanvas.strokeColor = strokeColor;
        }
        void fillSelect_FillColorSelected(object sender, RoutedEventArgs e)
        {
            fillColor = fillSelect.currentColor;
            inkCanvas.fillColor = fillColor;
        }

        void LineSelectExpanded(object sender, RoutedEventArgs e)
        {
            shapeSelect.expandable.Collapse();
        }
        void ShapeSelectExpanded(object sender, RoutedEventArgs e)
        {
            lineSelect.expandable.Collapse();

        }
        void WeightSliderExpanded(object sender, RoutedEventArgs e)
        {
            strokeSelect.expandable.Collapse();
            fillSelect.expandable.Collapse();
        }
        private void FillSelectExpanded(object sender, RoutedEventArgs e)
        {
            strokeSelect.expandable.Collapse();
            weightSlider.expandable.Collapse();
        }
        private void StrokeSelectExpanded(object sender, RoutedEventArgs e)
        {
            fillSelect.expandable.Collapse();
            weightSlider.expandable.Collapse();
        }
        private void CollapseAll()
        {
            strokeSelect.expandable.Collapse();
            fillSelect.expandable.Collapse();
            weightSlider.expandable.Collapse();
            shapeSelect.expandable.Collapse();
            lineSelect.expandable.Collapse();
        }
        private void UnToggleAll()
        {
            textTool.toggle.toggleControl.IsChecked = false;
            penTool.toggle.toggleControl.IsChecked = false;
            eraserTool.toggle.toggleControl.IsChecked = false;
        }

        private void StartDrawing()
        {
            activelyDrawing = true;
            inkCanvas.fillColor = fillColor;
            inkCanvas.strokeColor = strokeColor;
            inkCanvas.DefaultDrawingAttributes.Width = strokeWeight;
            inkCanvas.DefaultDrawingAttributes.Height = strokeWeight;
            inkCanvas.StrokeMode = currentStrokeType;
            inkCanvas.Background = new SolidColorBrush(Color.FromArgb(1,255,255,255));
            UnsetTransparent();
            parentToolbar.Topmost = true;
            parentToolbar.MouseEnter += parentToolbar_MouseEnter;
            parentToolbar.MouseLeave += parentToolbar_MouseLeave;
        }

        void parentToolbar_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        void parentToolbar_MouseEnter(object sender, MouseEventArgs e)
        {
            parentToolbar.Activate();
        }
        private void StopDrawing()
        {
            Mouse.OverrideCursor = Cursors.Arrow;
            inkCanvas.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            SetTransparent();
            activelyDrawing = false;
        }


        private void DrawingOverlay_Activated(object sender, EventArgs e)
        {
            this.Topmost = true;
            if (parentToolbar != null)
            {
                parentToolbar.Topmost = true;
                this.Topmost = false;
            }
        }
        private void DrawingOverlay_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;
            if (parentToolbar != null)
            {
                parentToolbar.Topmost = true;
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            originalExtendedStyle = WindowsServices.GetExtendedWindowStyle(hwnd);
            inkCanvas.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            SetTransparent();
        }
        private void SetTransparent()
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(hwnd);
        }
        private void UnsetTransparent()
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetExtendedWindowStyle(hwnd, originalExtendedStyle);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
