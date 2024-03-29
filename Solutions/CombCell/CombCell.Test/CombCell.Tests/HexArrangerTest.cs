// <copyright file="HexArrangerTest.cs" company="SJTU">Copyright ©  2009</copyright>

using System;
using CombCell;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Windows;

namespace CombCell
{
    [TestClass]
    [PexClass(typeof(HexArranger))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class HexArrangerTest
    {
[PexMethod]
public List<Pair<int>> NearBy(
    [PexAssumeUnderTest]HexArranger target,
    int row,
    int column
)
{
    // TODO: add assertions to method HexArrangerTest.NearBy(HexArranger, Int32, Int32)
    List<Pair<int>> result = target.NearBy(row, column);
    return result;
}
[PexMethod]
public string MarkIndex(
    [PexAssumeUnderTest]HexArranger target,
    int row,
    int column
)
{
    // TODO: add assertions to method HexArrangerTest.MarkIndex(HexArranger, Int32, Int32)
    string result = target.MarkIndex(row, column);
    return result;
}
[PexMethod]
public Pair<int> FromPointToPair([PexAssumeUnderTest]HexArranger target, Point point)
{
    // TODO: add assertions to method HexArrangerTest.FromPointToPair(HexArranger, Point)
    Pair<int> result = target.FromPointToPair(point);
    return result;
}
[PexMethod]
public CellShape CreateCellShape([PexAssumeUnderTest]HexArranger target)
{
    // TODO: add assertions to method HexArrangerTest.CreateCellShape(HexArranger)
    CellShape result = target.CreateCellShape();
    return result;
}
[PexMethod]
public Rect Arrange(
    [PexAssumeUnderTest]HexArranger target,
    int row,
    int column
)
{
    // TODO: add assertions to method HexArrangerTest.Arrange(HexArranger, Int32, Int32)
    Rect result = target.Arrange(row, column);
    return result;
}
    }
}
