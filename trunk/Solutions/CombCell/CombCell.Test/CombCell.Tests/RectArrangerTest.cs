// <copyright file="RectArrangerTest.cs" company="SJTU">Copyright ©  2009</copyright>

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
    [PexClass(typeof(RectArranger))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class RectArrangerTest
    {
[PexMethod]
public List<Pair<int>> NearBy(
    [PexAssumeUnderTest]RectArranger target,
    int row,
    int column
)
{
    // TODO: add assertions to method RectArrangerTest.NearBy(RectArranger, Int32, Int32)
    List<Pair<int>> result = target.NearBy(row, column);
    return result;
}
[PexMethod]
public string MarkIndex(
    [PexAssumeUnderTest]RectArranger target,
    int row,
    int column
)
{
    // TODO: add assertions to method RectArrangerTest.MarkIndex(RectArranger, Int32, Int32)
    string result = target.MarkIndex(row, column);
    return result;
}
[PexMethod]
public Pair<int> FromPointToPair([PexAssumeUnderTest]RectArranger target, Point point)
{
    // TODO: add assertions to method RectArrangerTest.FromPointToPair(RectArranger, Point)
    Pair<int> result = target.FromPointToPair(point);
    return result;
}
[PexMethod]
public CellShape CreateCellShape([PexAssumeUnderTest]RectArranger target)
{
    // TODO: add assertions to method RectArrangerTest.CreateCellShape(RectArranger)
    CellShape result = target.CreateCellShape();
    return result;
}
[PexMethod]
public Rect Arrange(
    [PexAssumeUnderTest]RectArranger target,
    int row,
    int column
)
{
    // TODO: add assertions to method RectArrangerTest.Arrange(RectArranger, Int32, Int32)
    Rect result = target.Arrange(row, column);
    return result;
}
    }
}
