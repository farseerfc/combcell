using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombCell.DSAlgo
{
    
    public class Graph<T>
    {
        private List<Vertex<T>> vertexes;
        private List<Edge<T>> edges;
        private Dictionary<T, Vertex<T>> vertexMap;


        public List<Vertex<T>> Vertexes
        {
            get { return vertexes; }
        }

        public List<Edge<T>> Edges
        {
            get { return edges; }
        }

        public Dictionary<T, Vertex<T>> VertexMap
        {
            get { return vertexMap; }
        }
        //public Dictionary<T, Edge> EdgeMap;

        /// <summary>
        /// Returns a number larger than count of vertexes and edges,
        /// which can be consider as positive infinitive in algorithms
        /// </summary>
        public int Infinitive
        {
            get 
            {
                int v = Vertexes.Count;
                int e = Edges.Count;
                return v + e + 1;
            }
        }

        public Graph()
        {
            vertexes = new List<Vertex<T>>();
            edges = new List<Edge<T>>();
            vertexMap = new Dictionary<T, Vertex<T>>();
        }

        public Vertex<T> CreateVertex(T key)
        {
            Vertex<T> vertex = new Vertex<T>();
            vertex.Key = key;
            Vertexes.Add(vertex);
            VertexMap.Add(key, vertex);
            return vertex;
        }

        public Edge<T> CreateEdge(Vertex<T> v1, Vertex<T> v2)
        {
            if(v1==v2||v1==null||v2==null)
            {
                if (v1 == null) throw new ArgumentNullException("v1");
                if (v2 == null) throw new ArgumentNullException("v2");
                throw new ArgumentException("Vertex should not be the same");
            }

            Edge<T> edge = new Edge<T>(v1,v2);
            v1.Edges.Add(edge);
            v2.Edges.Add(edge);
            Edges.Add(edge);
            return edge;
        }

        public void RemoveEdge(Edge<T> edge)
        {
            if (edge == null) throw new ArgumentNullException("edge");
            edge.From.Edges.Remove(edge);
            edge.To.Edges.Remove(edge);
            Edges.Remove(edge);
        }

        public void RemoveVertex(Vertex<T> vertex)
        {
            if (vertex == null) throw new ArgumentNullException("vertex");
            while(vertex.Edges.Count>0)
            {
                RemoveEdge(vertex.Edges[0]);
            }
            Vertexes.Remove(vertex);
            VertexMap.Remove(vertex.Key);
        }

        public void ClearAccessed()
        {
            foreach (Vertex<T> v in Vertexes)
            {
                v.Accessed = false;
            }
        }
    }
}
