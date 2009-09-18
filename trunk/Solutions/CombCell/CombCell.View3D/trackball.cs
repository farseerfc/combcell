//---------------------------------------------------------------------------
// <copyright file="Trackball.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//---------------------------------------------------------------------------


using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;


namespace CombCell.View3D
{
    /// <summary>
    /// The trackball class controls the 3d view transformations of the viewport3d
    /// </summary>
    public class Trackball
    {
        public Trackball()
        {
            Vector3D _translate = new Vector3D(0, 0, 0);
            Vector3D _translateDelta = new Vector3D(0, 0, 0);

            Reset();
        }

        /// <summary>
        /// Attach the mouse capturer onto this trackball, usually the parent of viewport3d
        /// </summary>
        /// <param name="element">the mouse capturer</param>
        public void Attach(FrameworkElement element)
        {
            element.MouseMove += this.MouseMoveHandler;
            element.MouseDown += this.MouseDownHandler;
            element.MouseUp += this.MouseUpHandler;
            element.MouseWheel += this.OnMouseWheel;
        }

        /// <summary>
        /// Detach the mouse capturer from this trackball, usually the parent of viewport3d
        /// </summary>
        /// <param name="element">the mouse capturer</param>
        public void Detach(FrameworkElement element)
        {
            element.MouseMove -= this.MouseMoveHandler;
            element.MouseRightButtonDown -= this.MouseDownHandler;
            element.MouseRightButtonUp -= this.MouseUpHandler;
            element.MouseWheel -= this.OnMouseWheel;
        }

        /// <summary>
        /// The viewport3d that controlled by this trackball
        /// </summary>
        public List<Viewport3D> Slaves
        {
            get
            {
                if (_slaves == null)
                    _slaves = new List<Viewport3D>();

                return _slaves;
            }
            set
            {
                _slaves = value;
            }
        }

        /// <summary>
        /// Whether this trackball is enabled or not
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return _enabled && (_slaves != null) && (_slaves.Count > 0);
            }
            set
            {
                _enabled = value;
            }
        }

        /// <summary>
        /// Updates the matrices of the slaves using the rotation quaternion.
        /// </summary>
        /// <param name="q">rotation quaternion</param>
        /// <param name="s">scale</param>
        /// <param name="t">translate</param>
        private void UpdateSlaves(Quaternion quaternion, double scale, Vector3D translate)
        {

            if (_slaves != null)
            {
                foreach (Viewport3D i in _slaves)
                {
                    Viewport2DVisual3D mv = i.Children[0] as Viewport2DVisual3D;
                    Transform3DGroup t3dg = mv.Transform as Transform3DGroup;

                    ScaleTransform3D _GroupScaleTransform = t3dg.Children[0] as ScaleTransform3D;
                    RotateTransform3D _GroupRotateTransform = t3dg.Children[1] as RotateTransform3D;
                    TranslateTransform3D _GroupTranslateTransform = t3dg.Children[2] as TranslateTransform3D;

                    _GroupScaleTransform.ScaleX = scale;
                    _GroupScaleTransform.ScaleY = scale;
                    _GroupScaleTransform.ScaleZ = scale;
                    _GroupRotateTransform.Rotation = new AxisAngleRotation3D(quaternion.Axis, quaternion.Angle);
                    _GroupTranslateTransform.OffsetX = translate.X;
                    _GroupTranslateTransform.OffsetY = translate.Y;
                    _GroupTranslateTransform.OffsetZ = translate.Z;

                }
            }
        }

        /// <summary>
        /// Capture mouse move
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseMoveHandler(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!IsEnabled) return;
            e.Handled = true;

            UIElement el = (UIElement)sender;

            if (el.IsMouseCaptured)
            {
                Vector delta = _point - e.MouseDevice.GetPosition(el);
                Vector3D t = new Vector3D();

                delta /= 2;
                Quaternion q = _rotation;

                if (_rotating == true)
                {
                    // We can redefine this 2D mouse delta as a 3D mouse delta
                    // where "into the screen" is Z
                    Vector3D mouse = new Vector3D(delta.X, -delta.Y, 0);
                    Vector3D axis = Vector3D.CrossProduct(mouse, new Vector3D(0, 0, 1));
                    double len = axis.Length;
                    if (len < 0.00001 || _scaling)
                        _rotationDelta = new Quaternion(new Vector3D(0, 0, 1), 0);
                    else
                        _rotationDelta = new Quaternion(axis, len);

                    q = _rotationDelta * _rotation;
                }
                else
                {
                    delta /= 20;
                    _translateDelta.X = delta.X * -1;
                    _translateDelta.Y = delta.Y;
                }

                t = _translate + _translateDelta;

                UpdateSlaves(q, _scale * _scaleDelta, t);

            }
        }


        /// <summary>
        /// Capture mouse rotate or translate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabled) return;
            if (e.ChangedButton == MouseButton.Left) return;
            e.Handled = true;
            if (e.MiddleButton == MouseButtonState.Pressed &&
                e.RightButton == MouseButtonState.Pressed)
            {
                Reset();
                return;
            }

            UIElement el = (UIElement)sender;
            _point = e.MouseDevice.GetPosition(el);
            // Initialize the center of rotation to the lookatpoint
            if (!_centered)
            {
                ProjectionCamera camera = (ProjectionCamera)_slaves[0].Camera;
                _center = camera.LookDirection;
                _centered = true;
            }

            _scaling = (e.MiddleButton == MouseButtonState.Pressed);

            if (e.ChangedButton==MouseButton.Right)
                _rotating = true;
            else
                _rotating = false;

            el.CaptureMouse();
        }


        /// <summary>
        /// Release the captured mouse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            if (!_enabled) return;
            if (e.ChangedButton == MouseButton.Left) return;
            e.Handled = true;

            // Stuff the current initial + delta into initial so when we next move we
            // start at the right place.
            if (_rotating == true)
                _rotation = _rotationDelta * _rotation;
            else
            {
                _translate += _translateDelta;
                _translateDelta.X = 0;
                _translateDelta.Y = 0;
            }

            //_scale = _scaleDelta*_scale;
            UIElement el = (UIElement)sender;
            el.ReleaseMouseCapture();
        }


        /// <summary>
        /// Zoom in/out the viewport3d
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                e.Handled = true;

                _scaleDelta += (double)((double)e.Delta / (double)1000);
                Quaternion q = _rotation;

                UpdateSlaves(q, _scale * _scaleDelta, _translate);
            }
        }

        private void MouseDoubleClickHandler(object sender, MouseButtonEventArgs e)
        {
            Reset();
        }


        /// <summary>
        /// Reset the transform state
        /// </summary>
        private void Reset()
        {
            _rotation = new Quaternion (0,0,0,1);
            _scale = 1;
            _translate.X = 0;
            _translate.Y = 0;
            _translate.Z = 0;
            _translateDelta.X = 0;
            _translateDelta.Y = 0;
            _translateDelta.Z = 0;

            // Clear delta too, because if reset is called because of a double click then the mouse
            // up handler will also be called and this way it won't do anything.
            _rotationDelta = Quaternion.Identity;
            _scaleDelta = 1;
            UpdateSlaves(_rotation, _scale, _translate);
        }

        // The state of the trackball
        private bool _enabled;
        private Vector3D _center;
        private bool _centered;   
        private double _scale;
        private Vector3D _translate;
        private Quaternion _rotation;
        private List<Viewport3D> _slaves;

        // The state of the current drag
        private bool _scaling;              
        private double _scaleDelta;          // Change to scale because of this drag
        private Quaternion _rotationDelta;  // Change to rotation because of this drag
        private System.Windows.Point _point;               // Initial point of drag
        private Vector3D _translateDelta;
        private bool _rotating;
        //private bool _translating;
    }
}

