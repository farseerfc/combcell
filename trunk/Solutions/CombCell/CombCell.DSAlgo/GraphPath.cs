using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombCell.DSAlgo
{
    /// <summary>
    /// A GraphPath represent a path on a graph
    /// </summary>
    /// <typeparam name="T">The key type of vertexes on graph</typeparam>
    public class GraphPath<T>
    {
        private List<T> keyVertexes;
        public List<T> KeyVertexes
        {
            get { return keyVertexes; }
            set { keyVertexes = value; }
        }

        private List<T> passedVertexes;
        public List<T> PassedVertexes
        {
            get { return passedVertexes; }
            set { passedVertexes = value; }
        }

        public int KeyCount
        {
            get { return keyVertexes.Count; }
        }

        public int PassedCount
        {
            get { return passedVertexes.Count; }
        }

        public int Count
        {
            get { return KeyCount + PassedCount; }
        }

        public GraphPath()
        {
            keyVertexes = new List<T>();
            passedVertexes = new List<T>();
        }

    }
}
