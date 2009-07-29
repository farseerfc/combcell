using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombCell.DSAlgo
{
    /// <summary>
    /// A Hamilton path is the shortest path that path through all selected vertexes.
    /// </summary>
    /// <typeparam name="T">The key type of vertex</typeparam>
    public class HamiltonSlow<T> : PathAlgorithm<T>
    {
        public override string Name
        {
            get { return "Hamilton(Slow)"; }
        }

        public override string Discription
        {
            get
            {
                return "Hamilton path is the shortest path" +
                    " that path through all selected vertexes." +
                    " This is a slow but accurate version.";
            }
        }


        private Dictionary<T, Dictionary<T, int>> distanceMap;
        private Dictionary<T, Dictionary<Vertex<T>, Vertex<T>>> preMap;
        private T longestStart;
        private T longestEnd;
        private List<T> keyPath;


        public override bool CanCalc
        {
            get { return Selected.Count > 1; }
        }

        public override void Calc()
        {
            //first calculate distance between every two selected vertexes
            CalcDistance();

            List<int> selectIndex = new List<int>();
            for (int i = 0; i < Selected.Count; ++i)
            {
                selectIndex.Add(i);
            }

            GraphPath<T> shortestPath = null;
            int shortest = Graph.Infinitive;

            while (NextPermutation(selectIndex, 0, selectIndex.Count))
            {
                //NextPermutation(selectIndex, 0, selectIndex.Count);
                keyPath = new List<T>();
                for (int i = 0; i < selectIndex.Count; ++i)
                {
                    keyPath.Add(Selected[selectIndex[i]]);
                }
                generatePath();
                if (Path.Count < shortest)
                {
                    shortest = Path.Count;
                    shortestPath = Path;
                }
            }

            Path = shortestPath;

        }

        /// <summary>
        /// Determine whether the given permutation has next,
        /// translated from C++ STL algorithms.h next_permutation
        /// 1,2,3=>true
        /// 3,1,2=>true
        /// 3,2,1=>false
        /// </summary>
        /// <param name="list">contains the given permutation</param>
        /// <param name="begin">the begin index of the given permutation</param>
        /// <param name="end">the end, i.e. the next of last item index of the given permutation</param>
        /// <returns>whether the given permutation has next</returns>
        public static bool HasNextPermutation(List<int> list, int begin, int end)
        {
            if (list == null) throw new ArgumentNullException("list");
            if (begin < 0 || begin >= list.Count) throw new ArgumentOutOfRangeException("begin");
            if (end < begin || end > list.Count) throw new ArgumentOutOfRangeException("end");
            for (int i = begin; i < end - 1; ++i)
            {
                if (list[i] < list[i + 1]) return true;
                if (list[i] == list[i + 1]) throw new ArgumentException("list");
            }
            return false;
        }

        /// <summary>
        /// Generate the next permutation of the given permutation,
        /// translated from C++ STL algorithms.h next_permutation
        /// The method directly changes the given list, the return value indicates whether has the next.
        /// 1,2,3=>1,3,2(true)
        /// 1,3,2=>2,1,3(true)
        /// 2,1,3=>2,3,1(true)
        /// 2,3,1=>3,1,2(true)
        /// 3,1,2=>3,2,1(true)
        /// 3,2,1=>1,2,3(false)
        /// </summary>
        /// <param name="list">contains the given permutation</param>
        /// <param name="begin">the begin index of the given permutation</param>
        /// <param name="end">the end, i.e. the next of last item index of the given permutation</param>
        /// <returns>whether the given permutation has next</returns>
        public static bool NextPermutation(List<int> list, int begin, int end)
        {
            if (list == null) throw new ArgumentNullException("list");
            if (begin < 0 || begin >= list.Count) throw new ArgumentOutOfRangeException("begin");
            if (end < begin || end > list.Count) throw new ArgumentOutOfRangeException("end");
            if (begin == end || begin + 1 == end) return false;
            

            int a = end - 2;
            while (a >= begin && list[a] >= list[a + 1])
            {
                a--;
            }
            if (a == begin - 1)
            {
                return false;
            }
            int b = end-1;
            while (list[b] <= list[a])
            {
                b--;
            }
            int t = list[a];
            list[a] = list[b];
            list[b] = t;
            for (int i = a + 1, j = end-1; i < j; i++, j--)
            {
                t = list[i];
                list[i] = list[j];
                list[j] = t;
            }
            return true;

        }


        public void CalcDistance()
        {
            if (Graph == null) throw new InvalidOperationException();
            if (Selected == null) throw new InvalidOperationException();
            if (Selected.Count <= 1) throw new InvalidOperationException();

            distanceMap = new Dictionary<T, Dictionary<T, int>>();
            preMap = new Dictionary<T, Dictionary<Vertex<T>, Vertex<T>>>();
            int longest = 0;
            for (int i = 0; i < Selected.Count; ++i)
            {
                //call SingleSource to calculate 
                //distance to every other vertex from Selected[0]
                SingleSource<T> singleSource = new SingleSource<T>();
                singleSource.Graph = Graph;
                singleSource.Selected = Selected;
                singleSource.Calc();

                //we only need the distance to Selected vertexes
                //so not copy all singleSource.Distance
                //we just copy what is necessary
                Dictionary<T, int> distance = new Dictionary<T, int>();
                for (int v = 1; v < Selected.Count; ++v)//jumped first
                {
                    int d = singleSource.Distance[Graph.VertexMap[Selected[v]]];
                    distance[Selected[v]] = d;
                    if (d > longest)
                    {
                        longestStart = Selected[0];
                        longestEnd = Selected[v];
                        longest = d;
                    }

                }
                distanceMap.Add(Selected[0], distance);
                preMap.Add(Selected[0], singleSource.pre);

                //Rotate first vertex to last
                //and calculate the distance start from the next vertex
                Selected.Add(Selected[0]);
                Selected.RemoveAt(0);
            }
        }

        private void generatePath()
        {
            List<T> path = new List<T>();
            List<T> crossPath = new List<T>();
            for (int i = 1; i < keyPath.Count; ++i)
            {
                T start = keyPath[i - 1];
                T end = keyPath[i];

                Vertex<T> pre = Graph.VertexMap[end];

                //crossPath.Add(start);
                List<T> singlePath = new List<T>();
                while (pre!=null && pre != Graph.VertexMap[start])
                {
                    pre = preMap[start][pre];
                    if (pre == null) continue;
                    if (!keyPath.Contains(pre.Key) && !path.Contains(pre.Key))
                    {
                        path.Add(pre.Key);
                    }
                    singlePath.Add(pre.Key);
                }
                singlePath.Reverse();
                crossPath.AddRange(singlePath);
            }
            crossPath.Add(keyPath[keyPath.Count - 1]);
            crossPath.Reverse();
            Path = new GraphPath<T>();
            Path.PassedVertexes = path;
            Path.KeyVertexes = Selected;
            Path.CrossVertexes = crossPath;
        }
    }
}
