using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

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
            set 
            {
                SetValue(ArrangerProperty, value);
            }
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
                if (value.Cell == null) return;
                CellShape lastCell = (CellShape)GetValue(MouseOverCellProperty);
                if(lastCell!=null&&lastCell.Cell.State==CellState.MouseOver){
                    lastCell.Cell.State = CellState.Normal;
                }
                
                if (value.Cell.State == CellState.Normal)
                {
                    value.Cell.State = CellState.MouseOver;
                }
                
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

 
        public bool IsUsingEffect
        {
            get { return (bool)GetValue(IsUsingEffectProperty);}
            set 
            { 
                SetValue(IsUsingEffectProperty,value);

            }
        }
        public static readonly DependencyProperty IsUsingEffectProperty=
            DependencyProperty.Register(
                "IsUsingEffect",typeof(bool),typeof(CombView),
                new FrameworkPropertyMetadata(true,FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion


/////////////////////////////////////////////////////////////////////////////////////


        private List<CellShape> children;
        private List<CellShape> animatedChildren;
        private List<CellShape> animatingChildren;


        private void init()
        {
            children = new List<CellShape>();
            animatedChildren = new List<CellShape>();
            animatingChildren = new List<CellShape>();
            
        }

        public CombView()
        {
            init();
        }

        public void ResetArranger()
        {
            foreach (var child in children)
            {
                this.RemoveLogicalChild(child);
                this.RemoveVisualChild(child);
            }
            init();
            Arranger.Comb.PathCalculated += new EventHandler(Comb_PathCalculated);
            EnsureChildren(this.RenderSize);
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


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            MousePosition = e.GetPosition(this);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            
            MousePosition = e.GetPosition(this);
            Pair<int> pos=Arranger.FromPointToPair(MousePosition);
            if (State == CombViewState.SelectCells)
            {
                if (MouseOverCell.Cell.State != CellState.Selected)
                {
                    if(MouseOverCell.Cell.State==CellState.Blocked)
                    {
                        Arranger.Comb.Unblock(pos);
                    }
                    Arranger.Comb.Select(pos);
                }
                else
                {
                    Arranger.Comb.Unselect(pos);
                }
            }

            if (State == CombViewState.BlockCells)
            {
                if (MouseOverCell.Cell.State != CellState.Blocked)
                {
                    if (MouseOverCell.Cell.State == CellState.Selected)
                    {
                        Arranger.Comb.Unselect(pos);
                    }
                    Arranger.Comb.Block(pos);
                }
                else
                {
                    Arranger.Comb.Unblock(pos);
                }
            }

            if (State == CombViewState.MarkIndex)
            {
                if (MouseOverCell.Cell.State != CellState.Blocked)
                {
                    Arranger.Comb.StartMarkIndex(pos);
                    InvalidateVisual();
                    AnimateChildrenByIndex();
                }
            }
            return;
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

        private void AnimateChildren()
        {
            foreach(CellShape cell in animatedChildren){
                if (cell != null)
                {
                    cell.Opacity = 0;
                }
            }

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval =new TimeSpan(0, 0, 0, 0, 50);
            timer.Tag = Math.Ceiling(.02 * animatedChildren.Count);
            timer.Tick+=delegate(object sender,EventArgs e){
                DispatcherTimer t = sender as DispatcherTimer;
                int c = 0;
                int tag=(int)Math.Ceiling((double)t.Tag);
                while(c<tag&&animatedChildren.Count>0)
                {
                    DoubleAnimation da = new DoubleAnimation();
                    da.From = 0;
                    da.To = 1;
                    da.Duration =new TimeSpan(0, 0, 0, 0, 500);
                    da.FillBehavior = FillBehavior.Stop;
                    da.Completed += delegate(object sender1, EventArgs e1)
                    {
                        if(animatingChildren.Count>0)
                        {
                            animatingChildren[0].Opacity = 1;
                            animatingChildren.RemoveAt(0);
                        }
                    };
                    animatedChildren[0].BeginAnimation(CellShape.OpacityProperty, da);
                    animatingChildren.Add(animatedChildren[0]);
                    animatedChildren.RemoveAt(0);
                    c++;
                }
                if(animatedChildren.Count==0){
                    t.IsEnabled = false;
                    foreach (CellShape cell in animatingChildren)
                    {
                        cell.Opacity = 1;
                    }
                    animatingChildren.Clear();
                }
            };
            timer.IsEnabled = true;
        }

        void Comb_PathCalculated(object sender,EventArgs e)
        {
            foreach(CellShape cell in animatingChildren)
            {
                cell.Opacity = 1;
            }
            animatingChildren.Clear();
            foreach (Pair<int> child in Arranger.Comb.GraphPath.CrossVertexes)
            {
                animatedChildren.Add(children[child.second + child.first * Arranger.XCount]);
            }
            AnimateChildren();
        }

        public void AnimateChildrenByRow()
        {
            foreach (CellShape cell in animatingChildren)
            {
                cell.Opacity = 1;
            }
            animatingChildren.Clear();
            foreach (CellShape cell in children)
            {
                animatedChildren.Add(cell);
            }
            AnimateChildren();
        }

        public void AnimateChildrenByIndex()
        {
            foreach (CellShape cell in animatingChildren)
            {
                cell.Opacity = 1;
            }
            animatingChildren.Clear();
            foreach (CellShape cell in children)
            {
                if (cell.Cell != null)
                {
                    while (animatedChildren.Count <= cell.Cell.Index)
                    {
                        animatedChildren.Add(null);
                    }
                    animatedChildren[cell.Cell.Index] = cell;
                }
            }
            if(animatedChildren.Count>0){
                animatedChildren.RemoveAt(0);
            }
            
            AnimateChildren();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {

            base.OnRender(drawingContext);
            if (IsUsingEffect)
            {
                this.Effect = new System.Windows.Media.Effects.DropShadowEffect();
            }
            else
            {
                this.Effect = null;
            }
            drawingContext.DrawRectangle(Brushes.Transparent, null, new Rect(0, 0, RenderSize.Width, RenderSize.Height));
        }

    }
}
