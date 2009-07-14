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
    public class End2End<T> : PathAlgorithm<T>
    {
        private T startPos;
        private Vertex<T> start;

        private Dictionary<Vertex<T>, int> distance;
        private Dictionary<Vertex<T>, Vertex<T>> pre;
        private List<Vertex<T>> determined;
        //private List<Vertex<T>> undetermined;
        private PriorityQueue<Vertex<T>, int> undetermined;

        public override bool CanCalc
        {
            get { return Selected.Count >= 1; }
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
            startPos = Selected[0];
            start = Graph.VertexMap[startPos];

            distance = new Dictionary<Vertex<T>, int>();
            pre = new Dictionary<Vertex<T>, Vertex<T>>();
            determined = new List<Vertex<T>>();

            undetermined = new PriorityQueue<Vertex<T>, int>();

            foreach (Vertex<T> v in Graph.Vertexes)
            {
                distance[v] = Graph.Infinitive;
                pre[v] = null;
                undetermined[v] = distance[v];
            }

            distance[start] = 0;
            undetermined[start] = 0;
            //undetermined = new List<Vertex<T>>(Graph.Vertexes);
            
        }

        private void Relax(Vertex<T> u, Vertex<T> v)
        {
            if (distance[v] > distance[u] + 1)
            {
                distance[v] = distance[u] + 1;
                undetermined[v] = distance[u] + 1;
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
            Path = new List<T>();
            for (int i=1;i<Selected.Count;++i)
            {
                Vertex<T> goal = Graph.VertexMap[Selected[i]];
                if (distance[goal] == Graph.Infinitive) continue;
                Vertex<T> preVertex = pre[goal];
                while(preVertex!=start&&preVertex!=null)
                {
                    T keyPre = preVertex.Key;
                    if(Selected.IndexOf(keyPre)==-1)
                    {
                        Path.Add(keyPre);
                    }
                    preVertex = pre[preVertex];
                }
            }
        }
    }
}
