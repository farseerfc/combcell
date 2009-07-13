using System.Collections.Generic;
using System.Windows;

namespace CombCell
{
    /// <summary>
    /// Arranger is to arrange the cells on a comb.
    /// </summary>
    public abstract class Arranger : Freezable
    {

        /////////////////////////////////////////////////////////////////////
        #region Dependency Properties

        /// <summary>
        /// The CellSize is a double factor that controls the size of each CellShape.
        /// Different CellShape may take CellSize as different use.
        /// </summary>
        public double CellSize
        {
            get { return (double)GetValue(CellSizeProperty); }
            set { SetValue(CellSizeProperty, value); }
        }
        /// <summary>
        /// The support dependency property of Arranger.CellSize.
        /// </summary>
        public static readonly DependencyProperty CellSizeProperty =
           DependencyProperty.Register(
           "CellSize", typeof(double), typeof(CombView),
           new FrameworkPropertyMetadata(15.0,
               FrameworkPropertyMetadataOptions.AffectsRender));


        /// <summary>
        /// The total render size of the 2D Comb View.
        /// </summary>
        protected Size RenderSize
        {
            set { SetValue(RenderSizeProperty, value); }
            get { return (Size)GetValue(RenderSizeProperty); }
        }
        /// <summary>
        /// The support dependency property of Arranger.RenderSize.
        /// </summary>
        protected static readonly DependencyProperty RenderSizeProperty =
           DependencyProperty.Register(
           "RenderSize", typeof(Size), typeof(Arranger),
           new FrameworkPropertyMetadata(new Size(0, 0)));


        /// <summary>
        /// Cells count in each row.
        /// </summary>
        public int XCount
        {
            get { return (int)GetValue(XCountProperty); }
            protected set { SetValue(XCountProperty, value); }
        }
        /// <summary>
        /// The support dependency property of Arranger.XCount.
        /// </summary>
        public static readonly DependencyProperty XCountProperty =
            DependencyProperty.Register(
            "XCount", typeof(int), typeof(Arranger),
            new FrameworkPropertyMetadata(0));

        /// <summary>
        /// Cells count in each column.
        /// </summary>
        public int YCount
        {
            get { return (int)GetValue(YCountProperty); }
            protected set { SetValue(YCountProperty, value); }
        }
        /// <summary>
        /// The support dependency property of Arranger.YCount.
        /// </summary>
        public static readonly DependencyProperty YCountProperty =
            DependencyProperty.Register(
            "YCount", typeof(int), typeof(Arranger),
            new FrameworkPropertyMetadata(0));


        /// <summary>
        /// The comb model that under the control of this arranger.
        /// </summary>
        public Comb Comb
        {
            get { return (Comb)GetValue(CombProperty); }
            set { SetValue(CombProperty, value); }
        }
        /// <summary>
        /// The support dependency property of Arranger.CombProperty.
        /// <see cref="CombCell.Arranger.Comb"/>
        /// </summary>
        public static readonly DependencyProperty CombProperty =
            DependencyProperty.Register(
            "Comb", typeof(Comb), typeof(Arranger),
            new FrameworkPropertyMetadata(null));

        #endregion

        /////////////////////////////////////////////////////////////////////



        /// <summary>
        /// The ctor of the Arranger is to create the Comb model under its control
        /// </summary>
        public Arranger()
        {
            Comb = new Comb(this);
        }

        /// <summary>
        /// Arrange a CellShape on row and column to provide its size and position.
        /// </summary>
        /// <param name="row">the cell is in which row, start from 0</param>
        /// <param name="column">the cell is in which column, start from 0</param>
        /// <returns>The Rect defines the size and position of the CellShape</returns>
        public abstract Rect Arrange(int row, int column);

        /// <summary>
        /// Give the cell with a string description.
        /// </summary>
        /// <param name="row">the cell is in which row, start from 0</param>
        /// <param name="column">the cell is in which column, start from 0</param>
        /// <returns>string description of the cell</returns>
        public abstract string MarkIndex(int row, int column);

        /// <summary>
        /// Identify the nearby cells' position of a cell.
        /// </summary>
        /// <param name="row">the cell is in which row, start from 0</param>
        /// <param name="column">the cell is in which column, start from 0</param>
        /// <returns>a list of the nearby cells' position specified by Pair of int.</returns>
        public abstract List<Pair<int>> NearBy(int row, int column);

        /// <summary>
        /// Maps a 2D screen point with the cell's location. Used in hit test.
        /// </summary>
        /// <param name="point">given 2D screen point</param>
        /// <returns>the cell's location</returns>
        public abstract Pair<int> FromPointToPair(Point point);

        /// <summary>
        /// Returns the concrete CellShape.
        /// </summary>
        /// <returns>new instance of the concrete CellShape.</returns>
        public abstract CellShape CreateCellShape();

        /// <summary>
        /// When the RenderSize changed, asked whether it is need to add more CellShapes.
        /// </summary>
        /// <param name="newSize">the new size of render size</param>
        /// <returns>whether it is need to add more CellShapes</returns>
        public virtual bool NeedAddChild(Size newSize)
        {
            RenderSize = newSize;
            return RecalcCount();
        }

        /// <summary>
        /// Maps a 2D screen point with the cell's index. Used in hit test.
        /// </summary>
        /// <param name="point">given 2D screen point</param>
        /// <returns>the cell's index</returns>
        public virtual int FromPointToIndex(Point point)
        {
            Pair<int> pair = FromPointToPair(point);
            return pair.first * XCount + pair.second;
        }

        /// <summary>
        /// Recalculate the XCount and YCount according to RenderSize. Called by NeedAddChild.
        /// </summary>
        /// <returns>whether it is need to add more CellShapes</returns>
        protected abstract bool RecalcCount();


    }
}
