using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CombCell
{
    class HexCell:CellShape
    {
        
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            double r = Math.Min(RenderSize.Width/2, RenderSize.Height/Math.Sqrt(3));
            double r2 = r / 2;
            double r3 = r2 * Math.Sqrt(3);

            PolyLineSegment pl = new PolyLineSegment();
            pl.Points.Add(new Point(0, r3));
            pl.Points.Add(new Point(r2, 0));
            pl.Points.Add(new Point(r+r2, 0));
            pl.Points.Add(new Point(r+r, r3));
            pl.Points.Add(new Point(r+r2, r3+r3));
            pl.Points.Add(new Point(r2, r3 + r3));
            pl.Points.Add(new Point(0, r3));

            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(0, r3);
            figure.Segments.Add(pl);
            PathGeometry geo = new PathGeometry();
            geo.Figures.Add(figure);

            Pen normalPen = new Pen(Brushes.Yellow, r /5.0);
            Brush normalBrush = Brushes.White;
            Pen selectPen = new Pen(Brushes.Blue, r / 5.0);
            Brush selectBrush = Brushes.PowderBlue;

            if(!IsMouseOver)
                dc.DrawGeometry(normalBrush, normalPen, geo);
            else
                dc.DrawGeometry(selectBrush, selectPen, geo);
        }
    }
}
