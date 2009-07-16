using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombCell
{
    /// <summary>
    /// A pair template
    /// </summary>
    /// <typeparam name="T">type of item</typeparam>
    public struct Pair<T>:IEquatable<Pair<T>>
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

        public override bool Equals(object obj)
        {
            if(obj is Pair<T>){
                Pair<T> other = (Pair<T>)obj;
                return Equals(other);
            }
            return false;
        }

        public bool Equals(Pair<T> obj)
        {
            return this.first.Equals(obj.first) 
                && this.second.Equals(obj.second);
        }

        public override int GetHashCode()
        {
            return (first.GetHashCode()*97)^second.GetHashCode();
        }

        public static bool operator == (Pair<T> p1,Pair<T> p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Pair<T> p1, Pair<T> p2)
        {
            return !p1.Equals(p2);
        }
    }
}
