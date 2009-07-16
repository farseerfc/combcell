using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CombCell
{
    /// <summary>
    /// RectArranger defines Rectangle shape 's arrangement
    /// </summary>
    public class RectArranger : Arranger
    {
        /// <summary>
        /// Needed by Freezable
        /// </summary>
        /// <returns>new HexArranger()</returns>
        protected override Freezable CreateInstanceCore()
        {
            return new RectArranger();
        }

        /// <summary>
        /// Recalculate the XCount and YCount according to RenderSize. Called by NeedAddChild.
        /// </summary>
        /// <returns>whether it is need to add more CellShapes</returns>
        protected override bool RecalcCount()
        {
            int xCount = (int)Math.Floor(RenderSize.Width / CellSize);
            int yCount = (int)Math.Floor(RenderSize.Height / CellSize);
            if (xCount <= this.XCount && yCount <= this.YCount)
            {
                return false;
            }
            this.XCount = xCount;
            this.YCount = yCount;
            Comb.EnsureCells(xCount, yCount);
            return true;
        }

        /// <summary>
        /// Arrange a CellShape on row and column to provide its size and position.
        /// </summary>
        /// <param name="row">the cell is in which row, start from 0</param>
        /// <param name="column">the cell is in which column, start from 0</param>
        /// <returns>The Rect defines the size and position of the CellShape</returns>
        public override Rect Arrange(int row, int column)
        {
            int i = column, j = row;
            return new Rect(CellSize * i,
                            CellSize * j,
                            CellSize,
                            CellSize); ;
        }

        /// <summary>
        /// Give the cell with a string description.
        /// </summary>
        /// <param name="row">the cell is in which row, start from 0</param>
        /// <param name="column">the cell is in which column, start from 0</param>
        /// <returns>string description of the cell</returns>
        public override string MarkIndex(int row, int column)
        {
            if (Comb[row, column].Index == 0)
            {
                return "";// +row + ":" + column;
            }
            else
            {
                return "" + Comb[row, column].Index;
            }
        }


        /// <summary>
        /// Maps a 2D screen point with the cell's location. Used in hit test.
        /// </summary>
        /// <param name="point">given 2D screen point</param>
        /// <returns>the cell's location</returns>
        public override Pair<int> FromPointToPair(Point point)
        {
            int x = (int)Math.Floor(point.X / CellSize);
            int y = (int)Math.Floor(point.Y / CellSize);
            if (x >= XCount) x = XCount - 1;
            if (y >= YCount) y = YCount - 1;
            return new Pair<int>(y, x);
        }


        /// <summary>
        /// Returns the concrete CellShape.
        /// </summary>
        /// <returns>new instance of the concrete CellShape.</returns>
        public override CellShape CreateCellShape()
        {
            return new RectCell();
        }


        /// <summary>
        /// Identify the nearby cells' position of a cell.
        /// </summary>
        /// <param name="row">the cell is in which row, start from 0</param>
        /// <param name="column">the cell is in which column, start from 0</param>
        /// <returns>a list of the nearby cells' position specified by Pair of int.</returns>
        public override List<Pair<int>> NearBy(int row, int column)
        {
            List<Pair<int>> result = new List<Pair<int>>();

            result.Add(new Pair<int>(row - 1, column));
            result.Add(new Pair<int>(row, column + 1));
            result.Add(new Pair<int>(row + 1, column));
            result.Add(new Pair<int>(row, column - 1));

            return result;
        }
    }
}
