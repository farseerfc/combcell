using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Xps.Packaging;
using CombCell.DSAlgo;

namespace CombCell.View3D
{
    /// <summary>
    /// Interaction logic for CombCell3DWindow.xaml
    /// </summary>
    public partial class CombCell3DWindow : Window
    {
        public bool IsUsingGlass
        {
            get { return (bool)GetValue(IsUsingGlassProperty); }
            set
            {

                SetValue(IsUsingGlassProperty, value);
            }
        }
        public static readonly DependencyProperty IsUsingGlassProperty =
            DependencyProperty.Register(
                "IsUsingGlass", typeof(bool), typeof(CombCell3DWindow),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));


        public CombCell3DWindow()
        {
            InitializeComponent();


            loadAlgorithms();
            Arranger arranger = new HexArranger();
            ResetArranger(arranger);

            LinearGradientBrush linearBackground = new LinearGradientBrush();
            linearBackground.GradientStops.Add(new GradientStop(Color.FromArgb(30, 0, 0, 0), 0.0));
            linearBackground.GradientStops.Add(new GradientStop(Color.FromArgb(150, 0, 0, 0), 0.2));
            linearBackground.GradientStops.Add(new GradientStop(Color.FromArgb(100, 0, 0, 0), 0.8));
            linearBackground.GradientStops.Add(new GradientStop(Color.FromArgb(100, 255, 255, 255), 1.0));
            linearBackground.StartPoint = new Point(0.0, 0.0);
            linearBackground.EndPoint = new Point(0.0, 1.0);
            halfTranspant = linearBackground;
        }

        private List<Type> algorithms;
        private Trackball trackball;
        private Brush brushSidebar;
        private Brush background;
        private Brush brushSaveImage;
        private Brush brushHelpButton;
        private Brush halfTranspant;
        private bool glassEnabled;


        private void Ready_Checked(object sender, RoutedEventArgs e)
        {
            if (combView == null) return;
            combView.AnimateChildrenByRow();
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            if (combView == null) return;
            combView.State = CombViewState.MarkIndex;
        }

        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {
            if (combView == null) return;
            combView.State = CombViewState.SelectCells;
        }

        private void radioButton4_Checked(object sender, RoutedEventArgs e)
        {
            if (combView == null) return;
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

            for (int i = 0; i < algorithms.Count; ++i)
            {
                RadioButton rd = stackAlgorithms.Children[i] as RadioButton;
                if ((bool)rd.IsChecked)
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

            trackball = new Trackball();
            trackball.Attach(gridViewport3D);
            trackball.Slaves.Add(viewport3D);
            trackball.IsEnabled = true;

            chk3DView.IsChecked = true;

            TryMakeGlass();

            TryLoadHelp();
        }

        private void loadAlgorithms()
        {
            algorithms = new List<Type>();
            Type pathAlgorithm = typeof(PathAlgorithm<>);
            Assembly assembly = Assembly.GetAssembly(pathAlgorithm);
            foreach (Type type in assembly.GetTypes())
            {
                if (type.BaseType.Name == pathAlgorithm.Name)
                {
                    algorithms.Add(type);

                    RadioButton rdb = new RadioButton();
                    PathAlgorithm<Pair<int>> pathAlgo = (PathAlgorithm<Pair<int>>)type.MakeGenericType(typeof(Pair<int>)).GetConstructor(Type.EmptyTypes).Invoke(null);
                    rdb.Content = new AlgoShower(pathAlgo);
                    rdb.Margin = new Thickness(3);
                    if (pathAlgo.Name.Equals("Hamilton(Slow)")) rdb.IsChecked = true;
                    rdb.Checked += delegate(object sender, RoutedEventArgs e)
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

        private void saveImageExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveDialog = new System.Windows.Forms.SaveFileDialog();

            saveDialog.CreatePrompt = false;
            saveDialog.OverwritePrompt = true;
            saveDialog.CheckPathExists = true;
            saveDialog.DefaultExt = "png";
            saveDialog.AddExtension = true;
            saveDialog.ValidateNames = true;
            saveDialog.Filter = "Portable Network Graphics (*.png) |*.png|" +
                "Joint Picture Experts Group (*.jpg;*.jpeg) |*.jpg;*.jpeg|" +
                "Windows or OS/2 Bitmap (*.bmp) |*.bmp|" +
                "Graphics Interchange Format (*.gif) |*.gif|" +
                "Tagged Image File Format (*.tif;*.tiff) |*.tif;*.tiff|" +
                "Windows Media Picture (*.wdp)|*.wdp";
            saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            System.Windows.Forms.DialogResult result = saveDialog.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK) return;

            gridViewport3D.RenderToFile(saveDialog.FileName);
        }

        private void canSaveImage(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        private void chk3DView_Checked(object sender, RoutedEventArgs e)
        {
            if (viewport2DVisual3D == null) return;
            viewport2DVisual3D.Visual = null;
            gridViewport3D.Children.Clear();
            gridViewport3D.Children.Add(viewport3D);
            viewport2DVisual3D.Visual = combView;
            trackball.IsEnabled = true;
        }

        private void chk3DView_Unchecked(object sender, RoutedEventArgs e)
        {
            if (viewport2DVisual3D == null) return;
            viewport2DVisual3D.Visual = null;
            gridViewport3D.Children.Clear();
            gridViewport3D.Children.Add(combView);
            trackball.IsEnabled = false;
        }

        private void TryMakeGlass()
        {
            brushSidebar = sidebar.Background;
            background = this.Background;
            brushSaveImage = saveImage.Background;
            brushHelpButton = helpButton.Background;

            sidebar.Background = Brushes.Transparent;
            this.Background = halfTranspant;//Brushes.Transparent;
            saveImage.Background = Brushes.Transparent;
            helpButton.Background = Brushes.Transparent;

            glassEnabled = true;
            if (!MakeGlass())
            {
                glassEnabled = false;
                sidebar.Background = brushSidebar;
                this.Background = background;
                saveImage.Background = brushSaveImage;
                helpButton.Background = brushHelpButton;

                gridCheckBoxs.Children.Remove(chkGlass);
            }
        }

        private bool MakeGlass()
        {
            if (Environment.OSVersion.Version.Major < 6) return false;

            try
            {
                Assembly thisAss = Assembly.GetAssembly(typeof(CombCell3DWindow));
                if (thisAss == null) return false;

                string location = thisAss.Location;
                location = location.Substring(0, location.LastIndexOf("\\"));
                Assembly glassAss = Assembly.LoadFile(location + "\\VistaGlass.dll");
                if (glassAss == null) return false;

                Type vistaGlass = null;
                foreach (Module module in glassAss.GetModules())
                {
                    foreach (Type type in module.GetTypes())
                    {
                        if (type.Name.Equals("VistaGlass"))
                            vistaGlass = type;
                    }
                }
                if (vistaGlass == null) return false;

                MethodBase isSupported = vistaGlass.GetMethod("IsSupported");
                if (isSupported == null) return false;
                bool supported = (bool)isSupported.Invoke(null, null);
                if (!supported) return false;

                MethodBase makeGlass = vistaGlass.GetMethod("MakeGlass");
                if (makeGlass == null) return false;
                makeGlass.Invoke(null, new object[] { this });
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void TryLoadHelp()
        {
            string filename = Properties.Settings.Default.UserManualFileName;
            try
            {
                Assembly thisAssembly = Assembly.GetAssembly(typeof(CombCell3DWindow));
                string current = thisAssembly.Location;
                current = current.Substring(0, current.LastIndexOf("\\"));
                string helpPath = current + "\\"+filename;
                XpsDocument xpsDocument = new XpsDocument(helpPath, System.IO.FileAccess.Read);

                helpViewer.Document = xpsDocument.GetFixedDocumentSequence();
                helpViewer.FitToWidth();
                helpViewer.VerticalOffset = Properties.Settings.Default.UserManualJump;
            }
            catch (Exception)
            {
                TextBlock text = new TextBlock();
                text.Margin = new Thickness(20);
                text.FontSize = 40;
                text.Foreground = Brushes.Red;
                text.Text = "Error:\n  Can not load\n"+
                            "    \""+filename+"\"\n"+
                            "    from installation folder.\n"+
                            "    Please reinstall CombCell.\n"+
                            "    We are sorry for that.";


                FixedDocument fixDocument = new FixedDocument();
                PageContent pageContent = new PageContent();
                FixedPage page = new FixedPage();
                page.Children.Add(text);

                ((System.Windows.Markup.IAddChild)pageContent).AddChild(page);

                fixDocument.Pages.Add(pageContent);

                helpViewer.Document = fixDocument;
                helpViewer.FitToWidth();
            }

            
            gridView.Children.Remove(helpViewer);
        }

        protected override void OnStateChanged(EventArgs e)
        {
            InvalidateVisual();
            base.OnStateChanged(e);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            int majorVersion = Environment.OSVersion.Version.Major;
            int minorVerion = Environment.OSVersion.Version.Minor;
            bool isWin7 = (majorVersion == 6 && minorVerion >= 1) || majorVersion >= 7;

            if (IsUsingGlass && (isWin7 || WindowState != WindowState.Maximized))
            {
                if (!glassEnabled) return;
                this.Background = halfTranspant;//Brushes.Transparent;
                sidebar.Background = Brushes.Transparent;
                saveImage.Background = Brushes.Transparent;
                helpButton.Background = Brushes.Transparent;
                
            }
            else
            {
                this.Background = background;
                sidebar.Background = brushSidebar;
                saveImage.Background = brushSaveImage;
                helpButton.Background = brushHelpButton;
            }
        }

        private void helpButton_Checked(object sender, RoutedEventArgs e)
        {
            gridView.Children.Remove(gridViewport3D);
            gridView.Children.Add(helpViewer);
        }

        private void helpButton_Unchecked(object sender, RoutedEventArgs e)
        {
            gridView.Children.Remove(helpViewer);
            gridView.Children.Add(gridViewport3D);
        }

        private void HelpCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            helpButton.IsChecked = !(bool)helpButton.IsChecked;
        }

        private void HelpCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


    }
}
