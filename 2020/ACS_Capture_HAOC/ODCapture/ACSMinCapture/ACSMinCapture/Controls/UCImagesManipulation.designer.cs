namespace ACSMinCapture.Controls
{
    public partial class UCImagesManipulation
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCImagesManipulation));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnQuad = new System.Windows.Forms.Button();
            this.btnRotate = new System.Windows.Forms.Button();
            this.btnEraser = new System.Windows.Forms.CheckBox();
            this.btnCrop = new System.Windows.Forms.CheckBox();
            this.btnReplicate = new System.Windows.Forms.Button();
            this.btnDuplex = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.cmsImages = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.codigoDeBarrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataValidadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.seleçãoMultiplaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.expandirTodosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.imlDocs = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ucImages = new ACSMinCapture.Controls.UCImages();
            this.tvDocs = new ACSMinCapture.Controls.TreeViewCustom(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.cmsImages.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(960, 623);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(954, 74);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.btnPreview);
            this.flowLayoutPanel1.Controls.Add(this.btnNext);
            this.flowLayoutPanel1.Controls.Add(this.btnZoomIn);
            this.flowLayoutPanel1.Controls.Add(this.btnZoomOut);
            this.flowLayoutPanel1.Controls.Add(this.btnQuad);
            this.flowLayoutPanel1.Controls.Add(this.btnRotate);
            this.flowLayoutPanel1.Controls.Add(this.btnEraser);
            this.flowLayoutPanel1.Controls.Add(this.btnCrop);
            this.flowLayoutPanel1.Controls.Add(this.btnReplicate);
            this.flowLayoutPanel1.Controls.Add(this.btnDuplex);
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(948, 68);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnPreview
            // 
            this.btnPreview.Enabled = false;
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Image = global::ACSMinCapture.Properties.Resources._1404977938_play_circle2;
            this.btnPreview.Location = new System.Drawing.Point(8, 8);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(60, 53);
            this.btnPreview.TabIndex = 21;
            this.btnPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip1.SetToolTip(this.btnPreview, "Anterior {← ↑}");
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnNext
            // 
            this.btnNext.Enabled = false;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Image = global::ACSMinCapture.Properties.Resources._1404977938_play_circle;
            this.btnNext.Location = new System.Drawing.Point(74, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(60, 53);
            this.btnNext.TabIndex = 22;
            this.toolTip1.SetToolTip(this.btnNext, "Próxima {→ ↓}");
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Enabled = false;
            this.btnZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomIn.Image = global::ACSMinCapture.Properties.Resources._1404977909_zoom_in;
            this.btnZoomIn.Location = new System.Drawing.Point(140, 8);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(60, 53);
            this.btnZoomIn.TabIndex = 14;
            this.toolTip1.SetToolTip(this.btnZoomIn, "Zoom + {Ctrl++}");
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Enabled = false;
            this.btnZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomOut.Image = global::ACSMinCapture.Properties.Resources._1404977914_zoom_out;
            this.btnZoomOut.Location = new System.Drawing.Point(206, 8);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(60, 53);
            this.btnZoomOut.TabIndex = 15;
            this.toolTip1.SetToolTip(this.btnZoomOut, "Zoom - {Ctrl+-}");
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.brnZoomOut_Click);
            // 
            // btnQuad
            // 
            this.btnQuad.Enabled = false;
            this.btnQuad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuad.Image = global::ACSMinCapture.Properties.Resources._1404977920_fullscreen;
            this.btnQuad.Location = new System.Drawing.Point(272, 8);
            this.btnQuad.Name = "btnQuad";
            this.btnQuad.Size = new System.Drawing.Size(60, 53);
            this.btnQuad.TabIndex = 17;
            this.toolTip1.SetToolTip(this.btnQuad, "Enquadrar {Ctrl+=}");
            this.btnQuad.UseVisualStyleBackColor = true;
            this.btnQuad.Click += new System.EventHandler(this.btnQuad_Click);
            // 
            // btnRotate
            // 
            this.btnRotate.Enabled = false;
            this.btnRotate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRotate.Image = global::ACSMinCapture.Properties.Resources._1404977926_retweet;
            this.btnRotate.Location = new System.Drawing.Point(338, 8);
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(60, 53);
            this.btnRotate.TabIndex = 18;
            this.toolTip1.SetToolTip(this.btnRotate, "Rotacionar {Ctrl+*}");
            this.btnRotate.UseVisualStyleBackColor = true;
            this.btnRotate.Click += new System.EventHandler(this.btnRotate_Click);
            // 
            // btnEraser
            // 
            this.btnEraser.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnEraser.Enabled = false;
            this.btnEraser.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnEraser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEraser.Image = global::ACSMinCapture.Properties.Resources._1359976693_eraser;
            this.btnEraser.Location = new System.Drawing.Point(404, 8);
            this.btnEraser.Name = "btnEraser";
            this.btnEraser.Size = new System.Drawing.Size(60, 53);
            this.btnEraser.TabIndex = 19;
            this.toolTip1.SetToolTip(this.btnEraser, "Borracha {Ctrl+B}");
            this.btnEraser.UseVisualStyleBackColor = true;
            this.btnEraser.CheckedChanged += new System.EventHandler(this.btnEraser_CheckedChanged);
            // 
            // btnCrop
            // 
            this.btnCrop.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnCrop.Enabled = false;
            this.btnCrop.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCrop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrop.Image = global::ACSMinCapture.Properties.Resources._1404977931_scissors;
            this.btnCrop.Location = new System.Drawing.Point(470, 8);
            this.btnCrop.Name = "btnCrop";
            this.btnCrop.Size = new System.Drawing.Size(60, 53);
            this.btnCrop.TabIndex = 20;
            this.toolTip1.SetToolTip(this.btnCrop, "Recortar {Ctrl+L}");
            this.btnCrop.UseVisualStyleBackColor = true;
            this.btnCrop.CheckedChanged += new System.EventHandler(this.btnCrop_CheckedChanged);
            // 
            // btnReplicate
            // 
            this.btnReplicate.Enabled = false;
            this.btnReplicate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReplicate.Image = global::ACSMinCapture.Properties.Resources._1404978673_list_alt;
            this.btnReplicate.Location = new System.Drawing.Point(536, 8);
            this.btnReplicate.Name = "btnReplicate";
            this.btnReplicate.Size = new System.Drawing.Size(59, 52);
            this.btnReplicate.TabIndex = 16;
            this.btnReplicate.UseVisualStyleBackColor = true;
            this.btnReplicate.Click += new System.EventHandler(this.btnReplicate_Click);
            // 
            // btnDuplex
            // 
            this.btnDuplex.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDuplex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.btnDuplex.Image = global::ACSMinCapture.Properties.Resources.duplexNot;
            this.btnDuplex.Location = new System.Drawing.Point(601, 8);
            this.btnDuplex.Name = "btnDuplex";
            this.btnDuplex.Size = new System.Drawing.Size(59, 52);
            this.btnDuplex.TabIndex = 23;
            this.btnDuplex.Text = "Duplex";
            this.btnDuplex.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDuplex.UseVisualStyleBackColor = true;
            this.btnDuplex.Click += new System.EventHandler(this.btnDuplex_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.btnClose.Image = global::ACSMinCapture.Properties.Resources._1404978947_678069_sign_error;
            this.btnClose.Location = new System.Drawing.Point(666, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(59, 52);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "Sair";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 83);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(954, 537);
            this.splitContainer1.SplitterDistance = 423;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ucImages);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tvDocs);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Size = new System.Drawing.Size(954, 423);
            this.splitContainer2.SplitterDistance = 816;
            this.splitContainer2.TabIndex = 2;
            // 
            // cmsImages
            // 
            this.cmsImages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.codigoDeBarrasToolStripMenuItem,
            this.dataValidadeToolStripMenuItem,
            this.toolStripMenuItem2,
            this.seleçãoMultiplaToolStripMenuItem,
            this.toolStripMenuItem3,
            this.expandirTodosToolStripMenuItem,
            this.toolStripMenuItem4});
            this.cmsImages.Name = "contextMenuStrip1";
            this.cmsImages.Size = new System.Drawing.Size(207, 138);
            this.cmsImages.Opening += new System.ComponentModel.CancelEventHandler(this.cmsImages_Opening);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(203, 6);
            // 
            // codigoDeBarrasToolStripMenuItem
            // 
            this.codigoDeBarrasToolStripMenuItem.Name = "codigoDeBarrasToolStripMenuItem";
            this.codigoDeBarrasToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.codigoDeBarrasToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.codigoDeBarrasToolStripMenuItem.Text = "Código de Barras";
            this.codigoDeBarrasToolStripMenuItem.Click += new System.EventHandler(this.codigoDeBarrasToolStripMenuItem_Click);
            // 
            // dataValidadeToolStripMenuItem
            // 
            this.dataValidadeToolStripMenuItem.Enabled = false;
            this.dataValidadeToolStripMenuItem.Name = "dataValidadeToolStripMenuItem";
            this.dataValidadeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.dataValidadeToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.dataValidadeToolStripMenuItem.Text = "Data Validade";
            this.dataValidadeToolStripMenuItem.Click += new System.EventHandler(this.dataValidadeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(203, 6);
            // 
            // seleçãoMultiplaToolStripMenuItem
            // 
            this.seleçãoMultiplaToolStripMenuItem.CheckOnClick = true;
            this.seleçãoMultiplaToolStripMenuItem.Name = "seleçãoMultiplaToolStripMenuItem";
            this.seleçãoMultiplaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.seleçãoMultiplaToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.seleçãoMultiplaToolStripMenuItem.Text = "Seleção Multipla";
            this.seleçãoMultiplaToolStripMenuItem.CheckedChanged += new System.EventHandler(this.seleçãoMultiplaToolStripMenuItem_CheckedChanged);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(203, 6);
            // 
            // expandirTodosToolStripMenuItem
            // 
            this.expandirTodosToolStripMenuItem.Checked = true;
            this.expandirTodosToolStripMenuItem.CheckOnClick = true;
            this.expandirTodosToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.expandirTodosToolStripMenuItem.Name = "expandirTodosToolStripMenuItem";
            this.expandirTodosToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.expandirTodosToolStripMenuItem.Text = "Expandir Todos";
            this.expandirTodosToolStripMenuItem.Click += new System.EventHandler(this.expandirTodosToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(203, 6);
            // 
            // imlDocs
            // 
            this.imlDocs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlDocs.ImageStream")));
            this.imlDocs.TransparentColor = System.Drawing.Color.Transparent;
            this.imlDocs.Images.SetKeyName(0, "1404782610_folder-close_Error.png");
            this.imlDocs.Images.SetKeyName(1, "1404783187_picture.png");
            this.imlDocs.Images.SetKeyName(2, "1404782610_folder-close_Check.png");
            this.imlDocs.Images.SetKeyName(3, "1404782610_folder-close_Search.png");
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 376);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 47);
            this.label1.TabIndex = 5;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(954, 110);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(946, 84);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Detalhes da Imagem";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(940, 78);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(946, 84);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Código de Barras";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(940, 78);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // ucImages
            // 
            this.ucImages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucImages.ColorsSelected = new System.Drawing.Color[] {
        System.Drawing.Color.Gainsboro,
        System.Drawing.Color.White};
            this.ucImages.ColorsUnSelected = new System.Drawing.Color[] {
        System.Drawing.Color.Silver,
        System.Drawing.Color.White};
            this.ucImages.Columns = 2;
            this.ucImages.ContextMenuStrip = this.cmsImages;
            this.ucImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucImages.Location = new System.Drawing.Point(0, 0);
            this.ucImages.Margin = new System.Windows.Forms.Padding(4);
            this.ucImages.Name = "ucImages";
            this.ucImages.Rows = 1;
            this.ucImages.Selected = null;
            this.ucImages.Size = new System.Drawing.Size(816, 423);
            this.ucImages.TabIndex = 2;
            this.ucImages.ZoomFitOnLoadBitmap = true;
            this.ucImages.AfterImageClick += new ACSMinCapture.Controls.UCImages.AfterImageClickEvent(this.ucImages_AfterImageClick);
            this.ucImages.AfterImageKeyDown += new ACSMinCapture.Controls.UCImages.AfterImageKeyDownEvent(this.ucImages_AfterImageKeyDown);
            this.ucImages.AfterLoadFromFile += new ACSMinCapture.Controls.UCImages.AfterLoadFromFileEvent(this.ucImages_AfterLoadFromFile);
            this.ucImages.AfterClearAllImages += new ACSMinCapture.Controls.UCImages.AfterClearAllImagesEvent(this.ucImages_AfterClearAllImages);
            this.ucImages.Load += new System.EventHandler(this.ucImages_Load);
            // 
            // tvDocs
            // 
            this.tvDocs.AllowDrop = true;
            this.tvDocs.ContextMenuStrip = this.cmsImages;
            this.tvDocs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDocs.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvDocs.HideSelection = false;
            this.tvDocs.ImageIndex = 0;
            this.tvDocs.ImageIndexDocumentNotValid = 0;
            this.tvDocs.ImageIndexDocumentOCR = 3;
            this.tvDocs.ImageIndexDocumentValid = 2;
            this.tvDocs.ImageIndexPageNotValid = 1;
            this.tvDocs.ImageIndexPageOCR = 1;
            this.tvDocs.ImageIndexPageValid = 1;
            this.tvDocs.ImageList = this.imlDocs;
            this.tvDocs.Location = new System.Drawing.Point(0, 0);
            this.tvDocs.MaskDocumentName = null;
            this.tvDocs.MaskPageName = null;
            this.tvDocs.Name = "tvDocs";
            this.tvDocs.SelectedImageIndex = 0;
            this.tvDocs.ShowNodeToolTips = true;
            this.tvDocs.Size = new System.Drawing.Size(134, 376);
            this.tvDocs.TabIndex = 4;
            this.tvDocs.AfterOnDragDrop += new ACSMinCapture.Controls.TreeViewCustom.AfterOnDragDropEvent(this.tvDocs_AfterOnDragDrop);
            this.tvDocs.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvDocs_BeforeSelect);
            this.tvDocs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDocs_AfterSelect);
            this.tvDocs.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvDocs_DragDrop);
            // 
            // UCImagesManipulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCImagesManipulation";
            this.Size = new System.Drawing.Size(960, 623);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.cmsImages.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnReplicate;
        private System.Windows.Forms.ImageList imlDocs;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem codigoDeBarrasToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem dataValidadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem seleçãoMultiplaToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem expandirTodosToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Button btnPreview;
        public System.Windows.Forms.Button btnNext;
        public System.Windows.Forms.Button btnZoomIn;
        public System.Windows.Forms.Button btnZoomOut;
        public System.Windows.Forms.Button btnQuad;
        public System.Windows.Forms.Button btnRotate;
        public System.Windows.Forms.CheckBox btnEraser;
        public System.Windows.Forms.CheckBox btnCrop;
        public System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        public System.Windows.Forms.ContextMenuStrip cmsImages;
        public System.Windows.Forms.Button btnDuplex;
        public System.Windows.Forms.Button btnClose;
        public UCImages ucImages;
        public TreeViewCustom tvDocs;
        //private System.Windows.Forms.Button btnAssinador;


    }
}
