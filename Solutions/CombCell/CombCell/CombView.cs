using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CombCell
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:cc="clr-namespace:CombCell"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:cc="clr-namespace:CombCell;assembly=CombCell"
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
    ///<![CDATA[
    ///     <cc:CombView/>
    ///]]>
    /// </summary>
    public class CombView : Control
    {

        #region Dependency Properties
        static CombView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(CombView),
                new FrameworkPropertyMetadata(typeof(CombView)));
        }

        public Arranger Arranger
        {
            set { SetValue(ArrangerProperty, value); }
            get { return (Arranger)GetValue(ArrangerProperty); }
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

        public Lengend Lengend
        {
            set { SetValue(LengendProperty, value); }
            get { return (Lengend)GetValue(LengendProperty); }
        }
        public static readonly DependencyProperty LengendProperty =
            DependencyProperty.Register(
                "Lengend",
                typeof(Lengend),
                typeof(CombView),
                new FrameworkPropertyMetadata(new Lengend(),
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public CellShape MouseOverCell
        {
            get { return (CellShape)GetValue(MouseOverCellProperty); }
            set 
            {
                CellShape lastCell = (CellShape)GetValue(MouseOverCellProperty);
                if(lastCell!=null&&lastCell!=value&&
                    lastCell.Cell.State==CellState.MouseOver){
                    lastCell.Cell.State = CellState.Normal;
                }
                if(value.Cell.State==CellState.Normal)
                    value.Cell.State = CellState.MouseOver;
                SetValue(MouseOverCellProperty, value); 
            }
        }
        public static readonly DependencyProperty MouseOverCellProperty =
            DependencyProperty.Register(
            "MouseOverCell",
            typeof(CellShape),
            typeof(CombView), 
            new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public Point MousePosition
        {
            get { return (Point)GetValue(MousePositionProperty); }
            set 
            {
                int index = Arranger.FromPointToIndex(value);
                if (index >= 0 && index < children.Count)
                {
                    MouseOverCell = children[index];
                }
                SetValue(MousePositionProperty, value); 
            }
        }
        public static readonly DependencyProperty MousePositionProperty =
            DependencyProperty.Register(
            "MousePosition",
            typeof(Point),
            typeof(CombView), 
            new FrameworkPropertyMetadata(new Point(0,0),
                    FrameworkPropertyMetadataOptions.AffectsRender));

        #endregion


/////////////////////////////////////////////////////////////////////////////////////


        private readonly List<CellShape> children;

        public CombView()
        {
            children = new List<CellShape>();
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size result = base.ArrangeOverride(finalSize);
            for (int j = 0; j < Arranger.YCount; ++j)
            {
                for (int i = 0; i < Arranger.XCount; ++i)
                {
                    children[i + j * Arranger.XCount].Arrange(Arranger.Arrange(j, i));
                    children[i + j * Arranger.XCount].Index = Arranger.MarkIndex(j, i);
                    children[i + j * Arranger.XCount].Cell = Arranger.Comb[i, j];
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
            EnsureChildren(RenderSize);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo info)
        {
            base.OnRenderSizeChanged(info);
            EnsureChildren(info.NewSize);
        }

        protected override Visual GetVisualChild(int index)
        {
            return children[index];
        }

        private void EnsureChildren(Size size)
        {
            if (Arranger.NeedAddChild(size))
            {
                Scheme scheme = Lengend["Normal"];
                int count = Arranger.XCount * Arranger.YCount;
                bool isAdded = false;
                while (count >= children.Count)
                {
                    CellShape child = Arranger.CreateCellShape();
                    child.Scheme = scheme;
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

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            MousePosition = e.GetPosition(this);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            MousePosition = e.GetPosition(this);
            if(MouseOverCell.Cell.State==CellState.MouseOver){
                MouseOverCell.Cell.State = CellState.Selected;
            }else{
                MouseOverCell.Cell.State = CellState.MouseOver;
            }
        }

    }
}
