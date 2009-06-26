using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCombControl1
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCombControl1"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCombControl1;assembly=WpfCombControl1"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:Comb/>
    ///
    /// </summary>
    public class Comb : Control
    {
        public static DependencyProperty CellSizeProperty=
            DependencyProperty.Register("CellSize", typeof(Comb), typeof(Comb));
        
        public int CellSize{
            get{
                return (int)GetValue(Comb.CellSizeProperty) ;
            }
            set{
                SetValue(Comb.CellSizeProperty, value);
            }
        }

        static Comb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Comb), new FrameworkPropertyMetadata(typeof(Comb)));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            double radix=RenderSize.Width>RenderSize.Height?RenderSize.Height:RenderSize.Width;
            radix = radix / 2;
            if (IsMouseOver)
            drawingContext.DrawEllipse(Brushes.Blue, new Pen(Brushes.Brown,3),
                new Point(RenderSize.Width / 2, RenderSize.Height / 2), radix, radix);
            else
                drawingContext.DrawEllipse(Brushes.Green, new Pen(Brushes.Red, 3),
                new Point(RenderSize.Width / 2, RenderSize.Height / 2), radix, radix);
        }
    }
}
