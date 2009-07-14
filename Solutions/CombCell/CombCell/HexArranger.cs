﻿using System;
using System.Collections.Generic;
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
            int x = (int)Math.Floor((point.X - CellSize * 0.5) / CellSize / 3);
            int y = (int)Math.Floor((point.Y - (x % 2 * CellSize * 1.5)) / CellSize / Math.Sqrt(12)) * 2;
            y += x % 2;
            x /= 2;
            if (x >= XCount) x = XCount - 1;
            if (y >= YCount) y = YCount - 1;
            return new Pair<int>(y, x);
        }

        public override CellShape CreateCellShape()
        {
            return new HexCell();
        }

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