using System;
using System.Windows;
using System.Windows.Controls;

namespace CombCell
{
    public abstract class CellShape : Control
    {
        
        
        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.Register(
                "Index",
                typeof(String),
                typeof(CellShape),
                new FrameworkPropertyMetadata(
                    "",
                    FrameworkPropertyMetadataOptions.AffectsRender
                    )
            );

        public string Index
        {
            get { return (string)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }

        public static readonly DependencyProperty SchemeProperty =
            DependencyProperty.Register(
                "Scheme",
                typeof(Scheme),
                typeof(CellShape),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsRender
                    )
            );

        public Scheme Scheme
        {
            get { return (Scheme)GetValue(SchemeProperty); }
            set { SetValue(SchemeProperty, value); }
        }



//         protected override void OnIsMouseDirectlyOverChanged(DependencyPropertyChangedEventArgs e)
//         {
//             base.OnIsMouseDirectlyOverChanged(e);
//             this.InvalidateVisual();
//         }
        
            
        public CellShape(){
            this.OverridesDefaultStyle = true;
        }
        
    }
}
