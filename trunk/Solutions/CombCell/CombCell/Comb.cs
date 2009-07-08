using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using CombCell.DSAlgo;

namespace CombCell
{
    public class Comb:Freezable
    {
        private List<List<Cell>> cells;
        private Graph graph;
        private Arranger arranger;

        public Comb(Arranger arranger)
        {
            cells = new List<List<Cell>>();
            graph = new Graph();
            this.arranger = arranger;
        }

        public void EnsureCells(int xCount,int yCount)
        {
            while(cells.Count<yCount){
                cells.Add(new List<Cell>());
            }
            for (int i = 0; i < cells.Count; ++i)
            {
                for(int j=cells[i].Count;j<xCount;++j){
                    Cell cell = new Cell();
                    cells[i].Add(cell);
                    Vertex vertex = graph.CreateVertex("" + i + "," + j);
                    List<Pair<int>> nearBy = arranger.NearBy(i, j);
                }
            }
        }

        public Cell this[int x,int y]
        {
            get { return cells[y][x]; }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new Comb(arranger);
        }
    }
}
