// <copyright file="SingleSourceTTest.cs" company="">Copyright ©  2009</copyright>

using System;
using CombCell.DSAlgo;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CombCell.DSAlgo
{
    [TestClass]
    [PexClass(typeof(SingleSource<>))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class SingleSourceTTest
    {
[PexGenericArguments(typeof(int))]
[PexMethod]
public void NameGet<T>([PexAssumeUnderTest]SingleSource<T> target)
{
    // TODO: add assertions to method SingleSourceTTest.NameGet(SingleSource`1<!!0>)
    string result = target.Name;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void DiscriptionGet<T>([PexAssumeUnderTest]SingleSource<T> target)
{
    // TODO: add assertions to method SingleSourceTTest.DiscriptionGet(SingleSource`1<!!0>)
    string result = target.Discription;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public SingleSource<T> Constructor<T>()
{
    // TODO: add assertions to method SingleSourceTTest.Constructor()
    SingleSource<T> target = new SingleSource<T>();
    return target;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void CanCalcGet<T>([PexAssumeUnderTest]SingleSource<T> target)
{
    // TODO: add assertions to method SingleSourceTTest.CanCalcGet(SingleSource`1<!!0>)
    bool result = target.CanCalc;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public void Calc<T>([PexAssumeUnderTest]SingleSource<T> target)
{
    // TODO: add assertions to method SingleSourceTTest.Calc(SingleSource`1<!!0>)
    target.Calc();
}
    }
}
