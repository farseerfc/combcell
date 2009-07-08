using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Globalization;

namespace CombCell
{
    public abstract class CellShape : Control
    {
        
        
        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.Register(
                "Index",
                typeof(String),
                typeof(CellShape),
                new FrameworkPropertyMetadata(
                    "",
                    FrameworkPropertyMetadataOptions.AffectsRender
                    )
            );

        public string Index
        {
            get { return (string)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }

        public static readonly DependencyProperty SchemeProperty =
            DependencyProperty.Register(
                "Scheme",
                typeof(Scheme),
                typeof(CellShape),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsRender
                    )
            );

        public Scheme Scheme
        {
            get { return (Scheme)GetValue(SchemeProperty); }
            set { SetValue(SchemeProperty, value); }
        }
            
        public CellShape(){
            this.OverridesDefaultStyle = true;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

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
        
    }
}
