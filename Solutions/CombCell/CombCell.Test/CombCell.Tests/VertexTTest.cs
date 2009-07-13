// <copyright file="VertexTTest.cs" company="">Copyright ©  2009</copyright>

using System;
using CombCell.DSAlgo;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CombCell.DSAlgo
{
    [TestClass]
    [PexClass(typeof(Vertex<>))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class VertexTTest
    {
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public string ToString<T>([PexAssumeUnderTest]Vertex<T> target)
        {
            // TODO: add assertions to method VertexTTest.ToString(Vertex`1<!!0>)
            string result = target.ToString();
            return result;
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public bool IsConnect<T>([PexAssumeUnderTest]Vertex<T> target, Vertex<T> other)
        {
            // TODO: add assertions to method VertexTTest.IsConnect(Vertex`1<!!0>, Vertex`1<!!0>)
            bool result = target.IsConnect(other);
            return result;
        }

        [PexGenericArguments(typeof(string))]
        [PexMethod]
        public string ToStringbyString<T>([PexAssumeUnderTest]Vertex<T> target)
        {
            string result = target.ToString();
            return result;
        }



        [PexMethod, PexAllowedException(typeof(ArgumentNullException))]
        public string ToStringbyString2([PexAssumeUnderTest]Vertex<string> target,string k)
        {
            if (k == null) throw new ArgumentNullException("k");
            target.Key = k;
            string result = target.ToString();
            Assert.AreEqual(result, k);
            return result;
        }
    }
}
