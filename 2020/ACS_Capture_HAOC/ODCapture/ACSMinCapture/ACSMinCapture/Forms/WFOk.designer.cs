namespace ACSMinCapture
{
    partial class WFOk
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.plMaster = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbCaptionForm
            // 
            this.lbCaptionForm.Size = new System.Drawing.Size(274, 45);
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            // 
            // btnMinimize
            // 
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnOk);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 213);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(362, 161);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // btnOk
            // 
            this.btnOk.Image = global::ACSMinCapture.Properties.Resources._1404791221_678134_sign_check;
            this.btnOk.Location = new System.Drawing.Point(230, 4);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(129, 139);
            this.btnOk.TabIndex = 1;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::ACSMinCapture.Properties.Resources._1404791208_678069_sign_error;
            this.btnCancel.Location = new System.Drawing.Point(95, 4);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(129, 139);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(255, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(13, 113);
            this.panel1.TabIndex = 6;
            // 
            // plMaster
            // 
            this.plMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMaster.Location = new System.Drawing.Point(0, 47);
            this.plMaster.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.plMaster.Name = "plMaster";
            this.plMaster.Size = new System.Drawing.Size(362, 166);
            this.plMaster.TabIndex = 0;
            // 
            // WFOk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(362, 374);
            this.Controls.Add(this.plMaster);
            this.Controls.Add(this.flowLayoutPanel1);
            this.IsShowning = true;
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.Name = "WFOk";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "JWill";
            this.Load += new System.EventHandler(this.WFOk_Load);
            this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.plMaster, 0);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel plMaster;
        public System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}