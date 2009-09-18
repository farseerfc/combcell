using System;

namespace CombCell.DSAlgo
{
    public class Edge<T>
    {
        private Vertex<T> from;
        private Vertex<T> to;

        public Vertex<T> From
        {
            get { return from; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                from=value;
            }
        }

        public Vertex<T> To
        {
            get { return to; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                to = value;
            }
        }

        //public string Key;
        public override string ToString()
        {
            return String.Format("{0}-{1}", From, To);
        }

        public Edge(Vertex<T> from,Vertex<T> to)
        {
            if (from == null) throw new ArgumentNullException("from");
            if (to == null) throw new ArgumentNullException("to");
            this.from = from;
            this.to = to;
        }

        public Edge()
        {

        }
    }
}
