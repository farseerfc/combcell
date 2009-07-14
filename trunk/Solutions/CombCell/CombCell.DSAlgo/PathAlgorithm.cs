using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombCell.DSAlgo
{
    public abstract class PathAlgorithm<T>
    {
        public abstract bool CanCalc
        {
            get;
        }

        private Graph<T> graph;
        public virtual Graph<T> Graph
        {
            get { return graph; }
            set { graph = value; }
        }

        private List<T> selected;
        public virtual List<T> Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        private GraphPath<T> path;
        public virtual GraphPath<T> Path
        {
            get { return path; }
            set { path = value; }
        }

        public abstract void Calc();
    }
}
