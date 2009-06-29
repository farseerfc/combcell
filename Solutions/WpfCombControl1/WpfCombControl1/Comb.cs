using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCombControl1
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCombControl1"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCombControl1;assembly=WpfCombControl1"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:Comb/>
    ///
    /// </summary>
    public class Comb : Control
    {
        public static DependencyProperty CellSizeProperty=
            DependencyProperty.Register(
            "CellSize", typeof(double), typeof(Comb),
            new FrameworkPropertyMetadata(30.0,
                FrameworkPropertyMetadataOptions.AffectsRender));
        
        public double CellSize{
            get{
                return (double)GetValue(Comb.CellSizeProperty);
            }
            set{
                SetValue(Comb.CellSizeProperty, value);
            }
        }

        public static DependencyProperty MouseLocationProperty =
            DependencyProperty.Register(
                "MouseLocation", typeof(Point), typeof(Comb),
                new FrameworkPropertyMetadata(new Point(0,0),
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public Point MouseLocation
        {
            get
            {
                return (Point)GetValue(Comb.MouseLocationProperty);
            }
            set
            {
                SetValue(Comb.MouseLocationProperty, value);
            }
        }

        static Comb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Comb), new FrameworkPropertyMetadata(typeof(Comb)));
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            double r = CellSize, r2 = r / 2, r3 = r2 * Math.Sqrt(3);
            PolyLineSegment pl = new PolyLineSegment();
            pl.Points.Add(new Point(-r2, -r3));
            pl.Points.Add(new Point(r2, -r3));
            pl.Points.Add(new Point(r, 0));
            pl.Points.Add(new Point(r2, r3));
            pl.Points.Add(new Point(-r2, r3));
            pl.Points.Add(new Point(-r, 0));
            
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(-r, 0);
            figure.Segments.Add(pl);

            PathGeometry geo = new PathGeometry();
            geo.Figures.Add(figure);


            Pen normalPen = new Pen(Brushes.Black, 3.0);
            Brush normalBrush = Brushes.White;
            Pen selectPen = new Pen(Brushes.Blue, 3.0);
            Brush selectBrush = Brushes.PowderBlue;

            bool isOdd = false;
            PathGeometry selected = geo.Clone();
            for (double dy = 0; dy < this.RenderSize.Height+r3; dy += r3)
            {
                isOdd = !isOdd;
                for (double dx = isOdd ? r * 1.5 : 0; dx < this.RenderSize.Width+3*r; dx += 3 * r)
                {
                    geo.Transform = new TranslateTransform(dx, dy);
                    if (geo.FillContains( MouseLocation))
                    {
                        selected = geo.Clone();
                    }
                    else
                    {
                        dc.DrawGeometry(normalBrush, normalPen, geo.Clone());
                    }
                }
            }
            dc.DrawGeometry(selectBrush, selectPen, selected.Clone());
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e){
            base.OnMouseWheel(e);

            double scale = 1 + (e.Delta / 1200.0);
            CellSize *= scale;
        }

        protected override void OnMouseMove(MouseEventArgs e){
            base.OnMouseMove(e);

            MouseLocation = e.GetPosition(this);
        }
    }
}
