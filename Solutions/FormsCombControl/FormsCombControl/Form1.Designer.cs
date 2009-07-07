namespace FormsCombControl
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axMMControl1 = new AxMCI.AxMMControl();
            ((System.ComponentModel.ISupportInitialize)(this.axMMControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // axMMControl1
            // 
            this.axMMControl1.Enabled = true;
            this.axMMControl1.Location = new System.Drawing.Point(36, 98);
            this.axMMControl1.Name = "axMMControl1";
            this.axMMControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMMControl1.OcxState")));
            this.axMMControl1.Size = new System.Drawing.Size(236, 23);
            this.axMMControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.axMMControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.axMMControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxMCI.AxMMControl axMMControl1;
    }
}

