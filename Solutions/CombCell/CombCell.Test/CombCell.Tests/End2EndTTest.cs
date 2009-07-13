// <copyright file="End2EndTTest.cs" company="">Copyright ©  2009</copyright>

using System;
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
    }
}
