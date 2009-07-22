// <copyright file="VertexTTest.IsConnect.g.cs" company="">Copyright ©  2009</copyright>
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
    public partial class VertexTTest
    {
[TestMethod]
[PexGeneratedBy(typeof(VertexTTest))]
public void IsConnect01()
{
    Vertex<int> vertex;
    bool b;
    vertex = new Vertex<int>();
    vertex.Accessed = false;
    vertex.Key = 0;
    b = this.IsConnect<int>(vertex, (Vertex<int>)null);
    Assert.AreEqual<bool>(false, b);
}
[TestMethod]
[PexGeneratedBy(typeof(VertexTTest))]
public void IsConnect02()
{
    Vertex<int> vertex;
    bool b;
    vertex = new Vertex<int>();
    vertex.Accessed = false;
    vertex.Key = 0;
    b = this.IsConnect<int>(vertex, vertex);
    Assert.AreEqual<bool>(false, b);
}
    }
}
