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
    }
}
