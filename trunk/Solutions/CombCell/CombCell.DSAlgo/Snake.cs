using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombCell.DSAlgo
{
    /// <summary>
    /// The Snake Algorithm is based on the SingleSource algorithm.
    /// It links every selected cells in order.
    /// </summary>
    /// <typeparam name="T">Key type of the vertex on a graph</typeparam>
    public class Snake<T>: PathAlgorithm<T>
    {
        public override string Name
        {
            get { return "Snake"; }
        }

        public override string Discription
        {
            get 
            {
                return "The Snake Algorithm is based "+
                "on the SingleSource algorithm. "+
                "It links every selected cells in order. "; 
            }
        }

        public override bool CanCalc
        {
            get { return Selected.Count > 1; }
        }

        public override void Calc()
        {
            for(int i=1;i<Selected.Count;++i)
            {
                T start = Selected[i - 1];
                T end = Selected[i];
                SingleSource<T> singleSource = new SingleSource<T>();
                singleSource.Graph = Graph;
                singleSource.Selected.Add(start);
                singleSource.Selected.Add(end);
                singleSource.Calc();
                Path.KeyVertexes.Add(start);
                Path.PassedVertexes.AddRange(singleSource.Path.PassedVertexes);
            }
            Path.KeyVertexes.Add(Selected[Selected.Count - 1]);
        }
    }
}
