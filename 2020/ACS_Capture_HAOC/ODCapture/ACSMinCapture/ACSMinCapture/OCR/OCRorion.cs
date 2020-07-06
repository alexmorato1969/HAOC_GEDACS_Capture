using ACSMinCapture.Config;
using ACSMinCapture.DataBase;
using ACSMinCapture.DataBase.Model;
using ACSMinCapture.Global;
using OCRTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACSMinCapture.Controls
{
    public class OCRorion : Control
    {
        Guid guidDoc;
        string fileName;
        string fileNameOld;
        int MaxLengthBarCode;

        public OCRorion(string FileName, Guid GuidDocument, int MaxLengthBarCode = 4)
        {
            this.MaxLengthBarCode = MaxLengthBarCode;
            this.fileNameOld = FileName;
            this.fileName = FileName;
            this.guidDoc = GuidDocument;
            this.TimeOut = 30;
        }

        bool CopyImage()
        {
            using (var bmp = new Bitmap(this.fileName))
            {
                var DirOCR = @"\OCR\";
                Directory.CreateDirectory(DirOCR);

                if (!Directory.Exists(DirOCR))
                {
                    return false;
                }

                var NewGuid = Guid.NewGuid();
                var FormatImage = Path.GetExtension(this.fileName);

                this.fileName = Path.GetFullPath(DirOCR + "\\" + NewGuid.ToString() + "." + FormatImage);
                bmp.Save(this.fileName);
                bmp.Dispose();
            }
            return File.Exists(this.fileName);
        }

        public delegate void AfterOCR_Event(Guid GuidDocument, List<GED_PROC_CodigosBarras_Result> ListBarCode);

        public event AfterOCR_Event AfterOCREvent;

        List<GED_PROC_CodigosBarras_Result> AfterProcess_Start(OCR ocr, ListView listView)
        {
            var ListBarCode = new List<GED_PROC_CodigosBarras_Result>();
            try
            {
                ocr.SetListViewBarcode(listView);
                //ocr.SetListViewResults(listView);

                ocr.Process_Thread.Abort();
                ocr.BitmapImage.Dispose();
                ocr.Dispose();

                if (listView.Items.Count > 0)
                {
                    foreach (ListViewItem lvi in listView.Items)
                    {
                        var BarCodeValue = lvi.SubItems[2].Text;

                        if (BarCodeValue.Length != this.MaxLengthBarCode)
                            continue;

                        if (ListBarCode.Where(b => b.TPD_CODIGOBARRA == BarCodeValue).Count() <= 0)
                        {
                            var bc = new GED_PROC_CodigosBarras_Result();
                            bc.TPD_CODIGOBARRA = BarCodeValue;
                            ListBarCode.Add(bc);
                        }
                    }
                }

                listView.Dispose();
                File.Delete(this.fileName);
            }
            catch (Exception ex)
            {

            }

            return ListBarCode;

        }

        List<GED_PROC_CodigosBarras_Result> OCRImage(OCR ocr, ListView listView)
        {

            if (!CopyImage())
                return AfterProcess_Start(ocr, listView);

            ocr.BitmapImage.Dispose();
            ocr.BitmapImageFile = this.fileName;
            ocr.Process_Start();
            var timeOutAbout = DateTime.Now.AddSeconds(this.TimeOut);
            while (ocr.Process_Thread.IsAlive)
            {
                if (!ACSConfig.GetScanner().Driver.ToUpper().Contains("FUJITSU") && !ACSConfig.GetScanner().Driver.ToUpper().Contains("SP-1120"))
                    Application.DoEvents(); 
                if (timeOutAbout < DateTime.Now)
                    ocr.Process_Thread.Abort();
            }

            SettingOCR(ref ocr);

            return AfterProcess_Start(ocr, listView);


        }

        void AfterStartOCR(List<GED_PROC_CodigosBarras_Result> ListBarCode)
        {
            if (ListBarCode == null)
                return;

            if (this.AfterOCREvent != null)
            {
                this.AfterOCREvent(this.guidDoc, ListBarCode);
                this.Dispose();
            }
        }

        public void StartOCR()
        {
            var ocr = NewOCR();
            var listView = new ListView();
            var listTask = new List<Task>();
            var tk = Task<List<GED_PROC_CodigosBarras_Result>>.Factory.StartNew(() => OCRImage(ocr, listView));
            listTask.Add(tk);
            Task.Factory.ContinueWhenAll(listTask.ToArray(), t => { AfterStartOCR((t.First() as Task<List<GED_PROC_CodigosBarras_Result>>).Result); });
        }

        public new void Dispose()
        {
        }

        void SettingOCR(ref OCR ocr)
        {
            ocr.BarcodeType = BarcodeTypes.All;
            ocr.OCRType = OCRType.Barcode;
            ocr.ProductName = "StandardBar";
            ocr.CustomerName = "Version5";
            ocr.OrderID = "5152";
            ocr.RegistrationCodes = "5445-3139-8282-8844";
            ocr.ActivationKey = "6930-2481-1896-4395";
            ocr.DisplayChecksum = false;
            ocr.CalculateChecksum = true;
            ocr.DisplayErrors = false;
            ocr.EnforceSpaceRules = false;
            ocr.StartStopCodesDisplay = false;
            ocr.StartStopCodesRequired = false;
            ocr.Statistics = false;
            ocr.ThinCharacters = true;
            ocr.Abort = false;
            ocr.ErrorCorrection = OCRTools.ErrorCorrectionTypes.None;
            ocr.AnalyzeAutomatic = true;
            ocr.StartStopCodesDisplay = false;
            ocr.StartStopCodesRequired = false;
            ocr.Statistics = false;
            ocr.ThinCharacters = true;
        }
        OCR NewOCR()
        {

            var ocr = new OCR();
            SettingOCR(ref ocr);

            return ocr;
        }

        public int TimeOut { get; set; }


    }
}
