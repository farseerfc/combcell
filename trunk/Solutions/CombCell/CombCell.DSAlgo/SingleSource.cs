using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombCell.DSAlgo
{
    /// <summary>
    /// Single source shortest path algorithm,
    /// which is a modified Dijkstra applied on a undirected graph,
    /// described in Introduction of Algorithm, 2 edition, Chapter 24.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the key on a vertex. 
    ///     Which is <![CDATA[ Pair<int> ]]> in CombCell. 
    /// </typeparam>
    public class SingleSource<T> : PathAlgorithm<T>
    {
        public override string Name
        {
            get { return "Single Source"; }
        }

        public override string Discription
        {
            get { return "Single source shortest path algorithm, "+
                "which is a modified Dijkstra applied on a undirected graph,"+
                "described in Introduction of Algorithm, 2 edition, Chapter 24."; }
        }
        private T startPos;
        private Vertex<T> start;

        internal Dictionary<Vertex<T>, int> Distance;
        internal Dictionary<Vertex<T>, Vertex<T>> pre;
        private List<Vertex<T>> determined;
        private PriorityQueue<Vertex<T>, int> undetermined;

        public override bool CanCalc
        {
            get { return Selected.Count > 1; }
        }

        public override void Calc()
        {
            if (Selected.Count < 1) return;
            Initialize();
            Dijkstra();
            GeneratePath();
        }

        private void Initialize()
        {
            if (!Graph.VertexMap.ContainsKey(Selected[0])) throw new InvalidOperationException();
            startPos = Selected[0];
            start = Graph.VertexMap[startPos];

            Distance = new Dictionary<Vertex<T>, int>();
            pre = new Dictionary<Vertex<T>, Vertex<T>>();
            determined = new List<Vertex<T>>();

            undetermined = new PriorityQueue<Vertex<T>, int>();

            foreach (Vertex<T> v in Graph.Vertexes)
            {
                Distance[v] = Graph.Infinitive;
                pre[v] = null;
                undetermined[v] = Distance[v];
            }

            Distance[start] = 0;
            undetermined[start] = 0;
            //undetermined = new List<Vertex<T>>(Graph.Vertexes);
            
        }

        private void Relax(Vertex<T> u, Vertex<T> v)
        {
            if (Distance[v] > Distance[u] + 1)
            {
                Distance[v] = Distance[u] + 1;
                undetermined[v] = Distance[u] + 1;
                pre[v] = u;
            }
        }

        private void Dijkstra()
        {
            while(undetermined.Count>0)
            {
                // Extract-Min from undetermined
//                 int minIndex = 0;
//                 int minDistance = Graph.Infinitive;
//                 for(int i=0;i<undetermined.Count;++i)
//                 {
//                     if(distance[undetermined[i]]<minDistance)
//                     {
//                         minIndex = i;
//                         minDistance = distance[undetermined[i]];
//                     }
//                 }
                KeyValuePair<Vertex<T>,int> pair= undetermined.Pop();
                Vertex<T> minVertex = pair.Key;

                // add into determined
                determined.Add(minVertex);

                // relax every edge
                foreach (Edge<T> edge in minVertex.Edges)
                {
                    Vertex<T> other = edge.From;
                    if (other==minVertex)
                    {
                        other = edge.To;
                    }

                    Relax(minVertex, other);
                }
            }
        }

        private void GeneratePath()
        {
            List<T> path = new List<T>();
            List<T> crossPath = new List<T>();
            for (int i=1;i<Selected.Count;++i)
            {
                List<T> singlePath = new List<T>();
                Vertex<T> goal = Graph.VertexMap[Selected[i]];
                if (Distance[goal] == Graph.Infinitive) continue;
                Vertex<T> preVertex = pre[goal];
                while(preVertex!=start&&preVertex!=null)
                {
                    T keyPre = preVertex.Key;
                    if(Selected.IndexOf(keyPre)==-1)
                    {
                        singlePath.Add(keyPre);
                    }
                    preVertex = pre[preVertex];
                }
                singlePath.Reverse();
                crossPath.Add(start.Key);
                foreach (T key in singlePath)
                {
                    if (!path.Contains(key)) path.Add(key);
                    crossPath.Add(key);
                }
                crossPath.Add(Selected[i]);
            }
            Path.KeyVertexes = Selected;
            Path.PassedVertexes = path;
            Path.CrossVertexes = crossPath;
        }
    }
}
