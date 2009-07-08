using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace CombCell
{
    public class Cell:Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new Cell();
        }



        public CellState State
        {
            get { return (CellState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(CellState), typeof(Cell), 
            new FrameworkPropertyMetadata(CellState.Normal));



        public int Index
        {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Index.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.Register("Index", typeof(int), typeof(Cell),
                new FrameworkPropertyMetadata(0));



    }
}
