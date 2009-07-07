using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace CombCell
{

    public class Lengend
    {
        public Lengend(){
            schemes = new Dictionary<string, Scheme>();
        }

        private Dictionary<String, Scheme> schemes;

        public ICollection<Scheme> Schemes
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
