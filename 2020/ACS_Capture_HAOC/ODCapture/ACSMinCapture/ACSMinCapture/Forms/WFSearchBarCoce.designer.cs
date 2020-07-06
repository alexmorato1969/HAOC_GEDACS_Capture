namespace ACSMinCapture
{
    partial class WFSearchBarCoce
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
            this.dgvBarCodes = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbTextBox = new System.Windows.Forms.Label();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.plMaster.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarCodes)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // plMaster
            // 
            this.plMaster.Controls.Add(this.tableLayoutPanel1);
            this.plMaster.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.plMaster.Size = new System.Drawing.Size(957, 358);
            // 
            // lbCaptionForm
            // 
            this.lbCaptionForm.Size = new System.Drawing.Size(869, 45);
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            // 
            // btnMinimize
            // 
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dgvBarCodes, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.72586F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.27415F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(957, 358);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // dgvBarCodes
            // 
            this.dgvBarCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBarCodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBarCodes.Location = new System.Drawing.Point(3, 103);
            this.dgvBarCodes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvBarCodes.Name = "dgvBarCodes";
            this.dgvBarCodes.ReadOnly = true;
            this.dgvBarCodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBarCodes.Size = new System.Drawing.Size(951, 251);
            this.dgvBarCodes.TabIndex = 0;
            this.dgvBarCodes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvXML_CellContentClick);
            this.dgvBarCodes.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBarCodes_RowValidated);
            this.dgvBarCodes.SelectionChanged += new System.EventHandler(this.dgvBarCodes_SelectionChanged);
            this.dgvBarCodes.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBarCodes_CellContentDoubleClick);
            this.dgvBarCodes.DoubleClick += new System.EventHandler(this.dgvBarCodes_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbTextBox);
            this.panel2.Controls.Add(this.tbSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 4);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(951, 91);
            this.panel2.TabIndex = 0;
            // 
            // lbTextBox
            // 
            this.lbTextBox.AutoSize = true;
            this.lbTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTextBox.Location = new System.Drawing.Point(22, 18);
            this.lbTextBox.Name = "lbTextBox";
            this.lbTextBox.Size = new System.Drawing.Size(54, 20);
            this.lbTextBox.TabIndex = 4;
            this.lbTextBox.Text = "Busca";
            // 
            // tbSearch
            // 
            this.tbSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSearch.Location = new System.Drawing.Point(26, 42);
            this.tbSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(266, 29);
            this.tbSearch.TabIndex = 0;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // WFSearchBarCoce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.ClientSize = new System.Drawing.Size(957, 566);
            this.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.Name = "WFSearchBarCoce";
            this.Text = "Selecione Código de Barras";
            this.plMaster.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarCodes)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbTextBox;
        public System.Windows.Forms.TextBox tbSearch;
        public System.Windows.Forms.DataGridView dgvBarCodes;

    }
}
