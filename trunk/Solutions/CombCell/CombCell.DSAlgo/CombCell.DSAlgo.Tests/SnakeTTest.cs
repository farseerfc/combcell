// <copyright file="SnakeTTest.cs" company="">Copyright ©  2009</copyright>

using System;
using CombCell.DSAlgo;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CombCell.DSAlgo
{
    [TestClass]
    [PexClass(typeof(Snake<>))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class SnakeTTest
    {
[PexGenericArguments(typeof(int))]
[PexMethod]
public void NameGet<T>([PexAssumeUnderTest]Snake<T> target)
{
    // TODO: add assertions to method SnakeTTest.NameGet(Snake`1<!!0>)
    string result = target.Name;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void DiscriptionGet<T>([PexAssumeUnderTest]Snake<T> target)
{
    // TODO: add assertions to method SnakeTTest.DiscriptionGet(Snake`1<!!0>)
    string result = target.Discription;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public Snake<T> Constructor<T>()
{
    // TODO: add assertions to method SnakeTTest.Constructor()
    Snake<T> target = new Snake<T>();
    return target;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void CanCalcGet<T>([PexAssumeUnderTest]Snake<T> target)
{
    // TODO: add assertions to method SnakeTTest.CanCalcGet(Snake`1<!!0>)
    bool result = target.CanCalc;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void Calc<T>([PexAssumeUnderTest]Snake<T> target)
{
    // TODO: add assertions to method SnakeTTest.Calc(Snake`1<!!0>)
    target.Calc();
}
    }
}
