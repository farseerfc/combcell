// <copyright file="AlgoShowerTest.cs" company="SJTU">Copyright ©  2009</copyright>

using System;
using CombCell;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CombCell.DSAlgo;

namespace CombCell
{
    [TestClass]
    [PexClass(typeof(AlgoShower))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class AlgoShowerTest
    {
[PexMethod]
public string ToString([PexAssumeUnderTest]AlgoShower target)
{
    // TODO: add assertions to method AlgoShowerTest.ToString(AlgoShower)
    string result = target.ToString();
    return result;
}
[PexMethod]
public AlgoShower Constructor(PathAlgorithm<Pair<int>> algo)
{
    // TODO: add assertions to method AlgoShowerTest.Constructor(PathAlgorithm`1<Pair`1<Int32>>)
    AlgoShower target = new AlgoShower(algo);
    return target;
}
[PexMethod]
public void AlgorithmGet([PexAssumeUnderTest]AlgoShower target)
{
    // TODO: add assertions to method AlgoShowerTest.AlgorithmGet(AlgoShower)
    PathAlgorithm<Pair<int>> result = target.Algorithm;
}
    }
}
