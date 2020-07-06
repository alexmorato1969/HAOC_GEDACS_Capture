using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TwainLib;
using System.Linq;
using ACSMinCapture.Config;
using ACSMinCapture.Global;
using ACSMinCapture.Log;
using ACSMinCapture.Auxiliar;

namespace ACSMinCapture.Forms
{
    public partial class WFTwain : ACSMinCapture.WFMDIChild, IMessageFilter
    {
        public WFTwain()
        {
            InitializeComponent();
        }

        #region Delegates

        public delegate void BeforeScan_Event(object sender);
        public delegate void AfterScan_Event(bool Scanning);
        public delegate void AfterEndingScan_Event(object sender, bool Scanned);

        #endregion

        #region Events

        public event TwainLib.Twain.TransferPicture_Event TransferPictureEvent;
        public event BeforeScan_Event BeforeScanEvent;
        public event AfterScan_Event AfterScanEvent;
        public event AfterEndingScan_Event AfterEndingScanEvent;

        #endregion

        #region Vars
        Twain tw;
        bool msgfilter = false;
        #endregion

        #region Methods

        public bool IsScanning()
        {
            return msgfilter;
        }

        public bool PreFilterMessage(ref Message m)
        {
            try
            {

                if (tw == null)
                    return true;

                TwainCommand cmd = tw.PassMessage(ref m);

                if (cmd == TwainCommand.Not)
                {
                    return false;
                }

                if (cmd == TwainCommand.Null)
                {
                    return false;
                }
                switch (cmd)
                {
                    case TwainCommand.CloseRequest:
                        {
                            //  EndingScan();
                            break;
                        }
                    case TwainCommand.CloseOk:
                        {
                            EndingScan();
                            break;
                        }
                    case TwainCommand.DeviceEvent:
                        {
                            break;
                        }
                    case TwainCommand.TransferReady:
                        {
                            tw.TransferPictures();
                            EndingScan();
                            break;
                        }
                }

                return true;

            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);
                return true;
            }
        }

        public virtual void Scan(short CountPages = 200, AxFiScnLib.AxFiScn _driverFujitsu = null, WFCapture wfCapture = null)
        {
            try
            {


                if (this.BeforeScanEvent != null)
                {
                    this.BeforeScanEvent(this);
                }

                if (!msgfilter)
                {
                    msgfilter = true;
                    Application.AddMessageFilter(this);
                }

                this.tw = new Twain(this.Handle);
                this.tw.TransferPictureEvent += TransferPictureEvent;
                this.tw.Brightness = ACSConfig.GetImages().Brightness;
                this.tw.Contrast = ACSConfig.GetImages().Contrast;
                this.tw.Resolution = ACSConfig.GetImages().Resolution;
                this.tw.ScanAs = ACSConfig.GetScanner().ScanAs;
                this.tw.SetDevice(ACSConfig.GetScanner().Driver);

                var nameDriver = ACSConfig.GetScanner().Driver;

                if (nameDriver.ToUpper().Contains("SP-1120"))
                {

                    Fujitsu clsFujitsu = new Fujitsu(_driverFujitsu, wfCapture, ACSGlobal.LoteSelecionado.DIRLOTEINBOX);
                    if (clsFujitsu.AcquireFujitsu() == 1)
                    {
                        if (this.AfterScanEvent != null)
                        {
                            //  this.AfterScanEvent(true);
                            EndingScan(false);
                        }
                    }
                    else
                    {

                        EndingScan(false);
                        WFMessageBox.Show("Sem comunicação com scanner!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                    if (nameDriver.ToUpper().Contains("LEXMARK"))
                    {
                        //WFLoading.ShowLoad();
                        bool isDuplex = ACSGlobal.Duplex;

                        if (this.tw.AcquireLexmark(CountPages, 0, 0, isDuplex) == TwRC.Success)
                        {
                            if (this.AfterScanEvent != null)
                            {
                                this.AfterScanEvent(true);

                            } //WFLoading.CloseLoad();
                        }
                        else
                        {
                            if (this.AfterScanEvent != null)
                            {
                                this.AfterScanEvent(false);
                            }
                            EndingScan(false);
                            WFMessageBox.Show("Sem comunicação com scanner!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }


                    }
                    else
                    {
                        bool isDuplex = ACSGlobal.Duplex;

                        if (this.tw.Acquire(CountPages, 0, 0, isDuplex) == TwRC.Success)
                        {
                            if (this.AfterScanEvent != null)
                            {
                                this.AfterScanEvent(true);

                            }
                            WFLoading.CloseLoad();
                        }
                        else
                        {
                            if (this.AfterScanEvent != null)
                            {
                                this.AfterScanEvent(false);
                            }
                            EndingScan(false);
                            WFMessageBox.Show("Sem comunicação com scanner!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }





                if (!ACSConfig.GetScanner().Driver.ToUpper().Contains("FUJITSU") && !ACSConfig.GetScanner().Driver.ToUpper().Contains("SP-1120"))
                    Application.DoEvents();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void StopScan()
        {
            if (this.tw != null)
            {
                this.tw.StopAcquire();
                this.EndingScan(true);
            }
        }

        public void EndingScan(bool Scanned = true)
        {
            if (msgfilter)
            {


                Application.RemoveMessageFilter(this);
                msgfilter = false;
                if (!ACSConfig.GetScanner().Driver.ToUpper().Contains("FUJITSU") && !ACSConfig.GetScanner().Driver.ToUpper().Contains("SP-1120"))
                    tw.Dispose();
                tw = null;
                if (this.AfterEndingScanEvent != null)
                    this.AfterEndingScanEvent(this, Scanned);

            }
        }

        #endregion



    }
}
