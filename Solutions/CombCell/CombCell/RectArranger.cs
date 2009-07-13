using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CombCell
{
    public class RectArranger : Arranger
    {
        protected override Freezable CreateInstanceCore()
        {
            return new RectArranger();
        }

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

        public override Rect Arrange(int row, int column)
        {
            int i = column, j = row;
            return new Rect(CellSize * i,
                            CellSize * j,
                            CellSize,
                            CellSize); ;
        }

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

        public override Pair<int> FromPointToPair(Point point)
        {
            int x = (int)Math.Floor(point.X / CellSize);
            int y = (int)Math.Floor(point.Y / CellSize);
            if (x >= XCount) x = XCount - 1;
            if (y >= YCount) y = YCount - 1;
            return new Pair<int>(y, x);
        }

        public override CellShape CreateCellShape()
        {
            return new RectCell();
        }

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
