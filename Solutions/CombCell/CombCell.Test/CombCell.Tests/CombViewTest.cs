// <copyright file="CombViewTest.cs" company="SJTU">Copyright ©  2009</copyright>

using System;
using CombCell;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CombCell
{
    [TestClass]
    [PexClass(typeof(CombView))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class CombViewTest
    {
[PexMethod]
public CombView Constructor()
{
    // TODO: add assertions to method CombViewTest.Constructor()
    CombView target = new CombView();
    return target;
}
    }
}
