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
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(CombView),
                new FrameworkPropertyMetadata(typeof(CombView)));
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
            set { SetValue(ArrangerProperty, value); }
            get { return (Arranger)GetValue(ArrangerProperty); }
        }

        public static readonly DependencyProperty LengendProperty =
            DependencyProperty.Register(
                "Lengend",
                typeof(Lengend),
                typeof(CombView),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender));

        private readonly List<CellShape> children;

        public CombView()
        {
            children = new List<CellShape>();
        }

        protected override Size ArrangeOverride(Size size)
        {
            Size result = base.ArrangeOverride(size);
            for (int j = 0; j < Arranger.YCount; ++j)
            {
                for (int i = 0; i < Arranger.XCount; ++i)
                {
                    children[i + j * Arranger.XCount].Arrange(Arranger.Arrange(j, i));
                    children[i + j * Arranger.XCount].Index = Arranger.MarkIndex(j, i);
                }
            }

            return result;
        }

        protected override int VisualChildrenCount
        {
            get { return children.Count; }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            double scale = 1 + (e.Delta / 1200.0);
            Arranger.CellSize *= scale;
            if (Arranger.NeedAddChild(RenderSize))
            {
                EnsureChildren(RenderSize);
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if (Arranger.NeedAddChild(sizeInfo.NewSize))
            {
                EnsureChildren(sizeInfo.NewSize);
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return children[index];
        }

        private void EnsureChildren(Size size)
        {
            int count = Arranger.XCount * Arranger.YCount;
            bool isAdded = false;
            while (count >= children.Count)
            {
                CellShape child = new HexCell();
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


    }
}
