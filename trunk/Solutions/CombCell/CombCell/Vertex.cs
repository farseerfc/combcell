using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombCell.DSAlgo
{
    public class Vertex <T>
    {
        public bool Accessed;
        public T Key;
        public List<Edge<T>> Edges;

        public Vertex()
        {
            Edges = new List<Edge<T>>();
        }

        public override string ToString()
        {
            return Key.ToString();
        }

        public bool IsConnect(Vertex<T> other)
        {
            if (other == this) return false;
            foreach (Edge<T> edge in Edges)
            {
                if (edge.To == other) return true;
                if (edge.From == other) return true;
            }
            return false;
        }
    }
}
