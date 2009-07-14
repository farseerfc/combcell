using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombCell.DSAlgo
{
    public class Snake<T>: PathAlgorithm<T>
    {

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
