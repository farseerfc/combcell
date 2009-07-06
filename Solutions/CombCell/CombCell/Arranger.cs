using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CombCell
{
    public abstract class Arranger : Freezable
    {
        public static readonly DependencyProperty CellSizeProperty =
           DependencyProperty.Register(
           "CellSize", typeof(double), typeof(CombView),
           new FrameworkPropertyMetadata(15.0,
               FrameworkPropertyMetadataOptions.AffectsRender));

        public double CellSize
        {
            get { return (double)GetValue(CellSizeProperty); }
            set { SetValue(CellSizeProperty, value); }
        }

        public static readonly DependencyProperty RenderSizeProperty =
            DependencyProperty.Register(
            "RenderSize", typeof(Size), typeof(Arranger),
            new FrameworkPropertyMetadata(new Size(0,0)));


        protected Size RenderSize
        {
            set { SetValue(RenderSizeProperty,value); }
            get { return (Size)GetValue(RenderSizeProperty); }
        }

        public static readonly DependencyProperty XCountProperty =
            DependencyProperty.Register(
            "XCount", typeof(int), typeof(Arranger),
            new FrameworkPropertyMetadata(0));

        public int XCount
        {
            get { return (int)GetValue(XCountProperty); }
            protected set { SetValue(XCountProperty, value); }
        }

        public static readonly DependencyProperty YCountProperty =
            DependencyProperty.Register(
            "YCount", typeof(int), typeof(Arranger),
            new FrameworkPropertyMetadata(0));

        public int YCount
        {
            get { return (int)GetValue(YCountProperty); }
            protected set { SetValue(YCountProperty, value); }
        }

        public abstract Rect Arrange(int row, int column);
        public abstract string MarkIndex(int row, int column);
        protected abstract bool RecalcCount();
        public abstract int FromPointToIndex(Point point);

        public virtual bool NeedAddChild(Size newSize)
        {
            RenderSize = newSize;
            return RecalcCount();
        }

        
    }
}
