// <copyright file="TriArrangerTest.MarkIndex.g.cs" company="SJTU">Copyright ©  2009</copyright>
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
    public partial class TriArrangerTest
    {
[TestMethod]
[PexGeneratedBy(typeof(TriArrangerTest))]
[PexRaisedException(typeof(ArgumentOutOfRangeException))]
public void MarkIndex01()
{
    TriArranger triArranger;
    string s;
    triArranger = new TriArranger();
    s = this.MarkIndex(triArranger, 0, 0);
}
[TestMethod]
[PexGeneratedBy(typeof(TriArrangerTest))]
[PexRaisedException(typeof(ArgumentOutOfRangeException))]
public void MarkIndex02()
{
    TriArranger triArranger;
    string s;
    triArranger = new TriArranger();
    s = this.MarkIndex(triArranger, int.MinValue, 0);
}
    }
}
