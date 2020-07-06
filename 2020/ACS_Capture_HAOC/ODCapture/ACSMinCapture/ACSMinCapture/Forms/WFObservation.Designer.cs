namespace ACSMinCapture.Forms
{
    partial class WFObservation
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
            this.tbObservation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.plMaster.SuspendLayout();
            this.SuspendLayout();
            // 
            // plMaster
            // 
            this.plMaster.Controls.Add(this.label1);
            this.plMaster.Controls.Add(this.tbObservation);
            this.plMaster.Size = new System.Drawing.Size(654, 351);
            // 
            // lbCaptionForm
            // 
            this.lbCaptionForm.Size = new System.Drawing.Size(566, 45);
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            // 
            // btnMinimize
            // 
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            // 
            // tbObservation
            // 
            this.tbObservation.Location = new System.Drawing.Point(12, 27);
            this.tbObservation.Multiline = true;
            this.tbObservation.Name = "tbObservation";
            this.tbObservation.Size = new System.Drawing.Size(629, 317);
            this.tbObservation.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Observação";
            // 
            // WFObservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.ClientSize = new System.Drawing.Size(654, 559);
            this.Name = "WFObservation";
            this.Text = "Observação";
            this.BeforeExitEvent += new ACSMinCapture.WFStandard.BeforeExit_Event(this.WFObservation_BeforeExitEvent);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WFObservation_FormClosing);
            this.plMaster.ResumeLayout(false);
            this.plMaster.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox tbObservation;
    }
}
