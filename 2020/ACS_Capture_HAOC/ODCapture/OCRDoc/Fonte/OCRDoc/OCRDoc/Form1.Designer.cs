namespace OCRDoc
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button4 = new System.Windows.Forms.Button();
            this.btnAddZone = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imageBox1 = new Cyotek.Windows.Forms.ImageBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.OCR1 = new OCRTools.OCR(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.84733F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.15267F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(725, 428);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.button4);
            this.flowLayoutPanel1.Controls.Add(this.btnAddZone);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Controls.Add(this.button3);
            this.flowLayoutPanel1.Controls.Add(this.button5);
            this.flowLayoutPanel1.Controls.Add(this.button6);
            this.flowLayoutPanel1.Controls.Add(this.button7);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(719, 51);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(3, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "Load Image";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnAddZone
            // 
            this.btnAddZone.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnAddZone.AutoSize = true;
            this.btnAddZone.Location = new System.Drawing.Point(84, 3);
            this.btnAddZone.Name = "btnAddZone";
            this.btnAddZone.Size = new System.Drawing.Size(64, 23);
            this.btnAddZone.TabIndex = 1;
            this.btnAddZone.Text = "Add Zone";
            this.btnAddZone.UseVisualStyleBackColor = true;
            this.btnAddZone.CheckedChanged += new System.EventHandler(this.btnAddZone_CheckedChanged);
            this.btnAddZone.Click += new System.EventHandler(this.btnAddZone_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(154, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Zoom+";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(235, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Zoom-";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(316, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "OCR";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(397, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(99, 23);
            this.button5.TabIndex = 6;
            this.button5.Text = "Save Schema";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(502, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(107, 23);
            this.button6.TabIndex = 7;
            this.button6.Text = "Load Schema";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(615, 3);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(92, 23);
            this.button7.TabIndex = 8;
            this.button7.Text = "Clear Schema";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.imageBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(719, 226);
            this.panel1.TabIndex = 1;
            // 
            // imageBox1
            // 
            this.imageBox1.AllowClickZoom = false;
            this.imageBox1.AutoPan = false;
            this.imageBox1.AutoSize = false;
            this.imageBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox1.GridDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.None;
            this.imageBox1.Location = new System.Drawing.Point(0, 0);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(719, 226);
            this.imageBox1.TabIndex = 0;
            this.imageBox1.Click += new System.EventHandler(this.imageBox1_Click);
            this.imageBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.imageBox1_Paint);
            this.imageBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imageBox1_MouseClick);
            this.imageBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageBox1_MouseDown);
            this.imageBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageBox1_MouseMove);
            this.imageBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageBox1_MouseUp);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 292);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(719, 133);
            this.textBox1.TabIndex = 2;
            // 
            // OCR1
            // 
            this.OCR1.Abort = false;
            this.OCR1.AlternateExceptionList = new int[] {
        0};
            this.OCR1.AnalyzeAutomatic = true;
            this.OCR1.AnalyzeBrightnessIncrements = 0.5D;
            this.OCR1.AnalyzeDefaultBrightness = 7D;
            this.OCR1.AnalyzeDefaultResizeBitmap = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.OCR1.AnalyzeEndingBrightness = 9D;
            this.OCR1.AnalyzeEndingResizeBitmap = new decimal(new int[] {
            30,
            0,
            0,
            65536});
            this.OCR1.AnalyzeResizeBitmapIncrements = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.OCR1.AnalyzeStartingBrightness = 5D;
            this.OCR1.AnalyzeStartingFontColorContrast = 50;
            this.OCR1.AnalyzeStartingResizeBitmap = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.OCR1.BitmapImage = ((System.Drawing.Bitmap)(resources.GetObject("OCR1.BitmapImage")));
            this.OCR1.BitmapImageFile = "";
            this.OCR1.Brightness = 7D;
            this.OCR1.BrightnessMargin = 0D;
            this.OCR1.BrightnessMarginIncrements = 0D;
            this.OCR1.DefaultFolder = "";
            this.OCR1.DisplayChecksum = false;
            this.OCR1.DisplayErrors = false;
            this.OCR1.EnforceSpaceRules = false;
            this.OCR1.ErrorCharacter = '?';
            this.OCR1.ErrorCorrection = OCRTools.ErrorCorrectionTypes.None;
            this.OCR1.FontColor = System.Drawing.Color.Black;
            this.OCR1.FontColorContrast = 0;
            this.OCR1.Language = OCRTools.LanguageType.English;
            this.OCR1.MaximumBarHeight = 500;
            this.OCR1.MaximumBarWidth = 100;
            this.OCR1.MaximumCharacters = 5000;
            this.OCR1.MaximumHeight = 150;
            this.OCR1.MaximumSize = 10000;
            this.OCR1.MaximumWidth = 150;
            this.OCR1.MinimumBarHeight = 20;
            this.OCR1.MinimumBarWidth = 1;
            this.OCR1.MinimumConfidence = new decimal(new int[] {
            1,
            0,
            0,
            458752});
            this.OCR1.MinimumHeight = 1;
            this.OCR1.MinimumSize = 2;
            this.OCR1.MinimumSpace = 4;
            this.OCR1.MinimumWidth = 1;
            this.OCR1.RemoveHorizontal = 0;
            this.OCR1.RemoveVertical = 0;
            this.OCR1.ResizeBitmap = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.OCR1.SectionHorizontalSpace = 1.5D;
            this.OCR1.SectionVerticalSpace = 1.5D;
            this.OCR1.StartStopCodesCharacter = "*";
            this.OCR1.StartStopCodesDisplay = false;
            this.OCR1.StartStopCodesRequired = false;
            this.OCR1.Statistics = false;
            this.OCR1.ThinCharacters = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 428);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox btnAddZone;
        private Cyotek.Windows.Forms.ImageBox imageBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button4;
        private OCRTools.OCR OCR1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
    }
}

