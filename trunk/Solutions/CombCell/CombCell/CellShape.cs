using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CombCell
{
    /// <summary>
    /// A CellShape is a user visible control that represents a Cell.
    /// </summary>
    public abstract class CellShape : Control
    {
        /// <summary>
        /// A string showed on the CellShape
        /// </summary>
        public string Index
        {
            get { return (string)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }
        /// <summary>
        /// Support dependency property of Index
        /// </summary>
        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.Register(
                "Index",
                typeof(String),
                typeof(CellShape),
                new FrameworkPropertyMetadata(
                    "",
                    FrameworkPropertyMetadataOptions.AffectsRender));



        public Scheme Scheme
        {
            get { return (Scheme)GetValue(SchemeProperty); }
            set { SetValue(SchemeProperty, value); }
        }
        public static readonly DependencyProperty SchemeProperty =
            DependencyProperty.Register(
            "Scheme",
            typeof(Scheme),
            typeof(CellShape),
            new FrameworkPropertyMetadata( null,
                FrameworkPropertyMetadataOptions.AffectsRender) );

        public Cell Cell
        {
            get { return (Cell)GetValue(CellProperty); }
            set { SetValue(CellProperty, value); }
        }
        public static readonly DependencyProperty CellProperty =
            DependencyProperty.Register(
            "Cell",typeof(Cell), typeof(CellShape),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.AffectsRender));



        public CellShape()
        {
            this.OverridesDefaultStyle = true;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Scheme = Lengend.Current[Cell.State.ToString()];

            OnRenderOverride(drawingContext);

            double r2 = Math.Min(RenderSize.Width / 2, RenderSize.Height / Math.Sqrt(3))/2;
            FormattedText txt = new FormattedText(
                Index,
                CultureInfo.CurrentCulture,
                FlowDirection,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                r2, Foreground);

            Point txtPosition = new Point(
                (RenderSize.Width - txt.Width - Padding.Left - Padding.Right) / 2,
                (RenderSize.Height - txt.Height - Padding.Top - Padding.Bottom) / 2);
            drawingContext.DrawText(txt, txtPosition);
        }

        protected abstract void OnRenderOverride(DrawingContext drawingContext);
        
    }
}
