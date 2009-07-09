using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombCell.DSAlgo
{
    
    public class Graph<T>
    {
        public List<Vertex<T>> Vertexes;
        public List<Edge<T>> Edges;
        public Dictionary<T, Vertex<T>> VertexMap;
        //public Dictionary<T, Edge> EdgeMap;

        public Graph()
        {
            Vertexes = new List<Vertex<T>>();
            Edges = new List<Edge<T>>();
            VertexMap = new Dictionary<T, Vertex<T>>();
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
            Edge<T> edge = new Edge<T>();
            edge.From = v1;
            edge.To = v2;
            v1.Edges.Add(edge);
            v2.Edges.Add(edge);
            Edges.Add(edge);
            return edge;
        }

        public void RemoveEdge(Edge<T> edge)
        {
            edge.From.Edges.Remove(edge);
            edge.To.Edges.Remove(edge);
            Edges.Remove(edge);
        }

        public void RemoveVertex(Vertex<T> vertex)
        {
            foreach (Edge<T> edge in vertex.Edges)
            {
                RemoveEdge(edge);
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
