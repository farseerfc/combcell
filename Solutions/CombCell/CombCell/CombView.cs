using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
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

        /// <summary>
        /// Get/Set the cell that under the mouse cursor
        /// </summary>
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
        /// <summary>
        /// Support dependency property for MouseOverCell
        /// </summary>
        public static readonly DependencyProperty MouseOverCellProperty =
            DependencyProperty.Register(
            "MouseOverCell",
            typeof(CellShape),
            typeof(CombView), 
            new FrameworkPropertyMetadata(null));


        /// <summary>
        /// Set/Get the point position of the mouse, driven by CombView
        /// </summary>
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
        /// <summary>
        /// Support dependency property for MousePosition
        /// </summary>
        public static readonly DependencyProperty MousePositionProperty =
            DependencyProperty.Register(
            "MousePosition",
            typeof(Point),
            typeof(CombView), 
            new FrameworkPropertyMetadata(new Point(0,0)));


        /// <summary>
        /// Set/Get the mouse behavior state of the CombView 
        /// </summary>
        public CombViewState State
        {
            get { return (CombViewState)GetValue(StateProperty);}
            set { SetValue(StateProperty,value);}
        }
        /// <summary>
        /// Support dependency property for State
        /// </summary>
        public static readonly DependencyProperty StateProperty=
            DependencyProperty.Register(
                "State",typeof(CombViewState),typeof(CombView),
                new FrameworkPropertyMetadata(CombViewState.Ready));


        /// <summary>
        /// Get/Set whether the CombView is using a effect
        /// </summary>
        public bool IsUsingEffect
        {
            get { return (bool)GetValue(IsUsingEffectProperty);}
            set 
            { 
                SetValue(IsUsingEffectProperty,value);

            }
        }
        /// <summary>
        /// Support dependency property for IsUsingEffectProperty
        /// </summary>
        public static readonly DependencyProperty IsUsingEffectProperty=
            DependencyProperty.Register(
                "IsUsingEffect",typeof(bool),typeof(CombView),
                new FrameworkPropertyMetadata(true,
                    FrameworkPropertyMetadataOptions.AffectsRender));

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

        /// <summary>
        /// Call this when arranger is chosen
        /// </summary>
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

        /// <summary>
        /// Arrange cells by calling arranger
        /// </summary>
        /// <param name="finalSize">finalSize</param>
        /// <returns>finalSize</returns>
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

        /// <summary>
        /// Get visible child count
        /// </summary>
        protected override int VisualChildrenCount
        {
            get { return children.Count; }
        }

        /// <summary>
        /// When mouse wheeled, zoom in/out, and re-calc the children count
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            double scale = 1 + (e.Delta / 1200.0);
            Arranger.CellSize *= scale;
            EnsureChildren(RenderSize);
        }

        /// <summary>
        /// Recalc the children count
        /// </summary>
        /// <param name="info">Contains new size and old size</param>
        protected override void OnRenderSizeChanged(SizeChangedInfo info)
        {
            base.OnRenderSizeChanged(info);
            EnsureChildren(info.NewSize);
        }

        /// <summary>
        /// Get visible child
        /// </summary>
        /// <param name="index">the index of the child</param>
        /// <returns>the indicated child</returns>
        protected override Visual GetVisualChild(int index)
        {
            return children[index];
        }

        /// <summary>
        /// When mouse move, changed the state of cell under it
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            MousePosition = e.GetPosition(this);
        }

        /// <summary>
        /// Simulate click event
        /// </summary>
        /// <param name="e">mouse state</param>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.ChangedButton == MouseButton.Left)
            {
                MousePosition = e.GetPosition(this);
                Pair<int> pos = Arranger.FromPointToPair(MousePosition);
                if (State == CombViewState.SelectCells)
                {
                    if (MouseOverCell.Cell.State != CellState.Selected)
                    {
                        if (MouseOverCell.Cell.State == CellState.Blocked)
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
            }
            return;
        }


        /// <summary>
        /// Ensure that there is enough child for the size
        /// </summary>
        /// <param name="size">RenderSize of the drawing area</param>
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

        /// <summary>
        /// Animate the children in the animatedChild list
        /// </summary>
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

        /// <summary>
        /// When a path is calculated, animate its passed cells
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Comb_PathCalculated(object sender,EventArgs e)
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

        /// <summary>
        /// Animate all cells from top to bottom, left to right
        /// </summary>
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

        /// <summary>
        /// Animate all cells from marked index
        /// </summary>
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

        /// <summary>
        /// Render the displaying comb into a picture file
        /// </summary>
        /// <param name="fileName">fileName</param>
        public void RenderToFile(string fileName)
        {
            VisualBrush brush=new VisualBrush(this);
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            drawingContext.DrawRectangle(Brushes.White, null, new Rect(0, 0, ActualWidth, ActualHeight));
            drawingContext.DrawRectangle(brush, null, new Rect(0, 0, ActualWidth, ActualHeight));
            drawingContext.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)ActualWidth, (int)ActualHeight, 120, 120, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            string[] nameSplit= fileName.Split('.');
            string extName = nameSplit[nameSplit.Length-1].ToLower();

            FileStream stream = new FileStream(fileName, FileMode.Create);
            BitmapEncoder encoder = null ;
            switch (extName)
            {
                case "bmp": encoder = new BmpBitmapEncoder(); break;
                case "jpg":
                case "jpeg": encoder = new JpegBitmapEncoder(); break;
                case "png": encoder = new PngBitmapEncoder(); break;
                case "gif": encoder = new GifBitmapEncoder(); break;
                case "tif":
                case "tiff": encoder = new TiffBitmapEncoder(); break;
                case "wmp":
                case "wdp": encoder = new WmpBitmapEncoder(); break;
            }
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(stream);
            stream.Close();
        }

        /// <summary>
        /// Draw the background to capture mouse event
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {

            base.OnRender(drawingContext);
            if (IsUsingEffect)
            {
                System.Windows.Media.Effects.Effect effect = new System.Windows.Media.Effects.DropShadowEffect();
                this.Effect = effect;

            }
            else
            {
                this.Effect = null;
            }
            drawingContext.DrawRectangle(this.Background, null, new Rect(0, 0, RenderSize.Width, RenderSize.Height));
        }

    }
}
