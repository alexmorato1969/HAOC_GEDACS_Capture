namespace ACSMinCapture
{
    partial class WFTitle
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
            this.plTitle = new System.Windows.Forms.Panel();
            this.lbCaptionForm = new System.Windows.Forms.Label();
            this.pnlTop2 = new System.Windows.Forms.Panel();
            this.pnlTopButtons = new System.Windows.Forms.Panel();
            this.btnCloseForm = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.plTitle.SuspendLayout();
            this.pnlTopButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // plTitle
            // 
            this.plTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(173)))), ((int)(((byte)(212)))));
            this.plTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plTitle.Controls.Add(this.lbCaptionForm);
            this.plTitle.Controls.Add(this.pnlTop2);
            this.plTitle.Controls.Add(this.pnlTopButtons);
            this.plTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTitle.Location = new System.Drawing.Point(0, 0);
            this.plTitle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.plTitle.Name = "plTitle";
            this.plTitle.Size = new System.Drawing.Size(493, 47);
            this.plTitle.TabIndex = 0;
            // 
            // lbCaptionForm
            // 
            this.lbCaptionForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCaptionForm.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCaptionForm.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lbCaptionForm.Location = new System.Drawing.Point(24, 0);
            this.lbCaptionForm.Name = "lbCaptionForm";
            this.lbCaptionForm.Size = new System.Drawing.Size(405, 45);
            this.lbCaptionForm.TabIndex = 2;
            this.lbCaptionForm.Text = "Orion ACS Capture";
            this.lbCaptionForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbCaptionForm.DoubleClick += new System.EventHandler(this.label1_DoubleClick);
            this.lbCaptionForm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label1_MouseMove);
            // 
            // pnlTop2
            // 
            this.pnlTop2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTop2.Location = new System.Drawing.Point(0, 0);
            this.pnlTop2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlTop2.Name = "pnlTop2";
            this.pnlTop2.Size = new System.Drawing.Size(24, 45);
            this.pnlTop2.TabIndex = 1;
            // 
            // pnlTopButtons
            // 
            this.pnlTopButtons.Controls.Add(this.btnCloseForm);
            this.pnlTopButtons.Controls.Add(this.btnMinimize);
            this.pnlTopButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlTopButtons.Location = new System.Drawing.Point(429, 0);
            this.pnlTopButtons.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlTopButtons.Name = "pnlTopButtons";
            this.pnlTopButtons.Size = new System.Drawing.Size(62, 45);
            this.pnlTopButtons.TabIndex = 0;
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            this.btnCloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseForm.Image = global::ACSMinCapture.Properties.Resources.exit_16;
            this.btnCloseForm.Location = new System.Drawing.Point(33, 13);
            this.btnCloseForm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(17, 17);
            this.btnCloseForm.TabIndex = 1;
            this.btnCloseForm.UseVisualStyleBackColor = true;
            this.btnCloseForm.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Image = global::ACSMinCapture.Properties.Resources._1347651974_Minimize_Square;
            this.btnMinimize.Location = new System.Drawing.Point(10, 13);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(17, 17);
            this.btnMinimize.TabIndex = 0;
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.button1_Click);
            // 
            // WFTitle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(493, 375);
            this.Controls.Add(this.plTitle);
            this.IsShowning = true;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "WFTitle";
            this.ShowInTaskbar = true;
            this.Enter += new System.EventHandler(this.WFTitle_Enter);
            this.plTitle.ResumeLayout(false);
            this.pnlTopButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plTitle;
        private System.Windows.Forms.Panel pnlTop2;
        private System.Windows.Forms.Panel pnlTopButtons;
        public System.Windows.Forms.Label lbCaptionForm;
        protected System.Windows.Forms.Button btnCloseForm;
        protected System.Windows.Forms.Button btnMinimize;
    }
}
