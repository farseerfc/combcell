using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombCell.DSAlgo
{
    /// <summary>
    /// A priority queue is a heap implemented queue that each item got a priority and they are popped as the order of priority.
    /// The default implement is a minimum heap. Can change to a maximum heap by given a greater comparer.
    /// Described in Introduction to Algorithm, 2nd edition, chapter 6.5.
    /// </summary>
    /// <typeparam name="TObj">The type of item</typeparam>
    /// <typeparam name="TPri">The type of priority, usually int, minimum is new TPri().</typeparam>
    public class PriorityQueue<TItem, TPri> : IDictionary<TItem, TPri>,
    ICollection<KeyValuePair<TItem, TPri>>, IEnumerable<KeyValuePair<TItem, TPri>> 
        where TPri : new() 
    {
        private IComparer<TPri> comparer;
        private List<KeyValuePair<TItem, TPri>> heap;
        private Dictionary<TItem, int> pos;
        private TPri zero;

        private void init()
        {
            heap = new List<KeyValuePair<TItem, TPri>>();
            pos = new Dictionary<TItem, int>();
            zero = new TPri();
        }

        /// <summary>
        /// Create a PriorityQueue with default comparer <code><![CDATA[Comparer<TKey>.Default]]></code>.
        /// </summary>
        public PriorityQueue()
        {
            comparer = Comparer<TPri>.Default;
            init();
        }

        /// <summary>
        /// Create a PriorityQueue with given comparer.
        /// Maximum heap is supported by this.
        /// </summary>
        public PriorityQueue(IComparer<TPri> comparer)
        {
            this.comparer = comparer;
            if (comparer == null) this.comparer = Comparer<TPri>.Default;
            init();
        }

        /// <summary>
        /// The count of items
        /// </summary>
        public int Count
        {
            get { return heap.Count; }
        }

        /// <summary>
        /// Whether count==0
        /// </summary>
        public bool Empty
        {
            get { return Count == 0; }
        }

        /// <summary>
        /// Get the KeyValuePair with minimum priority and not remove it.
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<TItem, TPri> Peek()
        {
            if (Empty) throw new InvalidOperationException("Peek when heap is empty");
            return heap[0];
        }

        /// <summary>
        /// Get the KeyValuePair with minimum priority and remove it.
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<TItem, TPri> Pop()
        {
            if (Empty) throw new InvalidOperationException("Pop when heap is empty");
            //get min
            KeyValuePair<TItem, TPri> min = heap[0];
            //move last to top
            heap[0] = heap[heap.Count - 1];
            pos.Remove(min.Key);
            pos[heap[0].Key] = 0;
            heap.RemoveAt(heap.Count - 1);
            siftDown(0);
            return min;
        }

        /// <summary>
        /// Insert the KeyValuePair
        /// </summary>
        /// <param name="pair"></param>
        public void Insert(KeyValuePair<TItem,TPri> pair)
        {
            heap.Add(pair);
            pos[pair.Key] = heap.Count - 1;
            siftUp(heap.Count - 1);
        }

        /// <summary>
        /// Insert the item with given initial priority
        /// </summary>
        /// <param name="key">item</param>
        /// <param name="value">initial priority</param>
        public void Insert(TItem item,TPri pri)
        {
            Insert(new KeyValuePair<TItem, TPri>(item, pri));
        }

        /// <summary>
        /// Change the priority of a given item
        /// </summary>
        /// <param name="item">the given item</param>
        /// <param name="newPriority">the new priority</param>
        public void ChangePriority(TItem item,TPri newPriority)
        {
            if (!pos.ContainsKey(item)) throw new ArgumentException("item");
            int index = pos[item];
            bool isGreater = comparer.Compare(newPriority,heap[index].Value)>0;
            heap[index] = new KeyValuePair<TItem, TPri>(item,newPriority);
            if (isGreater) siftDown(index);
            else siftUp(index);
        }

        private int parent(int index)
        {
            return (index - 1) >> 1; // floor((index-1)/2);
        }

        private int left(int index)
        {
            return index + index + 1;
        }

        private int right(int index)
        {
            return index + index + 2;
        }

        private bool compare(int a,int b)
        {
            return comparer.Compare(heap[a].Value, heap[b].Value) < 0;
        }

        private void swap(int a, int b)
        {
            if (a == b) return;
            pos[heap[a].Key] = b;
            pos[heap[b].Key] = a;
            KeyValuePair<TItem, TPri> temp = heap[a];
            heap[a] = heap[b];
            heap[b] = temp;
        }


        private void siftDown(int index)
        {
            do
            {
                int largest = index;
                int l = left(index);
                int r = right(index);
                if (l < heap.Count && compare(l, largest))
                {
                    largest = l;
                }

                if (r < heap.Count && compare(r, largest))
                {
                    largest = r;
                }

                if (largest == index) return;

                //swap heap[index] <-> heap[largest]
                swap(index, largest);

                index = largest;
            } while (true);
        }

        private void siftUp(int index)
        {
            do
            {
                if (index == 0) return;
                if (!compare(index, parent(index)))
                    return;
                swap(index, parent(index));
                index = parent(index);
            } while (true);
        }




        #region IDictionary<TItem,TPri> Members

        public void Add(TItem key, TPri value)
        {
            Insert(key, value);
        }

        public bool ContainsKey(TItem key)
        {
            return pos.ContainsKey(key);
        }

        public ICollection<TItem> Keys
        {
            get { return pos.Keys; }
        }

        public bool Remove(TItem key)
        {
            if (!ContainsKey(key)) return false;
            ChangePriority(key, zero);
            Pop();
            return true;
        }

        public bool TryGetValue(TItem key, out TPri value)
        {
            if(ContainsKey(key))
            {
                value = this[key];
                return true;
            }
            else
            {
                value = zero;
                return false;
            }
        }

        public ICollection<TPri> Values
        {
            get 
            {
                List<TPri> result = new List<TPri>();
                foreach (var pair in heap)
                {
                    result.Add(pair.Value);
                }
                return result;
            }
        }

        public TPri this[TItem key]
        {
            get
            {
                return heap[pos[key]].Value;
            }
            set
            {
                if (ContainsKey(key))
                    ChangePriority(key, value);
                else
                    Insert(key, value);
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TItem,TPri>> Members

        public void Add(KeyValuePair<TItem, TPri> item)
        {
            Insert(item);
        }

        public void Clear()
        {
            heap.Clear();
            pos.Clear();
        }

        public bool Contains(KeyValuePair<TItem, TPri> item)
        {
            return heap.Contains(item);
        }

        public void CopyTo(KeyValuePair<TItem, TPri>[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (arrayIndex<0||array.Length <= arrayIndex) throw new ArgumentOutOfRangeException("arrayIndex");
            heap.CopyTo(array, arrayIndex);
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(KeyValuePair<TItem, TPri> item)
        {
            return Remove(item.Key);
        }

        #endregion

        #region IEnumerable<KeyValuePair<TItem,TPri>> Members

        public IEnumerator<KeyValuePair<TItem, TPri>> GetEnumerator()
        {
            return heap.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return heap.GetEnumerator();
        }

        #endregion
    }
}
