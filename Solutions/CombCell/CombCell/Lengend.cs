using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace CombCell
{
    public class Scheme
    {
        public Pen Pen;
        public Brush Brushes;
        public String Key;
    }

    public class Lengend
    {
        private Dictionary<String, Scheme> schemes;

        public IEnumerable<Scheme> Schemes
        {
            get { return schemes.Values; }
            set 
            {
                schemes.Clear();
                foreach(Scheme scheme in value){
                    schemes.Add(scheme.Key, scheme);
                }
            }
        }

        public Scheme this[string key]{
            get{
                return schemes[key];
            }
            set{
                schemes[key]=value;
            }
        }
        
    }
}
