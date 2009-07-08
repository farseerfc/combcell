using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombCell.DSAlgo
{
    
    public class Graph
    {
        public List<Vertex> Vertexes;
        public List<Edge> Edges;
        public Dictionary<string, Vertex> VertexMap;
        public Dictionary<string, Edge> EdgeMap;

        public Graph()
        {
            Vertexes = new List<Vertex>();
            Edges = new List<Edge>();
            VertexMap = new Dictionary<string, Vertex>();
            EdgeMap = new Dictionary<string, Edge>();
        }

        public Vertex CreateVertex(string key)
        {
            Vertex vertex = new Vertex();
            vertex.Key = key;
            Vertexes.Add(vertex);
            VertexMap.Add(key, vertex);
            return vertex;
        }

        public Edge CreateEdge(string key,Vertex v1,Vertex v2)
        {
            Edge edge = new Edge();
            edge.Key = key;
            edge.From = v1;
            edge.To = v2;
            v1.Edges.Add(edge);
            v2.Edges.Add(edge);
            Edges.Add(edge);
            EdgeMap.Add(key, edge);
            return edge;
        }

        public void RemoveEdge(Edge edge)
        {
            edge.From.Edges.Remove(edge);
            edge.To.Edges.Remove(edge);
            EdgeMap.Remove(edge.Key);
            Edges.Remove(edge);
        }

        public void RemoveVertex(Vertex vertex)
        {
            foreach (Edge edge in vertex.Edges)
            {
                RemoveEdge(edge);
            }
            Vertexes.Remove(vertex);
            VertexMap.Remove(vertex.Key);
        }
    }
}
