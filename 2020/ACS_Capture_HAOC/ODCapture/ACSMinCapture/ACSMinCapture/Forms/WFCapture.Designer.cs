using ACSMinCapture.Config;
using System.Windows.Forms;
using System;
using TwainLib;
namespace ACSMinCapture.Forms
{
    partial class WFCapture
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WFCapture));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferenciasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.excluirLoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exibirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagensToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi1X1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi1X2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi2X2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi4X4 = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.açãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.digitalizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnLotes = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.btnProcessar = new System.Windows.Forms.Button();
            this.ucImagesManipulation1 = new ACSMinCapture.Controls.UCImagesManipulation(this);
            this.tlpToolBar = new System.Windows.Forms.TableLayoutPanel();
            this.tlpButtons2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblNomePaciente = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.lbTipoDesc = new System.Windows.Forms.Label();
            this.lbDescLote = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.driverFujitsu = new AxFiScnLib.AxFiScn();
            this.menuStrip1.SuspendLayout();
            this.tlpToolBar.SuspendLayout();
            this.tlpButtons2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.driverFujitsu)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem,
            this.exibirToolStripMenuItem,
            this.sairToolStripMenuItem,
            this.açãoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1471, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferenciasToolStripMenuItem,
            this.imagensToolStripMenuItem,
            this.toolStripMenuItem1,
            this.excluirLoteToolStripMenuItem});
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.arquivoToolStripMenuItem.Text = "Arquivo";
            // 
            // preferenciasToolStripMenuItem
            // 
            this.preferenciasToolStripMenuItem.Name = "preferenciasToolStripMenuItem";
            this.preferenciasToolStripMenuItem.Size = new System.Drawing.Size(164, 26);
            this.preferenciasToolStripMenuItem.Text = "Preferências";
            this.preferenciasToolStripMenuItem.Click += new System.EventHandler(this.preferenciasToolStripMenuItem_Click);
            // 
            // imagensToolStripMenuItem
            // 
            this.imagensToolStripMenuItem.Name = "imagensToolStripMenuItem";
            this.imagensToolStripMenuItem.Size = new System.Drawing.Size(164, 26);
            this.imagensToolStripMenuItem.Text = "Imagens...";
            this.imagensToolStripMenuItem.Visible = false;
            this.imagensToolStripMenuItem.Click += new System.EventHandler(this.imagensToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(161, 6);
            this.toolStripMenuItem1.Visible = false;
            // 
            // excluirLoteToolStripMenuItem
            // 
            this.excluirLoteToolStripMenuItem.Name = "excluirLoteToolStripMenuItem";
            this.excluirLoteToolStripMenuItem.Size = new System.Drawing.Size(164, 26);
            this.excluirLoteToolStripMenuItem.Text = "Excluir Lote";
            this.excluirLoteToolStripMenuItem.Visible = false;
            this.excluirLoteToolStripMenuItem.Click += new System.EventHandler(this.excluirLoteToolStripMenuItem_Click);
            // 
            // exibirToolStripMenuItem
            // 
            this.exibirToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imagensToolStripMenuItem1});
            this.exibirToolStripMenuItem.Name = "exibirToolStripMenuItem";
            this.exibirToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.exibirToolStripMenuItem.Text = "Exibir";
            // 
            // imagensToolStripMenuItem1
            // 
            this.imagensToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi1X1,
            this.tsmi1X2,
            this.tsmi2X2,
            this.tsmi4X4});
            this.imagensToolStripMenuItem1.Name = "imagensToolStripMenuItem1";
            this.imagensToolStripMenuItem1.Size = new System.Drawing.Size(140, 26);
            this.imagensToolStripMenuItem1.Text = "Imagens";
            // 
            // tsmi1X1
            // 
            this.tsmi1X1.CheckOnClick = true;
            this.tsmi1X1.Name = "tsmi1X1";
            this.tsmi1X1.Size = new System.Drawing.Size(117, 26);
            this.tsmi1X1.Text = "1 X 1";
            this.tsmi1X1.CheckedChanged += new System.EventHandler(this.x2ToolStripMenuItem_CheckedChanged);
            // 
            // tsmi1X2
            // 
            this.tsmi1X2.Checked = true;
            this.tsmi1X2.CheckOnClick = true;
            this.tsmi1X2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmi1X2.Name = "tsmi1X2";
            this.tsmi1X2.Size = new System.Drawing.Size(117, 26);
            this.tsmi1X2.Text = "1 X 2";
            this.tsmi1X2.CheckedChanged += new System.EventHandler(this.x2ToolStripMenuItem_CheckedChanged);
            // 
            // tsmi2X2
            // 
            this.tsmi2X2.CheckOnClick = true;
            this.tsmi2X2.Name = "tsmi2X2";
            this.tsmi2X2.Size = new System.Drawing.Size(117, 26);
            this.tsmi2X2.Text = "2 X 2";
            // 
            // tsmi4X4
            // 
            this.tsmi4X4.CheckOnClick = true;
            this.tsmi4X4.Name = "tsmi4X4";
            this.tsmi4X4.Size = new System.Drawing.Size(117, 26);
            this.tsmi4X4.Text = "4 X 4";
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // açãoToolStripMenuItem
            // 
            this.açãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.digitalizarToolStripMenuItem,
            this.importarToolStripMenuItem});
            this.açãoToolStripMenuItem.Name = "açãoToolStripMenuItem";
            this.açãoToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.açãoToolStripMenuItem.Text = "Ação";
            // 
            // digitalizarToolStripMenuItem
            // 
            this.digitalizarToolStripMenuItem.Name = "digitalizarToolStripMenuItem";
            this.digitalizarToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.digitalizarToolStripMenuItem.Text = "Digitalizar";
            this.digitalizarToolStripMenuItem.Visible = false;
            this.digitalizarToolStripMenuItem.Click += new System.EventHandler(this.digitalizarToolStripMenuItem_Click);
            // 
            // importarToolStripMenuItem
            // 
            this.importarToolStripMenuItem.Name = "importarToolStripMenuItem";
            this.importarToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.importarToolStripMenuItem.Text = "Importar";
            this.importarToolStripMenuItem.Visible = false;
            this.importarToolStripMenuItem.Click += new System.EventHandler(this.importarToolStripMenuItem_Click);
            // 
            // btnLotes
            // 
            this.btnLotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLotes.Enabled = false;
            this.btnLotes.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLotes.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLotes.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLotes.Image = global::ACSMinCapture.Properties.Resources._1366855840_Control_Panel;
            this.btnLotes.Location = new System.Drawing.Point(4, 4);
            this.btnLotes.Margin = new System.Windows.Forms.Padding(4);
            this.btnLotes.Name = "btnLotes";
            this.btnLotes.Size = new System.Drawing.Size(249, 176);
            this.btnLotes.TabIndex = 0;
            this.btnLotes.Text = "(1) Lista de Trabalho";
            this.btnLotes.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.btnLotes, "{F1}");
            this.btnLotes.UseVisualStyleBackColor = true;
            this.btnLotes.Click += new System.EventHandler(this.btnLotes_Click);
            // 
            // btnScan
            // 
            this.btnScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnScan.Enabled = false;
            this.btnScan.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScan.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScan.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnScan.Image = global::ACSMinCapture.Properties.Resources._1366855784_Scanner1;
            this.btnScan.Location = new System.Drawing.Point(4, 188);
            this.btnScan.Margin = new System.Windows.Forms.Padding(4);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(249, 176);
            this.btnScan.TabIndex = 1;
            this.btnScan.Text = "(2) Digitalizar Documentos";
            this.btnScan.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.btnScan, "{F5}");
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnProcessar
            // 
            this.btnProcessar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnProcessar.Enabled = false;
            this.btnProcessar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnProcessar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessar.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessar.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnProcessar.Image = global::ACSMinCapture.Properties.Resources._1371251931_kservices;
            this.btnProcessar.Location = new System.Drawing.Point(4, 372);
            this.btnProcessar.Margin = new System.Windows.Forms.Padding(4);
            this.btnProcessar.Name = "btnProcessar";
            this.btnProcessar.Size = new System.Drawing.Size(249, 177);
            this.btnProcessar.TabIndex = 2;
            this.btnProcessar.Text = "(3) Processar";
            this.btnProcessar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.btnProcessar, "{F6}");
            this.btnProcessar.UseVisualStyleBackColor = true;
            this.btnProcessar.Click += new System.EventHandler(this.btnProcessar_Click);
            // 
            // ucImagesManipulation1
            // 
            this.ucImagesManipulation1.AutoSize = true;
            this.ucImagesManipulation1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ucImagesManipulation1.BarCodes = null;
            this.ucImagesManipulation1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucImagesManipulation1.EnableAfterSelect = false;
            this.ucImagesManipulation1.LastDocSelected = null;
            this.ucImagesManipulation1.Location = new System.Drawing.Point(277, 5);
            this.ucImagesManipulation1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucImagesManipulation1.MaskDocumentName = "DOC";
            this.ucImagesManipulation1.MaskPageName = "00000000";
            this.ucImagesManipulation1.Name = "ucImagesManipulation1";
            this.ucImagesManipulation1.OCRInImage = true;
            this.ucImagesManipulation1.Size = new System.Drawing.Size(1190, 659);
            this.ucImagesManipulation1.TabIndex = 2;
            this.ucImagesManipulation1.ZoomFitOnLoadBitmap = true;
            this.ucImagesManipulation1.AfterAllDocumentsValid += new ACSMinCapture.Controls.UCImagesManipulation.AfterAllDocumentsValidEvent(this.ucImagesManipulation1_AfterAllDocumentsValid);
            this.ucImagesManipulation1.Load += new System.EventHandler(this.ucImagesManipulation1_Load);
            // 
            // tlpToolBar
            // 
            this.tlpToolBar.ColumnCount = 1;
            this.tlpToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpToolBar.Controls.Add(this.tlpButtons2, 0, 1);
            this.tlpToolBar.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tlpToolBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpToolBar.Location = new System.Drawing.Point(4, 4);
            this.tlpToolBar.Margin = new System.Windows.Forms.Padding(4);
            this.tlpToolBar.Name = "tlpToolBar";
            this.tlpToolBar.RowCount = 2;
            this.tlpToolBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpToolBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpToolBar.Size = new System.Drawing.Size(265, 661);
            this.tlpToolBar.TabIndex = 1;
            // 
            // tlpButtons2
            // 
            this.tlpButtons2.ColumnCount = 1;
            this.tlpButtons2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons2.Controls.Add(this.btnProcessar, 0, 2);
            this.tlpButtons2.Controls.Add(this.btnScan, 0, 1);
            this.tlpButtons2.Controls.Add(this.btnLotes, 0, 0);
            this.tlpButtons2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons2.Location = new System.Drawing.Point(4, 104);
            this.tlpButtons2.Margin = new System.Windows.Forms.Padding(4);
            this.tlpButtons2.Name = "tlpButtons2";
            this.tlpButtons2.RowCount = 3;
            this.tlpButtons2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tlpButtons2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tlpButtons2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tlpButtons2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            this.tlpButtons2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpButtons2.Size = new System.Drawing.Size(257, 553);
            this.tlpButtons2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.lblNomePaciente, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.lblNome, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lbTipoDesc, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbDescLote, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(257, 92);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // lblNomePaciente
            // 
            this.lblNomePaciente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNomePaciente.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblNomePaciente.Location = new System.Drawing.Point(4, 68);
            this.lblNomePaciente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNomePaciente.Name = "lblNomePaciente";
            this.lblNomePaciente.Size = new System.Drawing.Size(249, 24);
            this.lblNomePaciente.TabIndex = 2;
            this.lblNomePaciente.Text = "...";
            this.lblNomePaciente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNome
            // 
            this.lblNome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNome.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNome.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblNome.Location = new System.Drawing.Point(4, 44);
            this.lblNome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(249, 24);
            this.lblNome.TabIndex = 1;
            this.lblNome.Text = "Nome";
            this.lblNome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTipoDesc
            // 
            this.lbTipoDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTipoDesc.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTipoDesc.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbTipoDesc.Location = new System.Drawing.Point(4, 0);
            this.lbTipoDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTipoDesc.Name = "lbTipoDesc";
            this.lbTipoDesc.Size = new System.Drawing.Size(249, 22);
            this.lbTipoDesc.TabIndex = 0;
            this.lbTipoDesc.Text = "Atendimento";
            this.lbTipoDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDescLote
            // 
            this.lbDescLote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDescLote.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescLote.Location = new System.Drawing.Point(4, 22);
            this.lbDescLote.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDescLote.Name = "lbDescLote";
            this.lbDescLote.Size = new System.Drawing.Size(249, 22);
            this.lbDescLote.TabIndex = 0;
            this.lbDescLote.Text = "...";
            this.lbDescLote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 273F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tlpToolBar, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ucImagesManipulation1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1471, 669);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // driverFujitsu
            // 
            this.driverFujitsu.Enabled = true;
            this.driverFujitsu.Location = new System.Drawing.Point(1423, 0);
            this.driverFujitsu.Name = "driverFujitsu";
            this.driverFujitsu.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("driverFujitsu.OcxState")));
            this.driverFujitsu.Size = new System.Drawing.Size(48, 48);
            this.driverFujitsu.TabIndex = 5;
            this.driverFujitsu.ScanToFile += new AxFiScnLib._DFiScnEvents_ScanToFileEventHandler(this.driverFujitsu_ScanToFile);
            // 
            // WFCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.ClientSize = new System.Drawing.Size(1471, 697);
            this.Controls.Add(this.driverFujitsu);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "WFCapture";
            this.Text = "Captura e tratamento de imagens";
            this.TransferPictureEvent += new TwainLib.Twain.TransferPicture_Event(this.WFCapture_TransferPictureEvent);
            this.BeforeScanEvent += new ACSMinCapture.Forms.WFTwain.BeforeScan_Event(this.WFCapture_BeforeScanEvent);
            this.AfterScanEvent += new ACSMinCapture.Forms.WFTwain.AfterScan_Event(this.WFCapture_AfterScanEvent);
            this.AfterEndingScanEvent += new ACSMinCapture.Forms.WFTwain.AfterEndingScan_Event(this.WFCapture_AfterEndingScanEvent);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WFCapture_FormClosing);
            this.Load += new System.EventHandler(this.WFCapture_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tlpToolBar.ResumeLayout(false);
            this.tlpButtons2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.driverFujitsu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem arquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagensToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem exibirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagensToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmi2X2;
        private System.Windows.Forms.ToolStripMenuItem tsmi4X4;
        private System.Windows.Forms.ToolStripMenuItem preferenciasToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem excluirLoteToolStripMenuItem;
        public Controls.UCImagesManipulation ucImagesManipulation1;
        private TableLayoutPanel tlpToolBar;
        private TableLayoutPanel tlpButtons2;
        private Button btnProcessar;
        private Button btnScan;
        private Button btnLotes;
        private TableLayoutPanel tableLayoutPanel3;
        private Label lbTipoDesc;
        private Label lbDescLote;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblNomePaciente;
        private Label lblNome;
        private ToolStripMenuItem sairToolStripMenuItem;
        public MenuStrip menuStrip1;
        public ToolStripMenuItem tsmi1X1;
        public ToolStripMenuItem tsmi1X2;
        private ToolStripMenuItem açãoToolStripMenuItem;
        private ToolStripMenuItem digitalizarToolStripMenuItem;
        private ToolStripMenuItem importarToolStripMenuItem;
        private AxFiScnLib.AxFiScn driverFujitsu;

    }
}
