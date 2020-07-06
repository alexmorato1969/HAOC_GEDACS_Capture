using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using ACSMinCapture.Log;
using ACSMinCapture.Config;

namespace TwainLib
{
    public enum TwainCommand
    {
        Not = -1,
        Null = 0,
        TransferReady = 1,
        CloseRequest = 2,
        CloseOk = 3,
        DeviceEvent = 4
    }

    public class Twain : IDisposable
    {
        private const short CountryUSA = 1;
        private const short LanguageUSA = 13;

        public Twain(IntPtr hwndp)
        {
            appid = new TwIdentity();
            appid.Id = IntPtr.Zero;
            appid.Version.MajorNum = 1;
            appid.Version.MinorNum = 1;
            appid.Version.Language = LanguageUSA;
            appid.Version.Country = CountryUSA;
            appid.Version.Info = "Hack 1";
            appid.ProtocolMajor = TwProtocol.Major;
            appid.ProtocolMinor = TwProtocol.Minor;
            appid.SupportedGroups = (int)(TwDG.Image | TwDG.Control);
            appid.Manufacturer = "NETMaster";
            appid.ProductFamily = "Freeware";
            appid.ProductName = "Hack";

            srcds = new TwIdentity();
            srcds.Id = IntPtr.Zero;

            evtmsg.EventPtr = Marshal.AllocHGlobal(Marshal.SizeOf(winmsg));

            this.Brightness = 0.0f;
            this.Contrast = 0.0f;
            this.Resolution = 200.0f;
            this.ScanAs = 1;

            this.Init(hwndp);

        }

        public void Dispose()
        {
            Finish();
            Marshal.FreeHGlobal(evtmsg.EventPtr);
        }

        public void Init(IntPtr hwndp)
        {
            Finish();
            TwRC rc = DSMparent(appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.OpenDSM, ref hwndp);
            if (rc == TwRC.Success)
            {
                rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.GetDefault, srcds);
                if (rc == TwRC.Success)
                    hwnd = hwndp;
                else
                    rc = DSMparent(appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, ref hwndp);
            }
        }

        public void Select()
        {
            TwRC rc;
            CloseSrc();
            if (appid.Id == IntPtr.Zero)
            {
                Init(hwnd);
                if (appid.Id == IntPtr.Zero)
                    return;
            }
            rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.UserSelect, srcds);
        }
        public TwIdentity GetDevice(string ProductName)
        {
            try
            {
                var result = GetDevices().Where(d => d.ProductName == ProductName).First();
                return result;
            }
            catch
            {
                return null;
            }
        }
        public TwIdentity[] GetDevices()
        {
            TwRC rc;
            var _device = new TwIdentity();
            _device.Id = IntPtr.Zero;

            List<TwIdentity> _devices = new List<TwIdentity>();

            rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.GetFirst, _device);

            if (rc == TwRC.Success)
            {
                _devices.Add(_device);

                while (rc == TwRC.Success)
                {
                    _device = new TwIdentity();
                    _device.Id = IntPtr.Zero;

                    rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.GetNext, _device);
                    if (rc == TwRC.Success)
                    {
                        _devices.Add(_device);
                    }
                }
            }

            return _devices.ToArray();
        }
        public void SetDevice(string ProductName)
        {
            var result = GetDevice(ProductName);
            SetDevice(result);
        }
        public void SetDevice(TwIdentity device)
        {
            this.srcds = device;
        }
        public TwRC DeviceActive(ref TwIdentity device)
        {
            var rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.OpenDS, device);
            rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.CloseDS, device);
            return rc;
        }

        public TwRC Acquire(short CountPags = 200, short ShowUI = 0, short ModalUI = 0, bool isDuplex = false)
        {
            TwRC rc;
            CloseSrc();
            if (appid.Id == IntPtr.Zero)
            {
                Init(hwnd);
                if (appid.Id == IntPtr.Zero)
                    return TwRC.Failure;
            }
            rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.OpenDS, srcds);

            if (rc != TwRC.Success)
            {
                ACSLog.InsertLog(MessageBoxIcon.Information, "Sem comunicação com o Scanner: Primeira tentativa - " + rc.ToString());
                return rc;
            }

            //Quantidade de Imagens a Capturar
            TwFix32 f32 = new TwFix32();
            f32.FromFloat(CountPags);
            TwCapability cap = new TwCapability(TwCap.XferCount, f32);
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);

            if (rc != TwRC.Success)
            {
                ACSLog.InsertLog(MessageBoxIcon.Information, "Sem comunicação com o Scanner: Quantidade de imagens - " + rc.ToString());
                CloseSrc();
                return rc;
            }

            //Scanner como...
            f32 = new TwFix32();
            f32.FromFloat(this.ScanAs);
            TwCapability capPixelType = new TwCapability(TwCap.IPixelType, f32);
            //rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Get, capPixelType);
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capPixelType);

            if (rc != TwRC.Success)
            {
                ACSLog.InsertLog(MessageBoxIcon.Information, "Sem comunicação com o Scanner: Cor da captura - " + rc.ToString());
                CloseSrc();
                return rc;
            }

            //Resolução Imagem
            f32 = new TwFix32();
            f32.FromFloat(this.Resolution);//value of DPI 
            var capResolution = new TwCapability(TwCap.IXResolution, f32);
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.GetCurrent, capResolution);
            if (rc != TwRC.Success)
            {
                ACSLog.InsertLog(MessageBoxIcon.Information, "Sem comunicação com o Scanner: Resolução da Imagem - " + rc.ToString());
                CloseSrc();
                return rc;
            }

            //Brightness
            short TWON_ONEVALUE = 5;
            f32 = new TwFix32();
            f32.FromFloat(this.Brightness * 10);
            var capBrightness = new TwCapability(TwCap.ICAP_BRIGHTNESS, f32);
            capBrightness.ConType = TWON_ONEVALUE;
            //capBrightness.Handle = hwnd;
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capBrightness);

            //Contrast
            f32 = new TwFix32();
            f32.FromFloat(this.Contrast * 10);
            var capContrast = new TwCapability(TwCap.ICAP_CONTRAST, f32);
            capContrast.ConType = TWON_ONEVALUE;
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capContrast);

            // duplex
            if (isDuplex)
            {
                f32 = new TwFix32();
                f32.FromFloat(-2);
                var XferCount = new TwCapability(TwCap.XferCount, f32);

                rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, XferCount);

                f32 = new TwFix32();
                f32.FromFloat(1);
                var capDuplexEnable = new TwCapability(TwCap.DuplexEnabled, f32);

                rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capDuplexEnable);

                f32 = new TwFix32();
                f32.FromFloat(1);
                var capDuplex = new TwCapability(TwCap.Duplex, f32);
                capDuplex.ConType = 5;
                rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capDuplex);

            }
            else
            {

                f32 = new TwFix32();
                f32.FromFloat(-2);
                var XferCount = new TwCapability(TwCap.XferCount, f32);

                rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, XferCount);

                f32 = new TwFix32();
                f32.FromFloat(0);
                var capDuplexEnable = new TwCapability(TwCap.DuplexEnabled, f32);

                rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capDuplexEnable);

                f32 = new TwFix32();
                f32.FromFloat(0);
                var capDuplex = new TwCapability(TwCap.Duplex, f32);
                capDuplex.ConType = 5;
                rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capDuplex);



            }

            //Set Xfer file setting
            /*
            f32 = new TwFix32();
            f32.FromFloat(1);
            var capIXferMech = new TwCapability(TwCap.IXferMech, f32);//tesx_file
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capIXferMech);
            if (rc != TwRC.Success)
            {
                CloseSrc();
                return rc;
            } 
            */

            TwUserInterface guif = new TwUserInterface();
            guif.ShowUI = ShowUI;
            guif.ModalUI = ModalUI;
            guif.ParentHand = hwnd;
            rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, guif);
            if (rc != TwRC.Success)
            {
                ACSLog.InsertLog(MessageBoxIcon.Information, "Sem comunicação com o Scanner: Última verificação - " + rc.ToString());
                CloseSrc();
                return rc;
            }

            return rc;
        }
        public TwRC AcquireLexmark(short CountPags = 200, short ShowUI = 0, short ModalUI = 0, bool isDuplex = false)
        {
            TwRC rc;
            CloseSrc();
            if (appid.Id == IntPtr.Zero)
            {
                Init(hwnd);
                if (appid.Id == IntPtr.Zero)
                    return TwRC.Failure;
            }
            rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.OpenDS, srcds);
            if (rc != TwRC.Success)
            {
                ACSLog.InsertLog(MessageBoxIcon.Information, "Sem comunicação com o Scanner: Primeira tentativa - " + rc.ToString());
                return rc;
            }

            //Quantidade de Imagens a Capturar
            TwFix32 f32 = new TwFix32();
            f32.FromFloat(CountPags);
            TwCapability cap = new TwCapability(TwCap.XferCount, f32);
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);

            if (rc != TwRC.Success)
            {
                ACSLog.InsertLog(MessageBoxIcon.Information, "Sem comunicação com o Scanner: Quantidade de imagens - " + rc.ToString());
                CloseSrc();
                return rc;
            }

            //Scanner como...
            f32 = new TwFix32();
            f32.FromFloat(this.ScanAs);
            TwCapability capPixelType = new TwCapability(TwCap.IPixelType, f32);
            //rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Get, capPixelType);
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capPixelType);

            if (rc != TwRC.Success)
            {
                ACSLog.InsertLog(MessageBoxIcon.Information, "Sem comunicação com o Scanner: Cor da captura - " + rc.ToString());
                CloseSrc();
                return rc;
            }

            //Resolução Imagem
            f32 = new TwFix32();
            f32.FromFloat(this.Resolution);//value of DPI 
            var capResolution = new TwCapability(TwCap.IXResolution, f32);
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.GetCurrent, capResolution);
            if (rc != TwRC.Success)
            {
                ACSLog.InsertLog(MessageBoxIcon.Information, "Sem comunicação com o Scanner: Resolução da Imagem - " + rc.ToString());
                CloseSrc();
                return rc;
            }

            //Brightness
            short TWON_ONEVALUE = 5;
            f32 = new TwFix32();
            f32.FromFloat(this.Brightness * 10);
            var capBrightness = new TwCapability(TwCap.ICAP_BRIGHTNESS, f32);
            capBrightness.ConType = TWON_ONEVALUE;
            //capBrightness.Handle = hwnd;
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capBrightness);

            //Contrast
            f32 = new TwFix32();
            f32.FromFloat(this.Contrast * 10);
            var capContrast = new TwCapability(TwCap.ICAP_CONTRAST, f32);
            capContrast.ConType = TWON_ONEVALUE;
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capContrast);

            // duplex
            if (isDuplex)
            {
                f32 = new TwFix32();
                f32.FromFloat(-2);
                var XferCount = new TwCapability(TwCap.XferCount, f32);

                rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, XferCount);

                f32 = new TwFix32();
                f32.FromFloat(1);
                var capDuplexEnable = new TwCapability(TwCap.DuplexEnabled, f32);

                rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capDuplexEnable);

                f32 = new TwFix32();
                f32.FromFloat(1);
                var capDuplex = new TwCapability(TwCap.Duplex, f32);
                capDuplex.ConType = 5;
                rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capDuplex);

            }
            else
            {

                f32 = new TwFix32();
                f32.FromFloat(-2);
                var XferCount = new TwCapability(TwCap.XferCount, f32);

                rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, XferCount);

                f32 = new TwFix32();
                f32.FromFloat(0);
                var capDuplexEnable = new TwCapability(TwCap.DuplexEnabled, f32);

                rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capDuplexEnable);

                f32 = new TwFix32();
                f32.FromFloat(0);
                var capDuplex = new TwCapability(TwCap.Duplex, f32);
                capDuplex.ConType = 5;
                rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capDuplex);



            }


            TwUserInterface guif = new TwUserInterface();
            guif.ShowUI = ShowUI;
            guif.ModalUI = ModalUI;
            guif.ParentHand = hwnd;
            rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, guif);
            if (rc != TwRC.Success)
            {
                if (rc == TwRC.CheckStatus)
                {

                    // CloseSrc();
                    return TwRC.Success;
                }
                ACSLog.InsertLog(MessageBoxIcon.Information, "Sem comunicação com o Scanner: Última verificação - " + rc.ToString());
                CloseSrc();
                return rc;
            }

            return rc;
        }
        public void StopAcquire()
        {
            TwUserInterface guif = new TwUserInterface();
            var rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.StopFeeder, guif);
            if (!ACSConfig.GetScanner().Driver.ToUpper().Contains("FUJITSU") && !ACSConfig.GetScanner().Driver.ToUpper().Contains("SP-1120"))
                Application.DoEvents();
        }

        public void TransferPictures()
        {
            ArrayList pics = new ArrayList();
            if (srcds.Id == IntPtr.Zero)
                return;

            TwRC rc;
            IntPtr hbitmap = IntPtr.Zero;
            TwPendingXfers pxfr = new TwPendingXfers();

            do
            {
                pxfr.Count = 0;
                hbitmap = IntPtr.Zero;

                TwImageInfo iinf = new TwImageInfo();
                rc = DSiinf(appid, srcds, TwDG.Image, TwDAT.ImageInfo, TwMSG.Get, iinf);
                if (rc != TwRC.Success)
                {
                    CloseSrc();
                    return;
                }

                //verificar
                /*
                var guid = Guid.NewGuid();
                TwSetupFileXFfer tw_setfilexfer = new TwSetupFileXFfer(@"c:\temp\acs\"+guid.ToString()+".tiff", 0);//0=tif

                rc = DScap(appid, srcds, TwDG.Control, TwDAT.SetupFileXfer, TwMSG.Set, tw_setfilexfer);
                if (rc != TwRC.Success)
                {
                    CloseSrc();
                    return;
                }
                */
                //rc = DSixfer(appid, srcds, TwDG.Image, TwDAT.ImageFileXfer, TwMSG.Get, ref hbitmap);
                rc = DSixfer(appid, srcds, TwDG.Image, TwDAT.ImageNativeXfer, TwMSG.Get, ref hbitmap);
                if (rc != TwRC.XferDone)
                {
                    CloseSrc();
                    return;
                }

                rc = DSpxfer(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.EndXfer, pxfr);
                if (rc != TwRC.Success)
                {
                    CloseSrc();
                    return;
                }

                if (TransferPictureEvent != null && hbitmap != IntPtr.Zero)
                {
                    var bmp = TwainBitmapConvertor.ToBitmap(hbitmap);
                    TransferPictureEvent(ref bmp);
                }


            }
            while (pxfr.Count != 0);

            rc = DSpxfer(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.Reset, pxfr);
            return;
        }
        byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        public TwainCommand PassMessage(ref Message m)
        {
            if (srcds.Id == IntPtr.Zero)
                return TwainCommand.Not;

            int pos = GetMessagePos();

            winmsg.hwnd = m.HWnd;
            winmsg.message = m.Msg;
            winmsg.wParam = m.WParam;
            winmsg.lParam = m.LParam;
            winmsg.time = GetMessageTime();
            winmsg.x = (short)pos;
            winmsg.y = (short)(pos >> 16);

            Marshal.StructureToPtr(winmsg, evtmsg.EventPtr, false);
            evtmsg.Message = 0;
            TwRC rc = DSevent(appid, srcds, TwDG.Control, TwDAT.Event, TwMSG.ProcessEvent, ref evtmsg);
            if (rc == TwRC.NotDSEvent)
                return TwainCommand.Not;
            if (evtmsg.Message == (short)TwMSG.XFerReady)
                return TwainCommand.TransferReady;
            if (evtmsg.Message == (short)TwMSG.CloseDSReq)
                return TwainCommand.CloseRequest;
            if (evtmsg.Message == (short)TwMSG.CloseDSOK)
                return TwainCommand.CloseOk;
            if (evtmsg.Message == (short)TwMSG.DeviceEvent)
                return TwainCommand.DeviceEvent;

            return TwainCommand.Null;
        }

        public void CloseSrc()
        {
            try
            { 
                if (!ACSConfig.GetScanner().Driver.ToUpper().Contains("FUJITSU") && !ACSConfig.GetScanner().Driver.ToUpper().Contains("SP-1120"))
                {
                    TwRC rc;
                    if (srcds.Id != IntPtr.Zero)
                    {
                        TwUserInterface guif = new TwUserInterface();
                        rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.DisableDS, guif);
                        rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.CloseDS, srcds);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void Finish()
        {
            if (!ACSConfig.GetScanner().Driver.ToUpper().Contains("FUJITSU") && !ACSConfig.GetScanner().Driver.ToUpper().Contains("SP-1120"))
            {
                TwRC rc;
                CloseSrc();
                if (appid.Id != IntPtr.Zero)
                    rc = DSMparent(appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, ref hwnd);
                appid.Id = IntPtr.Zero;
            }
        }

        private IntPtr hwnd;
        private TwIdentity appid;
        private TwIdentity srcds;
        private TwEvent evtmsg;
        private WINMSG winmsg;

        public delegate void TransferPicture_Event(ref Bitmap bitmap);
        //public delegate void TransferPicture_Event(string FileName);
        public event TransferPicture_Event TransferPictureEvent;

        public float Brightness { get; set; }
        public float Contrast { get; set; }
        public float Resolution { get; set; }
        /*BlackWhite = 0 GrayScale = 1 Colorido = 2*/
        public int ScanAs { get; set; }

        // ------ DSM entry point DAT_ variants:
        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSMparent([In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr refptr);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSMident([In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwIdentity idds);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSMstatus([In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus dsmstat);

        // ------ DSM entry point DAT_ variants to DS:
        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSuserif([In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, TwUserInterface guif);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSevent([In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref TwEvent evt);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSstatus([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus dsmstat);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DScap([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwCapability capa);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DScap([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwSetupFileXFfer setupFileXFfer);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSiinf([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwImageInfo imginf);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSixfer([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr hbitmap);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSpxfer([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwPendingXfers pxfr);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSpxfer([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, TwSetupFileXfers sfxfr);


        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalAlloc(int flags, int size);
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalLock(IntPtr handle);
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern bool GlobalUnlock(IntPtr handle);
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalFree(IntPtr handle);

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern int GetMessagePos();
        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern int GetMessageTime();

        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr CreateDC(string szdriver, string szdevice, string szoutput, IntPtr devmode);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern bool DeleteDC(IntPtr hdc);




        public static int ScreenBitDepth
        {
            get
            {
                IntPtr screenDC = CreateDC("DISPLAY", null, null, IntPtr.Zero);
                int bitDepth = GetDeviceCaps(screenDC, 12);
                bitDepth *= GetDeviceCaps(screenDC, 14);
                DeleteDC(screenDC);
                return bitDepth;
            }
        }


        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        internal struct WINMSG
        {
            public IntPtr hwnd;
            public int message;
            public IntPtr wParam;
            public IntPtr lParam;
            public int time;
            public int x;
            public int y;
        }

        internal static class TwainBitmapConvertor
        {
            [StructLayout(LayoutKind.Sequential, Pack = 2)]
            private class BitmapInfoHeader
            {
                public int Size;
                public int Width;
                public int Height;
                public short Planes;
                public short BitCount;
                public int Compression;
                public int SizeImage;
                public int XPelsPerMeter;
                public int YPelsPerMeter;
                public int ClrUsed;
                public int ClrImportant;
            }

            [DllImport("gdi32.dll", ExactSpelling = true)]
            private static extern int SetDIBitsToDevice(IntPtr hdc,
                int xdst, int ydst, int width, int height, int xsrc,
                int ysrc, int start, int lines, IntPtr bitsptr,
                IntPtr bmiptr, int color);

            [DllImport("kernel32.dll", ExactSpelling = true)]
            private static extern IntPtr GlobalLock(IntPtr handle);

            [DllImport("kernel32.dll", ExactSpelling = true)]
            internal static extern bool GlobalUnlock(IntPtr handle);

            [DllImport("kernel32.dll", ExactSpelling = true)]
            internal static extern IntPtr GlobalFree(IntPtr handle);

            public static Bitmap ToBitmap(IntPtr dibHandle)
            {
                var bitmapPointer = GlobalLock(dibHandle);

                var bitmapInfo = new BitmapInfoHeader();
                Marshal.PtrToStructure(bitmapPointer, bitmapInfo);

                var rectangle = new Rectangle();
                rectangle.X = rectangle.Y = 0;
                rectangle.Width = bitmapInfo.Width;
                rectangle.Height = bitmapInfo.Height;

                if (bitmapInfo.SizeImage == 0)
                {
                    bitmapInfo.SizeImage =
                        ((((bitmapInfo.Width * bitmapInfo.BitCount) + 31) & ~31) >> 3)
                        * bitmapInfo.Height;
                }

                // The following code only works on x86
                //Debug.Assert(Marshal.SizeOf(typeof(IntPtr)) == 4);

                int pixelInfoPointer = bitmapInfo.ClrUsed;
                if ((pixelInfoPointer == 0) && (bitmapInfo.BitCount <= 8))
                {
                    pixelInfoPointer = 1 << bitmapInfo.BitCount;
                }

                pixelInfoPointer = (pixelInfoPointer * 4) + bitmapInfo.Size
                    + bitmapPointer.ToInt32();

                IntPtr pixelInfoIntPointer = new IntPtr(pixelInfoPointer);

                Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);

                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    IntPtr hdc = graphics.GetHdc();

                    try
                    {
                        SetDIBitsToDevice(hdc,
                            0, 0, rectangle.Width, rectangle.Height, 0, 0, 0,
                            rectangle.Height, pixelInfoIntPointer, bitmapPointer, 0);
                    }
                    finally
                    {
                        graphics.ReleaseHdc(hdc);
                    }
                }

                bitmap.SetResolution(PpmToDpi(bitmapInfo.XPelsPerMeter),
                    PpmToDpi(bitmapInfo.YPelsPerMeter));

                GlobalUnlock(dibHandle);
                GlobalFree(dibHandle);

                return bitmap;
            }

            private static float PpmToDpi(double pixelsPerMeter)
            {
                double pixelsPerMillimeter = (double)pixelsPerMeter / 1000.0;
                double dotsPerInch = pixelsPerMillimeter * 25.4;
                return (float)Math.Round(dotsPerInch, 2);
            }
        }



    } // class Twain
}
