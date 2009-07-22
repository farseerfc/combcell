// <copyright file="HamiltonSlowTTest.cs" company="">Copyright ©  2009</copyright>

using System;
using CombCell.DSAlgo;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CombCell.DSAlgo
{
    [TestClass]
    [PexClass(typeof(HamiltonSlow<>))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class HamiltonSlowTTest
    {
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public bool HasNextPermutation<T>(
            List<int> list,
            int begin,
            int end
        )
        {
            // TODO: add assertions to method HamiltonSlowTTest.HasNextPermutation(List`1<Int32>, Int32, Int32)
            bool result = HamiltonSlow<T>.HasNextPermutation(list, begin, end);
            return result;
        }

        [PexAllowedException(typeof(ArgumentNullException))]
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public bool HasNextPermutation01<T>(
            List<int> list
        )
        {
            if (list == null) throw new ArgumentNullException("list");
            bool result = HamiltonSlow<T>.HasNextPermutation(list, 0, list.Count);
            return result;
        }

        [PexAllowedException(typeof(ArgumentNullException))]
        [PexMethod]
        public bool HasNextPermutation01(int length)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < length; ++i)
            {
                list.Add(i);
            }
            bool result = HamiltonSlow<int>.HasNextPermutation(list, 0, list.Count);
            Assert.AreEqual(result, list.Count > 1 ? true : false);
            list.Reverse();
            result = HamiltonSlow<int>.HasNextPermutation(list, 0, list.Count);
            Assert.AreEqual(result, false);
            return result;
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public void NextPermutation<T>(
            List<int> list,
            int begin,
            int end
        )
        {
            // TODO: add assertions to method HamiltonSlowTTest.NextPermutation(List`1<Int32>, Int32, Int32)
            HamiltonSlow<T>.NextPermutation(list, begin, end);
        }

        [PexMethod]
        public void NextPermutationFc(
        )
        {
            List<int> list = new List<int>(new int[] { 0, 1, 2, 3, 4 });
            HamiltonSlow<int>.NextPermutation(list, 0, 5);
            List<int> obj = new List<int>(new int[] { 0, 1, 2, 4, 3 });
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(list[i], obj[i]);
            }

            HamiltonSlow<int>.NextPermutation(list, 0, 5);
            obj = new List<int>(new int[] { 0, 1, 3, 2, 4 });
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(list[i], obj[i]);
            }

            HamiltonSlow<int>.NextPermutation(list, 0, 5);
            obj = new List<int>(new int[] { 0, 1, 3, 4, 2 });
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(list[i], obj[i]);
            }

            HamiltonSlow<int>.NextPermutation(list, 0, 5);
            obj = new List<int>(new int[] { 0, 1, 4, 2, 3 });
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(list[i], obj[i]);
            }

            HamiltonSlow<int>.NextPermutation(list, 0, 5);
            obj = new List<int>(new int[] { 0, 1, 4, 3, 2 });
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(list[i], obj[i]);
            }

            HamiltonSlow<int>.NextPermutation(list, 0, 5);
            obj = new List<int>(new int[] { 0, 2, 1, 3, 4 });
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(list[i], obj[i]);
            }

            HamiltonSlow<int>.NextPermutation(list, 0, 5);
            obj = new List<int>(new int[] { 0, 2, 1, 4, 3 });
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(list[i], obj[i]);
            }

            HamiltonSlow<int>.NextPermutation(list, 0, 5);
            obj = new List<int>(new int[] { 0, 2, 3, 1, 4 });
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(list[i], obj[i]);
            }

            HamiltonSlow<int>.NextPermutation(list, 0, 5);
            obj = new List<int>(new int[] { 0, 2, 3, 4, 1 });
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(list[i], obj[i]);
            }

            HamiltonSlow<int>.NextPermutation(list, 0, 5);
            obj = new List<int>(new int[] { 0, 2, 4, 1, 3 });
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(list[i], obj[i]);
            }

            HamiltonSlow<int>.NextPermutation(list, 0, 5);
            obj = new List<int>(new int[] { 0, 2, 4, 3, 1 });
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(list[i], obj[i]);
            }
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public HamiltonSlow<T> Constructor<T>()
        {
            // TODO: add assertions to method HamiltonSlowTTest.Constructor()
            HamiltonSlow<T> target = new HamiltonSlow<T>();
            return target;
        }

        [PexMethod(MaxBranches = 10000,Timeout=2)]        
        public void CalcFc01(int count)
        {
            HamiltonSlow<int> target = new HamiltonSlow<int>();
            Graph<int> graph = new Graph<int>();
            Vertex<int> first = graph.CreateVertex(0);
            Vertex<int> last=first;
            target.Selected = new System.Collections.Generic.List<int>();
            target.Selected.Add(0);

            if (count < 2) count = 2;

            for (int i = 1; i <= count; ++i)
            {
                Vertex<int> v = graph.CreateVertex(i);
                graph.CreateEdge(last, v);
                if(i%2==0)target.Selected.Add(i);
                last = v;
            }

            target.Graph = graph;            
            target.Path = new GraphPath<int>();
            target.Calc();
        }
    }
}
