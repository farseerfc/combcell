using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CombCell
{
    public class TriArranger : Arranger
    {
        protected override Freezable CreateInstanceCore()
        {
            return new TriArranger();
        }


        protected override bool RecalcCount()
        {
            int xCount = (int)Math.Floor(RenderSize.Width / CellSize * 2);
            int yCount = (int)Math.Floor(RenderSize.Height / (CellSize * Math.Sqrt(3) / 2));
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
            return new Rect(CellSize * i/2,
                            CellSize * Math.Sqrt(3) / 2 * j,
                            CellSize,
                            CellSize * Math.Sqrt(3) / 2); ;
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
            double dx = point.X / CellSize * 2;
            dx = dx - Math.Floor(dx);
            double dy = point.Y /  (CellSize * Math.Sqrt(3) / 2);
            dy = dy - Math.Floor(dy);
            int x = (int)Math.Floor(point.X / CellSize*2);
            int y = (int)Math.Floor(point.Y / (CellSize * Math.Sqrt(3) / 2));
            Console.WriteLine("{0},{1}", dx, dy);
            if ((x + y) % 2 == 1) 
            {
                if (dx <dy) x--; 
            }
            else 
            {
                if (dx + dy<1) x--;
            }
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x >= XCount) x = XCount - 1;
            if (y >= YCount) y = YCount - 1;
            return new Pair<int>(y, x);
        }

        public override CellShape CreateCellShape()
        {
            return new TriCell();
        }

        public override List<Pair<int>> NearBy(int row, int column)
        {
            List<Pair<int>> result = new List<Pair<int>>();
            if ((row + column) % 2 == 1) result.Add(new Pair<int>(row - 1, column));
            result.Add(new Pair<int>(row, column + 1));
            if ((row + column) % 2 == 0) result.Add(new Pair<int>(row + 1, column));
            result.Add(new Pair<int>(row, column - 1));

            return result;
        }
    }
}
