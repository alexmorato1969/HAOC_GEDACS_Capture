namespace ACSMinCapture
{
    partial class WFSetBarCode
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
            this.btnSearchBarCode = new System.Windows.Forms.Button();
            this.tbText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gpPFpj = new System.Windows.Forms.GroupBox();
            this.rbJuridico = new System.Windows.Forms.RadioButton();
            this.rbFisica = new System.Windows.Forms.RadioButton();
            this.plMaster.SuspendLayout();
            this.gpPFpj.SuspendLayout();
            this.SuspendLayout();
            // 
            // plMaster
            // 
            this.plMaster.Controls.Add(this.gpPFpj);
            this.plMaster.Controls.Add(this.label1);
            this.plMaster.Controls.Add(this.btnSearchBarCode);
            this.plMaster.Controls.Add(this.tbText);
            this.plMaster.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.plMaster.Size = new System.Drawing.Size(545, 314);
            // 
            // lbCaptionForm
            // 
            this.lbCaptionForm.Size = new System.Drawing.Size(457, 45);
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            // 
            // btnMinimize
            // 
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            // 
            // btnSearchBarCode
            // 
            this.btnSearchBarCode.Image = global::ACSMinCapture.Properties.Resources._1404792927_question_sign;
            this.btnSearchBarCode.Location = new System.Drawing.Point(390, 69);
            this.btnSearchBarCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSearchBarCode.Name = "btnSearchBarCode";
            this.btnSearchBarCode.Size = new System.Drawing.Size(129, 118);
            this.btnSearchBarCode.TabIndex = 1;
            this.btnSearchBarCode.UseVisualStyleBackColor = true;
            this.btnSearchBarCode.Click += new System.EventHandler(this.btnSearchBarCode_Click);
            // 
            // tbText
            // 
            this.tbText.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbText.Font = new System.Drawing.Font("Arial", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbText.Location = new System.Drawing.Point(26, 69);
            this.tbText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbText.MaxLength = 4;
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(354, 118);
            this.tbText.TabIndex = 0;
            this.tbText.TextChanged += new System.EventHandler(this.tbText_TextChanged);
            this.tbText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbText_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Código de Barras";
            // 
            // gpPFpj
            // 
            this.gpPFpj.Controls.Add(this.rbJuridico);
            this.gpPFpj.Controls.Add(this.rbFisica);
            this.gpPFpj.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpPFpj.Location = new System.Drawing.Point(29, 194);
            this.gpPFpj.Name = "gpPFpj";
            this.gpPFpj.Size = new System.Drawing.Size(351, 91);
            this.gpPFpj.TabIndex = 10;
            this.gpPFpj.TabStop = false;
            this.gpPFpj.Text = "Tipo de Cliente";
            // 
            // rbJuridico
            // 
            this.rbJuridico.AutoSize = true;
            this.rbJuridico.Location = new System.Drawing.Point(215, 38);
            this.rbJuridico.Name = "rbJuridico";
            this.rbJuridico.Size = new System.Drawing.Size(81, 24);
            this.rbJuridico.TabIndex = 1;
            this.rbJuridico.TabStop = true;
            this.rbJuridico.Text = "Jurídico";
            this.rbJuridico.UseVisualStyleBackColor = true;
            this.rbJuridico.CheckedChanged += new System.EventHandler(this.rbJuridico_CheckedChanged);
            // 
            // rbFisica
            // 
            this.rbFisica.AutoSize = true;
            this.rbFisica.Location = new System.Drawing.Point(56, 38);
            this.rbFisica.Name = "rbFisica";
            this.rbFisica.Size = new System.Drawing.Size(68, 24);
            this.rbFisica.TabIndex = 0;
            this.rbFisica.TabStop = true;
            this.rbFisica.Text = "Físico";
            this.rbFisica.UseVisualStyleBackColor = true;
            this.rbFisica.CheckedChanged += new System.EventHandler(this.rbFisica_CheckedChanged);
            // 
            // WFSetBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.ClientSize = new System.Drawing.Size(545, 522);
            this.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.Name = "WFSetBarCode";
            this.Text = "Microsoft® Visual Studio® 2010";
            this.Load += new System.EventHandler(this.WFSetBarCode_Load);
            this.plMaster.ResumeLayout(false);
            this.plMaster.PerformLayout();
            this.gpPFpj.ResumeLayout(false);
            this.gpPFpj.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSearchBarCode;
        public System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gpPFpj;
        private System.Windows.Forms.RadioButton rbJuridico;
        private System.Windows.Forms.RadioButton rbFisica;

    }
}
