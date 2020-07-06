namespace OrionRecognizeLibraryTest
{
    partial class F_Test_DockControl
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
            this.imageBoxRecognizeManager1 = new OrionRecognizeLibrary.ImageBoxRecognizeManager();
            this.SuspendLayout();
            // 
            // imageBoxRecognizeManager1
            // 
            this.imageBoxRecognizeManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxRecognizeManager1.Location = new System.Drawing.Point(0, 0);
            this.imageBoxRecognizeManager1.Name = "imageBoxRecognizeManager1";
            this.imageBoxRecognizeManager1.Size = new System.Drawing.Size(405, 262);
            this.imageBoxRecognizeManager1.TabIndex = 0;
            // 
            // F_Test_DockControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 262);
            this.Controls.Add(this.imageBoxRecognizeManager1);
            this.Name = "F_Test_DockControl";
            this.Text = "F_Test_DockControl";
            this.ResumeLayout(false);

        }

        #endregion

        private OrionRecognizeLibrary.ImageBoxRecognizeManager imageBoxRecognizeManager1;

    }
}