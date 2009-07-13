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
    /// Interaction logic for CombCellWindow.xaml
    /// </summary>
    public sealed partial class CombCellWindow : Window
    {
        public CombCellWindow()
        {
            InitializeComponent();
            Arranger arranger = new HexArranger() ;
            ResetArranger(arranger);
        }


        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void Ready_Checked(object sender, RoutedEventArgs e)
        {
            if (combView == null) return;
            combView.AnimateChildrenByRow();
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            combView.State = CombViewState.MarkIndex;
        }

        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {
            combView.State = CombViewState.SelectCells;
        }

        private void radioButton4_Checked(object sender, RoutedEventArgs e)
        {
            combView.State = CombViewState.BlockCells;
        }

        private void TriArranger_Checked(object sender, RoutedEventArgs e)
        {
            if (combView == null) return;
            Arranger arranger = new HexArranger();
            arranger.CellSize = 15;
            ResetArranger(arranger);
        }
        private void RectArranger_Checked(object sender, RoutedEventArgs e)
        {
            if (combView == null) return;
            Arranger arranger = new RectArranger();
            arranger.CellSize = 60;
            ResetArranger(arranger);
        }
        private void HexArranger_Checked(object sender, RoutedEventArgs e)
        {
            if (combView == null) return;
            Arranger arranger = new HexArranger();
            arranger.CellSize = 15;
            ResetArranger(arranger);
        }

        private void ResetArranger(Arranger arranger)
        {
            if (combView == null) return;
            combView.Arranger = arranger;

            CellShape cs1 = arranger.CreateCellShape();
            cs1.Height = 60;
            cs1.Width = 50;
            cs1.Index = "1:1";
            cs1.Cell = new Cell();
            cs1.Cell.State = CellState.MouseOver;
            radioButton1.Content = cs1;

            CellShape cs2 = arranger.CreateCellShape();
            cs2.Height = 60;
            cs2.Width = 50;
            cs2.Index = "1";
            cs2.Cell = new Cell();
            cs2.Cell.State = CellState.Normal;
            radioButton2.Content = cs2;

            CellShape cs3 = arranger.CreateCellShape();
            cs3.Height = 60;
            cs3.Width = 50;
            cs3.Index = "1";
            cs3.Cell = new Cell();
            cs3.Cell.State = CellState.Selected;
            radioButton3.Content = cs3;

            CellShape cs4 = arranger.CreateCellShape();
            cs4.Height = 60;
            cs4.Width = 50;
            cs4.Index = "1";
            cs4.Cell = new Cell();
            cs4.Cell.State = CellState.Blocked;
            radioButton4.Content = cs4;

            combView.ResetArranger();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            combView.AnimateChildrenByRow();
        }
    }
}
