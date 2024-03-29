// <copyright file="TriArrangerTest.cs" company="SJTU">Copyright ©  2009</copyright>

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
    [PexClass(typeof(TriArranger))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class TriArrangerTest
    {
[PexMethod]
public List<Pair<int>> NearBy(
    [PexAssumeUnderTest]TriArranger target,
    int row,
    int column
)
{
    // TODO: add assertions to method TriArrangerTest.NearBy(TriArranger, Int32, Int32)
    List<Pair<int>> result = target.NearBy(row, column);
    return result;
}
[PexMethod]
public string MarkIndex(
    [PexAssumeUnderTest]TriArranger target,
    int row,
    int column
)
{
    // TODO: add assertions to method TriArrangerTest.MarkIndex(TriArranger, Int32, Int32)
    string result = target.MarkIndex(row, column);
    return result;
}
[PexMethod]
public Pair<int> FromPointToPair([PexAssumeUnderTest]TriArranger target, Point point)
{
    // TODO: add assertions to method TriArrangerTest.FromPointToPair(TriArranger, Point)
    Pair<int> result = target.FromPointToPair(point);
    return result;
}
[PexMethod]
public CellShape CreateCellShape([PexAssumeUnderTest]TriArranger target)
{
    // TODO: add assertions to method TriArrangerTest.CreateCellShape(TriArranger)
    CellShape result = target.CreateCellShape();
    return result;
}
[PexMethod]
public Rect Arrange(
    [PexAssumeUnderTest]TriArranger target,
    int row,
    int column
)
{
    // TODO: add assertions to method TriArrangerTest.Arrange(TriArranger, Int32, Int32)
    Rect result = target.Arrange(row, column);
    return result;
}
    }
}
