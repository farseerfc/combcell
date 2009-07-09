using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombCell.DSAlgo
{
    public class Edge<T>
    {
        public Vertex<T> From;
        public Vertex<T> To;
        //public string Key;
        public override string ToString()
        {
            return String.Format("{0}-{1}", From, To);
        }
    }
}
