﻿namespace ACSMinCapture
{
    partial class WFSetSetor
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.cbbSetores = new System.Windows.Forms.ComboBox();
            this.labelEscolheSetor = new System.Windows.Forms.Label();
            this.plMaster.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // plMaster
            // 
            this.plMaster.Controls.Add(this.panel2);
            this.plMaster.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.plMaster.Size = new System.Drawing.Size(350, 142);
            // 
            // lbCaptionForm
            // 
            this.lbCaptionForm.Size = new System.Drawing.Size(262, 45);
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            // 
            // btnMinimize
            // 
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnSelecionar);
            this.panel2.Controls.Add(this.cbbSetores);
            this.panel2.Controls.Add(this.labelEscolheSetor);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(350, 142);
            this.panel2.TabIndex = 0;
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Location = new System.Drawing.Point(12, 135);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(323, 45);
            this.btnSelecionar.TabIndex = 13;
            this.btnSelecionar.Text = "Selecionar";
            this.btnSelecionar.UseVisualStyleBackColor = true;
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // cbbSetores
            // 
            this.cbbSetores.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbSetores.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.cbbSetores.FormattingEnabled = true;
            this.cbbSetores.Location = new System.Drawing.Point(12, 71);
            this.cbbSetores.Name = "cbbSetores";
            this.cbbSetores.Size = new System.Drawing.Size(323, 24);
            this.cbbSetores.TabIndex = 12;
            // 
            // labelEscolheSetor
            // 
            this.labelEscolheSetor.AutoSize = true;
            this.labelEscolheSetor.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEscolheSetor.Location = new System.Drawing.Point(76, 23);
            this.labelEscolheSetor.Name = "labelEscolheSetor";
            this.labelEscolheSetor.Size = new System.Drawing.Size(178, 26);
            this.labelEscolheSetor.TabIndex = 11;
            this.labelEscolheSetor.Text = "Escolha o setor";
            // 
            // WFSetTipoCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.ClientSize = new System.Drawing.Size(350, 350);
            this.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.Name = "WFSetTipoCliente";
            this.Text = "Orion - ACS - Capture";
            this.Load += new System.EventHandler(this.WFSetValues_Load);
            this.plMaster.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelEscolheSetor;
        private System.Windows.Forms.ComboBox cbbSetores;
        private System.Windows.Forms.Button btnSelecionar;
    }
}