namespace OrionRecognizeLibrary
{
    partial class F_ImageBoxRecognize
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
            this.ibrMaster = new OrionRecognizeLibrary.ImageBoxRecognize();
            this.SuspendLayout();
            // 
            // ibrMaster
            // 
            this.ibrMaster.AllowClickZoom = false;
            this.ibrMaster.AllowZone = false;
            this.ibrMaster.AutoPan = false;
            this.ibrMaster.AutoSize = false;
            this.ibrMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ibrMaster.Location = new System.Drawing.Point(0, 0);
            this.ibrMaster.Name = "ibrMaster";
            this.ibrMaster.Size = new System.Drawing.Size(284, 262);
            this.ibrMaster.TabIndex = 0;
            // 
            // F_ImageBoxRecognize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.ibrMaster);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "F_ImageBoxRecognize";
            this.Text = "F_ImageBoxRecognize";
            this.ResumeLayout(false);

        }

        #endregion

        private ImageBoxRecognize ibrMaster;
    }
}