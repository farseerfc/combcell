﻿using System;
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
using System.Reflection;
using CombCell.DSAlgo;

namespace CombCell
{
    class AlgoShower
    {
        public Type Algorithm;
        public override string ToString()
        {
            return Algorithm.Name;
        }
        public AlgoShower(Type algo)
        {
            Algorithm=algo;
        }
    }

    /// <summary>
    /// Interaction logic for CombCellWindow.xaml
    /// </summary>
    public sealed partial class CombCellWindow : Window
    {
        private List<Type> algorithms;

        public CombCellWindow()
        {
            InitializeComponent();
            loadAlgorithms();
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
            Arranger arranger = new TriArranger();
            arranger.CellSize = 70;
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
            combView.AnimateChildrenByRow();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            combView.AnimateChildrenByRow();
        }

        private void loadAlgorithms()
        {
            algorithms = new List<Type>();
            Type pathAlgorithm = typeof(PathAlgorithm<>);
            Assembly assembly = Assembly.GetAssembly(pathAlgorithm);
            foreach(Type type in assembly.GetTypes())
            {
                if(type.BaseType.Name==pathAlgorithm.Name)
                {
                    algorithms.Add(type);

                    RadioButton rdb = new RadioButton();
                    rdb.Content = new AlgoShower(type);
                    rdb.Margin = new Thickness(3);
                    rdb.Checked+=delegate(object sender,RoutedEventArgs e)
                    {
                        RadioButton btn = sender as RadioButton;
                        AlgoShower algo = btn.Content as AlgoShower;
                        combView.Arranger.Comb.ChoosedAlgorithm = algo.Algorithm;
                    };
                    stackAlgorithms.Children.Add(rdb);
                }
            }
        }
    }
}
