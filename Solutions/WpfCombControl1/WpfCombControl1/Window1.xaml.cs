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
using System.Data;
using System.Configuration;
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation;
using System.Reflection;
using System.Windows.Threading;
using System.IO;

namespace WpfCombControl1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            // setup trackball for moving the model around
            _trackball = new Trackball();
            _trackball.Attach(this);
            _trackball.Slaves.Add(myViewport3D);
            _trackball.Enabled = true;

        }

        #region Events
        private void OnImage1Animate(object sender, RoutedEventArgs e)
        {
            Storyboard s;

            s = (Storyboard)this.FindResource("RotateStoryboard");
            this.BeginStoryboard(s);
        }
        #endregion

        #region Globals

        Trackball _trackball;

        #endregion

        private void HitTest3D(object sender, MouseEventArgs e)
        {
            Point point2d = e.GetPosition(myViewport3D);
            Point3D testpoint3D = new Point3D(point2d.X, point2d.Y, 0);
            Vector3D testdirection = new Vector3D(point2d.X, point2d.Y, 10);
            PointHitTestParameters pointparams = new PointHitTestParameters(point2d);
            RayHitTestParameters rayparams = new RayHitTestParameters(testpoint3D, testdirection);

            //test for a result in the Viewport3D
            VisualTreeHelper.HitTest(myViewport3D, null, HTResult, pointparams);
        }

        public HitTestResultBehavior HTResult(System.Windows.Media.HitTestResult rawresult)
        {
            //MessageBox.Show(rawresult.ToString());
            RayHitTestResult rayResult = rawresult as RayHitTestResult;

            if (rayResult != null)
            {
                RayMeshGeometry3DHitTestResult rayMeshResult = rayResult as RayMeshGeometry3DHitTestResult;

                if (rayMeshResult != null)
                {
                    GeometryModel3D hitgeo = rayMeshResult.ModelHit as GeometryModel3D;
                    //Comb comb=(Comb)this.Resources["comb"];
                    //comb.MouseLocation = new Point((rayMeshResult.PointHit.X + 0.5) * comb.ActualWidth, (rayMeshResult.PointHit.Y + 0.5) * comb.ActualHeight);
                    //MessageBox.Show("" + comb.MouseLocation);
                    
                }
            }

            return HitTestResultBehavior.Continue;
        }

    }
}
