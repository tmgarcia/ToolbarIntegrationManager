using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TestProject.Models
{
    class ScreenClipInk_DynamicRenderer : DynamicRenderer
    {
        private Point prevPoint;
        private Point firstPoint;
        private bool strokeStart = true;
        private bool needRedraw = true;

        public BitmapSource fullScreenCap;
        public BitmapSource currentClipping;

        private ScreenClipInk_InkCanvas canvas;

        public ScreenClipInk_DynamicRenderer(BitmapSource fullScreenCap)
        {
            this.fullScreenCap = fullScreenCap;
        }

        protected override void OnAdded()
        {
            base.OnAdded();
            canvas = ((ScreenClipInk_InkCanvas)this.Element);
        }

        protected override void OnStylusUp(RawStylusInput rawStylusInput)
        {
            canvas.clippedImage = currentClipping;
            strokeStart = true;
            needRedraw = true;
            base.OnStylusUp(rawStylusInput);
        }

        protected override void OnDraw(DrawingContext drawingContext, StylusPointCollection stylusPoints, Geometry geometry, Brush fillBrush)
        {
            if (strokeStart)
            {
                strokeStart = false;
                firstPoint = (Point)stylusPoints[0];
                prevPoint = firstPoint;
            }
            else
            {
                DrawClippingPreview(drawingContext, stylusPoints);
            }
        }

        private void DrawClippingPreview(DrawingContext drawingContext, StylusPointCollection stylusPoints)
        {
            Point current = (Point)stylusPoints[stylusPoints.Count - 1];
            Vector v = Point.Subtract(firstPoint, current);
            Vector pre = Point.Subtract(current, prevPoint);
            if (Math.Abs(current.X-firstPoint.X)>2 && Math.Abs(current.Y-firstPoint.Y)>2)
            {
                if (needRedraw)
                {
                    if ((firstPoint.X <= fullScreenCap.PixelWidth && firstPoint.Y <= fullScreenCap.PixelHeight)
                        && (current.X <= fullScreenCap.PixelWidth && current.Y <= fullScreenCap.PixelHeight))
                    {
                        Rect containingRectanlge = new Rect(firstPoint, current);
                        var croppedBitmap = new CroppedBitmap(fullScreenCap, new Int32Rect((int)containingRectanlge.TopLeft.X, (int)containingRectanlge.TopLeft.Y, (int)containingRectanlge.Width, (int)containingRectanlge.Height));

                        BitmapSource croppedSource = croppedBitmap.Clone();
                        //BitmapSource croppedSource = 
                        drawingContext.DrawRectangle(null, new Pen(new SolidColorBrush(Colors.Black), 1), containingRectanlge);
                        drawingContext.DrawImage(croppedSource, containingRectanlge);
                        currentClipping = croppedSource;

                        needRedraw = false;
                        prevPoint = current;
                    }
                    else
                    {
                        currentClipping = null;
                    }
                }
                else if (pre.Length > 4)
                {
                    needRedraw = true;
                    this.Reset(Stylus.CurrentStylusDevice, stylusPoints);
                }
            }
            
        }
    }
}
