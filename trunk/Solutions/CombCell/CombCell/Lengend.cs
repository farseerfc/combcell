using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;

namespace CombCell
{
    public class Lengend
    {
        static Lengend()
        {
            Scheme mouseOver = new Scheme();
            mouseOver.Key = "MouseOver";
            mouseOver.Brush = Brushes.LightBlue;
            mouseOver.Pen = new Pen(Brushes.Blue, 1);
            Add(mouseOver);

            Scheme normal = new Scheme();
            normal.Key = "Normal";
            normal.Brush = Brushes.Transparent;
            normal.Pen = new Pen(Brushes.Yellow, 1);
            Add(normal);
        }


        public Scheme this[string key]{
            get{
                return Application.Current.Resources[key + "Scheme"] as Scheme;
            }
            set{
                Application.Current.Resources.Add(key + "Scheme", value);
            }
        }
        
        public static void Add(Scheme scheme)
        {
            Application.Current.Resources.Add(scheme.Key+ "Scheme", scheme);
        }
    }
}
