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
        private List<Pair<int>> blockList;
        private List<Pair<int>> selectList;

        public Cell this[int row, int column]
        {
            get { return cells[row][column]; }
        }


        protected override Freezable CreateInstanceCore()
        {
            return new Comb(arranger);
        }

        public Comb(Arranger arranger)
        {
            cells = new List<List<Cell>>();
            graph = new Graph<Pair<int>>();
            blockList = new List<Pair<int>>();
            selectList = new List<Pair<int>>();
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
                            Vertex<Pair<int>> nearByVertex = graph.VertexMap[c];
                            Edge<Pair<int>> edge = graph.CreateEdge(vertex, nearByVertex);
                        }
                    }
                }
            }
        }

        public void Block(Pair<int> pos)
        {
            cells[pos.first][pos.second].State = CellState.Blocked;
            cells[pos.first][pos.second].Index = 0;
            blockList.Add(pos);
            graph.RemoveVertex(graph.VertexMap[pos]);

        }

        public void Unblock(Pair<int> pos)
        {
            cells[pos.first][pos.second].State = CellState.MouseOver;
            blockList.Remove(pos);
            Vertex<Pair<int>> v=graph.CreateVertex(pos);
            List<Pair<int>> nearBy = arranger.NearBy(pos.first,pos.second);
            foreach (Pair<int> c in nearBy)
            {
                if (c.first >= 0 && c.second >= 0 && c.first < arranger.YCount && c.second < arranger.XCount) //assure that the cell refered by c is exist
                {
                    if (graph.VertexMap.ContainsKey(c))
                    {
                        Vertex<Pair<int>> nearByVertex = graph.VertexMap[c];
                        Edge<Pair<int>> edge = graph.CreateEdge(v, nearByVertex);
                    }
                }
            }
        }

        public void Select(Pair<int> pos)
        {
            cells[pos.first][pos.second].State = CellState.Selected;
            selectList.Add(pos);
        }

        public void Unselect(Pair<int> pos)
        {
            cells[pos.first][pos.second].State = CellState.MouseOver;
            selectList.Remove(pos);
        }


        public void StartMarkIndex(int row, int column)
        {
            foreach(List<Cell> line in cells){
                foreach(Cell cell in line){
                    cell.Index = 0;
                }
            }
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

                    if(nearByCells.Count==0)
                    {
                        continue;
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
