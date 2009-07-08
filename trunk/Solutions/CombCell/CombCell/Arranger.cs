using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CombCell
{
    /// <summary>
    /// Arranger is to arrange the cells on a comb
    /// </summary>
    public abstract class Arranger : Freezable
    {

        /////////////////////////////////////////////////////////////////////
        #region Dependency Properties


        public double CellSize
        {
            get { return (double)GetValue(CellSizeProperty); }
            set { SetValue(CellSizeProperty, value); }
        }
        public static readonly DependencyProperty CellSizeProperty =
           DependencyProperty.Register(
           "CellSize", typeof(double), typeof(CombView),
           new FrameworkPropertyMetadata(15.0,
               FrameworkPropertyMetadataOptions.AffectsRender));

        protected Size RenderSize
        {
            set { SetValue(RenderSizeProperty, value); }
            get { return (Size)GetValue(RenderSizeProperty); }
        }
        protected static readonly DependencyProperty RenderSizeProperty =
           DependencyProperty.Register(
           "RenderSize", typeof(Size), typeof(Arranger),
           new FrameworkPropertyMetadata(new Size(0, 0)));



        public int XCount
        {
            get { return (int)GetValue(XCountProperty); }
            protected set { SetValue(XCountProperty, value); }
        }
        public static readonly DependencyProperty XCountProperty =
            DependencyProperty.Register(
            "XCount", typeof(int), typeof(Arranger),
            new FrameworkPropertyMetadata(0));


        public int YCount
        {
            get { return (int)GetValue(YCountProperty); }
            protected set { SetValue(YCountProperty, value); }
        }
        public static readonly DependencyProperty YCountProperty =
            DependencyProperty.Register(
            "YCount", typeof(int), typeof(Arranger),
            new FrameworkPropertyMetadata(0));


        public Comb Comb
        {
            get { return (Comb)GetValue(CombProperty); }
            set { SetValue(CombProperty, value); }
        }

        public static readonly DependencyProperty CombProperty =
            DependencyProperty.Register(
            "Comb", typeof(Comb), typeof(Arranger),
            new FrameworkPropertyMetadata(new Comb()));


        #endregion

        /////////////////////////////////////////////////////////////////////


        public abstract Rect Arrange(int row, int column);
        public abstract string MarkIndex(int row, int column);
        public abstract int FromPointToIndex(Point point);
        public abstract CellShape CreateCellShape();

        public virtual bool NeedAddChild(Size newSize)
        {
            RenderSize = newSize;
            return RecalcCount();
        }


        protected abstract bool RecalcCount();


    }
}
