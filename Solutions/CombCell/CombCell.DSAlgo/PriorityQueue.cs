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
    /// <typeparam name="TPri">The type of priority, usually int, minimium of which is new TPri().</typeparam>
    /// <typeparam name="TObj">The type of item</typeparam>
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

        public PriorityQueue(IComparer<TPri> comparer)
        {
            this.comparer = comparer;
            init();
        }


        public int Count
        {
            get { return heap.Count; }
        }

        public bool Empty
        {
            get { return Count == 0; }
        }


        public KeyValuePair<TItem, TPri> Peek()
        {
            if (Empty) throw new InvalidOperationException("Peek when heap is empty");
            return heap[0];
        }

        public KeyValuePair<TItem, TPri> Pop()
        {
            if (Empty) throw new InvalidOperationException("Pop when heap is empty");
            //get min
            KeyValuePair<TItem, TPri> min = heap[0];
            //move last to top
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            siftDown(0);
            pos.Remove(min.Key);
            return min;
        }

        public void Insert(KeyValuePair<TItem,TPri> pair)
        {
            heap.Add(pair);
            pos[pair.Key] = heap.Count - 1;
            siftUp(heap.Count - 1);
        }

        public void Insert(TItem key,TPri value)
        {
            Insert(new KeyValuePair<TItem, TPri>(key, value));
        }

        public void ChangePriority(TItem key,TPri newPriority)
        {
            int index = pos[key];
            bool isGreater = comparer.Compare(newPriority,heap[index].Value)>0;
            heap[index] = new KeyValuePair<TItem, TPri>(key,newPriority);
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
            KeyValuePair<TItem, TPri> temp = heap[a];
            heap[a] = heap[b];
            heap[b] = temp;
            pos[heap[a].Key] = a;
            pos[heap[b].Key] = b;
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
