// <copyright file="PriorityQueueTItemTPriTest.Remove01.g.cs" company="">Copyright ©  2009</copyright>
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
public void Remove0101()
{
    PriorityQueue<int, int> priorityQueue;
    bool b;
    priorityQueue = new PriorityQueue<int, int>((IComparer<int>)null);
    b = this.Remove01<int, int>(priorityQueue, default(KeyValuePair<int, int>));
    Assert.AreEqual<bool>(false, b);
}
[TestMethod]
[PexGeneratedBy(typeof(PriorityQueueTItemTPriTest))]
public void Remove02()
{
    PriorityQueue<int, int> priorityQueue;
    bool b;
    priorityQueue = new PriorityQueue<int, int>((IComparer<int>)null);
    b = this.Remove<int, int>(priorityQueue, 0);
    Assert.AreEqual<bool>(false, b);
}
    }
}
