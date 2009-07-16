// <copyright file="CombCellWindowTest.cs" company="SJTU">Copyright ©  2009</copyright>

using System;
using CombCell;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CombCell
{
    [TestClass]
    [PexClass(typeof(CombCellWindow))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class CombCellWindowTest
    {
[PexMethod]
public CombCellWindow Constructor()
{
    // TODO: add assertions to method CombCellWindowTest.Constructor()
    CombCellWindow target = new CombCellWindow();
    return target;
}
    }
}
