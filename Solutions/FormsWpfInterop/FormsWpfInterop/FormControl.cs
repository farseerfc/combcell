using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FormsWpfInterop
{
    public partial class FormControl : Control
    {
        public FormControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics g = pe.Graphics;
            Pen p=new Pen(Brushes.Blue,10);
            g.DrawEllipse(p, this.ClientRectangle);
            Brush b = Brushes.Black;
            g.FillEllipse(b, this.ClientRectangle);
        }
    }
}
