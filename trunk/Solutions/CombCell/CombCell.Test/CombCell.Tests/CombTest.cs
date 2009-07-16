// <copyright file="CombTest.cs" company="SJTU">Copyright ©  2009</copyright>

using System;
using CombCell;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CombCell.DSAlgo;

namespace CombCell
{
    [TestClass]
    [PexClass(typeof(Comb))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class CombTest
    {
[PexMethod]
public void Unselect([PexAssumeUnderTest]Comb target, Pair<int> pos)
{
    // TODO: add assertions to method CombTest.Unselect(Comb, Pair`1<Int32>)
    target.Unselect(pos);
}
[PexMethod]
public void Unblock([PexAssumeUnderTest]Comb target, Pair<int> pos)
{
    // TODO: add assertions to method CombTest.Unblock(Comb, Pair`1<Int32>)
    target.Unblock(pos);
}
[PexMethod]
public void StartMarkIndex([PexAssumeUnderTest]Comb target, Pair<int> pos)
{
    // TODO: add assertions to method CombTest.StartMarkIndex(Comb, Pair`1<Int32>)
    target.StartMarkIndex(pos);
}
[PexMethod]
public void Select([PexAssumeUnderTest]Comb target, Pair<int> pos)
{
    // TODO: add assertions to method CombTest.Select(Comb, Pair`1<Int32>)
    target.Select(pos);
}
[PexMethod]
public void PathDescriptionGetSet([PexAssumeUnderTest]Comb target, string value)
{
    // TODO: add assertions to method CombTest.PathDescriptionGetSet(Comb)
    target.PathDescription = value;
    string result = target.PathDescription;
    PexAssert.AreEqual<string>(value, result);
}
[PexMethod]
public void ItemGet01([PexAssumeUnderTest]Comb target, Pair<int> pos)
{
    // TODO: add assertions to method CombTest.ItemGet01(Comb, Pair`1<Int32>)
    Cell result = target[pos];
}
[PexMethod]
public void ItemGet(
    [PexAssumeUnderTest]Comb target,
    int row,
    int column
)
{
    // TODO: add assertions to method CombTest.ItemGet(Comb, Int32, Int32)
    Cell result = target[row, column];
}
[PexMethod]
public void GraphPathGetSet([PexAssumeUnderTest]Comb target, GraphPath<Pair<int>> value)
{
    // TODO: add assertions to method CombTest.GraphPathGetSet(Comb)
    target.GraphPath = value;
    GraphPath<Pair<int>> result = target.GraphPath;
    PexAssert.AreEqual<GraphPath<Pair<int>>>(value, result);
}
[PexMethod]
public void EnsureCells(
    [PexAssumeUnderTest]Comb target,
    int xCount,
    int yCount
)
{
    // TODO: add assertions to method CombTest.EnsureCells(Comb, Int32, Int32)
    target.EnsureCells(xCount, yCount);
}
[PexMethod]
public Comb Constructor(Arranger arranger)
{
    // TODO: add assertions to method CombTest.Constructor(Arranger)
    Comb target = new Comb(arranger);
    return target;
}
[PexMethod]
public void ChoosedAlgorithmGetSet([PexAssumeUnderTest]Comb target, PathAlgorithm<Pair<int>> value)
{
    // TODO: add assertions to method CombTest.ChoosedAlgorithmGetSet(Comb)
    target.ChoosedAlgorithm = value;
    PathAlgorithm<Pair<int>> result = target.ChoosedAlgorithm;
    PexAssert.AreEqual<PathAlgorithm<Pair<int>>>(value, result);
}
[PexMethod]
public void Block([PexAssumeUnderTest]Comb target, Pair<int> pos)
{
    // TODO: add assertions to method CombTest.Block(Comb, Pair`1<Int32>)
    target.Block(pos);
}
    }
}
