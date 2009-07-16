// <copyright file="ArrangerTest.FromPointToIndex.g.cs" company="SJTU">Copyright ©  2009</copyright>
// <auto-generated>
// This file contains automatically generated unit tests.
// Do NOT modify this file manually.
// 
// When Pex is invoked again,
// it might remove or update any previously generated unit tests.
// 
// If the contents of this file becomes outdated, e.g. if it does not
// compile anymore, you may delete this file and invoke Pex again.
// </auto-generated>
using System;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework.Generated;

namespace CombCell
{
    public partial class ArrangerTest
    {
[TestMethod]
[PexGeneratedBy(typeof(ArrangerTest))]
public void FromPointToIndex06()
{
    HexArranger hexArranger;
    int i;
    hexArranger = new HexArranger();
    i = this.FromPointToIndex((Arranger)hexArranger, default(Point));
    Assert.AreEqual<int>(-1, i);
}
[TestMethod]
[PexGeneratedBy(typeof(ArrangerTest))]
public void FromPointToIndex07()
{
    HexArranger hexArranger;
    int i;
    hexArranger = new HexArranger();
    Point s0 = default(Point);
    s0.X = 2.659692223582751E+191;
    s0.Y = default(double);
    i = this.FromPointToIndex((Arranger)hexArranger, s0);
    Assert.AreEqual<int>(-1073741824, i);
}
    }
}
