using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace CombCell
{
    public class RectCell : CellShape
    {
        protected override void OnRenderOverride(DrawingContext drawingContext)
        {


            double r = Math.Min(RenderSize.Width ,RenderSize.Height);
            double r1 = r * 0.1;
            double r2 = r - r1 - r1;

            PolyLineSegment pl = new PolyLineSegment();
            pl.Points.Add(new Point(r1, r2));
            pl.Points.Add(new Point(r2, r2));
            pl.Points.Add(new Point(r2, r1));

            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(r1, r1);
            figure.Segments.Add(pl);
            figure.IsClosed = true;
            PathGeometry geo = new PathGeometry();
            geo.Figures.Add(figure);

            Pen normalPen = new Pen(Brushes.Yellow, r / 10.0);
            Brush normalBrush = Brushes.White;

            if (Scheme != null)
            {
                Pen pen = Scheme.Pen.Clone();
                pen.Thickness = r / 10.0;
                drawingContext.DrawGeometry(Scheme.Brush, pen, geo);
            }
            else
            {
                drawingContext.DrawGeometry(normalBrush, normalPen, geo);
            }

        }
    }
}
