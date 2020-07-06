namespace ACSMinCapture.Forms
{
    partial class wfImagem
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
            this.imagePanel3AA = new ACSMinCapture.Forms.ImagePanel();
            this.imagePanel2 = new ACSMinCapture.Forms.ImagePanel();
            this.imagePanel1 = new ACSMinCapture.Forms.ImagePanel();
            this.imagePanel4 = new ACSMinCapture.Forms.ImagePanel();
            this.SuspendLayout();
            // 
            // imagePanel3AA
            // 
            this.imagePanel3AA.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imagePanel3AA.CanvasSize = new System.Drawing.Size(600, 400);
            this.imagePanel3AA.Image = null;
            this.imagePanel3AA.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.imagePanel3AA.Location = new System.Drawing.Point(25, 21);
            this.imagePanel3AA.Name = "imagePanel3AA";
            this.imagePanel3AA.Size = new System.Drawing.Size(426, 460);
            this.imagePanel3AA.TabIndex = 2;
            this.imagePanel3AA.Zoom = 1F;
            // 
            // imagePanel2
            // 
            this.imagePanel2.CanvasSize = new System.Drawing.Size(60, 40);
            this.imagePanel2.Image = null;
            this.imagePanel2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            this.imagePanel2.Location = new System.Drawing.Point(87, 75);
            this.imagePanel2.Name = "imagePanel2";
            this.imagePanel2.Size = new System.Drawing.Size(150, 150);
            this.imagePanel2.TabIndex = 1;
            this.imagePanel2.Zoom = 1F;
            // 
            // imagePanel1
            // 
            this.imagePanel1.CanvasSize = new System.Drawing.Size(60, 40);
            this.imagePanel1.Image = null;
            this.imagePanel1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            this.imagePanel1.Location = new System.Drawing.Point(79, 67);
            this.imagePanel1.Name = "imagePanel1";
            this.imagePanel1.Size = new System.Drawing.Size(150, 150);
            this.imagePanel1.TabIndex = 0;
            this.imagePanel1.Zoom = 1F;
            // 
            // imagePanel4
            // 
            this.imagePanel4.CanvasSize = new System.Drawing.Size(60, 40);
            this.imagePanel4.Image = null;
            this.imagePanel4.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            this.imagePanel4.Location = new System.Drawing.Point(189, 119);
            this.imagePanel4.Name = "imagePanel4";
            this.imagePanel4.Size = new System.Drawing.Size(8, 8);
            this.imagePanel4.TabIndex = 3;
            this.imagePanel4.Zoom = 1F;
            // 
            // wfImagem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(478, 517);
            this.Controls.Add(this.imagePanel4);
            this.Controls.Add(this.imagePanel3AA);
            this.Controls.Add(this.imagePanel2);
            this.Controls.Add(this.imagePanel1);
            this.Name = "wfImagem";
            this.Text = "wfImagem";
            this.Load += new System.EventHandler(this.wfImagem_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ImagePanel imagePanel1;
        private ImagePanel imagePanel2;
        private ImagePanel imagePanel3AA;
        private ImagePanel imagePanel4;


    }
}