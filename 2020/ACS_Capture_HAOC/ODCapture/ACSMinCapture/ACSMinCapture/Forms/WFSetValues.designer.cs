namespace ACSMinCapture
{
    partial class WFSetValues
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.fDocs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fBarCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbInterDocs = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbBarCode = new System.Windows.Forms.TextBox();
            this.plMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // plMaster
            // 
            this.plMaster.Controls.Add(this.panel2);
            this.plMaster.Controls.Add(this.dataGridView1);
            this.plMaster.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.plMaster.Size = new System.Drawing.Size(311, 351);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fDocs,
            this.fBarCode});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 197);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(311, 154);
            this.dataGridView1.TabIndex = 0;
            // 
            // fDocs
            // 
            this.fDocs.DataPropertyName = "fDocs";
            this.fDocs.HeaderText = "Documentos";
            this.fDocs.Name = "fDocs";
            this.fDocs.ReadOnly = true;
            // 
            // fBarCode
            // 
            this.fBarCode.DataPropertyName = "fBarCode";
            this.fBarCode.HeaderText = "Código de Barras";
            this.fBarCode.Name = "fBarCode";
            this.fBarCode.ReadOnly = true;
            this.fBarCode.Width = 113;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.tbInterDocs);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.tbBarCode);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(311, 197);
            this.panel2.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Image = global::ACSMinCapture.Properties.Resources._1377822080_FAQ;
            this.button1.Location = new System.Drawing.Point(196, 146);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(51, 41);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 26);
            this.label2.TabIndex = 11;
            this.label2.Text = "Documentos";
            // 
            // tbInterDocs
            // 
            this.tbInterDocs.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInterDocs.Location = new System.Drawing.Point(20, 55);
            this.tbInterDocs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbInterDocs.Name = "tbInterDocs";
            this.tbInterDocs.Size = new System.Drawing.Size(227, 33);
            this.tbInterDocs.TabIndex = 0;
            this.tbInterDocs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbInterDocs_KeyPress_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 26);
            this.label1.TabIndex = 10;
            this.label1.Text = "Código de Barras";
            // 
            // tbBarCode
            // 
            this.tbBarCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbBarCode.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBarCode.Location = new System.Drawing.Point(20, 146);
            this.tbBarCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbBarCode.MaxLength = 4;
            this.tbBarCode.Name = "tbBarCode";
            this.tbBarCode.Size = new System.Drawing.Size(168, 33);
            this.tbBarCode.TabIndex = 1;
            this.tbBarCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbBarCode_KeyDown);
            // 
            // WFSetValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.ClientSize = new System.Drawing.Size(311, 559);
            this.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.Name = "WFSetValues";
            this.Text = "Orion - ACS - Capture";
            this.Load += new System.EventHandler(this.WFSetValues_Load);
            this.plMaster.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn fDocs;
        private System.Windows.Forms.DataGridViewTextBoxColumn fBarCode;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbInterDocs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbBarCode;
    }
}
