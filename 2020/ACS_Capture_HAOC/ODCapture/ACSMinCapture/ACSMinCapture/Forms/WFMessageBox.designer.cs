namespace ACSMinCapture
{
    partial class WFMessageBox
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.plSimNao = new System.Windows.Forms.Panel();
            this.btnSim = new System.Windows.Forms.Button();
            this.btnNão = new System.Windows.Forms.Button();
            this.plOk = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.pllbMessage = new System.Windows.Forms.Panel();
            this.lbMessage = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.plSimNao.SuspendLayout();
            this.plOk.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.pllbMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbCaptionForm
            // 
            this.lbCaptionForm.Size = new System.Drawing.Size(435, 45);
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            // 
            // btnMinimize
            // 
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.plSimNao);
            this.panel1.Controls.Add(this.plOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 261);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(523, 66);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // plSimNao
            // 
            this.plSimNao.Controls.Add(this.btnSim);
            this.plSimNao.Controls.Add(this.btnNão);
            this.plSimNao.Dock = System.Windows.Forms.DockStyle.Right;
            this.plSimNao.Location = new System.Drawing.Point(36, 0);
            this.plSimNao.Name = "plSimNao";
            this.plSimNao.Size = new System.Drawing.Size(302, 66);
            this.plSimNao.TabIndex = 0;
            this.plSimNao.Paint += new System.Windows.Forms.PaintEventHandler(this.plSimNao_Paint);
            // 
            // btnSim
            // 
            this.btnSim.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnSim.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSim.Image = global::ACSMinCapture.Properties.Resources._1404978957_678134_sign_check;
            this.btnSim.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSim.Location = new System.Drawing.Point(63, 5);
            this.btnSim.Name = "btnSim";
            this.btnSim.Size = new System.Drawing.Size(110, 54);
            this.btnSim.TabIndex = 0;
            this.btnSim.Text = "&Sim";
            this.btnSim.UseVisualStyleBackColor = true;
            // 
            // btnNão
            // 
            this.btnNão.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnNão.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNão.Image = global::ACSMinCapture.Properties.Resources._1404978947_678069_sign_error;
            this.btnNão.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNão.Location = new System.Drawing.Point(179, 5);
            this.btnNão.Name = "btnNão";
            this.btnNão.Size = new System.Drawing.Size(110, 54);
            this.btnNão.TabIndex = 1;
            this.btnNão.Text = "&Não";
            this.btnNão.UseVisualStyleBackColor = true;
            // 
            // plOk
            // 
            this.plOk.Controls.Add(this.btnOk);
            this.plOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.plOk.Location = new System.Drawing.Point(338, 0);
            this.plOk.Name = "plOk";
            this.plOk.Size = new System.Drawing.Size(185, 66);
            this.plOk.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(63, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(110, 54);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(523, 214);
            this.panel2.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.67496F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.32504F));
            this.tableLayoutPanel1.Controls.Add(this.pbImage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pllbMessage, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(523, 214);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // pbImage
            // 
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.Location = new System.Drawing.Point(3, 3);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(159, 208);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbImage.TabIndex = 1;
            this.pbImage.TabStop = false;
            // 
            // pllbMessage
            // 
            this.pllbMessage.AutoScroll = true;
            this.pllbMessage.Controls.Add(this.lbMessage);
            this.pllbMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pllbMessage.Location = new System.Drawing.Point(168, 3);
            this.pllbMessage.Name = "pllbMessage";
            this.pllbMessage.Size = new System.Drawing.Size(352, 208);
            this.pllbMessage.TabIndex = 2;
            // 
            // lbMessage
            // 
            this.lbMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMessage.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMessage.Location = new System.Drawing.Point(0, 0);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(352, 208);
            this.lbMessage.TabIndex = 1;
            this.lbMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WFMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.ClientSize = new System.Drawing.Size(523, 327);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.IsShowning = true;
            this.Name = "WFMessageBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel1.ResumeLayout(false);
            this.plSimNao.ResumeLayout(false);
            this.plOk.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.pllbMessage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel plSimNao;
        private System.Windows.Forms.Button btnSim;
        private System.Windows.Forms.Button btnNão;
        private System.Windows.Forms.Panel plOk;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Panel pllbMessage;
        private System.Windows.Forms.Label lbMessage;
    }
}
