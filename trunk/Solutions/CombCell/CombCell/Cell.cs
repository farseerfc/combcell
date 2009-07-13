using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace CombCell
{
    /// <summary>
    /// Model layer of the cell.
    /// Based on freezable's changed notification and Dependency Properties.
    /// </summary>
    public class Cell:Freezable
    {
        /// <summary>
        /// Create a new instance. Simply returns <code>new Cell()</code>.
        /// </summary>
        /// <returns>new Cell()</returns>
        protected override Freezable CreateInstanceCore()
        {
            return new Cell();
        }


        /// <summary>
        /// The state emun of the cell. Which will affect the scheme of CellShape.
        /// </summary>
        public CellState State
        {
            get { return (CellState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }
        /// <summary>
        /// Support dependency property of State
        /// </summary>
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(CellState), typeof(Cell), 
            new FrameworkPropertyMetadata(CellState.Normal));


        /// <summary>
        /// The index marked on the cell. Which will affect the displayed number on the cell.
        /// </summary>
        public int Index
        {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }
        /// <summary>
        /// Support dependency property of Index
        /// </summary>
        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.Register("Index", typeof(int), typeof(Cell),
                new FrameworkPropertyMetadata(0));


        /// <summary>
        /// The position of the cell.
        /// </summary>
        public Pair<int> Position
        {
            get { return (Pair<int>)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }
        /// <summary>
        /// Support dependency property of Position
        /// </summary>
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(Pair<int>), typeof(Cell),
                new FrameworkPropertyMetadata(new Pair<int>()));



    }
}
