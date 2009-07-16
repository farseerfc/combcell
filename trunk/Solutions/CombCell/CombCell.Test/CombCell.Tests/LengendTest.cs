// <copyright file="LengendTest.cs" company="SJTU">Copyright ©  2009</copyright>

using System;
using CombCell;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CombCell
{
    [TestClass]
    [PexClass(typeof(Lengend))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class LengendTest
    {
[PexMethod]
public void ItemGetSet(
    [PexAssumeUnderTest]Lengend target,
    string key,
    Scheme value
)
{
    // TODO: add assertions to method LengendTest.ItemGetSet(Lengend, String)
    target[key] = value;
    Scheme result = target[key];
    PexAssert.AreEqual<Scheme>(value, result);
}
[PexMethod]
public void CurrentGet()
{
    // TODO: add assertions to method LengendTest.CurrentGet()
    Lengend result = Lengend.Current;
}
[PexMethod]
public void Add(Scheme scheme)
{
    // TODO: add assertions to method LengendTest.Add(Scheme)
    Lengend.Add(scheme);
}
    }
}
