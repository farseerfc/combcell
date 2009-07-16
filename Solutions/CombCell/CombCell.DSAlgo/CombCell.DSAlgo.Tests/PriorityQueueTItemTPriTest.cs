// <copyright file="PriorityQueueTItemTPriTest.cs" company="">Copyright ©  2009</copyright>

using System;
using CombCell.DSAlgo;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CombCell.DSAlgo
{
    [TestClass]
    [PexClass(typeof(PriorityQueue<, >))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class PriorityQueueTItemTPriTest
    {
[PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod, PexAllowedException(typeof(ArgumentOutOfRangeException))]
public void CopyTo<TItem,TPri>(
    [PexAssumeUnderTest]PriorityQueue<TItem, TPri> target,
    KeyValuePair<TItem, TPri>[] array,
    int arrayIndex
)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.CopyTo(PriorityQueue`2<!!0,!!1>, KeyValuePair`2<!!0,!!1>[], Int32)
    target.CopyTo(array, arrayIndex);
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public void ValuesGet<TItem,TPri>([PexAssumeUnderTest]PriorityQueue<TItem, TPri> target)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.ValuesGet(PriorityQueue`2<!!0,!!1>)
    ICollection<TPri> result = target.Values;
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public bool TryGetValue<TItem,TPri>(
    [PexAssumeUnderTest]PriorityQueue<TItem, TPri> target,
    TItem key,
    out TPri value
)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.TryGetValue(PriorityQueue`2<!!0,!!1>, !!0, !!1&)
    bool result = target.TryGetValue(key, out value);
    return result;
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public bool Remove01<TItem,TPri>(
    [PexAssumeUnderTest]PriorityQueue<TItem, TPri> target,
    KeyValuePair<TItem, TPri> item
)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.Remove01(PriorityQueue`2<!!0,!!1>, KeyValuePair`2<!!0,!!1>)
    bool result = target.Remove(item);
    return result;
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public bool Remove<TItem,TPri>([PexAssumeUnderTest]PriorityQueue<TItem, TPri> target, TItem key)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.Remove(PriorityQueue`2<!!0,!!1>, !!0)
    bool result = target.Remove(key);
    return result;
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public KeyValuePair<TItem, TPri> Pop<TItem,TPri>([PexAssumeUnderTest]PriorityQueue<TItem, TPri> target)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.Pop(PriorityQueue`2<!!0,!!1>)
    KeyValuePair<TItem, TPri> result = target.Pop();
    return result;
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public KeyValuePair<TItem, TPri> Peek<TItem,TPri>([PexAssumeUnderTest]PriorityQueue<TItem, TPri> target)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.Peek(PriorityQueue`2<!!0,!!1>)
    KeyValuePair<TItem, TPri> result = target.Peek();
    return result;
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public void KeysGet<TItem,TPri>([PexAssumeUnderTest]PriorityQueue<TItem, TPri> target)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.KeysGet(PriorityQueue`2<!!0,!!1>)
    ICollection<TItem> result = target.Keys;
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public void ItemGetSet<TItem,TPri>(
    [PexAssumeUnderTest]PriorityQueue<TItem, TPri> target,
    TItem key,
    TPri value
)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.ItemGetSet(PriorityQueue`2<!!0,!!1>, !!0)
    target[key] = value;
    TPri result = target[key];
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public void IsReadOnlyGet<TItem,TPri>([PexAssumeUnderTest]PriorityQueue<TItem, TPri> target)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.IsReadOnlyGet(PriorityQueue`2<!!0,!!1>)
    bool result = target.IsReadOnly;
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public void Insert01<TItem,TPri>(
    [PexAssumeUnderTest]PriorityQueue<TItem, TPri> target,
    TItem item,
    TPri pri
)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.Insert01(PriorityQueue`2<!!0,!!1>, !!0, !!1)
    target.Insert(item, pri);
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public void Insert<TItem,TPri>(
    [PexAssumeUnderTest]PriorityQueue<TItem, TPri> target,
    KeyValuePair<TItem, TPri> pair
)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.Insert(PriorityQueue`2<!!0,!!1>, KeyValuePair`2<!!0,!!1>)
    target.Insert(pair);
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public IEnumerator<KeyValuePair<TItem, TPri>> GetEnumerator<TItem,TPri>([PexAssumeUnderTest]PriorityQueue<TItem, TPri> target)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.GetEnumerator(PriorityQueue`2<!!0,!!1>)
    IEnumerator<KeyValuePair<TItem, TPri>> result = target.GetEnumerator();
    return result;
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public void EmptyGet<TItem,TPri>([PexAssumeUnderTest]PriorityQueue<TItem, TPri> target)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.EmptyGet(PriorityQueue`2<!!0,!!1>)
    bool result = target.Empty;
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public void CountGet<TItem,TPri>([PexAssumeUnderTest]PriorityQueue<TItem, TPri> target)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.CountGet(PriorityQueue`2<!!0,!!1>)
    int result = target.Count;
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public bool ContainsKey<TItem,TPri>([PexAssumeUnderTest]PriorityQueue<TItem, TPri> target, TItem key)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.ContainsKey(PriorityQueue`2<!!0,!!1>, !!0)
    bool result = target.ContainsKey(key);
    return result;
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public bool Contains<TItem,TPri>(
    [PexAssumeUnderTest]PriorityQueue<TItem, TPri> target,
    KeyValuePair<TItem, TPri> item
)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.Contains(PriorityQueue`2<!!0,!!1>, KeyValuePair`2<!!0,!!1>)
    bool result = target.Contains(item);
    return result;
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public PriorityQueue<TItem, TPri> Constructor01<TItem,TPri>(IComparer<TPri> comparer)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.Constructor01(IComparer`1<!!1>)
    PriorityQueue<TItem, TPri> target = new PriorityQueue<TItem, TPri>(comparer);
    return target;
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public PriorityQueue<TItem, TPri> Constructor<TItem,TPri>()
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.Constructor()
    PriorityQueue<TItem, TPri> target = new PriorityQueue<TItem, TPri>();
    return target;
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public void Clear<TItem,TPri>([PexAssumeUnderTest]PriorityQueue<TItem, TPri> target)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.Clear(PriorityQueue`2<!!0,!!1>)
    target.Clear();
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public void ChangePriority<TItem,TPri>(
    [PexAssumeUnderTest]PriorityQueue<TItem, TPri> target,
    TItem item,
    TPri newPriority
)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.ChangePriority(PriorityQueue`2<!!0,!!1>, !!0, !!1)
    target.ChangePriority(item, newPriority);
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public void Add01<TItem,TPri>(
    [PexAssumeUnderTest]PriorityQueue<TItem, TPri> target,
    KeyValuePair<TItem, TPri> item
)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.Add01(PriorityQueue`2<!!0,!!1>, KeyValuePair`2<!!0,!!1>)
    target.Add(item);
}
[PexGenericArguments(typeof(int), typeof(int))]
[PexMethod]
public void Add<TItem,TPri>(
    [PexAssumeUnderTest]PriorityQueue<TItem, TPri> target,
    TItem key,
    TPri value
)
    where TPri : new()
{
    // TODO: add assertions to method PriorityQueueTItemTPriTest.Add(PriorityQueue`2<!!0,!!1>, !!0, !!1)
    target.Add(key, value);
}
    }
}
