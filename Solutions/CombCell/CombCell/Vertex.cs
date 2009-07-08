using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombCell.DSAlgo
{
    public class Vertex
    {
        public string Key;
        public List<Edge> Edges;

        public Vertex()
        {
            Edges = new List<Edge>();
        }
    }
}
