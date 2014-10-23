using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ScrapProject.Enums;
using System.IO;

namespace ScrapProject
{
    /// <summary>
    /// Interaction logic for Drawing.xaml
    /// </summary>
    public partial class Drawing : Window
    {
        public Drawing()
        {
            InitializeComponent();
            List<Color> cs = new List<Color>();
            cs.Add(Colors.Red);
            cs.Add(Colors.Orange);
            cs.Add(Colors.Yellow);
            cs.Add(Colors.Green);
            cs.Add(Colors.Blue);
            cs.Add(Colors.Purple);
            StrokeSelect.ItemsSource = cs;
            FillSelect.ItemsSource = cs;
            
            List<DrawingStrokeType> l = new List<DrawingStrokeType>();
            l.Add(DrawingStrokeType.Shape_Ellipse);
            l.Add(DrawingStrokeType.Shape_Rectangle);
            l.Add(DrawingStrokeType.Shape_Triangle);
            l.Add(DrawingStrokeType.Line_Line);
            l.Add(DrawingStrokeType.Line_Arrow);
            l.Add(DrawingStrokeType.Line_CoordQuad);
            l.Add(DrawingStrokeType.Line_Coord2D);
            l.Add(DrawingStrokeType.Line_Coord3D);
            l.Add(DrawingStrokeType.Pen);
            l.Add(DrawingStrokeType.Text);
            StrokeType.ItemsSource = l;
        }

        private void inkEditMode_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas1.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void selectEditMode_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas1.EditingMode = InkCanvasEditingMode.Select;
        }

        private void eraseStrokeEditMode_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas1.EditingMode = InkCanvasEditingMode.EraseByStroke;
        }

        private void erasePointEditMode_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas1.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void WidthSelect_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (inkCanvas1 != null)
            {
                inkCanvas1.DefaultDrawingAttributes.Width = WidthSelect.Value;
                inkCanvas1.DefaultDrawingAttributes.Height = WidthSelect.Value;
            }
        }

        private void FitToCurveCheck_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas1.DefaultDrawingAttributes.FitToCurve = (bool)FitToCurveCheck.IsChecked;
        }

        private void IsHighlightCheck_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas1.DefaultDrawingAttributes.IsHighlighter = (bool)IsHighlightCheck.IsChecked;
        }

        private void IgnorePressureCheck_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas1.DefaultDrawingAttributes.IgnorePressure = (bool)IgnorePressureCheck.IsChecked;
        }


        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas1.Strokes.Clear();
            
        }

        private void StrokeSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (inkCanvas1 != null)
            {
                inkCanvas1.strokeColor = (Color)StrokeSelect.SelectedItem;
                inkCanvas1.DefaultDrawingAttributes.Color = (Color)StrokeSelect.SelectedItem;
            }
        }

        private void FillSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (inkCanvas1 != null)
            {
                inkCanvas1.fillColor = (Color)FillSelect.SelectedItem;
            }
        }

        private void StrokeType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (inkCanvas1 != null)
            {
                inkCanvas1.StrokeMode = (DrawingStrokeType)StrokeType.SelectedValue;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.DefaultExt = ".bmp";
            sfd.Filter = "Bitmap Image (.bmp)|*.bmp";
            bool? result = sfd.ShowDialog();

            if (result == true)
            {
                string fileName = sfd.FileName;

                RenderTargetBitmap rtb = new RenderTargetBitmap((int)inkCanvas1.ActualWidth, (int)inkCanvas1.ActualHeight, 96d, 96d, PixelFormats.Default);
                rtb.Render(inkCanvas1);
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                encoder.Save(fs);
                fs.Close();

                //IFormatter formatter = new BinaryFormatter();
                //Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                //formatter.Serialize(stream, gameData);
                //stream.Close();
            }


        }

    }
}
