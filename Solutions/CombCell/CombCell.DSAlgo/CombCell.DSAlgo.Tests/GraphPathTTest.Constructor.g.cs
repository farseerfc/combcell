// <copyright file="GraphPathTTest.Constructor.g.cs" company="">Copyright ©  2009</copyright>
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
    public partial class GraphPathTTest
    {
[TestMethod]
[PexGeneratedBy(typeof(GraphPathTTest))]
public void Constructor01()
{
    GraphPath<int> graphPath;
    graphPath = this.Constructor<int>();
    Assert.IsNotNull((object)graphPath);
    Assert.IsNotNull(graphPath.KeyVertexes);
    Assert.AreEqual<int>(0, graphPath.KeyVertexes.Capacity);
    Assert.AreEqual<int>(0, graphPath.KeyVertexes.Count);
    Assert.IsNotNull(graphPath.PassedVertexes);
    Assert.AreEqual<int>(0, graphPath.PassedVertexes.Capacity);
    Assert.AreEqual<int>(0, graphPath.PassedVertexes.Count);
    Assert.IsNotNull(graphPath.CrossVertexes);
    Assert.AreEqual<int>(0, graphPath.CrossVertexes.Capacity);
    Assert.AreEqual<int>(0, graphPath.CrossVertexes.Count);
}
    }
}
