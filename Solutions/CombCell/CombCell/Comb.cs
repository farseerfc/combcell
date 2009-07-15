﻿using System;
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
        
        private Pair<int> initial;

        public GraphPath<Pair<int>> GraphPath
        {
            get { return (GraphPath<Pair<int>>)GetValue(GraphPathProperty); }
            set { SetValue(GraphPathProperty, value); }
        }
        public static readonly DependencyProperty GraphPathProperty =
            DependencyProperty.Register("GraphPath", typeof(GraphPath<Pair<int>>), typeof(Comb),
            new FrameworkPropertyMetadata(new GraphPath<Pair<int>>()));

        public string PathDiscription
        {
            get { return (string)GetValue(PathDiscriptionProperty);}
            set { SetValue(PathDiscriptionProperty,value);}
        }
        public static readonly DependencyProperty PathDiscriptionProperty=
            DependencyProperty.Register(
                "PathDiscription",typeof(string),typeof(Comb),
                new FrameworkPropertyMetadata(""));

        public Type ChoosedAlgorithm
        {
            get { return (Type)GetValue(ChoosedAlgorithmProperty);}
            set 
            {
                SetValue(ChoosedAlgorithmProperty,value);
                UpdatePath();
            }
        }
        public static readonly DependencyProperty ChoosedAlgorithmProperty=
            DependencyProperty.Register(
                "ChoosedAlgorithm",typeof(Type),typeof(Comb),
                new FrameworkPropertyMetadata(typeof(SingleSource<>)));

        public Cell this[int row, int column]
        {
            get { return cells[row][column]; }
        }

        public Cell this[Pair<int> pos]
        {
            get { return cells[pos.first][pos.second];}
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
            MarkIndex();
            UpdatePath();
        }

        public void Block(Pair<int> pos)
        {
            cells[pos.first][pos.second].State = CellState.Blocked;
            cells[pos.first][pos.second].Index = 0;
            blockList.Add(pos);
            graph.RemoveVertex(graph.VertexMap[pos]);
            MarkIndex();
            UpdatePath();
        }

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

        public void Select(Pair<int> pos)
        {
            
            cells[pos.first][pos.second].State = CellState.Selected;
            selectList.Add(pos);

            UpdatePath();
        }

        public void Unselect(Pair<int> pos)
        {
            cells[pos.first][pos.second].State = CellState.MouseOver;
            selectList.Remove(pos);

            UpdatePath();
        }

        private void UpdatePath()
        {
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
            PathDiscription = "";

            Type typeAlgo = ChoosedAlgorithm.MakeGenericType(typeof(Pair<int>));

            PathAlgorithm<Pair<int>> algo = (PathAlgorithm<Pair<int>>)typeAlgo.GetConstructor(Type.EmptyTypes).Invoke(null);
            algo.Graph = graph;
            algo.Selected = selectList;
            if(algo.CanCalc)
            {
                algo.Calc();
                GraphPath = algo.Path;
                foreach (Pair<int> passed in GraphPath.PassedVertexes)
                {
                    cells[passed.first][passed.second].State = CellState.Passed;
                }

                PathDiscription = "Passed " +GraphPath.PassedCount+"<";
                foreach (Pair<int> pair in GraphPath.PassedVertexes)
                {
                    PathDiscription += this[pair].Index+", ";
                }
                PathDiscription += ">";
            }
            
        }

        private void MarkIndex()
        {
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


        public void StartMarkIndex(Pair<int> pos)
        {
            initial = pos;
            MarkIndex();
        }
    }
}
