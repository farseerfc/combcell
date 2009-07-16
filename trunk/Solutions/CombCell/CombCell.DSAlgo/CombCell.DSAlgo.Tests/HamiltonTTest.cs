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
    }
}
