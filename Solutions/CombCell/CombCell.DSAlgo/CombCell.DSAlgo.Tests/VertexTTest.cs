// <copyright file="VertexTTest.cs" company="">Copyright ©  2009</copyright>

using System;
using CombCell.DSAlgo;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework.Exceptions;
using System.Collections.Generic;

namespace CombCell.DSAlgo
{
    [TestClass]
    [PexClass(typeof(Vertex<>))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class VertexTTest
    {
[PexGenericArguments(typeof(int))]
        [PexMethod, PexAllowedException(typeof(PexAssertFailedException))]
public void AccessedGetSet<T>([PexAssumeUnderTest]Vertex<T> target, bool value)
{
    // TODO: add assertions to method VertexTTest.AccessedGetSet(Vertex`1<!!0>)
    target.Accessed = value;
    bool result = target.Accessed;
    PexAssert.AreEqual<bool>(value, result);
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public string ToString<T>([PexAssumeUnderTest]Vertex<T> target)
{
    // TODO: add assertions to method VertexTTest.ToString(Vertex`1<!!0>)
    string result = target.ToString();
    return result;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void KeyGetSet<T>([PexAssumeUnderTest]Vertex<T> target, T value)
{
    // TODO: add assertions to method VertexTTest.KeyGetSet(Vertex`1<!!0>)
    target.Key = value;
    T result = target.Key;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public bool IsConnect<T>([PexAssumeUnderTest]Vertex<T> target, Vertex<T> other)
{
    // TODO: add assertions to method VertexTTest.IsConnect(Vertex`1<!!0>, Vertex`1<!!0>)
    bool result = target.IsConnect(other);
    return result;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void EdgesGet<T>([PexAssumeUnderTest]Vertex<T> target)
{
    // TODO: add assertions to method VertexTTest.EdgesGet(Vertex`1<!!0>)
    List<Edge<T>> result = target.Edges;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public Vertex<T> Constructor<T>()
{
    // TODO: add assertions to method VertexTTest.Constructor()
    Vertex<T> target = new Vertex<T>();
    return target;
}
    }
}
