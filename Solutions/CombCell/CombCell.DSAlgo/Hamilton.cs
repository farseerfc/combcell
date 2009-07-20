using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombCell.DSAlgo
{
    /// <summary>
    /// A Hamilton path is the shortest path that path through all selected vertexes.
    /// </summary>
    /// <typeparam name="T">The key type of vertex</typeparam>
    public class Hamilton<T> : PathAlgorithm<T>
    {
        public override string Name
        {
            get { return "Hamilton(approx)"; }
        }

        public override string Discription
        {
            get { return "Hamilton path is the shortest path"+
                " that path through all selected vertexes."+
                " This is a approx fast version";
            }
        }


        private Dictionary<T, Dictionary<T, int>> distanceMap;
        private Dictionary<T, Dictionary<Vertex<T>, Vertex<T>>> preMap;
        private T longestStart;
        private T longestEnd;
        private Graph<T> minTree;
        private List<T> keyPath;


        public override bool CanCalc
        {
            get { return Selected.Count > 1; }
        }

        public override void Calc()
        {
            //first calculate distance between every two selected vertexes
            calcDistance();
            //now that we got distance between every 2 vertex of selected
            //this can be thought as a complete-graph with value on edge
            //then build a minimum tree on this complete graph using prim algorithm
            prim();
            //last we generate path using Approx-Tsp-Tour algorithm
            approxTravelingSalesman();
            // and then generate the path
            generatePath();

        }

        private void calcDistance()
        {
            distanceMap = new Dictionary<T, Dictionary<T, int>>();
            preMap = new Dictionary<T, Dictionary<Vertex<T>, Vertex<T>>>();
            int longest = 0;
            for (int i = 0; i < Selected.Count; ++i)
            {
                //call SingleSource to calculate 
                //distance to every other vertex from Selected[0]
                SingleSource<T> singleSource = new SingleSource<T>();
                singleSource.Graph = Graph;
                singleSource.Selected = Selected;
                singleSource.Calc();

                //we only need the distance to Selected vertexes
                //so not copy all singleSource.Distance
                //we just copy what is necessary
                Dictionary<T, int> distance = new Dictionary<T, int>();
                for (int v = 1; v < Selected.Count; ++v)//jumped first
                {
                    int d = singleSource.Distance[Graph.VertexMap[Selected[v]]];
                    distance[Selected[v]] =d;
                    if(d>longest)
                    {
                        longestStart=Selected[0];
                        longestEnd=Selected[v];
                        longest=d;
                    }
                    
                }
                distanceMap.Add(Selected[0], distance);
                preMap.Add(Selected[0], singleSource.pre);

                //Rotate first vertex to last
                //and calculate the distance start from the next vertex
                Selected.Add(Selected[0]);
                Selected.RemoveAt(0);
            }
        }


        /// <summary>
        /// Prim algorithm that generate the minimum tree
        /// of the complete-graph defined by distanceMap.
        /// According to Introduction to Algorithm, 2nd edition, Chapter 23.2
        /// Using PriorityQueue
        /// </summary>
        private void prim()
        {
            PriorityQueue<T, int> keyPrim = new PriorityQueue<T, int>();
            Dictionary<T, T> prePrim = new Dictionary<T, T>();

            foreach (T vertex in distanceMap.Keys)
            {
                keyPrim[vertex] = Graph.Infinitive;
            }
            keyPrim[longestStart] = 0;
            while (!keyPrim.Empty)
            {
                KeyValuePair<T, int> pair = keyPrim.Pop();
                T vertex = pair.Key;
                foreach (T nearBy in distanceMap.Keys)
                {
                    if (nearBy.Equals(vertex)) continue;
                    if (keyPrim.ContainsKey(nearBy) &&
                        distanceMap[vertex][nearBy] < keyPrim[nearBy])
                    {
                        prePrim[nearBy] = vertex;
                        keyPrim[nearBy] = distanceMap[vertex][nearBy];
                    }
                }
            }

            //above is the implement of prim on Introduction to Algorithm
            //next we form a minTree based on preTrim
            minTree = new Graph<T>();
            foreach (T v in distanceMap.Keys)
            {
                minTree.CreateVertex(v);
            }
            foreach(Vertex<T> v in minTree.Vertexes)
            {
                if(prePrim.ContainsKey(v.Key))
                {
                    T pre = prePrim[v.Key];
                    minTree.CreateEdge(v, minTree.VertexMap[pre]);
                }
            }
        }


        /// <summary>
        /// A approx algorithm for traveling salesman problem. \n
        /// Defined in Introduction to Algorithm, 2nd edition, chapter 35.2.1
        /// </summary>
        private void approxTravelingSalesman()
        {
            keyPath = new List<T>();
            minTree.ClearAccessed();
            Stack<T> stack = new Stack<T>();//stack used in pre-order of the tree
            stack.Push(longestEnd);//first vertex;
            while(stack.Count>0)
            {
                T next = stack.Pop();
                if (minTree.VertexMap[next].Accessed) continue;
                keyPath.Add(next);
                minTree.VertexMap[next].Accessed = true;
                foreach(Edge<T> edge in minTree.VertexMap[next].Edges)
                {
                    Vertex<T> other = edge.From;
                    if(other==minTree.VertexMap[next])other=edge.To;
                    if(!other.Accessed)
                    {
                        stack.Push(other.Key);
                    }
                }
            }
        }

        private void generatePath()
        {
            List<T> path = new List<T>();
            List<T> crossPath = new List<T>();
            for(int i=1;i<keyPath.Count;++i)
            {
                T start = keyPath[i-1];
                T end = keyPath[i];

                Vertex<T> pre = Graph.VertexMap[end];

                //crossPath.Add(start);
                List<T> singlePath = new List<T>();
                while(pre!=Graph.VertexMap[start])
                {
                    pre = preMap[start][pre];
                    if(!keyPath.Contains(pre.Key)&&!path.Contains(pre.Key))
                    {
                        path.Add(pre.Key);
                    }
                    singlePath.Add(pre.Key);
                }
                singlePath.Reverse();
                crossPath.AddRange(singlePath);
            }
            crossPath.Add(keyPath[keyPath.Count - 1]);
            crossPath.Reverse();
            Path=new GraphPath<T>();
            Path.PassedVertexes=path;
            Path.KeyVertexes=Selected;
            Path.CrossVertexes = crossPath;
        }
    }
}
