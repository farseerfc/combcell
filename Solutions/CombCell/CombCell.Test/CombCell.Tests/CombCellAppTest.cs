// <copyright file="CombCellAppTest.cs" company="SJTU">Copyright ©  2009</copyright>

using System;
using CombCell;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CombCell
{
    [TestClass]
    [PexClass(typeof(CombCellApp))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class CombCellAppTest
    {
[PexMethod]
public void Main()
{
    // TODO: add assertions to method CombCellAppTest.Main()
    CombCellApp.Main();
}
    }
}
