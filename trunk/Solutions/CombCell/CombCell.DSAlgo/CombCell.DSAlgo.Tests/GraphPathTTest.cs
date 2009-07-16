// <copyright file="GraphPathTTest.cs" company="">Copyright ©  2009</copyright>

using System;
using CombCell.DSAlgo;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CombCell.DSAlgo
{
    [TestClass]
    [PexClass(typeof(GraphPath<>))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class GraphPathTTest
    {
[PexGenericArguments(typeof(int))]
[PexMethod]
public void PassedVertexesGetSet<T>([PexAssumeUnderTest]GraphPath<T> target, List<T> value)
{
    // TODO: add assertions to method GraphPathTTest.PassedVertexesGetSet(GraphPath`1<!!0>)
    target.PassedVertexes = value;
    List<T> result = target.PassedVertexes;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void PassedCountGet<T>([PexAssumeUnderTest]GraphPath<T> target)
{
    // TODO: add assertions to method GraphPathTTest.PassedCountGet(GraphPath`1<!!0>)
    int result = target.PassedCount;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void KeyVertexesGetSet<T>([PexAssumeUnderTest]GraphPath<T> target, List<T> value)
{
    // TODO: add assertions to method GraphPathTTest.KeyVertexesGetSet(GraphPath`1<!!0>)
    target.KeyVertexes = value;
    List<T> result = target.KeyVertexes;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void KeyCountGet<T>([PexAssumeUnderTest]GraphPath<T> target)
{
    // TODO: add assertions to method GraphPathTTest.KeyCountGet(GraphPath`1<!!0>)
    int result = target.KeyCount;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void CrossVertexesGetSet<T>([PexAssumeUnderTest]GraphPath<T> target, List<T> value)
{
    // TODO: add assertions to method GraphPathTTest.CrossVertexesGetSet(GraphPath`1<!!0>)
    target.CrossVertexes = value;
    List<T> result = target.CrossVertexes;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void CountGet<T>([PexAssumeUnderTest]GraphPath<T> target)
{
    // TODO: add assertions to method GraphPathTTest.CountGet(GraphPath`1<!!0>)
    int result = target.Count;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public GraphPath<T> Constructor<T>()
{
    // TODO: add assertions to method GraphPathTTest.Constructor()
    GraphPath<T> target = new GraphPath<T>();
    return target;
}
    }
}
