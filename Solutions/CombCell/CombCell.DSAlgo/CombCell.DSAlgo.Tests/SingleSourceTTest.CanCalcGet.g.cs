// <copyright file="SingleSourceTTest.CanCalcGet.g.cs" company="">Copyright ©  2009</copyright>
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

namespace CombCell.DSAlgo
{
    public partial class SingleSourceTTest
    {
[TestMethod]
[PexGeneratedBy(typeof(SingleSourceTTest))]
public void CanCalcGet01()
{
    SingleSource<int> singleSource;
    singleSource = new SingleSource<int>();
    ((PathAlgorithm<int>)singleSource).Graph = (Graph<int>)null;
    singleSource.Calc();
    this.CanCalcGet<int>(singleSource);
}
    }
}
