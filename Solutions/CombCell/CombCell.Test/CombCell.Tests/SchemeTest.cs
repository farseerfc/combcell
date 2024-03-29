// <copyright file="SchemeTest.cs" company="SJTU">Copyright ©  2009</copyright>

using System;
using CombCell;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media;

namespace CombCell
{
    [TestClass]
    [PexClass(typeof(Scheme))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class SchemeTest
    {
[PexMethod]
public void PenGetSet([PexAssumeUnderTest]Scheme target, Pen value)
{
    // TODO: add assertions to method SchemeTest.PenGetSet(Scheme)
    target.Pen = value;
    Pen result = target.Pen;
    PexAssert.AreEqual<Pen>(value, result);
}
[PexMethod]
public void KeyGetSet([PexAssumeUnderTest]Scheme target, string value)
{
    // TODO: add assertions to method SchemeTest.KeyGetSet(Scheme)
    target.Key = value;
    string result = target.Key;
    PexAssert.AreEqual<string>(value, result);
}
[PexMethod]
public void BrushGetSet([PexAssumeUnderTest]Scheme target, Brush value)
{
    // TODO: add assertions to method SchemeTest.BrushGetSet(Scheme)
    target.Brush = value;
    Brush result = target.Brush;
    PexAssert.AreEqual<Brush>(value, result);
}
    }
}
