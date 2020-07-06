namespace OrionRecognizeLibrary
{
    partial class ImageBoxRecognizeManager
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.dcMaster = new Crom.Controls.Docking.DockContainer();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(471, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // dcMaster
            // 
            this.dcMaster.BackColor = System.Drawing.Color.Azure;
            this.dcMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dcMaster.Location = new System.Drawing.Point(0, 25);
            this.dcMaster.Name = "dcMaster";
            this.dcMaster.Size = new System.Drawing.Size(471, 331);
            this.dcMaster.TabIndex = 2;
            // 
            // ImageBoxRecognizeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dcMaster);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ImageBoxRecognizeManager";
            this.Size = new System.Drawing.Size(471, 356);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private Crom.Controls.Docking.DockContainer dcMaster;
    }
}
