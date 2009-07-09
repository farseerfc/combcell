using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CombCell
{
    /// <summary>
    /// CombView is a Control that contains a <![CDATA[ List<CellShape> ]]> .
    ///     xmlns:cc="clr-namespace:CombCell"
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

        /// <summary>
        /// The Arranger of the CombView defines the may between the Points and the CellShapes
        /// </summary>
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

        public CombViewState State
        {
            get { return (CombViewState)GetValue(StateProperty);}
            set { SetValue(StateProperty,value);}
        }
        public static readonly DependencyProperty StateProperty=
            DependencyProperty.Register(
                "State",typeof(CombViewState),typeof(CombView),
                new FrameworkPropertyMetadata(CombViewState.Ready));

        #endregion


/////////////////////////////////////////////////////////////////////////////////////


        private readonly List<CellShape> children;

        public CombView()
        {
            children = new List<CellShape>();
        }

        protected override Size ArrangeOverride(Size finalSize)
        {       
            for (int j = 0; j < Arranger.YCount; ++j)
            {
                for (int i = 0; i < Arranger.XCount; ++i)
                {
                    children[i + j * Arranger.XCount].Cell = Arranger.Comb[j, i];
                    children[i + j * Arranger.XCount].Index = Arranger.MarkIndex(j, i);
                    children[i + j * Arranger.XCount].Arrange(Arranger.Arrange(j, i));
                }
            }
            return finalSize;
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
                Scheme scheme = Lengend.Current["Normal"];
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
            if (State == CombViewState.SelectCells)
            {
                if (MouseOverCell.Cell.State == CellState.MouseOver)
                {
                    MouseOverCell.Cell.State = CellState.Selected;
                }
                else
                {
                    MouseOverCell.Cell.State = CellState.MouseOver;
                }
            }

            if (State == CombViewState.MarkIndex)
            {
                Pair<int> pos = Arranger.FromPointToPair(MousePosition);
                Arranger.Comb.StartMarkIndex(pos.first, pos.second);
            }

        }

    }
}
