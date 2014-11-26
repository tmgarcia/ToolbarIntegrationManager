using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Media;
using TestProject.Enums;


namespace TestProject.Tools
{
    class Drawing_DynamicRenderer : DynamicRenderer
    {
        [ThreadStatic]
        static private SolidColorBrush brush = null;

        [ThreadStatic]
        static private Pen pen = null;

        private Point prevPoint;
        private Point firstPoint;

        private bool strokeStart = true;
        private bool needRedraw = true;

        private Drawing_InkCanvas canvas;

        private DrawingStrokeType currentStrokeMode;
        
        protected override void OnStylusDown(RawStylusInput rawStylusInput)
        {
            currentStrokeMode = canvas.StrokeMode;
            ((SolidColorBrush)pen.Brush).Color = canvas.strokeColor;
            pen.Thickness = canvas.DefaultDrawingAttributes.Width;
            brush.Color = canvas.fillColor;
            base.OnStylusDown(rawStylusInput);
        }
        protected override void OnAdded()
        {
            base.OnAdded();
            canvas = ((Drawing_InkCanvas)this.Element);
            brush = new SolidColorBrush(Colors.White);
            pen = new Pen(new SolidColorBrush(), canvas.DefaultDrawingAttributes.Width);
        }

        protected override void OnStylusUp(RawStylusInput rawStylusInput)
        {
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
                DrawCurrentStrokeType(drawingContext, stylusPoints, geometry);
            }
        }

        private void DrawCurrentStrokeType(DrawingContext drawingContext, StylusPointCollection stylusPoints, Geometry geometry)
        {
            if (currentStrokeMode == DrawingStrokeType.Pen)
            {
                base.OnDraw(drawingContext, stylusPoints, geometry, pen.Brush);
            }
            else
            {
                Point current = (Point)stylusPoints[stylusPoints.Count - 1];
                Vector v = Point.Subtract(firstPoint, current);
                Vector pre = Point.Subtract(current, prevPoint);
                Point vHalf = new Point((firstPoint.X + current.X) / 2, (firstPoint.Y + current.Y) / 2);
                double weight = canvas.DefaultDrawingAttributes.Width;
                //if (((SolidColorBrush)pen.Brush).Color.A == 0)
                //{
                //    ((SolidColorBrush)pen.Brush).Color = Color.FromArgb(255,255,255,255);
                //}
                if (v.Length > 4)
                {
                    if (needRedraw)
                    {
                        switch (currentStrokeMode)
                        {
                            case DrawingStrokeType.Shape_Ellipse:
                                drawingContext.DrawEllipse(brush, pen, vHalf, v.Length / 2, v.Length / 2);
                                break;
                            case DrawingStrokeType.Shape_Rectangle:
                                drawingContext.DrawRectangle(brush, pen, new Rect(firstPoint, current));
                                break;
                            case DrawingStrokeType.Shape_Triangle:
                                Point p3 = new Point(firstPoint.X + (firstPoint.X - current.X), current.Y);
                                StreamGeometry triGeo = new StreamGeometry();
                                using (StreamGeometryContext ctx = triGeo.Open())
                                {
                                    ctx.BeginFigure((Point)firstPoint, true, true);
                                    ctx.LineTo((Point)current, true, false);
                                    ctx.LineTo((Point)p3, true, false);
                                }
                                triGeo.Freeze();
                                drawingContext.DrawGeometry(brush, pen, triGeo);
                                break;
                            case DrawingStrokeType.Line_Line:
                                drawingContext.DrawLine(pen, firstPoint, current);
                                break;
                            case DrawingStrokeType.Line_Arrow:
                                Vector fir = new Vector(firstPoint.X, firstPoint.Y);
                                Vector cur = new Vector(current.X, current.Y);
                                float h = (10.0f + (float)(weight / 2.0)) * (float)Math.Sqrt(3);
                                float w = 10.0f + (float)(weight / 2.0);
                                Vector u = (cur - fir) / (cur - fir).Length;
                                Vector uv = new Vector(-u.Y, u.X);
                                Vector av1 = cur - h * u + w * uv;
                                Vector av2 = cur - h * u - w * uv;
                                Point a1 = new Point(av1.X, av1.Y);
                                Point a2 = new Point(av2.X, av2.Y);
                                StreamGeometry arrowGeo = new StreamGeometry();
                                using (StreamGeometryContext ctx = arrowGeo.Open())
                                {
                                    ctx.BeginFigure(firstPoint, false, false);
                                    ctx.LineTo(current, true, false);
                                    ctx.BeginFigure(current, false, false);
                                    ctx.LineTo(a1, true, false);
                                    ctx.BeginFigure(current, false, false);
                                    ctx.LineTo(a2, true, false);
                                }
                                arrowGeo.Freeze();
                                drawingContext.DrawGeometry(brush, pen, arrowGeo);
                                break;
                            case DrawingStrokeType.Line_CoordQuad:
                                StreamGeometry quadGeo = new StreamGeometry();
                                Point qP = new Point(firstPoint.X, current.Y);
                                using (StreamGeometryContext ctx = quadGeo.Open())
                                {
                                    ctx.BeginFigure(firstPoint, false, false);
                                    ctx.LineTo(qP, true, false);
                                    ctx.BeginFigure(qP, false, false);
                                    ctx.LineTo(current, true, false);
                                }
                                quadGeo.Freeze();
                                drawingContext.DrawGeometry(brush, pen, quadGeo);
                                break;
                            case DrawingStrokeType.Line_Coord2D:
                                Point xNeg2d = new Point(firstPoint.X + (firstPoint.X - current.X), current.Y);
                                Point yNeg2d = new Point(firstPoint.X, current.Y + (current.Y - firstPoint.Y));
                                StreamGeometry coord2Geo = new StreamGeometry();
                                using (StreamGeometryContext ctx = coord2Geo.Open())
                                {
                                    ctx.BeginFigure((Point)firstPoint, true, true);
                                    ctx.LineTo((Point)yNeg2d, true, false);
                                    ctx.BeginFigure((Point)current, true, true);
                                    ctx.LineTo((Point)xNeg2d, true, false);
                                }
                                coord2Geo.Freeze();
                                drawingContext.DrawGeometry(brush, pen, coord2Geo);
                                break;
                            case DrawingStrokeType.Line_Coord3D:
                                int numDashes = 42;
                                Point xNeg3d = new Point(firstPoint.X + (firstPoint.X - current.X), current.Y);
                                Point yNeg3d = new Point(firstPoint.X, current.Y + (current.Y - firstPoint.Y));
                                Point zNeg3d = new Point(current.X, firstPoint.Y);
                                Point zPos3d = new Point(xNeg3d.X, yNeg3d.Y);
                                StreamGeometry coord3Geo = new StreamGeometry();
                                using (StreamGeometryContext ctx = coord3Geo.Open())
                                {
                                    ctx.BeginFigure((Point)firstPoint, true, true);
                                    ctx.LineTo((Point)yNeg3d, true, false);
                                    ctx.BeginFigure((Point)current, true, true);
                                    ctx.LineTo((Point)xNeg3d, true, false);
                                    for (int i = 0; i < numDashes; i++)
                                    {
                                        Point a;
                                        a = new Point(zNeg3d.X - ((zNeg3d.X - zPos3d.X) * ((double)i / (numDashes))), zNeg3d.Y - ((zNeg3d.Y - zPos3d.Y) * ((double)i / (numDashes))));
                                        i += 1;
                                        Point b = new Point(zNeg3d.X - ((zNeg3d.X - zPos3d.X) * ((double)i / (numDashes))), (zNeg3d.Y - (zNeg3d.Y - zPos3d.Y) * ((double)i / (numDashes))));
                                        ctx.BeginFigure((Point)a, true, true);
                                        ctx.LineTo((Point)b, true, false);
                                        i += 2;
                                    }
                                }
                                coord3Geo.Freeze();
                                drawingContext.DrawGeometry(brush, pen, coord3Geo);
                                break;

                        }
                        needRedraw = false;
                        prevPoint = current;
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
}
