using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Globalization;

namespace CombCell
{
    public class HexCell:CellShape
    {

        protected override void OnRender(DrawingContext dc)
        {
           

            double r = Math.Min(RenderSize.Width/2, RenderSize.Height/Math.Sqrt(3));
            double r1 = r * 0.1;
            double r2 = r / 2;
            double r3 = r2 * Math.Sqrt(3);

            PolyLineSegment pl = new PolyLineSegment();
            pl.Points.Add(new Point(r1, r3));
            pl.Points.Add(new Point(r1 + r2, r1));
            pl.Points.Add(new Point(r + r2-r1, r1));
            pl.Points.Add(new Point(r + r-r1, r3));
            pl.Points.Add(new Point(r + r2-r1, r3 + r3-r1));
            pl.Points.Add(new Point(r1 + r2, r3 + r3-r1));
            //pl.Points.Add(new Point(r1 , r3));

            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(r1 , r3);
            figure.Segments.Add(pl);
            figure.IsClosed = true;
            PathGeometry geo = new PathGeometry();
            geo.Figures.Add(figure);

            Pen normalPen = new Pen(Brushes.Yellow, r /10.0);
            Brush normalBrush = Brushes.White;
            

            if (Scheme!=null)
            {
                Pen pen = Scheme.Pen.Clone();
                pen.Thickness = r / 10.0;
                dc.DrawGeometry(Scheme.Brush, pen, geo);
            }
            else
            {
                dc.DrawGeometry(normalBrush, normalPen, geo);
            }

            base.OnRender(dc);
        }
    }
}
