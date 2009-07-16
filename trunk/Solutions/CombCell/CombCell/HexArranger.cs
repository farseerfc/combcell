using System;
using System.Collections.Generic;
using System.Windows;

namespace CombCell
{

    /// <summary>
    /// HexArranger defines Hex shape 's arrangement
    /// </summary>
    public class HexArranger : Arranger
    {
        /// <summary>
        /// Needed by Freezable
        /// </summary>
        /// <returns>new HexArranger()</returns>
        protected override Freezable CreateInstanceCore()
        {
            return new HexArranger();
        }

        /// <summary>
        /// Recalculate the XCount and YCount according to RenderSize. Called by NeedAddChild.
        /// </summary>
        /// <returns>whether it is need to add more CellShapes</returns>
        protected override bool RecalcCount()
        {
            int xCount = (int)Math.Floor(RenderSize.Width / CellSize / 6.0);
            int yCount = (int)Math.Floor(RenderSize.Height / CellSize / Math.Sqrt(3.0));
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
            return new Rect(CellSize * (i * 6 + j % 2 * 3),
                            CellSize * Math.Sqrt(3) * j,
                            CellSize * 4,
                            CellSize * Math.Sqrt(12)); ;
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
            int x = (int)Math.Floor((point.X - CellSize * 0.5) / CellSize / 3);
            int y = (int)Math.Floor((point.Y - (x % 2 * CellSize * 1.5)) / CellSize / Math.Sqrt(12)) * 2;
            y += x % 2;
            x /= 2;
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
            return new HexCell();
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

            if(row%2==0)
            {
                result.Add(new Pair<int>(row - 2, column));
                result.Add(new Pair<int>(row - 1, column));
                result.Add(new Pair<int>(row + 1, column));
                result.Add(new Pair<int>(row + 2, column));
                result.Add(new Pair<int>(row + 1, column - 1));
                result.Add(new Pair<int>(row - 1, column - 1));     
            }
            else
            {
                result.Add(new Pair<int>(row - 2, column));
                result.Add(new Pair<int>(row - 1, column + 1));
                result.Add(new Pair<int>(row + 1, column + 1));
                result.Add(new Pair<int>(row + 2, column));
                result.Add(new Pair<int>(row + 1, column));
                result.Add(new Pair<int>(row - 1, column));
                
            }
            return result;
        }
    }
}
