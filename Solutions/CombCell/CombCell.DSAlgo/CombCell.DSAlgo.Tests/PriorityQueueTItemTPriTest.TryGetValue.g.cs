// <copyright file="PriorityQueueTItemTPriTest.TryGetValue.g.cs" company="">Copyright ©  2009</copyright>
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
    public partial class PriorityQueueTItemTPriTest
    {
[TestMethod]
[PexGeneratedBy(typeof(PriorityQueueTItemTPriTest))]
public void TryGetValue01()
{
    PriorityQueue<int, int> priorityQueue;
    bool b;
    priorityQueue = new PriorityQueue<int, int>((IComparer<int>)null);
    int i = default(int);
    b = this.TryGetValue<int, int>(priorityQueue, 0, out i);
    Assert.AreEqual<bool>(false, b);
    Assert.AreEqual<int>(0, i);
}
    }
}
