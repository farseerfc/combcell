// <copyright file="End2EndTTest.cs" company="">Copyright ©  2009</copyright>

using System;
using System.Collections.Generic;
using CombCell.DSAlgo;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CombCell.DSAlgo
{
    [TestClass]
    [PexClass(typeof(End2End<>))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class End2EndTTest
    {
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public void CanCalcGet<T>([PexAssumeUnderTest]End2End<T> target)
        {
            // TODO: add assertions to method End2EndTTest.CanCalcGet(End2End`1<!!0>)
            bool result = target.CanCalc;
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public void Calc<T>([PexAssumeUnderTest]End2End<T> target)
        {
            // TODO: add assertions to method End2EndTTest.Calc(End2End`1<!!0>)
            target.Calc();
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public void CanCalc1<T>([PexAssumeUnderTest]End2End<T> target)
        {
            bool result = target.CanCalc;
            Assert.AreEqual(result, target.Selected.Count > 1);
        }


        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public End2End<T> Constructor<T>()
        {
            // TODO: add assertions to method End2EndTTest.Constructor()
            End2End<T> target = new End2End<T>();
            return target;
        }


        [PexGenericArguments(typeof(int))]
        [PexMethod(MaxBranches = 2000)]
        public void Calc001(int numbersOfVertex)
        {
            End2End<int> target = new End2End<int>();
            List<int> selected = new List<int>();
            target.Selected = selected;
            Assert.AreEqual(target.CanCalc, false);

            Graph<int> graph = new Graph<int>();
            Vertex<int> vs = graph.CreateVertex(0);
            Vertex<int> ve = graph.CreateVertex(-1);

            Vertex<int> last = vs;
            for (int i = 1; i <= numbersOfVertex;++i )
            {
                Vertex<int> vm = graph.CreateVertex(i);
                graph.CreateEdge(last, vm);
                last = vm;
            }

            graph.CreateEdge(last, ve);

            selected = new List<int>();
            selected.Add(0); selected.Add(-1);

            target.Graph = graph;
            target.Selected = selected;
            Assert.AreEqual(target.CanCalc, true);

            target.Calc();

            List<int> path=target.Path;
            Assert.AreEqual(target.Path.Count, numbersOfVertex>0?numbersOfVertex:0);
            for (int i = 0; i < numbersOfVertex; ++i)
            {
                Assert.AreEqual(target.Path[i], numbersOfVertex-i);
            }
        }
    }
}
