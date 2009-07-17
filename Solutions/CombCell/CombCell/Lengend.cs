using System.Windows;
using System.Windows.Media;

namespace CombCell
{
    /// <summary>
    /// The lengend class defines all color sets for all CellShape state.
    /// This is a singleton.
    /// </summary>
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
            normal.Brush = Brushes.White.Clone();
            normal.Brush.Opacity = 0;
            normal.Pen = new Pen(Brushes.Yellow, 1);
            Add(normal);

            Scheme selected = new Scheme();
            selected.Key = "Selected";
            selected.Brush = Brushes.LightPink;
            selected.Pen = new Pen(Brushes.Red, 1);
            Add(selected);

            Scheme blocked = new Scheme();
            blocked.Key = "Blocked";
            blocked.Brush = Brushes.Black;
            blocked.Pen = new Pen(Brushes.DarkCyan, 1);
            Add(blocked);

            Scheme passed = new Scheme();
            passed.Key = "Passed";
            passed.Brush = Brushes.LightGreen;
            passed.Pen = new Pen(Brushes.LimeGreen, 1);
            Add(passed);
        }

        /// <summary>
        /// Get/Set the scheme with given key
        /// </summary>
        /// <param name="key">key for the scheme</param>
        /// <returns>scheme</returns>
        public Scheme this[string key]{
            get{
                return Application.Current.Resources[key + "Scheme"] as Scheme;
            }
            set{
                Application.Current.Resources.Add(key + "Scheme", value);
            }
        }
        
        /// <summary>
        /// Add a scheme
        /// </summary>
        /// <param name="scheme"></param>
        public static void Add(Scheme scheme)
        {
            if (!Application.Current.Resources.Contains(scheme.Key + "Scheme"))
            {
                Application.Current.Resources.Add(scheme.Key + "Scheme", scheme);
            }
        }

        /// <summary>
        /// Singleton current
        /// </summary>
        private static Lengend current;
        public static Lengend Current{
            get{
                if (current != null) return current;
                current = new Lengend();
                return current;
            }
        }
    }
}
