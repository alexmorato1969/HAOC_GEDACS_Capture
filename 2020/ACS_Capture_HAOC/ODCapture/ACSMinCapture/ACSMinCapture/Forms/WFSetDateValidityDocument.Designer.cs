namespace ACSMinCapture.Forms
{
    partial class WFSetDateValidityDocument
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
            this.tbStartDate = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDateValidity = new System.Windows.Forms.MaskedTextBox();
            this.mcCalendar = new System.Windows.Forms.MonthCalendar();
            this.btnCalendar = new System.Windows.Forms.CheckBox();
            this.btnCalendar2 = new System.Windows.Forms.CheckBox();
            this.plMaster.SuspendLayout();
            this.SuspendLayout();
            // 
            // plMaster
            // 
            this.plMaster.Controls.Add(this.btnCalendar2);
            this.plMaster.Controls.Add(this.btnCalendar);
            this.plMaster.Controls.Add(this.mcCalendar);
            this.plMaster.Controls.Add(this.label2);
            this.plMaster.Controls.Add(this.tbDateValidity);
            this.plMaster.Controls.Add(this.label1);
            this.plMaster.Controls.Add(this.tbStartDate);
            this.plMaster.Size = new System.Drawing.Size(772, 366);
            // 
            // lbCaptionForm
            // 
            this.lbCaptionForm.Size = new System.Drawing.Size(684, 45);
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            // 
            // btnMinimize
            // 
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            // 
            // tbStartDate
            // 
            this.tbStartDate.Font = new System.Drawing.Font("Arial", 72F, System.Drawing.FontStyle.Bold);
            this.tbStartDate.Location = new System.Drawing.Point(29, 47);
            this.tbStartDate.Mask = "00/00/0000";
            this.tbStartDate.Name = "tbStartDate";
            this.tbStartDate.Size = new System.Drawing.Size(578, 118);
            this.tbStartDate.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Início Vigência";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Fim Vigência";
            // 
            // tbDateValidity
            // 
            this.tbDateValidity.Font = new System.Drawing.Font("Arial", 72F, System.Drawing.FontStyle.Bold);
            this.tbDateValidity.Location = new System.Drawing.Point(29, 210);
            this.tbDateValidity.Mask = "00/00/0000";
            this.tbDateValidity.Name = "tbDateValidity";
            this.tbDateValidity.Size = new System.Drawing.Size(578, 118);
            this.tbDateValidity.TabIndex = 3;
            // 
            // mcCalendar
            // 
            this.mcCalendar.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mcCalendar.Location = new System.Drawing.Point(378, 45);
            this.mcCalendar.Name = "mcCalendar";
            this.mcCalendar.TabIndex = 6;
            this.mcCalendar.Visible = false;
            this.mcCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.mcCalendar_DateSelected);
            // 
            // btnCalendar
            // 
            this.btnCalendar.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnCalendar.Image = global::ACSMinCapture.Properties.Resources._1404802902_678116_calendar;
            this.btnCalendar.Location = new System.Drawing.Point(617, 47);
            this.btnCalendar.Name = "btnCalendar";
            this.btnCalendar.Size = new System.Drawing.Size(119, 118);
            this.btnCalendar.TabIndex = 7;
            this.btnCalendar.UseVisualStyleBackColor = true;
            this.btnCalendar.CheckedChanged += new System.EventHandler(this.btnCalendar_CheckedChanged);
            // 
            // btnCalendar2
            // 
            this.btnCalendar2.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnCalendar2.Image = global::ACSMinCapture.Properties.Resources._1404802902_678116_calendar;
            this.btnCalendar2.Location = new System.Drawing.Point(617, 210);
            this.btnCalendar2.Name = "btnCalendar2";
            this.btnCalendar2.Size = new System.Drawing.Size(119, 118);
            this.btnCalendar2.TabIndex = 7;
            this.btnCalendar2.UseVisualStyleBackColor = true;
            // 
            // WFSetDateValidityDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(772, 574);
            this.Name = "WFSetDateValidityDocument";
            this.Text = "Orion A.C.S. Capture";
            this.plMaster.ResumeLayout(false);
            this.plMaster.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MonthCalendar mcCalendar;
        public System.Windows.Forms.MaskedTextBox tbStartDate;
        public System.Windows.Forms.MaskedTextBox tbDateValidity;
        public System.Windows.Forms.CheckBox btnCalendar2;
        public System.Windows.Forms.CheckBox btnCalendar;


    }
}
