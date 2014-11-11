using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Globalization;
using TestProject.Enums;
using System.Windows.Ink;

namespace TestProject.Tools
{
    class Drawing_InkCanvas : InkCanvas
    {
        Drawing_DynamicRenderer customRenderer = new Drawing_DynamicRenderer();
        public Color strokeColor;
        public Color fillColor;
        public DrawingStrokeType StrokeMode;
        public TextBox box;
        private bool textBeingInput = false;
        private Point textInputPos;
        public Drawing_InkCanvas()  : base()
        {
            // Use the custom dynamic renderer on the
            // custom InkCanvas.
            strokeColor = Colors.Black;
            fillColor = Color.FromRgb(250,250,250);
            this.DynamicRenderer = customRenderer;
            StrokeMode = DrawingStrokeType.Pen;
            this.DefaultDrawingAttributes.Color = strokeColor;

            box = new TextBox();
            box.FontFamily = new FontFamily("Arial");
            box.FontSize = 32;
            box.Background = new SolidColorBrush(Color.FromArgb(0,255,255,255));
            box.BorderThickness = new Thickness(0);
            box.LostKeyboardFocus += box_LostKeyboardFocus;
            box.AcceptsReturn = false;
            box.KeyDown += box_KeyDown;

            
        }

        protected override void OnPreviewMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DefaultDrawingAttributes.Color = strokeColor;
            if (StrokeMode == DrawingStrokeType.Text && !textBeingInput && EditingMode == InkCanvasEditingMode.Ink)
            {
                textBeingInput = true;
                textInputPos = e.GetPosition(this);
                InkCanvas.SetLeft(box, textInputPos.X);
                InkCanvas.SetTop(box, textInputPos.Y);
                box.Width = 200;
                box.Foreground = new SolidColorBrush(fillColor);
                Children.Add(box);
                Keyboard.Focus(box);
                e.Handled = true;
            }
            else
            {
                base.OnPreviewMouseLeftButtonDown(e);
            }
        }

        void box_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBeingInput)
            {
                if (e.Key == Key.Return)
                {
                    textBeingInput = false;
                    processText();
                }
            }
        }

        void box_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (textBeingInput)
            {
                textBeingInput = false;
                processText();
            }
        }

        private void processText()
        {
            string text = box.Text;
            Children.Remove(box);
            box.Clear();
            if (!String.IsNullOrWhiteSpace(text))
            {
                StylusPointCollection sp = new StylusPointCollection();
                sp.Add(new StylusPoint(textInputPos.X, textInputPos.Y));
                sp.Add(new StylusPoint(textInputPos.X + text.Length*32, textInputPos.Y));
                Drawing_Stroke customStroke;
                customStroke = new Drawing_Stroke(sp, strokeColor, fillColor, StrokeMode, text);
                customStroke.DrawingAttributes = DefaultDrawingAttributes.Clone();
                this.Strokes.Add(customStroke);

                InkCanvasStrokeCollectedEventArgs args =
                    new InkCanvasStrokeCollectedEventArgs(customStroke);
                base.OnStrokeCollected(args);
            }
        }

        protected override void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs e)
        {
            if (StrokeMode != DrawingStrokeType.Pen)
            {
                // Remove the original stroke and add a custom stroke.
                this.Strokes.Remove(e.Stroke);

                Drawing_Stroke customStroke;
                customStroke = new Drawing_Stroke(e.Stroke.StylusPoints, strokeColor, fillColor, StrokeMode);
                customStroke.DrawingAttributes = DefaultDrawingAttributes.Clone();
                this.Strokes.Add(customStroke);


                // Pass the custom stroke to base class' OnStrokeCollected method.
                InkCanvasStrokeCollectedEventArgs args =
                    new InkCanvasStrokeCollectedEventArgs(customStroke);
                base.OnStrokeCollected(args);
            }
            else
            {
                base.OnStrokeCollected(e);
            }
        }
    }
}
