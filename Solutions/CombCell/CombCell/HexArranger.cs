using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CombCell
{
    public class HexArranger : Arranger
    {
        protected override Freezable CreateInstanceCore()
        {
            return new HexArranger();
        }

        protected override bool RecalcCount()
        {
            int xCount = (int)Math.Ceiling(RenderSize.Width / CellSize / 6.0);
            int yCount = (int)Math.Ceiling(RenderSize.Height / CellSize / Math.Sqrt(3.0));
            if (xCount <= this.XCount && yCount <= this.YCount)
            {
                return false;
            }
            this.XCount = xCount;

            this.YCount = yCount;

            return true;
        }

        public override Rect Arrange(int row, int column)
        {
            int i = column, j = row;
            return new Rect(CellSize * (i * 6 + j % 2 * 3),
                            CellSize * Math.Sqrt(3) * j,
                            CellSize * 4,
                            CellSize * Math.Sqrt(12)); ;
        }

        public override string MarkIndex(int row, int column)
        {
            return "" + row + ":" + column;
        }

        public override int FromPointToIndex(Point point)
        {
            int x = (int)Math.Floor((point.X - CellSize * 0.5) / CellSize / 3);
            int y = (int)Math.Floor((point.Y-(x%2*CellSize*1.5)) / CellSize / Math.Sqrt(12))*2;
            y += x % 2;
            x /= 2;
            
            return y * XCount + x;
        }
    }
}
