using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombCell
{
    public struct Pair<T>
    {
        public T first;
        public T second;

        public Pair(T first,T second)
        {
            this.first = first;
            this.second = second;
        }

        public override string ToString()
        {
            return String.Format("<{0},{1}>", first, second);
        }
    }
}
