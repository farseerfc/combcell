// <copyright file="ArrangerTest.CreateCellShape.g.cs" company="SJTU">Copyright ©  2009</copyright>
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework.Generated;

namespace CombCell
{
    public partial class ArrangerTest
    {
[TestMethod]
[PexGeneratedBy(typeof(ArrangerTest))]
[PexRaisedException(typeof(InvalidOperationException))]
public void CreateCellShape01()
{
    HexArranger hexArranger;
    CellShape cellShape;
    hexArranger = new HexArranger();
    cellShape = this.CreateCellShape((Arranger)hexArranger);
}
    }
}
