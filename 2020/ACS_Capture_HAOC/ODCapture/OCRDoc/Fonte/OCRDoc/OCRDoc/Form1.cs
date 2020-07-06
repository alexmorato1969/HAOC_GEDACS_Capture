using Cyotek.Windows.Forms;
using OCRTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCRDoc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        List<RectangleF> Schema = new List<RectangleF>();
        Nicomsoft_Recognize NSOCR;

    
        private void imageBox1_MouseDown(object sender, MouseEventArgs e)
        {
    
        }

        private void imageBox1_MouseUp(object sender, MouseEventArgs e)
        {
            var img = (ImageBox)sender;

            if (this.btnAddZone.Checked)
            {
                Schema.Add(this.imageBox1.SelectionRegion);

                this.NSOCR.AddZone(this.imageBox1.SelectionRegion);               

                this.btnAddZone.Checked = false;
                this.imageBox1.SelectionMode = ImageBoxSelectionMode.None;
                this.imageBox1.SelectNone();
                img.AutoPan = true;
            }
        }

        PointF MousePositionInImage(MouseEventArgs e)
        {

            var _startMousePosition = e.Location;

            float x;
            float y;

            Point imageOffset;

            imageOffset = this.imageBox1.GetImageViewPort().Location;

            if (e.X < _startMousePosition.X)
            {
                x = e.X;
            }
            else
            {
                x = _startMousePosition.X;
            }

            if (e.Y < _startMousePosition.Y)
            {
                y = e.Y;
            }
            else
            {
                y = _startMousePosition.Y;
            }

            x = x - imageOffset.X - this.imageBox1.AutoScrollPosition.X;
            y = y - imageOffset.Y - this.imageBox1.AutoScrollPosition.Y;

            x = x / (float)this.imageBox1.ZoomFactor;
            y = y / (float)this.imageBox1.ZoomFactor;

           return new PointF(x, y);

        }



        protected virtual void DrawSelection(PaintEventArgs e, RectangleF rectReg)
        {
            RectangleF rect;

            e.Graphics.SetClip(this.imageBox1.GetInsideViewPort(true));

            rect = this.imageBox1.GetOffsetRectangle(rectReg);

            using (Brush brush = new SolidBrush(Color.FromArgb(64, this.imageBox1.SelectionColor)))
                e.Graphics.FillRectangle(brush, rect);

            using (Pen pen = new Pen(this.imageBox1.SelectionColor))
                e.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);

            e.Graphics.ResetClip();

        }

        
        private void imageBox1_Paint(object sender, PaintEventArgs e)
        {
            var img = (ImageBox)sender;
            foreach (var rec in Schema)
            {
                DrawSelection(e, rec);
            }            
        }

      
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.imageBox1.ZoomIn();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.imageBox1.ZoomOut();
        }

        private void imageBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                var pos = MousePositionInImage(e);
                //var pos = e.Location;

                int index = -1;
                foreach(var rect in Schema)
                {
                    index++;
                    if (pos.X >= rect.X && pos.Y >= rect.Y && pos.X <= (rect.X + rect.Width) && pos.Y <= (rect.Y + rect.Height))
                    {
                        if (MessageBox.Show("Deseja deletar Zona?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Schema.Remove(rect);
                            this.NSOCR.DeleteZone(index);
                            this.imageBox1.Update();
                            this.imageBox1.Invalidate();
                            break;
                        }
                    }
                }
            }
        }

        private void btnAddZone_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void imageBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void btnAddZone_Click(object sender, EventArgs e)
        {
            this.imageBox1.AutoPan = !(sender as CheckBox).Checked;
            if ((sender as CheckBox).Checked)
            {
                this.imageBox1.SelectionMode = ImageBoxSelectionMode.Rectangle;
            }
            else
            {
                this.imageBox1.SelectionMode = ImageBoxSelectionMode.None;
            }
        }

        private void imageBox1_Click(object sender, EventArgs e)
        {
            
        }

        void OCRTools_Recognize()
        {
            var i = 0;
            this.textBox1.Text = string.Empty;
            foreach (var rect in Schema)
            {
                i++;
                //var ocr1 = new OCR();
                //ocr1.BarcodeType = BarcodeTypes.All;
                //ocr1.OCRType = OCRType.Text;

                //ocr.ProductName = "StandardBar";
                //ocr.CustomerName = "Version5";
                //ocr.OrderID = "5152";
                //ocr.RegistrationCodes = "5445-3139-8282-8844";
                //ocr.ActivationKey = "6930-2481-1896-4395";


                //ocr1.ProductName = "Demo";
                //ocr1.CustomerName = "Demo";
                //ocr1.OrderID = "Demo";
                //ocr1.RegistrationCodes = "Demo";
                //ocr1.ActivationKey = "Demo";


                //ocr.DisplayChecksum = false;
                //ocr.CalculateChecksum = true;
                //ocr.DisplayErrors = false;
                //ocr.EnforceSpaceRules = false;
                //ocr.StartStopCodesDisplay = false;
                //ocr.StartStopCodesRequired = false;
                //ocr.Statistics = false;
                //ocr.ThinCharacters = true;
                //ocr.Abort = false;
                //ocr.ErrorCorrection = OCRTools.ErrorCorrectionTypes.None;
                //ocr.AnalyzeAutomatic = true;
                //ocr1.StartStopCodesDisplay = false;
                //ocr1.StartStopCodesRequired = false;
                //var picB = new PictureBox();
                //ocr.Statistics = false;
                //ocr.ThinCharacters = true;
                //OCR1.LoadPictureBox(this.FileName);
                OCR1.BitmapImageFile = this.FileName;
                //ocr1.BitmapImage = new Bitmap(this.imageBox1.Image);
                OCR1.SetRegion((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
                var processOCR = OCR1.Process_Start();
                while (processOCR.IsAlive)
                {
                    Application.DoEvents();
                }

                if (OCR1.ThreadError)
                    System.Windows.Forms.MessageBox.Show("Error: " + OCR1.ThreadErrorMessage);
                else
                    this.textBox1.Text = this.textBox1.Text + "Zona " + i.ToString() + ": " + OCR1.Text + "\r\n";


                //OCR1.Dispose();
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            //Nicomsoft
            this.textBox1.Text = this.NSOCR.Recognize();
        }

        string FileName = @"c:\teste\RG2_300dpi_2050_710.tif";
        private void button4_Click(object sender, EventArgs e)
        {
            using(var ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.FileName = ofd.FileName;
                    this.imageBox1.Image = new Bitmap(this.FileName);

                    //Nicomsoft
                    this.NSOCR.OpenFile(this.FileName);

                    
                }
                ofd.Dispose();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<string> fileSchema = new List<string>();
            foreach (var rect in Schema)
            {
                fileSchema.Add(string.Format("{0};{1};{2};{3}",rect.X, rect.Y, rect.Width,rect.Height));
            }

            using (var sf = new SaveFileDialog())
            {
                if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    File.WriteAllLines(sf.FileName,fileSchema.ToArray());
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using(var od = new OpenFileDialog())
            {
               if (od.ShowDialog() == System.Windows.Forms.DialogResult.OK)
               {
                   Schema.Clear();


                   var fileSchema = File.ReadAllLines(od.FileName);
                   this.NSOCR.DeleteAllZone();

                   foreach (var rec in fileSchema)
                   {
                       char[] del = { ';' };
                       var paramsRec = rec.Split(del);


                       var zone = new Rectangle() { X = Convert.ToInt32(paramsRec[0]), Y = Convert.ToInt32(paramsRec[1]), Width = Convert.ToInt32(paramsRec[2]), Height = Convert.ToInt32(paramsRec[3]) };
                       Schema.Add(zone);
                       this.NSOCR.AddZone(zone); 
                   }
                   this.imageBox1.Update();
                   this.imageBox1.Invalidate();
               }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.imageBox1.GridDisplayMode = ImageBoxGridDisplayMode.Client;
            this.NSOCR = new Nicomsoft_Recognize();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Schema.Clear();
            this.imageBox1.Update();
            this.imageBox1.Invalidate();
        }
    }
}
