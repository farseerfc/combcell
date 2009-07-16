// <copyright file="PairTTest.cs" company="SJTU">Copyright ©  2009</copyright>

using System;
using CombCell;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CombCell
{
    [TestClass]
    [PexClass(typeof(Pair<>))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class PairTTest
    {
[PexGenericArguments(typeof(int))]
[PexMethod]
public string ToString<T>(Pair<T> target)
{
    // TODO: add assertions to method PairTTest.ToString(Pair`1<!!0>)
    string result = target.ToString();
    return result;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public bool Inequality<T>(Pair<T> p1, Pair<T> p2)
{
    // TODO: add assertions to method PairTTest.Inequality(Pair`1<!!0>, Pair`1<!!0>)
    bool result = p1 != p2;
    return result;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public int GetHashCode<T>(Pair<T> target)
{
    // TODO: add assertions to method PairTTest.GetHashCode(Pair`1<!!0>)
    int result = target.GetHashCode();
    return result;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public bool Equals<T>(Pair<T> target, object obj)
{
    // TODO: add assertions to method PairTTest.Equals(Pair`1<!!0>, Object)
    bool result = target.Equals(obj);
    return result;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public bool Equals01<T>(Pair<T> target, Pair<T> obj)
{
    // TODO: add assertions to method PairTTest.Equals01(Pair`1<!!0>, Pair`1<!!0>)
    bool result = target.Equals(obj);
    return result;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public bool Equality<T>(Pair<T> p1, Pair<T> p2)
{
    // TODO: add assertions to method PairTTest.Equality(Pair`1<!!0>, Pair`1<!!0>)
    bool result = p1 == p2;
    return result;
}
[PexGenericArguments(typeof(int))]
[PexMethod]
public Pair<T> Constructor<T>(T first, T second)
{
    // TODO: add assertions to method PairTTest.Constructor(!!0, !!0)
    Pair<T> target = new Pair<T>(first, second);
    return target;
}
    }
}
