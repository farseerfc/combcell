// <copyright file="LengendTest.Add.g.cs" company="SJTU">Copyright ©  2009</copyright>
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
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework.Generated;

namespace CombCell
{
    public partial class LengendTest
    {
[TestMethod]
[PexGeneratedBy(typeof(LengendTest))]
public void Add03()
{
    Pen pen;
    Scheme scheme;
    pen = new Pen((Brush)null, 0);
    scheme = new Scheme();
    scheme.Pen = pen;
    this.Add(scheme);
}
[TestMethod]
[PexGeneratedBy(typeof(LengendTest))]
public void Add02()
{
    Scheme scheme;
    scheme = new Scheme();
    scheme.Pen = (Pen)null;
    this.Add(scheme);
}
[TestMethod]
[PexGeneratedBy(typeof(LengendTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void Add01()
{
    this.Add((Scheme)null);
}
    }
}
