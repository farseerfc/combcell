using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using CombCell.DSAlgo;

namespace CombCell
{
    /// <summary>
    /// Model of a comb, including the algorithm of marking the indexes
    /// </summary>
    public class Comb : Freezable
    {
        private List<List<Cell>> cells;
        private Graph<Pair<int>> graph;
        private Arranger arranger;
        private List<Pair<int>> blockList;
        private List<Pair<int>> selectList;
        private Pair<int> initial;

#region dependency properties


        /// <summary>
        /// Calculated path
        /// </summary>
        public GraphPath<Pair<int>> GraphPath
        {
            get { return (GraphPath<Pair<int>>)GetValue(GraphPathProperty); }
            set { SetValue(GraphPathProperty, value); }
        }
        /// <summary>
        /// Support dependency property for GraphPath
        /// </summary>
        public static readonly DependencyProperty GraphPathProperty =
            DependencyProperty.Register("GraphPath", typeof(GraphPath<Pair<int>>), typeof(Comb),
            new FrameworkPropertyMetadata(new GraphPath<Pair<int>>()));

        /// <summary>
        /// Generated string description for GraphPath
        /// </summary>
        public string PathDescription
        {
            get { return (string)GetValue(PathDescriptionProperty);}
            set { SetValue(PathDescriptionProperty,value);}
        }
        /// <summary>
        /// Support dependency property for PathDescriptions
        /// </summary>
        public static readonly DependencyProperty PathDescriptionProperty=
            DependencyProperty.Register(
                "PathDescription",typeof(string),typeof(Comb),
                new FrameworkPropertyMetadata(""));

        /// <summary>
        /// Choosed Algorithm
        /// </summary>
        public PathAlgorithm<Pair<int>> ChoosedAlgorithm
        {
            get { return (PathAlgorithm<Pair<int>>)GetValue(ChoosedAlgorithmProperty); }
            set 
            {
                SetValue(ChoosedAlgorithmProperty,value);
                UpdatePath();
            }
        }
        /// <summary>
        /// Support dependency property for ChoosedAlgorithm
        /// </summary>
        public static readonly DependencyProperty ChoosedAlgorithmProperty=
            DependencyProperty.Register(
                "ChoosedAlgorithm", typeof(PathAlgorithm<Pair<int>>), typeof(Comb),
                new FrameworkPropertyMetadata(new Hamilton<Pair<int>>()));



#endregion

        /// <summary>
        /// Get a cell by indicate row and column
        /// </summary>
        /// <param name="row">row of the cell </param>
        /// <param name="column">column of the cell</param>
        /// <returns>cell int given row and column</returns>
        public Cell this[int row, int column]
        {
            get 
            {
                if (!(row < cells.Count)) throw new ArgumentOutOfRangeException("row");
                if (!(column < cells[row].Count)) throw new ArgumentOutOfRangeException("column");
                return cells[row][column]; 
            }
        }

        /// <summary>
        /// Get a cell by a pair
        /// </summary>
        /// <param name="pair">row and column of the cell </param>
        /// <returns>cell int given row and column</returns>
        public Cell this[Pair<int> pos]
        {
            get { return this[pos.first,pos.second];}
        }



        /// <summary>
        /// Create a comb with a given arranger, usually called by the arranger
        /// </summary>
        /// <param name="arranger">Given arranger</param>
        public Comb(Arranger arranger)
        {
            cells = new List<List<Cell>>();
            graph = new Graph<Pair<int>>();
            blockList = new List<Pair<int>>();
            selectList = new List<Pair<int>>();
            this.arranger = arranger;
        }

        /// <summary>
        /// Create the comb, needed by Freezable
        /// </summary>
        /// <returns></returns>
        protected override Freezable CreateInstanceCore()
        {
            return new Comb(arranger);
        }

#region operations

        /// <summary>
        /// Ensure that the comb has at least xCount column and yCount row.
        /// Remark indexes if necessary
        /// </summary>
        /// <param name="xCount">at least xCount column</param>
        /// <param name="yCount">at least yCount row</param>
        public void EnsureCells(int xCount, int yCount)
        {
            if (arranger == null) throw new InvalidOperationException();
            bool isAdd = false;
            while (cells.Count < yCount)
            {
                cells.Add(new List<Cell>());
                isAdd = true;
            }
            for (int i = 0; i < cells.Count; ++i)
            {
                for (int j = cells[i].Count; j < xCount; ++j)
                {
                    isAdd = true;
                    //Add new cell into comb
                    Cell cell = new Cell();
                    cell.Position = new Pair<int>(i, j);
                    cells[i].Add(cell);
                    //maps a vertex to it
                    Vertex<Pair<int>> vertex = graph.CreateVertex(new Pair<int>(i, j));
                    List<Pair<int>> nearBy = arranger.NearBy(i, j);
                    foreach (Pair<int> c in nearBy)
                    {
                        if (graph.VertexMap.ContainsKey(c))
                            //assure that the cell referred by c is exist
                        {
                            Vertex<Pair<int>> nearByVertex = graph.VertexMap[c];
                            Edge<Pair<int>> edge = graph.CreateEdge(vertex, nearByVertex);
                        }
                    }
                }
            }
            if (isAdd)
            {
                MarkIndex();
            }
            //UpdatePath();
        }

        /// <summary>
        /// Set the cell in given position as blocked
        /// </summary>
        /// <param name="pos">position of the cell</param>
        public void Block(Pair<int> pos)
        {
            cells[pos.first][pos.second].State = CellState.Blocked;
            cells[pos.first][pos.second].Index = 0;
            blockList.Add(pos);
            graph.RemoveVertex(graph.VertexMap[pos]);
            MarkIndex();
            UpdatePath();
        }

        /// <summary>
        /// Unblock the cell in given position
        /// </summary>
        /// <param name="pos">position of the cell</param>
        public void Unblock(Pair<int> pos)
        {
            cells[pos.first][pos.second].State = CellState.MouseOver;
            blockList.Remove(pos);
            Vertex<Pair<int>> v = graph.CreateVertex(pos);
            List<Pair<int>> nearBy = arranger.NearBy(pos.first, pos.second);
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
            MarkIndex();
            UpdatePath();
        }


        /// <summary>
        /// Select the cell in given position
        /// </summary>
        /// <param name="pos">position of the cell</param>
        public void Select(Pair<int> pos)
        {
            
            cells[pos.first][pos.second].State = CellState.Selected;
            selectList.Add(pos);

            UpdatePath();
        }

        /// <summary>
        /// Unelect the cell in given position
        /// </summary>
        /// <param name="pos">position of the cell</param>
        public void Unselect(Pair<int> pos)
        {
            cells[pos.first][pos.second].State = CellState.MouseOver;
            selectList.Remove(pos);

            UpdatePath();
        }

        /// <summary>
        /// Start mark the indexes from the cell in given position
        /// </summary>
        /// <param name="pos">position of the cell</param>
        public void StartMarkIndex(Pair<int> pos)
        {
            initial = pos;
            MarkIndex();
        }


#endregion


#region support algorithms


        /// <summary>
        /// Calc and show the path
        /// </summary>
        private void UpdatePath()
        {
            //first clear all passed flags
            foreach (Pair<int> passed in GraphPath.PassedVertexes)
            {
                Cell cell = cells[passed.first][passed.second];
                if(blockList.Contains(passed))
                {
                    cell.State = CellState.Blocked;
                }else if(selectList.Contains(passed))
                {
                    cell.State = CellState.Selected;
                }
                else
                {
                    cell.State = CellState.Normal;
                }
            }
            PathDescription = "";

            //get the algorithm and set the parameters
            PathAlgorithm<Pair<int>> algo = ChoosedAlgorithm;
            algo.Graph = graph;
            algo.Selected = selectList;
            algo.Path = new GraphPath<Pair<int>>();
            if(algo.CanCalc)
            {
                //calculate 
                algo.Calc();
                //get the calculated path
                GraphPath = algo.Path;
                //set the passed cells as in the path
                foreach (Pair<int> passed in GraphPath.PassedVertexes)
                {
                    if (cells.Count > passed.first &&
                        cells[passed.first].Count>passed.second&&
                        (cells[passed.first][passed.second].State == CellState.Normal ||
                        cells[passed.first][passed.second].State == CellState.MouseOver))
                    cells[passed.first][passed.second].State = CellState.Passed;
                }
                //generate the path description
                PathDescription = "Passed " +GraphPath.Count+" <";
                foreach (Pair<int> pair in GraphPath.CrossVertexes)
                {
                    PathDescription += this[pair].Index+", ";
                }
                PathDescription += ">";
                //triggered the event
                if (PathCalculated != null)
                {
                    PathCalculated(this, EventArgs.Empty);
                }
            }
            
        }


        /// <summary>
        /// The algorithm of marking the indexes
        /// </summary>
        private void MarkIndex()
        {
            //if we do not know where to start, then do nothing
            if (!graph.VertexMap.ContainsKey(initial)) return;

            //clear marked indexes
            foreach (List<Cell> line in cells)
            {
                foreach (Cell cell in line)
                {
                    cell.Index = 0;
                }
            }

            //prepare for the algorithm
            int index = 1;
            graph.ClearAccessed();
            List<Vertex<Pair<int>>> queue = new List<Vertex<Pair<int>>>();

            //first vertex enqueue
            queue.Add(graph.VertexMap[initial]);
            while (queue.Count > 0)
            {
                //next vertex dequeue
                Vertex<Pair<int>> v = queue[0];
                queue.RemoveAt(0);
                if (v.Accessed) continue;

                //access and mark index
                cells[v.Key.first][v.Key.second].Index = index++;
                v.Accessed = true;

                //get accessible nearby cells
                List<Pair<int>> nearByCells = arranger.NearBy(v.Key.first, v.Key.second);
                for (int i = 0; i < nearByCells.Count; ++i)
                {
                    if (!graph.VertexMap.ContainsKey(nearByCells[i]))
                    {
                        nearByCells.Remove(nearByCells[i--]);
                    }
                }
                if (nearByCells.Count == 0) continue;

                //rotate the order of nearby list to ensure clock-wise
                //firstly jump all non-accessed cells
                int circleCount = 0;
                while (!graph.VertexMap[nearByCells[0]].Accessed &&
                    !queue.Contains(graph.VertexMap[nearByCells[0]]))
                {
                    nearByCells.Add(nearByCells[0]);
                    nearByCells.RemoveAt(0);
                    if (++circleCount >= nearByCells.Count) break;
                }
                //secondly jump all accessed cells
                circleCount = 0;
                while (graph.VertexMap[nearByCells[0]].Accessed ||
                    queue.Contains(graph.VertexMap[nearByCells[0]]))
                {
                    nearByCells.Add(nearByCells[0]);
                    nearByCells.RemoveAt(0);
                    if (++circleCount >= nearByCells.Count) break;
                }

                //then enqueue the nearby cells in order
                foreach (Pair<int> nearBy in nearByCells)
                {
                    Vertex<Pair<int>> otherEnd = graph.VertexMap[nearBy];
                    if (!otherEnd.Accessed && !queue.Contains(otherEnd))
                    {
                        queue.Add(otherEnd);
                    }

                }
            }
        }


#endregion

        #region events

        /// <summary>
        /// Triggered when the path is calculated
        /// </summary>
        public event EventHandler PathCalculated;

        #endregion
    }
}
