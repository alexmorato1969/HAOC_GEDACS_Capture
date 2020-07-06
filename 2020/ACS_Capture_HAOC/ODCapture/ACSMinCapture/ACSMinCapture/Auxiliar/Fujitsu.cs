using ACSMinCapture.Config;
using ACSMinCapture.Forms;
using ACSMinCapture.Global;
using ACSMinCapture.Log;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACSMinCapture.Auxiliar
{
    public class Fujitsu
    {
        WFCapture captureForm = null;
        AxFiScnLib.AxFiScn driverFujitsu = null;
        bool setDuplex = false;
        string pathSaveInbox = "";

        public Fujitsu(AxFiScnLib.AxFiScn _driverFujitsu, WFCapture _captureForm, string _pathSaveInbox)
        {
            driverFujitsu = _driverFujitsu;
            captureForm = _captureForm;
            pathSaveInbox = _pathSaveInbox;
        }


        public int AcquireFujitsu()
        {
            try
            {
                int status;
                int ErrorCode;


                //Open the scanner (method)
                status = driverFujitsu.OpenScanner2(driverFujitsu.Handle.ToInt32()); //OpenScanner2 is recommended

                if (status == -1)
                {
                    //Display the error information
                    ErrorCode = driverFujitsu.ErrorCode;
                    return ErrorCode;
                }


                driverFujitsu.ShowSourceUI = false;
                driverFujitsu.ScanTo = 0; //Data output method: file
                driverFujitsu.CompressionType = 5; //Compression format: JPEG compression
                driverFujitsu.FileType = 3; //File format: JPEG file

                var BRss =  (short)(ACSConfig.GetImages().Brightness );
                var CONss = (short)(ACSConfig.GetImages().Contrast );
                driverFujitsu.Brightness =BRss;
                driverFujitsu.Contrast = CONss;
                //set color
                if (ACSConfig.GetScanner().ScanAs == 0) { driverFujitsu.PixelType = 0; }//Branco e Preto  }
                else if (ACSConfig.GetScanner().ScanAs == 1) { driverFujitsu.PixelType = 1; }//Escala de cinza  }
                else { driverFujitsu.PixelType = 2; } //Colorido }


                if (ACSConfig.GetImages().Resolution <= 200)
                {
                    driverFujitsu.Resolution = 0;
                }
                else if (ACSConfig.GetImages().Resolution < 300)
                {
                    driverFujitsu.Resolution = 1;
                }
                else if (ACSConfig.GetImages().Resolution < 400)
                {
                    driverFujitsu.Resolution = 2;
                }
                else if (ACSConfig.GetImages().Resolution < 500)
                {
                    driverFujitsu.Resolution = 3;
                }
                else if (ACSConfig.GetImages().Resolution < 600)
                {
                    driverFujitsu.Resolution = 4;
                }
                else if (ACSConfig.GetImages().Resolution < 700)
                {
                    driverFujitsu.Resolution = 5;
                }
                else if (ACSConfig.GetImages().Resolution < 800)
                {
                    driverFujitsu.Resolution = 6;
                }
                else if (ACSConfig.GetImages().Resolution < 900)
                {
                    driverFujitsu.Resolution = 7;
                }
                else driverFujitsu.Resolution = 9;


                setDuplex = ACSGlobal.Duplex;

                //set duplex 
                if (setDuplex)
                    driverFujitsu.PaperSupply = 2;
                else
                    driverFujitsu.PaperSupply = 1;

                driverFujitsu.FileName = pathSaveInbox + "\\000";
                driverFujitsu.BarcodeDetection = true;

                if (!driverFujitsu.FeederLoaded(driverFujitsu.Handle.ToInt32()))
                {
                    return -1;
                }

                driverFujitsu.SetCapability(0x1013, 1, 1);

                driverFujitsu.SetCapability(0x1012, 1, 3);


                status = driverFujitsu.StartScan(driverFujitsu.Handle.ToInt32());
                // WFCapture_AfterScanEvent();

                // An error occurred during a scan
                if (status == -1)
                {
                    //Display the error information
                    ErrorCode = driverFujitsu.ErrorCode;
                    return ErrorCode;
                }



                return 1;
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);
                throw;
            }
        }




        private void WFCapture_AfterScanEvent()
        {
            try
            {

                captureForm.WFCapture_AfterScanEvent(true);
            }
            catch
            {
            }

        }


    }
}
