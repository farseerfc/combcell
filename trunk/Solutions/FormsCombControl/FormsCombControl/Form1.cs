using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System;
using System.Runtime.InteropServices;

namespace FormsCombControl
{
    public partial class Form1 : Form
    {
        private Point MouseLocation;
        private float CellSize;


        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(
           IntPtr hWnd,
           ref MARGINS pMarInset
        );

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }


        public Form1()
        {
            InitializeComponent();
            CellSize = 30.0f;
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseWheel);
            this.DoubleBuffered = true;


            MARGINS margins = new MARGINS();
            margins.cxLeftWidth = 100;
            margins.cxRightWidth = 100;
            margins.cyTopHeight = 100;
            margins.cyBottomHeight = 100;

            IntPtr hWnd = this.Handle;
            int result = DwmExtendFrameIntoClientArea(hWnd, ref margins);
            this.BackColor = Color.White;
            this.TransparencyKey = this.BackColor;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

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
