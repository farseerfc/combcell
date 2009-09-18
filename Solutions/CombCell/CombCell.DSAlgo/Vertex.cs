using System;
using System.Collections.Generic;

namespace CombCell.DSAlgo
{
    public class Vertex <T>
    {
        private bool accessed;
        private T key;
        private List<Edge<T>> edges;


        public bool Accessed
        {
            set { accessed = value; }
            get { return accessed; }
        }

        public T Key
        {
            set 
            {
                if (value == null) throw new ArgumentNullException("value");
                key = value; 
            }
            get { return key; }
        }

        public List<Edge<T>> Edges
        {
            get { return edges; }
        }

        public Vertex()
        {
            edges = new List<Edge<T>>();
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
