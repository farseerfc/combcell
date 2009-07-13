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
    }
}
