// <copyright file="CellTest.cs" company="SJTU">Copyright ©  2009</copyright>

using System;
using CombCell;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CombCell
{
    [TestClass]
    [PexClass(typeof(Cell))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class CellTest
    {
[PexMethod]
public void IndexGetSet([PexAssumeUnderTest]Cell target, int value)
{
    // TODO: add assertions to method CellTest.IndexGetSet(Cell)
    target.Index = value;
    int result = target.Index;
    PexAssert.AreEqual<int>(value, result);
}
[PexMethod]
public void PositionGetSet([PexAssumeUnderTest]Cell target, Pair<int> value)
{
    // TODO: add assertions to method CellTest.PositionGetSet(Cell)
    target.Position = value;
    Pair<int> result = target.Position;
    PexAssert.AreEqual<Pair<int>>(value, result);
}
[PexMethod]
public void StateGetSet([PexAssumeUnderTest]Cell target, CellState value)
{
    // TODO: add assertions to method CellTest.StateGetSet(Cell)
    target.State = value;
    CellState result = target.State;
    PexAssert.AreEqual<CellState>(value, result);
}
    }
}
