// <copyright file="EdgeTTest.cs" company="">Copyright ©  2009</copyright>

using System;
using CombCell.DSAlgo;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CombCell.DSAlgo
{
    [TestClass]
    [PexClass(typeof(Edge<>))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class EdgeTTest
    {
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public string ToString<T>([PexAssumeUnderTest]Edge<T> target)
        {
            // TODO: add assertions to method EdgeTTest.ToString(Edge`1<!!0>)
            string result = target.ToString();
            return result;
        }
[PexGenericArguments(typeof(int))]
[PexMethod]
public Edge<T> Constructor<T>(Vertex<T> from, Vertex<T> to)
{
    // TODO: add assertions to method EdgeTTest.Constructor(Vertex`1<!!0>, Vertex`1<!!0>)
    Edge<T> target = new Edge<T>(from, to);
    return target;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public Edge<T> Constructor01<T>()
{
    // TODO: add assertions to method EdgeTTest.Constructor01()
    Edge<T> target = new Edge<T>();
    return target;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void FromGetSet<T>([PexAssumeUnderTest]Edge<T> target, Vertex<T> value)
{
    // TODO: add assertions to method EdgeTTest.FromGetSet(Edge`1<!!0>)
    target.From = value;
    Vertex<T> result = target.From;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void ToGetSet<T>([PexAssumeUnderTest]Edge<T> target, Vertex<T> value)
{
    // TODO: add assertions to method EdgeTTest.ToGetSet(Edge`1<!!0>)
    target.To = value;
    Vertex<T> result = target.To;
}
    }
}
