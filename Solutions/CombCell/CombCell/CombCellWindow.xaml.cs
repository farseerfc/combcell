using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using CombCell.DSAlgo;

namespace CombCell
{
    /// <summary>
    /// Interaction logic for CombCellWindow.xaml
    /// The main window of the program
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
            cs1.Index = "1";
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

            for (int i = 0; i < algorithms.Count;++i )
            {
                RadioButton rd = stackAlgorithms.Children[i] as RadioButton;
                if((bool)rd.IsChecked)
                {
                    PathAlgorithm<Pair<int>> pathAlgo = (PathAlgorithm<Pair<int>>)algorithms[i].MakeGenericType(typeof(Pair<int>)).GetConstructor(Type.EmptyTypes).Invoke(null);
                    combView.Arranger.Comb.ChoosedAlgorithm = pathAlgo;
                    algorithmDiscription.Text = pathAlgo.Discription;
                }
            }

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
                    PathAlgorithm<Pair<int>> pathAlgo = (PathAlgorithm<Pair<int>>)type.MakeGenericType(typeof(Pair<int>)).GetConstructor(Type.EmptyTypes).Invoke(null);
                    rdb.Content = new AlgoShower(pathAlgo);
                    rdb.Margin = new Thickness(3);
                    rdb.Checked+=delegate(object sender,RoutedEventArgs e)
                    {
                        RadioButton btn = sender as RadioButton;
                        AlgoShower algo = btn.Content as AlgoShower;
                        combView.Arranger.Comb.ChoosedAlgorithm = algo.Algorithm;
                        algorithmDiscription.Text = algo.Algorithm.Discription;
                    };
                    stackAlgorithms.Children.Add(rdb);
                }
            }
        }

        private void saveImage_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveDialog = new System.Windows.Forms.SaveFileDialog();

            saveDialog.CreatePrompt = false;
            saveDialog.OverwritePrompt = true;
            saveDialog.CheckPathExists = true;
            saveDialog.DefaultExt = "png";
            saveDialog.AddExtension = true;
            saveDialog.ValidateNames = true;
            saveDialog.Filter = "Portable Network Graphics (*.png) |*.png|"+
                "Joint Picture Experts Group (*.jpg;*.jpeg) |*.jpg;*.jpeg|" +
                "Windows or OS/2 Bitmap (*.bmp) |*.bmp|"+
                "Graphics Interchange Format (*.gif) |*.gif|"+
                "Tagged Image File Format (*.tif;*.tiff) |*.tif;*.tiff|" +
                "Windows Media Picture (*.wdp)|*.wdp";
            saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            System.Windows.Forms.DialogResult result=saveDialog.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK) return;
            combView.RenderToFile(saveDialog.FileName);
        }
    }
}
