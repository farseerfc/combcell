// <copyright file="End2EndTTest.CanCalcGet.g.cs" company="">Copyright ©  2009</copyright>
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
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework.Generated;

namespace CombCell.DSAlgo
{
    public partial class End2EndTTest
    {
[TestMethod]
[PexGeneratedBy(typeof(End2EndTTest))]
public void CanCalcGet01()
{
    List<int> list;
    End2End<int> end2End;
    int[] ints = new int[0];
    list = new List<int>((IEnumerable<int>)ints);
    end2End = new End2End<int>();
    ((PathAlgorithm<int>)end2End).Graph = (Graph<int>)null;
    ((PathAlgorithm<int>)end2End).Selected = list;
    ((PathAlgorithm<int>)end2End).Path = (List<int>)null;
    end2End.Calc();
    this.CanCalcGet<int>(end2End);
}
    }
}
