using System;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Input;
using System.Windows.Ink;
using System.Diagnostics;
using System.Linq;
using ScrapProject.Enums;
using System.Collections.Generic;
using System.Globalization;

namespace ScrapProject
{
    class CustomStroke : Stroke
    {
        Brush brush;
        Pen pen;
        Point firstPoint;
        Point lastPoint;
        private DrawingStrokeType strokeMode;
        string text;

        public CustomStroke(StylusPointCollection stylusPoints, Color stroke, Color fill, DrawingStrokeType strokeMode) : base(stylusPoints)
        {
            brush = new SolidColorBrush(fill);
            pen = new Pen(new SolidColorBrush(stroke), 2);
            this.strokeMode = strokeMode;
        }
        public CustomStroke(StylusPointCollection stylusPoints, Color stroke, Color fill, DrawingStrokeType strokeMode, string text) : base(stylusPoints)
        {
            brush = new SolidColorBrush(fill);
            pen = new Pen(new SolidColorBrush(stroke), 2);
            this.strokeMode = strokeMode;
            this.text = text;
        }

        protected override void DrawCore(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
        {
            if (pen != null)
            {
                pen.Thickness = DrawingAttributes.Width;
            }
            Geometry g = DrawCurrentStrokeMode();
            drawingContext.DrawGeometry(brush, pen, g);
        }

        private Geometry DrawCurrentStrokeMode()
        {
            Geometry g = new StreamGeometry();
            firstPoint = (Point)StylusPoints[0];
            lastPoint = (Point)StylusPoints[StylusPoints.Count - 1];
            Vector v = Point.Subtract(firstPoint, lastPoint);
            Point vHalf = new Point((firstPoint.X + lastPoint.X) / 2, (firstPoint.Y + lastPoint.Y) / 2);

            Geometry shapeGeo;
            switch (strokeMode)
            {
                case DrawingStrokeType.Text:
                    shapeGeo = new EllipseGeometry(vHalf, v.Length / 2, v.Length / 2);
                    if (text != null)
                    {
                        FormattedText formText = new FormattedText(text, CultureInfo.GetCultureInfo("en-us"), System.Windows.FlowDirection.LeftToRight, new Typeface("Arial"), 32, brush);
                        shapeGeo = formText.BuildGeometry(firstPoint);
                        pen = null;
                    }
                    break;
                case DrawingStrokeType.Shape_Ellipse:
                    shapeGeo = new EllipseGeometry(vHalf, v.Length / 2, v.Length / 2);
                    break;
                case DrawingStrokeType.Shape_Rectangle:
                    shapeGeo = new RectangleGeometry(new Rect(firstPoint, lastPoint));
                    break;
                case DrawingStrokeType.Shape_Triangle:
                    Point p3 = new Point(firstPoint.X + (firstPoint.X - lastPoint.X), lastPoint.Y);
                    shapeGeo = new StreamGeometry();
                    using (StreamGeometryContext ctx = ((StreamGeometry)shapeGeo).Open())
                    {
                        ctx.BeginFigure((Point)firstPoint, true, true);
                        ctx.LineTo((Point)lastPoint, true, false);
                        ctx.LineTo((Point)p3, true, false);
                    }
                    break;
                case DrawingStrokeType.Line_Line:
                    shapeGeo = new LineGeometry(firstPoint, lastPoint);
                    break;
                case DrawingStrokeType.Line_Arrow:
                    Vector fir = new Vector(firstPoint.X, firstPoint.Y);
                    Vector cur = new Vector(lastPoint.X, lastPoint.Y);
                    float h = (10.0f+(float)(pen.Thickness/2.0)) * (float)Math.Sqrt(3);
                    float w = 10.0f + (float)(pen.Thickness / 2.0);
                    Vector u = (cur - fir) / (cur - fir).Length;
                    Vector uv = new Vector(-u.Y, u.X);
                    Vector av1 = cur - h * u + w * uv;
                    Vector av2 = cur - h * u - w * uv;
                    Point a1 = new Point(av1.X, av1.Y);
                    Point a2 = new Point(av2.X, av2.Y);
                    shapeGeo = new StreamGeometry();
                    using (StreamGeometryContext ctx = ((StreamGeometry)shapeGeo).Open())
                    {
                        ctx.BeginFigure(firstPoint, true, false);
                        ctx.LineTo(lastPoint, true, false);
                        ctx.BeginFigure(lastPoint, true, false);
                        ctx.LineTo(a1, true, false);
                        ctx.BeginFigure(lastPoint, true, false);
                        ctx.LineTo(a2, true, false);
                    }
                    shapeGeo.Freeze();
                    break;
                case DrawingStrokeType.Line_CoordQuad:
                    shapeGeo = new StreamGeometry();
                    Point qP = new Point(firstPoint.X, lastPoint.Y);
                    using (StreamGeometryContext ctx = ((StreamGeometry)shapeGeo).Open())
                    {
                        ctx.BeginFigure(firstPoint, true, false);
                        ctx.LineTo(qP, true, false);
                        ctx.BeginFigure(qP, true, false);
                        ctx.LineTo(lastPoint, true, false);
                    }
                    shapeGeo.Freeze();
                    break;
                case DrawingStrokeType.Line_Coord2D:
                    Point xNeg2d = new Point(firstPoint.X + (firstPoint.X - lastPoint.X), lastPoint.Y);
                    Point yNeg2d = new Point(firstPoint.X, lastPoint.Y + (lastPoint.Y - firstPoint.Y));
                    shapeGeo = new StreamGeometry();
                    using (StreamGeometryContext ctx = ((StreamGeometry)shapeGeo).Open())
                    {
                        ctx.BeginFigure((Point)firstPoint, true, true);
                        ctx.LineTo((Point)yNeg2d, true, false);
                        ctx.BeginFigure((Point)lastPoint, true, true);
                        ctx.LineTo((Point)xNeg2d, true, false);
                    }
                    shapeGeo.Freeze();
                    break;
                case DrawingStrokeType.Line_Coord3D:
                    int numDashes = 42;
                    Point xNeg3d = new Point(firstPoint.X + (firstPoint.X - lastPoint.X), lastPoint.Y);
                    Point yNeg3d = new Point(firstPoint.X, lastPoint.Y + (lastPoint.Y - firstPoint.Y));
                    Point zNeg3d = new Point(lastPoint.X, firstPoint.Y);
                    Point zPos3d = new Point(xNeg3d.X, yNeg3d.Y);
                    shapeGeo = new StreamGeometry();
                    using (StreamGeometryContext ctx = ((StreamGeometry)shapeGeo).Open())
                    {
                        ctx.BeginFigure((Point)firstPoint, true, true);
                        ctx.LineTo((Point)yNeg3d, true, false);
                        ctx.BeginFigure((Point)lastPoint, true, true);
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
                    shapeGeo.Freeze();
                    break;
                default:
                    shapeGeo = new EllipseGeometry(vHalf, v.Length / 2, v.Length / 2);
                    break;
            }
            return shapeGeo;
        }
    }
}
