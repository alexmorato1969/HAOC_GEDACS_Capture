namespace ACSMinCapture
{
    partial class WFAssinaNivel2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WFAssinaNivel2));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dtgv_DocsN2 = new System.Windows.Forms.DataGridView();
            this.DOC_IDDOCUMENTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPF_NOME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NUMEROATENDIMENTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAATENDIMENTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PAS_REGISTRO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STD_Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOC_NOMEARQUIVO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOC_DATAHORACADASTRO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOC_ASSINATURA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Imagem = new System.Windows.Forms.DataGridViewButtonColumn();
            this.DOC_DIVISAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PAS_CODIGOPASSAGEM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOC_EXTENSAONOMEARQUIVO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pctSelected = new ACSMinCapture.Forms.ImagePanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnAllInfo = new System.Windows.Forms.Panel();
            this.pn02 = new System.Windows.Forms.Panel();
            this.lblTotalDocumentosSelecionados = new System.Windows.Forms.Label();
            this.lblNumeroDocumentosSelecionados = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblNumeroDocumentos = new System.Windows.Forms.Label();
            this.lblTotalDocumentos = new System.Windows.Forms.Label();
            this.pnBtnAll = new System.Windows.Forms.Panel();
            this.btnSelecionarTodos = new System.Windows.Forms.Button();
            this.lblInformativo = new System.Windows.Forms.Label();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnCoAssinador = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.pn01 = new System.Windows.Forms.GroupBox();
            this.chkVerTodos = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDataAtendimento = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumeroAtendimento = new System.Windows.Forms.TextBox();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_DocsN2)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnAllInfo.SuspendLayout();
            this.pn02.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnBtnAll.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pn01.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.dtgv_DocsN2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pctSelected, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 127);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1095, 472);
            this.tableLayoutPanel1.TabIndex = 27;
            // 
            // dtgv_DocsN2
            // 
            this.dtgv_DocsN2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgv_DocsN2.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dtgv_DocsN2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgv_DocsN2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgv_DocsN2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DOC_IDDOCUMENTO,
            this.CPF_NOME,
            this.CPF,
            this.NUMEROATENDIMENTO,
            this.DATAATENDIMENTO,
            this.PAS_REGISTRO,
            this.STD_Nome,
            this.DOC_NOMEARQUIVO,
            this.DOC_DATAHORACADASTRO,
            this.DOC_ASSINATURA,
            this.Imagem,
            this.DOC_DIVISAO,
            this.PAS_CODIGOPASSAGEM,
            this.DOC_EXTENSAONOMEARQUIVO});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgv_DocsN2.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtgv_DocsN2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgv_DocsN2.Location = new System.Drawing.Point(4, 4);
            this.dtgv_DocsN2.Margin = new System.Windows.Forms.Padding(4);
            this.dtgv_DocsN2.Name = "dtgv_DocsN2";
            this.dtgv_DocsN2.ReadOnly = true;
            this.dtgv_DocsN2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dtgv_DocsN2.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dtgv_DocsN2.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 10F);
            this.dtgv_DocsN2.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgv_DocsN2.RowTemplate.Height = 35;
            this.dtgv_DocsN2.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgv_DocsN2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgv_DocsN2.Size = new System.Drawing.Size(1, 464);
            this.dtgv_DocsN2.TabIndex = 1;
            this.dtgv_DocsN2.Visible = false;
            this.dtgv_DocsN2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgv_DocsN2_CellContentClick);
            this.dtgv_DocsN2.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgv_DocsN2_CellContentDoubleClick);
            this.dtgv_DocsN2.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dtgv_DocsN2_CellPainting);
            this.dtgv_DocsN2.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dtgv_DocsN2_DataBindingComplete);
            this.dtgv_DocsN2.SelectionChanged += new System.EventHandler(this.dtgv_DocsN2_SelectionChanged);
            // 
            // DOC_IDDOCUMENTO
            // 
            this.DOC_IDDOCUMENTO.DataPropertyName = "DOC_IDDOCUMENTO";
            this.DOC_IDDOCUMENTO.HeaderText = "DOC_IDDOCUMENTO";
            this.DOC_IDDOCUMENTO.Name = "DOC_IDDOCUMENTO";
            this.DOC_IDDOCUMENTO.ReadOnly = true;
            this.DOC_IDDOCUMENTO.Visible = false;
            // 
            // CPF_NOME
            // 
            this.CPF_NOME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CPF_NOME.DataPropertyName = "CPF_NOME";
            this.CPF_NOME.HeaderText = "Cliente";
            this.CPF_NOME.Name = "CPF_NOME";
            this.CPF_NOME.ReadOnly = true;
            // 
            // CPF
            // 
            this.CPF.DataPropertyName = "CPF_CPF";
            this.CPF.HeaderText = "CPF";
            this.CPF.Name = "CPF";
            this.CPF.ReadOnly = true;
            // 
            // NUMEROATENDIMENTO
            // 
            this.NUMEROATENDIMENTO.DataPropertyName = "PAS_CODIGOPASSAGEM";
            this.NUMEROATENDIMENTO.HeaderText = "Número Atendimento";
            this.NUMEROATENDIMENTO.Name = "NUMEROATENDIMENTO";
            this.NUMEROATENDIMENTO.ReadOnly = true;
            // 
            // DATAATENDIMENTO
            // 
            this.DATAATENDIMENTO.DataPropertyName = "PAS_DATAHORAPASSAGEM";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DATAATENDIMENTO.DefaultCellStyle = dataGridViewCellStyle1;
            this.DATAATENDIMENTO.HeaderText = "Data Atendimento";
            this.DATAATENDIMENTO.Name = "DATAATENDIMENTO";
            this.DATAATENDIMENTO.ReadOnly = true;
            // 
            // PAS_REGISTRO
            // 
            this.PAS_REGISTRO.DataPropertyName = "PAS_REGISTRO";
            this.PAS_REGISTRO.HeaderText = "Prontuário";
            this.PAS_REGISTRO.Name = "PAS_REGISTRO";
            this.PAS_REGISTRO.ReadOnly = true;
            // 
            // STD_Nome
            // 
            this.STD_Nome.DataPropertyName = "STD_Nome";
            this.STD_Nome.HeaderText = "Tipo";
            this.STD_Nome.Name = "STD_Nome";
            this.STD_Nome.ReadOnly = true;
            // 
            // DOC_NOMEARQUIVO
            // 
            this.DOC_NOMEARQUIVO.DataPropertyName = "DOC_NOMEARQUIVO";
            this.DOC_NOMEARQUIVO.HeaderText = "Arquivo";
            this.DOC_NOMEARQUIVO.Name = "DOC_NOMEARQUIVO";
            this.DOC_NOMEARQUIVO.ReadOnly = true;
            this.DOC_NOMEARQUIVO.Visible = false;
            // 
            // DOC_DATAHORACADASTRO
            // 
            this.DOC_DATAHORACADASTRO.DataPropertyName = "DOC_DATAHORACADASTRO";
            this.DOC_DATAHORACADASTRO.HeaderText = "Capturado Em";
            this.DOC_DATAHORACADASTRO.Name = "DOC_DATAHORACADASTRO";
            this.DOC_DATAHORACADASTRO.ReadOnly = true;
            // 
            // DOC_ASSINATURA
            // 
            this.DOC_ASSINATURA.DataPropertyName = "DOC_ASSINATURA";
            this.DOC_ASSINATURA.HeaderText = "Assinatura em:";
            this.DOC_ASSINATURA.Name = "DOC_ASSINATURA";
            this.DOC_ASSINATURA.ReadOnly = true;
            // 
            // Imagem
            // 
            this.Imagem.DataPropertyName = "DOC_NOMEARQUIVO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Imagem.DefaultCellStyle = dataGridViewCellStyle2;
            this.Imagem.HeaderText = "Documento";
            this.Imagem.Name = "Imagem";
            this.Imagem.ReadOnly = true;
            // 
            // DOC_DIVISAO
            // 
            this.DOC_DIVISAO.DataPropertyName = "DOC_DIVISAO";
            this.DOC_DIVISAO.HeaderText = "DOC_DIVISAO";
            this.DOC_DIVISAO.Name = "DOC_DIVISAO";
            this.DOC_DIVISAO.ReadOnly = true;
            this.DOC_DIVISAO.Visible = false;
            // 
            // PAS_CODIGOPASSAGEM
            // 
            this.PAS_CODIGOPASSAGEM.DataPropertyName = "PAS_CODIGOPASSAGEM";
            this.PAS_CODIGOPASSAGEM.HeaderText = "PAS_CODIGOPASSAGEM";
            this.PAS_CODIGOPASSAGEM.Name = "PAS_CODIGOPASSAGEM";
            this.PAS_CODIGOPASSAGEM.ReadOnly = true;
            this.PAS_CODIGOPASSAGEM.Visible = false;
            // 
            // DOC_EXTENSAONOMEARQUIVO
            // 
            this.DOC_EXTENSAONOMEARQUIVO.DataPropertyName = "DOC_EXTENSAONOMEARQUIVO";
            this.DOC_EXTENSAONOMEARQUIVO.HeaderText = "DOC_EXTENSAONOMEARQUIVO";
            this.DOC_EXTENSAONOMEARQUIVO.Name = "DOC_EXTENSAONOMEARQUIVO";
            this.DOC_EXTENSAONOMEARQUIVO.ReadOnly = true;
            this.DOC_EXTENSAONOMEARQUIVO.Visible = false;
            // 
            // pctSelected
            // 
            this.pctSelected.AutoSize = true;
            this.pctSelected.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pctSelected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctSelected.CanvasSize = new System.Drawing.Size(500, 500);
            this.pctSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pctSelected.Image = null;
            this.pctSelected.ImeMode = System.Windows.Forms.ImeMode.On;
            this.pctSelected.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            this.pctSelected.Location = new System.Drawing.Point(-6987, 5);
            this.pctSelected.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pctSelected.Name = "pctSelected";
            this.pctSelected.Size = new System.Drawing.Size(8078, 462);
            this.pctSelected.TabIndex = 2;
            this.pctSelected.Zoom = 1F;
            this.pctSelected.Load += new System.EventHandler(this.pctSelected_Load);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnAllInfo);
            this.panel2.Controls.Add(this.pnBtnAll);
            this.panel2.Controls.Add(this.lblInformativo);
            this.panel2.Controls.Add(this.btnVoltar);
            this.panel2.Controls.Add(this.btnCoAssinador);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 599);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1095, 134);
            this.panel2.TabIndex = 26;
            // 
            // pnAllInfo
            // 
            this.pnAllInfo.Controls.Add(this.pn02);
            this.pnAllInfo.Controls.Add(this.panel3);
            this.pnAllInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnAllInfo.Location = new System.Drawing.Point(363, 0);
            this.pnAllInfo.Margin = new System.Windows.Forms.Padding(4);
            this.pnAllInfo.Name = "pnAllInfo";
            this.pnAllInfo.Size = new System.Drawing.Size(544, 134);
            this.pnAllInfo.TabIndex = 31;
            this.pnAllInfo.Visible = false;
            // 
            // pn02
            // 
            this.pn02.Controls.Add(this.lblTotalDocumentosSelecionados);
            this.pn02.Controls.Add(this.lblNumeroDocumentosSelecionados);
            this.pn02.Dock = System.Windows.Forms.DockStyle.Top;
            this.pn02.Location = new System.Drawing.Point(0, 39);
            this.pn02.Margin = new System.Windows.Forms.Padding(4);
            this.pn02.Name = "pn02";
            this.pn02.Size = new System.Drawing.Size(544, 39);
            this.pn02.TabIndex = 35;
            // 
            // lblTotalDocumentosSelecionados
            // 
            this.lblTotalDocumentosSelecionados.AutoSize = true;
            this.lblTotalDocumentosSelecionados.Location = new System.Drawing.Point(4, 13);
            this.lblTotalDocumentosSelecionados.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalDocumentosSelecionados.Name = "lblTotalDocumentosSelecionados";
            this.lblTotalDocumentosSelecionados.Size = new System.Drawing.Size(269, 21);
            this.lblTotalDocumentosSelecionados.TabIndex = 32;
            this.lblTotalDocumentosSelecionados.Text = "Total de Documentos selecionados";
            // 
            // lblNumeroDocumentosSelecionados
            // 
            this.lblNumeroDocumentosSelecionados.AutoSize = true;
            this.lblNumeroDocumentosSelecionados.Location = new System.Drawing.Point(282, 13);
            this.lblNumeroDocumentosSelecionados.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumeroDocumentosSelecionados.Name = "lblNumeroDocumentosSelecionados";
            this.lblNumeroDocumentosSelecionados.Size = new System.Drawing.Size(19, 21);
            this.lblNumeroDocumentosSelecionados.TabIndex = 31;
            this.lblNumeroDocumentosSelecionados.Text = "0";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblNumeroDocumentos);
            this.panel3.Controls.Add(this.lblTotalDocumentos);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(544, 39);
            this.panel3.TabIndex = 34;
            // 
            // lblNumeroDocumentos
            // 
            this.lblNumeroDocumentos.AutoSize = true;
            this.lblNumeroDocumentos.Location = new System.Drawing.Point(282, 13);
            this.lblNumeroDocumentos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumeroDocumentos.Name = "lblNumeroDocumentos";
            this.lblNumeroDocumentos.Size = new System.Drawing.Size(19, 21);
            this.lblNumeroDocumentos.TabIndex = 31;
            this.lblNumeroDocumentos.Text = "0";
            // 
            // lblTotalDocumentos
            // 
            this.lblTotalDocumentos.AutoSize = true;
            this.lblTotalDocumentos.Location = new System.Drawing.Point(4, 13);
            this.lblTotalDocumentos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalDocumentos.Name = "lblTotalDocumentos";
            this.lblTotalDocumentos.Size = new System.Drawing.Size(273, 21);
            this.lblTotalDocumentos.TabIndex = 30;
            this.lblTotalDocumentos.Text = "Total de Documentos para Assinar:";
            // 
            // pnBtnAll
            // 
            this.pnBtnAll.Controls.Add(this.btnSelecionarTodos);
            this.pnBtnAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnBtnAll.Location = new System.Drawing.Point(188, 0);
            this.pnBtnAll.Margin = new System.Windows.Forms.Padding(4);
            this.pnBtnAll.Name = "pnBtnAll";
            this.pnBtnAll.Size = new System.Drawing.Size(175, 134);
            this.pnBtnAll.TabIndex = 30;
            // 
            // btnSelecionarTodos
            // 
            this.btnSelecionarTodos.BackColor = System.Drawing.Color.White;
            this.btnSelecionarTodos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelecionarTodos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionarTodos.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionarTodos.ForeColor = System.Drawing.Color.Black;
            this.btnSelecionarTodos.Image = global::ACSMinCapture.Properties.Resources._1404978673_list_alt;
            this.btnSelecionarTodos.Location = new System.Drawing.Point(0, 0);
            this.btnSelecionarTodos.Margin = new System.Windows.Forms.Padding(13, 4, 4, 4);
            this.btnSelecionarTodos.Name = "btnSelecionarTodos";
            this.btnSelecionarTodos.Size = new System.Drawing.Size(175, 134);
            this.btnSelecionarTodos.TabIndex = 34;
            this.btnSelecionarTodos.Text = "Selecionar Todos";
            this.btnSelecionarTodos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSelecionarTodos.UseVisualStyleBackColor = false;
            this.btnSelecionarTodos.Visible = false;
            this.btnSelecionarTodos.Click += new System.EventHandler(this.btnSelecionarTodos_Click);
            // 
            // lblInformativo
            // 
            this.lblInformativo.AutoSize = true;
            this.lblInformativo.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblInformativo.Font = new System.Drawing.Font("Tahoma", 14F);
            this.lblInformativo.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblInformativo.Location = new System.Drawing.Point(907, 0);
            this.lblInformativo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInformativo.Name = "lblInformativo";
            this.lblInformativo.Size = new System.Drawing.Size(0, 29);
            this.lblInformativo.TabIndex = 28;
            this.lblInformativo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnVoltar
            // 
            this.btnVoltar.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnVoltar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoltar.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.Image = global::ACSMinCapture.Properties.Resources._1404977938_play_circle2;
            this.btnVoltar.Location = new System.Drawing.Point(0, 0);
            this.btnVoltar.Margin = new System.Windows.Forms.Padding(4);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(188, 134);
            this.btnVoltar.TabIndex = 27;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Visible = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // btnCoAssinador
            // 
            this.btnCoAssinador.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCoAssinador.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCoAssinador.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCoAssinador.Image = ((System.Drawing.Image)(resources.GetObject("btnCoAssinador.Image")));
            this.btnCoAssinador.Location = new System.Drawing.Point(907, 0);
            this.btnCoAssinador.Margin = new System.Windows.Forms.Padding(4);
            this.btnCoAssinador.Name = "btnCoAssinador";
            this.btnCoAssinador.Size = new System.Drawing.Size(188, 134);
            this.btnCoAssinador.TabIndex = 26;
            this.btnCoAssinador.Text = "Assinar Documentos";
            this.btnCoAssinador.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCoAssinador.UseVisualStyleBackColor = true;
            this.btnCoAssinador.Click += new System.EventHandler(this.btnCoAssinador_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.pn01);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnPesquisa);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1095, 127);
            this.panel1.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ACSMinCapture.Properties.Resources.iconClose;
            this.btnClose.Location = new System.Drawing.Point(1063, 18);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(131, 99);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Sair";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pn01
            // 
            this.pn01.Controls.Add(this.chkVerTodos);
            this.pn01.Font = new System.Drawing.Font("Verdana", 12F);
            this.pn01.Location = new System.Drawing.Point(612, 18);
            this.pn01.Margin = new System.Windows.Forms.Padding(4);
            this.pn01.Name = "pn01";
            this.pn01.Padding = new System.Windows.Forms.Padding(4);
            this.pn01.Size = new System.Drawing.Size(257, 99);
            this.pn01.TabIndex = 9;
            this.pn01.TabStop = false;
            this.pn01.Text = "Opção 3";
            // 
            // chkVerTodos
            // 
            this.chkVerTodos.AutoSize = true;
            this.chkVerTodos.Location = new System.Drawing.Point(44, 44);
            this.chkVerTodos.Margin = new System.Windows.Forms.Padding(4);
            this.chkVerTodos.Name = "chkVerTodos";
            this.chkVerTodos.Size = new System.Drawing.Size(131, 29);
            this.chkVerTodos.TabIndex = 5;
            this.chkVerTodos.Text = "Ver Todos";
            this.chkVerTodos.UseVisualStyleBackColor = true;
            this.chkVerTodos.CheckedChanged += new System.EventHandler(this.chkVerTodos_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtDataAtendimento);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 12F);
            this.groupBox2.Location = new System.Drawing.Point(360, 18);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(244, 99);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opção 2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Data Atendimento:";
            // 
            // txtDataAtendimento
            // 
            this.txtDataAtendimento.Location = new System.Drawing.Point(8, 56);
            this.txtDataAtendimento.Margin = new System.Windows.Forms.Padding(4);
            this.txtDataAtendimento.Mask = "99/99/9999";
            this.txtDataAtendimento.Name = "txtDataAtendimento";
            this.txtDataAtendimento.Size = new System.Drawing.Size(172, 32);
            this.txtDataAtendimento.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNumeroAtendimento);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 12F);
            this.groupBox1.Location = new System.Drawing.Point(15, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(323, 99);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opção 1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Número Atendimento:";
            // 
            // txtNumeroAtendimento
            // 
            this.txtNumeroAtendimento.Location = new System.Drawing.Point(12, 58);
            this.txtNumeroAtendimento.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumeroAtendimento.Name = "txtNumeroAtendimento";
            this.txtNumeroAtendimento.Size = new System.Drawing.Size(297, 32);
            this.txtNumeroAtendimento.TabIndex = 3;
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisa.Image")));
            this.btnPesquisa.Location = new System.Drawing.Point(912, 18);
            this.btnPesquisa.Margin = new System.Windows.Forms.Padding(4);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(131, 99);
            this.btnPesquisa.TabIndex = 6;
            this.btnPesquisa.Text = "Pesquisar";
            this.btnPesquisa.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // WFAssinaNivel2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.ClientSize = new System.Drawing.Size(1095, 733);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "WFAssinaNivel2";
            this.Opacity = 0.90000000000000024D;
            this.Text = "Selecione o tipo de processo ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WFTipoAcao_FormClosing);
            this.Load += new System.EventHandler(this.WFTipoAcao_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.WFAssinaNivel2_KeyUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.WFAssinaNivel2_PreviewKeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_DocsN2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnAllInfo.ResumeLayout(false);
            this.pn02.ResumeLayout(false);
            this.pn02.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnBtnAll.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pn01.ResumeLayout(false);
            this.pn01.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkVerTodos;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumeroAtendimento;
        private System.Windows.Forms.GroupBox pn01;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox txtDataAtendimento;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCoAssinador;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dtgv_DocsN2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Forms.ImagePanel pctSelected;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOC_IDDOCUMENTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPF_NOME;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPF;
        private System.Windows.Forms.DataGridViewTextBoxColumn NUMEROATENDIMENTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAATENDIMENTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PAS_REGISTRO;
        private System.Windows.Forms.DataGridViewTextBoxColumn STD_Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOC_NOMEARQUIVO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOC_DATAHORACADASTRO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOC_ASSINATURA;
        private System.Windows.Forms.DataGridViewButtonColumn Imagem;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOC_DIVISAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PAS_CODIGOPASSAGEM;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOC_EXTENSAONOMEARQUIVO;
        private System.Windows.Forms.Label lblInformativo;
        private System.Windows.Forms.Panel pnAllInfo;
        private System.Windows.Forms.Panel pn02;
        private System.Windows.Forms.Label lblTotalDocumentosSelecionados;
        private System.Windows.Forms.Label lblNumeroDocumentosSelecionados;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblNumeroDocumentos;
        private System.Windows.Forms.Label lblTotalDocumentos;
        private System.Windows.Forms.Panel pnBtnAll;
        private System.Windows.Forms.Button btnSelecionarTodos;




    }
}
