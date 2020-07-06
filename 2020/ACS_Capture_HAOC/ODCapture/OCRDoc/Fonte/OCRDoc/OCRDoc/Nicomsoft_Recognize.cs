using NSOCR_NameSpace;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NSOCR_NameSpace;

namespace OCRDoc
{
    class Nicomsoft_Recognize : IDisposable
    {
        public NSOCRLib.NSOCRClass NsOCR; 
        public int CfgObj = 0;
        public int OcrObj = 0;
        public int ImgObj = 0;
        public int ScanObj = 0;
        public int SvrObj = 0;

        bool NoEvent;
        bool Inited = false;
        int pmBlockTag = -1;

        public Nicomsoft_Recognize()
        {
            // TestDirectCallsToDLL(); //minimal code to demonstrate how to use DLL directly, without COM

            //it is a good idea to create NSOCR instance here instead of creting it in the same line with "NsOCR" definition
            //this way we can handle possible errors if NSOCR is not installed
            try
            {
                NsOCR = new NSOCRLib.NSOCRClass();
            }
            catch (Exception exc) //some error
            {
                string msg = "Cannot create NSOCR COM object instance. Possible reasons:\r\n - NSOCR.dll is missed.\r\n - NSOCR.dll was not registered with Regsvr32.\r\n - NSOCR.dll is x32 but this application is x64.\r\n";
                msg = msg + "\r\n Exception message:\r\n\r\n" + exc.Message;
                System.Windows.Forms.MessageBox.Show(msg);
                //Close();
                return;
            }

           // gr = PicBox.CreateGraphics();

            Inited = true; //NSOCR instance created successfully

            //get NSOCR version
            string val;
            NsOCR.Engine_GetVersion(out val);
            //Text = Text + " [ NSOCR version: " + val + " ] ";

            //init engine and create ocr-related objects
            if (true /*false*/) //change to "false" to reduce code and initialize engine + create main objects in one line
            {
                NsOCR.Engine_Initialize();
                NsOCR.Cfg_Create(out CfgObj);
                //load options, if path not specified, folder with NSOCR.dll will be checked
                NsOCR.Cfg_LoadOptions(CfgObj, "Config.dat");
                NsOCR.Ocr_Create(CfgObj, out OcrObj);
                NsOCR.Img_Create(OcrObj, out ImgObj);
            }
            else //do the same in one line
                NsOCR.Engine_InitializeAdvanced(out CfgObj, out OcrObj, out ImgObj);

            NsOCR.Scan_Create(CfgObj, out ScanObj);

            //bkRecognize.Enabled = false;

            NoEvent = true;
            //cbScale.SelectedIndex = 0;
            NoEvent = false;
            //bkSave.Enabled = false;

            //copy some settings to GUI
            //if (NsOCR.Cfg_GetOption(CfgObj, TNSOCR.BT_DEFAULT, "ImgAlizer/AutoScale", out val) < TNSOCR.ERROR_FIRST)
            //    cbScale.Enabled = (val == "1");
        }

        public void Dispose()
        {
            if (Inited)
            {
                if (ImgObj != 0) NsOCR.Img_Destroy(ImgObj);
                if (OcrObj != 0) NsOCR.Ocr_Destroy(OcrObj);
                if (CfgObj != 0) NsOCR.Cfg_Destroy(CfgObj);
                if (ScanObj != 0) NsOCR.Scan_Destroy(ScanObj);
                NsOCR.Engine_Uninitialize();
            }
        }

        public void OpenFile(string FileName)
        {
            NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "ImgAlizer/AutoScale", "0");
            NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "ImgAlizer/ScaleFactor", "1.0"); //default scale if cannot detect it automatically

            int res = NsOCR.Img_LoadFile(ImgObj, FileName);

            if (res > TNSOCR.ERROR_FIRST)
            {
                MessageBox.Show("Error Load File!");
                return;
            }
            
            DoImageLoaded();
            
        }

        public void DoImageLoaded()
        {
            int res, w, h;

            //check if image has multiple page and ask user if he wants process and save all pages automatically
            res = NsOCR.Img_GetPageCount(ImgObj);
            if (res > TNSOCR.ERROR_FIRST)
            {
                ShowError("Img_GetPageCount", res);
                return;
            }
            
            //now apply image scaling, binarize image, deskew etc,
            //everything except OCR itself
            res = NsOCR.Img_OCR(ImgObj, TNSOCR.OCRSTEP_FIRST, TNSOCR.OCRSTEP_ZONING - 1, TNSOCR.OCRFLAG_NONE);
            if (res > TNSOCR.ERROR_FIRST)
            {
                ShowError("Img_OCR", res);
                return;
            }

            res = NsOCR.Img_GetSkewAngle(ImgObj);
            if (res > TNSOCR.ERROR_FIRST)
            {
                ShowError("Img_GetSkewAngle", res);
            }

            //pixel lines info
            res = NsOCR.Img_GetPixLineCnt(ImgObj);
            if (res > TNSOCR.ERROR_FIRST)
            {
                ShowError("Img_GetPixLineCnt", res);
                return;
            }

            res = NsOCR.Img_GetProperty(ImgObj, TNSOCR.IMG_PROP_INVERTED);

            //final size after pre-processing
            NsOCR.Img_GetSize(ImgObj, out w, out h);

        }

        public void ShowError(string api, int err)
        {
            string s;
            s = api + " Error #" + err.ToString("X");
            System.Windows.Forms.MessageBox.Show(s);
        }

        public float GetCurDocScale(RectangleF r)
        {

            int w = (int)r.Width;
            int h = (int)r.Height - 45;

            int ww, hh;
            NsOCR.Img_GetSize(ImgObj, out ww, out hh);
            float kX = (float)w / ww;
            float kY = (float)h / hh;
            float k;
            if (kX > kY) k = kY;
            else k = kX;

            return k;
        }

        public void AddZone(RectangleF r)
        {
            int res;

            int BlkObj, w, h;

            NsOCR.Img_GetSize(ImgObj, out w, out h);
            
            res = NsOCR.Img_AddBlock(ImgObj, (int)r.Left, (int)r.Top, (int)r.Width, (int)r.Height, out BlkObj);
            //res = 0;
            if (res > TNSOCR.ERROR_FIRST)
            {
                ShowError("Img_AddBlock", res);
                return;
            }

            //detect text block inversion
            NsOCR.Blk_Inversion(BlkObj, TNSOCR.BLK_INVERSE_DETECT);

            //detect text block rotation
            NsOCR.Blk_Rotation(BlkObj, TNSOCR.BLK_ROTATE_DETECT);

        }

        public void DeleteAllZone()
        {
            this.NsOCR.Img_DeleteAllBlocks(this.ImgObj);
        }

        public void DeleteZone(int ZoneIndex)
        {
            int blIndex;
            this.NsOCR.Img_GetBlock(ImgObj, ZoneIndex, out blIndex);
            this.NsOCR.Img_DeleteBlock(ImgObj, blIndex);
        }

        public string Recognize()
        {
            bool InSameThread;
            int res;

            InSameThread = false; //perform OCR in non-blocking mode
            //InSameThread = true; //uncomment to perform OCR from this thread (GUI will be freezed)

            //perform OCR itself
            if (InSameThread)
                res = NsOCR.Img_OCR(ImgObj, TNSOCR.OCRSTEP_ZONING, TNSOCR.OCRSTEP_LAST, TNSOCR.OCRFLAG_NONE);
            else
            {
                //do it in non-blocking mode and then wait for result
                res = NsOCR.Img_OCR(ImgObj, TNSOCR.OCRSTEP_ZONING, TNSOCR.OCRSTEP_LAST, TNSOCR.OCRFLAG_THREAD);
                if (res > TNSOCR.ERROR_FIRST)
                {
                    ShowError("Ocr_OcrImg(1)", res);
                    return string.Empty;
                }


                res = TNSOCR.ERROR_PENDING;
                while(res == TNSOCR.ERROR_PENDING)
                {
                    Application.DoEvents();
                    res = NsOCR.Img_OCR(ImgObj, 0, 0, TNSOCR.OCRFLAG_GETRESULT);
                }
                
            }


            if (res > TNSOCR.ERROR_FIRST)
            {
                if (res == TNSOCR.ERROR_OPERATIONCANCELLED)
                    System.Windows.Forms.MessageBox.Show("Operation was cancelled.");
                else
                {
                    ShowError("Img_OCR", res);
                    return string.Empty;
                }
            }

            int flags = false ? TNSOCR.FMT_EXACTCOPY : TNSOCR.FMT_EDITCOPY;
            string txt;
            NsOCR.Img_GetImgText(ImgObj, out txt, flags);
            return txt;

        }
    }
}
