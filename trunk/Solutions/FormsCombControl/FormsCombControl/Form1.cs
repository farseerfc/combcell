using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace FormsCombControl
{
    public partial class Form1 : Form
    {
        private Point MouseLocation;
        private float CellSize;

        public Form1()
        {
            InitializeComponent();
            CellSize = 30.0f;
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseWheel);
            this.DoubleBuffered = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
            float r=CellSize, r2=r/2,r3=r2*(float)Math.Sqrt(3);
            GraphicsPath path = new GraphicsPath();
            path.AddLines(new PointF[]{
                    new PointF(-r, 0),
                    new PointF(-r2, -r3),
                    new PointF(r2, -r3),
                    new PointF(r,0),
                    new PointF(r2,r3),
                    new PointF(-r2,r3),
                    new PointF(-r, 0)
                });
            path.CloseFigure();

            Pen normalPen = new Pen(Brushes.Black, 3.0f);
            Brush normalBrush = Brushes.White;
            Pen selectPen = new Pen(Brushes.Blue, 3.0f);
            Brush selectBrush = Brushes.PowderBlue;

            float sdx=0, sdy=0;
            Graphics g = e.Graphics;
                bool isOdd = false;
                for (float dy = 0; dy < this.Height+r3; dy += r3)
                {
                    isOdd = !isOdd;
                    for (float dx = isOdd?r*1.5f:0; dx < this.Width+3*r; dx += 3 * r)
                    {
                        GraphicsPath p = (GraphicsPath)path.Clone();
                        Matrix matrix = new Matrix();
                        matrix.Translate(dx, dy);
                        p.Transform(matrix);
                        
                        if (p.IsVisible(MouseLocation) )
                        {
                            sdx = dx; sdy = dy;
                        }
                        else
                        {
                            g.FillPath(normalBrush, p);
                            g.DrawPath(normalPen, p);
                        }
                    }
                }

                GraphicsPath ps = (GraphicsPath)path.Clone();
                Matrix m = new Matrix();
                m.Translate(sdx, sdy);
                ps.Transform(m);
                g.FillPath(selectBrush, ps);
                g.DrawPath(selectPen, ps);

            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseLocation = e.Location;
            Rectangle rc = new Rectangle(e.X - (int)CellSize * 3, e.Y - (int)CellSize * 3, (int)CellSize * 6, (int)CellSize * 6);
            this.Invalidate();
        }


        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            float scale = (float)(1+e.Delta/1200.0f);
            CellSize *= scale;
            this.Invalidate();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = textBox1.Text;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

       
    }
}
