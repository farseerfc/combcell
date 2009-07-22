// <copyright file="HamiltonTTest.cs" company="">Copyright ©  2009</copyright>

using System;
using CombCell.DSAlgo;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CombCell.DSAlgo
{
    [TestClass]
    [PexClass(typeof(Hamilton<>))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class HamiltonTTest
    {
[PexGenericArguments(typeof(int))]
[PexMethod]
public Hamilton<T> Constructor<T>()
{
    // TODO: add assertions to method HamiltonTTest.Constructor()
    Hamilton<T> target = new Hamilton<T>();
    return target;
}


[PexMethod(MaxBranches = 10000, Timeout = 2)]   
public void CalcFc01(int count)
{
    Hamilton<int> target = new Hamilton<int>();
    Graph<int> graph = new Graph<int>();
    Vertex<int> first = graph.CreateVertex(0);
    Vertex<int> last = first;
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
