using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using CombCell.DSAlgo;

namespace CombCell
{
    public class Comb : Freezable
    {
        private List<List<Cell>> cells;
        private Graph<Pair<int>> graph;
        private Arranger arranger;

        public Comb(Arranger arranger)
        {
            cells = new List<List<Cell>>();
            graph = new Graph<Pair<int>>();
            this.arranger = arranger;
        }

        public void EnsureCells(int xCount, int yCount)
        {
            while (cells.Count < yCount)
            {
                cells.Add(new List<Cell>());
            }
            for (int i = 0; i < cells.Count; ++i)
            {
                for (int j = cells[i].Count; j < xCount; ++j)
                {
                    //Add new cell into comb
                    Cell cell = new Cell();
                    cells[i].Add(cell);
                    //maps a vertex to it
                    Vertex<Pair<int>> vertex = graph.CreateVertex(new Pair<int>(i, j));
                    List<Pair<int>> nearBy = arranger.NearBy(i, j);
                    foreach (Pair<int> c in nearBy)
                    {
                        if ((c.first < i || (c.first == i && c.second < j)) &&
                            c.first >= 0 && c.second >= 0 && c.first < yCount && c.second < xCount) //assure that the cell refered by c is exist
                        {
                            Vertex<Pair<int>> nearByVertex = graph.VertexMap[new Pair<int>(c.first, c.second)];
                            Edge<Pair<int>> edge = graph.CreateEdge(vertex, nearByVertex);
                        }
                    }
                }
            }
        }

        public Cell this[int row, int column]
        {
            get { return cells[row][column]; }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new Comb(arranger);
        }

        public void StartMarkIndex(int row, int column)
        {
            int index = 1;
            graph.ClearAccessed();
            List<Vertex<Pair<int>>> queue = new List<Vertex<Pair<int>>>();
            queue.Add(graph.VertexMap[new Pair<int>(row, column)]);
            Vertex<Pair<int>> lastVertex = null;
            while (queue.Count > 0)
            {
                Vertex<Pair<int>> v = queue[0];
                queue.RemoveAt(0);
                if (!v.Accessed)
                {
                    cells[v.Key.first][v.Key.second].Index = index++;
                    v.Accessed = true;
                    List<Pair<int>> nearByCells = arranger.NearBy(v.Key.first, v.Key.second);

                    for (int i = 0; i < nearByCells.Count; ++i)
                    {
                        if (!graph.VertexMap.ContainsKey(nearByCells[i]))
                        {
                            nearByCells.Remove(nearByCells[i--]);
                        }
                    }

                    int circleCount = 0;
                    while (!graph.VertexMap[nearByCells[0]].Accessed &&
                        !queue.Contains(graph.VertexMap[nearByCells[0]]))
                    {
                        nearByCells.Add(nearByCells[0]);
                        nearByCells.RemoveAt(0);
                        if (++circleCount >= nearByCells.Count) break;
                    }
                    circleCount = 0;
                    while (graph.VertexMap[nearByCells[0]].Accessed ||
                        queue.Contains(graph.VertexMap[nearByCells[0]]))
                    {
                        nearByCells.Add(nearByCells[0]);
                        nearByCells.RemoveAt(0);
                        if (++circleCount >= nearByCells.Count) break;
                    }

                    foreach (Pair<int> nearBy in nearByCells)
                    {
                        Pair<int> pair = new Pair<int>(nearBy.first, nearBy.second);

                        Vertex<Pair<int>> otherEnd = graph.VertexMap[pair];
                        if (!otherEnd.Accessed)
                        {
                            if (!queue.Contains(otherEnd))
                                queue.Add(otherEnd);
                        }

                    }
                    lastVertex = v;

                }

            }
        }
    }
}
