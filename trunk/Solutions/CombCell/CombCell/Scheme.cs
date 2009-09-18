using System.Windows;
using System.Windows.Media;

namespace CombCell
{
    public class Scheme: Freezable
    {
   
        public Pen Pen
        {
            get { return (Pen)GetValue(PenProperty); }
            set { SetValue(PenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Pen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PenProperty =
            DependencyProperty.Register("Pen", typeof(Pen), typeof(Scheme));



        public Brush Brush
        {
            get { return (Brush)GetValue(BrushProperty); }
            set { SetValue(BrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Brush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BrushProperty =
            DependencyProperty.Register("Brush", typeof(Brush), typeof(Scheme));



        public string Key
        {
            get { return (string)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Key.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.Register("Key", typeof(string), typeof(Scheme));

        protected override Freezable CreateInstanceCore()
        {
            return new Scheme();
        }
    }

}
