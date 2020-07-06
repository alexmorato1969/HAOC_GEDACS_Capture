namespace ACSMinCapture
{
    partial class WFTipoAcao
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnProcessar = new System.Windows.Forms.Button();
            this.btnAssinar = new System.Windows.Forms.Button();
            this.btnEscanear = new System.Windows.Forms.Button();
            this.btnImportar = new System.Windows.Forms.Button();
            this.lblTipoUsuario = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 771F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTipoUsuario, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(852, 617);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.btnProcessar, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnAssinar, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnEscanear, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnImportar, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(43, 211);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 194F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(765, 194);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // btnProcessar
            // 
            this.btnProcessar.AutoSize = true;
            this.btnProcessar.BackColor = System.Drawing.Color.Transparent;
            this.btnProcessar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnProcessar.FlatAppearance.BorderSize = 0;
            this.btnProcessar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessar.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessar.Image = global::ACSMinCapture.Properties.Resources._1371251931_kservices;
            this.btnProcessar.Location = new System.Drawing.Point(568, 3);
            this.btnProcessar.Name = "btnProcessar";
            this.btnProcessar.Size = new System.Drawing.Size(185, 188);
            this.btnProcessar.TabIndex = 13;
            this.btnProcessar.Text = "Processar";
            this.btnProcessar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnProcessar.UseVisualStyleBackColor = false;
            this.btnProcessar.Click += new System.EventHandler(this.btnProcessar_Click);
            // 
            // btnAssinar
            // 
            this.btnAssinar.AutoSize = true;
            this.btnAssinar.FlatAppearance.BorderSize = 0;
            this.btnAssinar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAssinar.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAssinar.Image = global::ACSMinCapture.Properties.Resources.iconSignature128x;
            this.btnAssinar.Location = new System.Drawing.Point(393, 3);
            this.btnAssinar.Name = "btnAssinar";
            this.btnAssinar.Size = new System.Drawing.Size(169, 188);
            this.btnAssinar.TabIndex = 11;
            this.btnAssinar.Text = "Assinar";
            this.btnAssinar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAssinar.UseVisualStyleBackColor = true;
            this.btnAssinar.Click += new System.EventHandler(this.btnAssinar_Click);
            // 
            // btnEscanear
            // 
            this.btnEscanear.AutoSize = true;
            this.btnEscanear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEscanear.FlatAppearance.BorderSize = 0;
            this.btnEscanear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscanear.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEscanear.Image = global::ACSMinCapture.Properties.Resources._1366855784_Scanner1;
            this.btnEscanear.Location = new System.Drawing.Point(202, 3);
            this.btnEscanear.Name = "btnEscanear";
            this.btnEscanear.Size = new System.Drawing.Size(185, 188);
            this.btnEscanear.TabIndex = 10;
            this.btnEscanear.Text = "Escanear";
            this.btnEscanear.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEscanear.UseVisualStyleBackColor = true;
            this.btnEscanear.Click += new System.EventHandler(this.btnEscanear_Click);
            // 
            // btnImportar
            // 
            this.btnImportar.AutoSize = true;
            this.btnImportar.FlatAppearance.BorderSize = 0;
            this.btnImportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportar.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportar.Image = global::ACSMinCapture.Properties.Resources._1389844340_document_import;
            this.btnImportar.Location = new System.Drawing.Point(3, 3);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(193, 188);
            this.btnImportar.TabIndex = 12;
            this.btnImportar.Text = "Importar";
            this.btnImportar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // lblTipoUsuario
            // 
            this.lblTipoUsuario.AutoSize = true;
            this.lblTipoUsuario.Location = new System.Drawing.Point(3, 0);
            this.lblTipoUsuario.Name = "lblTipoUsuario";
            this.lblTipoUsuario.Size = new System.Drawing.Size(0, 16);
            this.lblTipoUsuario.TabIndex = 5;
            // 
            // WFTipoAcao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.ClientSize = new System.Drawing.Size(852, 617);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "WFTipoAcao";
            this.Text = "Selecione o tipo de processo ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WFTipoAcao_FormClosing);
            this.Load += new System.EventHandler(this.WFTipoAcao_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblTipoUsuario;
        public System.Windows.Forms.Button btnEscanear;
        public System.Windows.Forms.Button btnAssinar;
        public System.Windows.Forms.Button btnImportar;
        public System.Windows.Forms.Button btnProcessar;



    }
}
