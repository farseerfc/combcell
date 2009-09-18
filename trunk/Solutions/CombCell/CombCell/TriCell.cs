using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace CombCell
{
    public class TriCell:CellShape
    {
        /// <summary>
        /// draw the border of the cell
        /// </summary>
        /// <param name="drawingContext">drawingContext recieved from OnRender</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            Scheme = Lengend.Current[Cell.State.ToString()];

            double r = Math.Min(RenderSize.Width, RenderSize.Height);
            double r1 = r * 0.1;
            double r2 = r/2;
            double r3 = r2 * Math.Sqrt(3);

            PolyLineSegment pl = new PolyLineSegment();
            PathFigure figure = new PathFigure();

            if (Cell == null || (Cell.Position.first + Cell.Position.second) % 2 == 0)
            {
                pl.Points.Add(new Point(r-r1, r3));
                pl.Points.Add(new Point(r1, r3));
                figure.StartPoint = new Point(r2, r1);
            }
            else
            {
                pl.Points.Add(new Point(r-r1, r1));
                pl.Points.Add(new Point(r2, r3));
                figure.StartPoint = new Point(r1, r1);
            }

            figure.Segments.Add(pl);
            figure.IsClosed = true;
            PathGeometry geo = new PathGeometry();
            geo.Figures.Add(figure);

            Pen normalPen = new Pen(Brushes.Yellow, r / 20.0);
            Brush normalBrush = Brushes.White;

            if (Scheme != null)
            {
                Pen pen = Scheme.Pen.Clone();
                pen.Thickness = r / 20.0;
                drawingContext.DrawGeometry(Scheme.Brush, pen, geo);
            }
            else
            {
                drawingContext.DrawGeometry(normalBrush, normalPen, geo);
            }


            double fontSize = Math.Min(RenderSize.Width , RenderSize.Height );
            if (Index.Length > 2)
            {
                fontSize = fontSize / (Index.Length+2);
            }
            else
            {
                fontSize = fontSize / 4;
            }
            FormattedText txt = new FormattedText(
                Index,
                CultureInfo.CurrentCulture,
                FlowDirection,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                fontSize, Foreground);
            bool isOdd = (Cell.Position.first + Cell.Position.second) % 2==0;
            int updown = isOdd ? 1 : -1;

            Point txtPosition = new Point(
                (RenderSize.Width - txt.Width - Padding.Left - Padding.Right) / 2,
                (RenderSize.Height - txt.Height - Padding.Top - Padding.Bottom + RenderSize.Width * 0.2 * updown) / 2);
            drawingContext.DrawText(txt, txtPosition);
        }

        protected override void OnRenderOverride(DrawingContext drawingContext)
        {
            throw new NotImplementedException();
        }
    }
}
