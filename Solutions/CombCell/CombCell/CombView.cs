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

namespace CombCell
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CombCell"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CombCell;assembly=CombCell"
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
    ///     <MyNamespace:CombView/>
    ///
    /// </summary>
    public class CombView : Control
    {
        static CombView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CombView), new FrameworkPropertyMetadata(typeof(CombView)));
        }

        public static readonly DependencyProperty CellSizeProperty =
           DependencyProperty.Register(
           "CellSize", typeof(double), typeof(CombView),
           new FrameworkPropertyMetadata(15.0,
               FrameworkPropertyMetadataOptions.AffectsRender));

        public double CellSize
        {
            get
            {
                return (double)GetValue(CellSizeProperty);
            }
            set
            {
                SetValue(CellSizeProperty, value);
            }
        }

        public static readonly DependencyProperty ArrangerProperty =
            DependencyProperty.Register(
                "Arranger",
                typeof(Arranger),
                typeof(CombView),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsArrange |
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        public Arranger Arranger
        {
            set
            {
                SetValue(ArrangerProperty, value);
            }
            get
            {
                return (Arranger)GetValue(ArrangerProperty);
            }
        }

        private readonly List<CellShape> children;

        public CombView()
        {
            children = new List<CellShape>(); xCount = yCount = 0;
        }

        protected override Size ArrangeOverride(Size size)
        {
            Size result = base.ArrangeOverride(size);
            for (int j = 0; j < yCount; ++j)
            {
                for (int i = 0; i < xCount; ++i)
                {
                    Rect rect = new Rect(
                            CellSize * (i * 6 + j % 2 * 3),
                            CellSize * Math.Sqrt(3) * j,
                            CellSize * 4,
                            CellSize * Math.Sqrt(12));
                    children[i + j * xCount].Arrange(rect);
                }
            }

            return result;
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return children.Count;
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            EnsureChildren(sizeInfo.NewSize);
        }

        protected override Visual GetVisualChild(int index)
        {
            return children[index];
        }

        private void EnsureChildren(Size size)
        {
            xCount = (int)Math.Ceiling(size.Width / CellSize / 6.0);
            yCount = (int)Math.Ceiling(size.Height / CellSize / Math.Sqrt(3.0));
            int count = xCount * yCount;
            bool isAdded = false;
            while (count >= children.Count)
            {
                CellShape child = new HexCell();
                TextBlock tb = new TextBlock();
                tb.Text = ""+children.Count;
                tb.FontSize = CellSize;
                tb.Foreground = Brushes.Black;
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                tb.VerticalAlignment = VerticalAlignment.Center;
                child.Content = tb;
                children.Add(child);
                AddLogicalChild(child);
                AddVisualChild(child);
                isAdded = true;
            }
            if (isAdded)
            {
                InvalidateArrange();
            }
        }

        private int xCount;
        private int yCount;

    }
}
