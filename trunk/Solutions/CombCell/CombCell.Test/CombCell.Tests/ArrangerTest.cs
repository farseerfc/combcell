// <copyright file="ArrangerTest.cs" company="SJTU">Copyright ©  2009</copyright>

using System;
using CombCell;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.Collections.Generic;

namespace CombCell
{
    [TestClass]
    [PexClass(typeof(Arranger))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ArrangerTest
    {
[PexMethod]
public void CellSizeGetSet([PexAssumeNotNull]Arranger target, double value)
{
    // TODO: add assertions to method ArrangerTest.CellSizeGetSet(Arranger)
    target.CellSize = value;
    double result = target.CellSize;
    PexAssert.AreEqual<double>(value, result);
}
[PexMethod]
public void CombGetSet([PexAssumeNotNull]Arranger target, Comb value)
{
    // TODO: add assertions to method ArrangerTest.CombGetSet(Arranger)
    target.Comb = value;
    Comb result = target.Comb;
    PexAssert.AreEqual<Comb>(value, result);
}
[PexMethod]
public int FromPointToIndex([PexAssumeNotNull]Arranger target, Point point)
{
    // TODO: add assertions to method ArrangerTest.FromPointToIndex(Arranger, Point)
    int result = target.FromPointToIndex(point);
    return result;
}
[PexMethod]
public Pair<int> FromPointToPair([PexAssumeNotNull]Arranger target, Point point)
{
    // TODO: add assertions to method ArrangerTest.FromPointToPair(Arranger, Point)
    Pair<int> result = target.FromPointToPair(point);
    return result;
}
[PexMethod]
public List<Pair<int>> NearBy(
    [PexAssumeNotNull]Arranger target,
    int row,
    int column
)
{
    // TODO: add assertions to method ArrangerTest.NearBy(Arranger, Int32, Int32)
    List<Pair<int>> result = target.NearBy(row, column);
    return result;
}
[PexMethod]
public bool NeedAddChild([PexAssumeNotNull]Arranger target, Size newSize)
{
    // TODO: add assertions to method ArrangerTest.NeedAddChild(Arranger, Size)
    bool result = target.NeedAddChild(newSize);
    return result;
}
[PexMethod]
public void XCountGet([PexAssumeNotNull]Arranger target)
{
    // TODO: add assertions to method ArrangerTest.XCountGet(Arranger)
    int result = target.XCount;
}
[PexMethod]
public void YCountGet([PexAssumeNotNull]Arranger target)
{
    // TODO: add assertions to method ArrangerTest.YCountGet(Arranger)
    int result = target.YCount;
}
[PexMethod]
public string MarkIndex(
    [PexAssumeNotNull]Arranger target,
    int row,
    int column
)
{
    // TODO: add assertions to method ArrangerTest.MarkIndex(Arranger, Int32, Int32)
    string result = target.MarkIndex(row, column);
    return result;
}
[PexMethod]
public CellShape CreateCellShape([PexAssumeNotNull]Arranger target)
{
    // TODO: add assertions to method ArrangerTest.CreateCellShape(Arranger)
    CellShape result = target.CreateCellShape();
    return result;
}
[PexMethod]
public Rect Arrange(
    [PexAssumeNotNull]Arranger target,
    int row,
    int column
)
{
    // TODO: add assertions to method ArrangerTest.Arrange(Arranger, Int32, Int32)
    Rect result = target.Arrange(row, column);
    return result;
}
    }
}
