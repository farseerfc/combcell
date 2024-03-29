// <copyright file="GraphTTest.cs" company="">Copyright ©  2009</copyright>

using System;
using CombCell.DSAlgo;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CombCell.DSAlgo
{
    [TestClass]
    [PexClass(typeof(Graph<>))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class GraphTTest
    {
[PexGenericArguments(typeof(int))]
[PexMethod]
public void VertexesGet<T>([PexAssumeUnderTest]Graph<T> target)
{
    // TODO: add assertions to method GraphTTest.VertexesGet(Graph`1<!!0>)
    List<Vertex<T>> result = target.Vertexes;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void VertexMapGet<T>([PexAssumeUnderTest]Graph<T> target)
{
    // TODO: add assertions to method GraphTTest.VertexMapGet(Graph`1<!!0>)
    Dictionary<T, Vertex<T>> result = target.VertexMap;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void RemoveVertex<T>([PexAssumeUnderTest]Graph<T> target, Vertex<T> vertex)
{
    // TODO: add assertions to method GraphTTest.RemoveVertex(Graph`1<!!0>, Vertex`1<!!0>)
    target.RemoveVertex(vertex);
}

[PexMethod]
public void RemoveVertexFc01([PexAssumeUnderTest]Graph<int> target)
{
    Vertex<int> v0 = target.CreateVertex(0);
    Vertex<int> v1 = target.CreateVertex(1);
    Vertex<int> v2 = target.CreateVertex(2);
    Edge<int> e0 = target.CreateEdge(v0, v1);
    Edge<int> e1 = target.CreateEdge(v2, v0);

    target.RemoveVertex(v0);
}

[PexGenericArguments(typeof(int))]
[PexMethod]
public void RemoveEdge<T>([PexAssumeUnderTest]Graph<T> target, Edge<T> edge)
{
    // TODO: add assertions to method GraphTTest.RemoveEdge(Graph`1<!!0>, Edge`1<!!0>)
    target.RemoveEdge(edge);
}

[PexMethod]
public void RemoveEdgeFc01([PexAssumeUnderTest]Graph<int> target)
{
    Vertex<int> v0 = target.CreateVertex(0);
    Vertex<int> v1 = target.CreateVertex(1);
    Edge<int> e = target.CreateEdge(v0, v1);
    target.RemoveEdge(e);
}

[PexGenericArguments(typeof(int))]
[PexMethod]
public void InfinitiveGet<T>([PexAssumeUnderTest]Graph<T> target)
{
    // TODO: add assertions to method GraphTTest.InfinitiveGet(Graph`1<!!0>)
    int result = target.Infinitive;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void EdgesGet<T>([PexAssumeUnderTest]Graph<T> target)
{
    // TODO: add assertions to method GraphTTest.EdgesGet(Graph`1<!!0>)
    List<Edge<T>> result = target.Edges;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public Vertex<T> CreateVertex<T>([PexAssumeUnderTest]Graph<T> target, T key)
{
    // TODO: add assertions to method GraphTTest.CreateVertex(Graph`1<!!0>, !!0)
    Vertex<T> result = target.CreateVertex(key);
    return result;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public Edge<T> CreateEdge<T>(
    [PexAssumeUnderTest]Graph<T> target,
    Vertex<T> v1,
    Vertex<T> v2
)
{
    // TODO: add assertions to method GraphTTest.CreateEdge(Graph`1<!!0>, Vertex`1<!!0>, Vertex`1<!!0>)
    Edge<T> result = target.CreateEdge(v1, v2);
    return result;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public Graph<T> Constructor<T>()
{
    // TODO: add assertions to method GraphTTest.Constructor()
    Graph<T> target = new Graph<T>();
    return target;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void ClearAccessed<T>([PexAssumeUnderTest]Graph<T> target)
{
    // TODO: add assertions to method GraphTTest.ClearAccessed(Graph`1<!!0>)
    target.ClearAccessed();
}
    }
}
