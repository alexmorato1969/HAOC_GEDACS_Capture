using ACSMinCapture.Config;
using ACSMinCapture.Controls;
using ACSMinCapture.DataBase;
using ACSMinCapture.Global;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwainLib;
using ACSMinCapture.DataBase.Model;
using Cyotek.Windows.Forms;
using System.Drawing.Imaging;
using ACSMinCapture.Log;
using NewACSMinCapture;
using ACSMinCapture.Auxiliar;
using System.Security.Cryptography;
using ACSMinCapture.DataBase.ModelOracle;
using OrionDigital.Framework;
using System.Reflection;

namespace ACSMinCapture.Forms
{

    public partial class WFCapture : WFTwain
    {





        int iEscolhaCaptura = 0;

        public bool ReScan = false;
        public Document LastDocumentScan = null;
        public ToolStripMenuItem tsmiReScan = null;
        public ToolStripMenuItem tsmiReLoad = null;
        public int IndexImagesCapture = 0;
        public short CountScan = 200;
        public GEDLOTESXUSUARIOS Lote;


        public WFCapture()
        {
            try
            {

                if (ACSGlobal.Duplex) { }

                InitializeComponent();

                this.tsmiReScan = new ToolStripMenuItem("ReScan", null, tsmiReScan_Click, (Keys)(Keys.Control | Keys.R));
                this.tsmiReLoad = new ToolStripMenuItem("Reprocessar Código", null, tsmiReLoad_Click, (Keys)(Keys.Control | Keys.L));

                this.ucImagesManipulation1.cmsImages.Items.Add(this.tsmiReScan);
                this.ucImagesManipulation1.cmsImages.Items.Add(this.tsmiReLoad);
            }
            catch (Exception e)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, e);
                WFMessageBox.Show(e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {


                if (keyData == (Keys.Control | Keys.B))
                {
                    this.ucImagesManipulation1.btnEraser.Checked = !this.ucImagesManipulation1.btnEraser.Checked;
                    return true;
                }
                else if (keyData == (Keys.Control | Keys.L))
                {
                    this.ucImagesManipulation1.btnCrop.Checked = !this.ucImagesManipulation1.btnCrop.Checked;
                    return true;
                }
                else if (keyData == (Keys.Control | Keys.Multiply))
                {
                    this.ucImagesManipulation1.btnRotate.PerformClick();
                    return true;
                }
                else if (keyData == (Keys.Control | Keys.Add))
                {
                    this.ucImagesManipulation1.btnZoomIn.PerformClick();
                    return true;
                }
                else if (keyData == (Keys.Control | Keys.Subtract))
                {
                    this.ucImagesManipulation1.btnZoomOut.PerformClick();
                    return true;
                }
                else if (keyData == (Keys.Control | Keys.F11))
                {
                    this.ucImagesManipulation1.btnQuad.PerformClick();
                    return true;
                }
                else if (keyData == Keys.F5)
                {
                    this.btnScan.PerformClick();
                    return true;
                }
                else if (keyData == Keys.F6)
                {
                    this.btnProcessar.PerformClick();
                    return true;
                }
                else if (keyData == Keys.F1)
                {
                    this.btnLotes.PerformClick();
                    return true;
                }

                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch (Exception e)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, e);
                WFMessageBox.Show(e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #region Methods

        public void SetColumnsRows()
        {
            if (tsmi1X1.Checked)
            {
                ucImagesManipulation1.ucImages.Columns = 1;
                ucImagesManipulation1.ucImages.Rows = 1;
            }
            else if (tsmi1X2.Checked)
            {
                ucImagesManipulation1.ucImages.Columns = 2;
                ucImagesManipulation1.ucImages.Rows = 1;
            }
            else if (tsmi2X2.Checked)
            {
                ucImagesManipulation1.ucImages.Columns = 2;
                ucImagesManipulation1.ucImages.Rows = 2;
            }
            else if (tsmi4X4.Checked)
            {
                ucImagesManipulation1.ucImages.Columns = 4;
                ucImagesManipulation1.ucImages.Rows = 4;
            }
        }

        #endregion

        private void tsmiReScan_Click(object sender, EventArgs e)
        {
            this.ReScan = true;
            try
            {
                this.LastDocumentScan = this.ucImagesManipulation1.Pages.Where(pg => pg.Selected == true).First().Master;
                if (this.LastDocumentScan == null)
                    this.LastDocumentScan = this.ucImagesManipulation1.Documents.Where(doc => doc.Selected == true).First();
            }
            catch
            {
                try
                {
                    this.LastDocumentScan = this.ucImagesManipulation1.Documents.Where(doc => doc.Selected == true).First();
                }
                catch (Exception es)
                {
                    ACSLog.InsertLog(MessageBoxIcon.Error, es);
                    WFMessageBox.Show(es.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }

            if (this.LastDocumentScan == null)
            {
                WFMessageBox.Show("Selecione um documento!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ReScan = false;
                return;
            }

            this.LastDocumentScan.Selected = false;
            this.ucImagesManipulation1.EnableAfterSelect = false;

            this.ucImagesManipulation1.ClearUCImages();
            this.ucImagesManipulation1.LastDocSelected = null;
            this.LastDocumentScan.Nodes.OfType<Page>().ToList().ForEach(pg => pg.Checked = true);
            this.CountScan = 2;

            btnScan.PerformClick();
        }

        private void tsmiReLoad_Click(object sender, EventArgs e)
        {

            try
            {

                var Doc = this.ucImagesManipulation1.Pages.Where(pg => pg.Selected == true).First().Master;

                var newName = Doc.PathDir.FullName + "\\" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".jpeg";

                ExceptionCustom ss = new ExceptionCustom("LOG !", new Exception("Login TURL IMAGEM: " + Doc.Pages.FirstOrDefault().FileName));
                ACSLog.InsertLog(MessageBoxIcon.Asterisk, ss);

                Bitmap originalBmp = new Bitmap(Doc.Pages.FirstOrDefault().FileName);

                // Create a blank bitmap with the same dimensions
                Bitmap tempBitmap = new Bitmap(originalBmp.Width, originalBmp.Height);


                //CodecInfo para imagens Jpeg
                ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders().First(enc => enc.FormatID == ImageFormat.Jpeg.Guid);
                //EncoderParameters que vai setar o nível de qualidade (compressão)
                EncoderParameters imgParams = new EncoderParameters(1);
                //Qualidade em 100L = 100% de qualidade - sem compressão
                imgParams.Param = new[] { new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L) };
                //Salvar a imagem a imagem
                float brightness = (ACSConfig.GetImages().BrightnessReload / 10); // no change in brightness
                float contrast = 2.0f; // twice the contrast
                float gamma = 1.0f; // no change in gamma

                float adjustedBrightness = brightness - 1.0f;
                // create matrix that will brighten and contrast the image
                float[][] ptsArray ={
        new float[] {contrast, 0, 0, 0, 0}, // scale red
        new float[] {0, contrast, 0, 0, 0}, // scale green
        new float[] {0, 0, contrast, 0, 0}, // scale blue
        new float[] {0, 0, 0, 1.0f, 0}, // don't scale alpha
        new float[] {adjustedBrightness, adjustedBrightness, adjustedBrightness, 0, 1}};

                ImageAttributes imageAttributes = new ImageAttributes();
                imageAttributes.ClearColorMatrix();
                imageAttributes.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                imageAttributes.SetGamma(gamma, ColorAdjustType.Bitmap);



                using (Graphics g = Graphics.FromImage(tempBitmap))
                {

                    g.DrawImage(originalBmp, new Rectangle(0, 0, originalBmp.Width, originalBmp.Height)
                        , 0, 0, originalBmp.Width, originalBmp.Height,
                        GraphicsUnit.Pixel, imageAttributes);
                      
                }

                tempBitmap.Save(newName, codec, imgParams);

                ucImagesManipulation1.ReoloadDocument(Doc, newName);

            }
            catch (Exception ex)
            {

                ACSLog.InsertLog(MessageBoxIcon.Error, ex);
                WFMessageBox.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void imagensToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void x2ToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            try
            {


                if (!(sender as ToolStripMenuItem).Checked) return;

                tsmi1X1.CheckState = (sender == tsmi1X1) ? (sender as ToolStripMenuItem).CheckState : CheckState.Unchecked;
                tsmi1X2.CheckState = (sender == tsmi1X2) ? (sender as ToolStripMenuItem).CheckState : CheckState.Unchecked;
                tsmi2X2.CheckState = (sender == tsmi2X2) ? (sender as ToolStripMenuItem).CheckState : CheckState.Unchecked;
                tsmi4X4.CheckState = (sender == tsmi4X4) ? (sender as ToolStripMenuItem).CheckState : CheckState.Unchecked;
                SetColumnsRows();
            }
            catch (Exception ex)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, ex);
                WFMessageBox.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void SetopenFileDialog1()
        {

            var dlg = this.openFileDialog1;
            dlg.Filter = "";

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            string sep = string.Empty;
            var exts = string.Empty;
            var Index = 1;

            foreach (var c in codecs)
            {
                string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                dlg.Filter = String.Format("{0}{1}{2} ({3})|{3}", dlg.Filter, sep, codecName, c.FilenameExtension);
                sep = "|";
                exts = c.FilenameExtension + ";" + exts;
                Index++;
            }

            dlg.Filter = String.Format("{0}{1}{2} ({3})|{3}", dlg.Filter, sep, "PDF File", "*.PDF");
            sep = "|";
            exts = "*.PDF;" + exts;
            Index++;

            dlg.Filter = String.Format("{0}{1}{2} ({3})|{3}", dlg.Filter, sep, "All Files", exts);

            dlg.FilterIndex = Index;
            dlg.DefaultExt = ".jpg"; // Default file extension 

        }

        private void VerificaPermissoesTela()
        {
            var idGrupoUsuario = ACSGlobal.UsuarioLogado.USR_IDGRUPOUSUARIO;
            var fPreferencia = ACSGlobal.GrupoUsuario.GRP_FLAGPREFERENCIA;
            var fSuporte = ACSGlobal.GrupoUsuario.GRP_FLAGSUPORTE;
            menuStrip1.Items[2].Visible = false;
            //SUPORTE
            if (fPreferencia == 1 && fSuporte == 1)
            {
                menuStrip1.Items[2].Visible = true;
                tableLayoutPanel1.Visible = false;
                tableLayoutPanel1.Enabled = false;

                WFMessageBox.Show("Usuário com acesso de Suporte!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //clinicos e Digtalização e Importação
            if (fPreferencia == 0)
            {
                menuStrip1.Items[0].Visible = false;
            }


            if (ACSGlobal.UsuarioLogado.USR_FLAGDIGITALIZACAO == 1)
            {
                digitalizarToolStripMenuItem.Visible = true;
            }

            if (ACSGlobal.UsuarioLogado.USR_FLAGIMPORTACAO == 1)
            {
                importarToolStripMenuItem.Visible = true;
            }

        }

        private void WFCapture_Load(object sender, EventArgs e)
        {



            VerificaPermissoesTela();



            tsmi1X1.CheckedChanged += x2ToolStripMenuItem_CheckedChanged;
            tsmi1X2.CheckedChanged += x2ToolStripMenuItem_CheckedChanged;
            tsmi2X2.CheckedChanged += x2ToolStripMenuItem_CheckedChanged;
            tsmi4X4.CheckedChanged += x2ToolStripMenuItem_CheckedChanged;



            if ((ACSConfig.SystemAction & ModeSystem.Import) == ModeSystem.Import)
            {
                SetImagesbtnScan(TypeImagebtnScan.NotCheck);

                this.btnScan.Text = "(2) Importar Guias";
                this.tsmiReScan.Enabled = false;
                this.SetopenFileDialog1();
            }

            if (ACSConfig.GetApp().User == ModeUser.Multi)
            {
                if (ACSConfig.SystemAction == ModeSystem.Process)
                {
                    this.btnProcessar.Text = "(2) Concluir";
                    this.btnScan.Visible = false;
                }
                else
                    this.btnProcessar.Text = "(3) Concluir";

                SetImagesbtnProcessar(false);
                this.btnProcessar.Invalidate();
            }

            // verifica se interação é do tipo escaner e se tem escaner configurado

            // verificar se existe o driver no config

            //var driverValido = false;
            //var confInvalida = false;
            //if (!String.IsNullOrEmpty(ACSConfig.GetScanner().Driver))
            //{
            //    using (var tw = new Twain(this.Handle))
            //    {


            //        foreach (var device in tw.GetDevices())
            //        {
            //            if (device.ProductName == ACSConfig.GetScanner().Driver)
            //            {
            //                driverValido = true;
            //                break;
            //            }
            //        }

            //        //cbDrivers.Text = ACSConfig.GetScanner().Driver;
            //    }



            //}

            //if (String.IsNullOrEmpty(ACSGlobal.UsuarioLogado.USR_SERIALNUMBERCERT) || !driverValido)
            //{                
            //    confInvalida = true;
            //}

            if ((ACSConfig.SystemAction & ModeSystem.Scan) == ModeSystem.Scan && !ACSGlobal.configScanValida)
            {
                //WFMessageBox.Show("Driver de escâner não configurado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.btnLotes.Enabled = false;
            }
            else
            {
                this.btnLotes.Enabled = true;
            }


            if (ACSConfig.GetApp().DuplexInit == "YES")
            {
                ucImagesManipulation1.btnDuplex.PerformClick();
                ACSGlobal.Duplex = true;
            }
            else
            {
                ACSGlobal.Duplex = false;
                tsmi1X1.Checked = true;
                tsmi1X2.Checked = false;
                ucImagesManipulation1.ucImages.Columns = 1;
                ucImagesManipulation1.ucImages.Rows = 1;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void preferenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var wfPre = new WFPreferencias();
            wfPre.MdiParent = Program.MainForm;
            wfPre.WindowState = FormWindowState.Normal;
            wfPre.Dock = DockStyle.Fill;
            wfPre.BeforeExitEvent += BeforeExitPreferences;
            wfPre.Show();
        }

        public void BeforeExitWFWorkList(object sender)
        {
            var wfWorkL = (WFWorkList)sender;
            this.btnScan.Enabled = ACSGlobal.LoteSelecionado != null && this.ucImagesManipulation1.RefreshBarCodes();
            if (this.btnScan.Enabled)
            {
                ACSGlobal.LoteSelecionado.DIRLOTEINBOX = ACSConfig.GetStorage().Input + "\\" + ACSGlobal.LoteSelecionado.PAS_CODIGOPASSAGEM + "\\";
                if (!Directory.Exists(ACSGlobal.LoteSelecionado.DIRLOTEINBOX))
                    Directory.CreateDirectory(ACSGlobal.LoteSelecionado.DIRLOTEINBOX);

                SetImagesbtnLotes(true);

                SetImagesbtnProcessar(false);

                this.lbDescLote.Text = ACSGlobal.LoteSelecionado.PAS_CODIGOPASSAGEM.ToString();
                this.lblNomePaciente.Text = ACSGlobal.LoteSelecionado.NOME.ToString();

                this.Lote = ACSDataBase.NewGEDLotesXUsuarios(ACSGlobal.UsuarioLogado.USR_IDUSUARIO, ACSGlobal.LoteSelecionado.PAS_CODIGOPASSAGEM, (int)StatusLote.Capturando);


                if (ACSConfig.SystemAction == ModeSystem.Process)
                {
                    var Dirinf = new DirectoryInfo(ACSGlobal.LoteSelecionado.LTU_PathImagens);

                    this.ucImagesManipulation1.OCRInImage = true;
                    foreach (var fl in Dirinf.GetFiles("*." + ACSConfig.GetImages().Format.ToString()))
                    {
                        this.ucImagesManipulation1.AddDocument(fl.FullName);
                    }
                    SetImagesbtnScan(TypeImagebtnScan.Check);
                    this.ucImagesManipulation1.EnableAfterSelect = true;

                    ACSDataBase.SaveGedLotes(this.Lote, (int)StatusLote.Processando);
                }
            }
            else
            {
                ACSGlobal.LoteSelecionado = null;
                this.Lote = null;
            }


        }

        public void BeforeExitPreferences(object sender)
        {
            if (ACSGlobal.TipoCaptura == 1)
            {
                if (!String.IsNullOrEmpty(ACSConfig.GetScanner().Driver))
                {
                    this.btnLotes.Enabled = true;
                }
                else
                {
                    this.btnLotes.Enabled = false;
                }
            }
            else
            {
                this.btnLotes.Enabled = true;
            }
        }

        private void btnLotes_Click(object sender, EventArgs e)
        {

            if (ACSGlobal.LoteSelecionado != null)
            {
                var old = this.Lote;

                if (!this.Clear())
                    return;

                ACSDataBase.SaveGedLotes(old, (int)StatusLote.CapturaCancelada);

            }

            var wfWorkL = new WFWorkList();
            wfWorkL.MdiParent = Program.MainForm;
            wfWorkL.WindowState = FormWindowState.Normal;
            wfWorkL.Dock = DockStyle.Fill;
            wfWorkL.BeforeExitEvent += BeforeExitWFWorkList;
            wfWorkL.Show();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {

                this.ucImagesManipulation1.OCRInImage = ACSConfig.GetApp().User == ModeUser.Mono;
                var nameDriver = ACSConfig.GetScanner().Driver;

                if ((ACSConfig.SystemAction & ModeSystem.Scan) == ModeSystem.Scan)
                {
                    if (IsScanning())
                    {
                        StopScan();

                        if (nameDriver.ToUpper().Contains("LEXMARK"))
                        {
                            WFMessageBox.Show("Serviço de Scanner cancelado, clique em Digitalizar Documentos novamente!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.btnScan.Image = global::ACSMinCapture.Properties.Resources._1366855784_Scanner1;
                        }
                    }
                    else if (nameDriver.ToUpper().Contains("FUJITSU") || nameDriver.ToUpper().Contains("SP-1120"))
                        Scan(this.CountScan, this.driverFujitsu, this);
                    else
                        Scan(this.CountScan);
                }
                else
                {
                    if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        WFTranparentLoading.ShowLoading(Program.MainForm);
                        //this.ucImagesManipulation1.AddDocument(this.openFileDialog1.FileNames);
                        foreach (var fl in this.openFileDialog1.FileNames)
                        {
                            var newfl = GetPathInbox() + Path.GetFileName(fl);
                            var Copied = false;

                            if (Path.GetExtension(fl).ToUpper() != ".PDF")
                                Copied = CopyImage(fl, newfl);
                            else
                            {
                                Copied = true;
                                newfl = fl;
                            }

                            if (!Copied)
                            {
                                WFMessageBox.Show("Não foi possivél copiar " + fl, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            else
                                this.ucImagesManipulation1.AddDocument(newfl);
                        }

                        SetImagesbtnScan(TypeImagebtnScan.Check);
                        this.ucImagesManipulation1.EnableAfterSelect = true;
                    }
                    WFTranparentLoading.CloseLoading();
                }

            }
            catch (Exception ex)
            {
                WFTranparentLoading.CloseLoading();
                ACSLog.InsertLog(MessageBoxIcon.Error, ex);
            }
        }

        private void ucImagesManipulation1_AfterAllDocumentsValid(bool Valids)
        {
            this.btnProcessar.Enabled = Valids || ACSConfig.GetApp().User == ModeUser.Multi;
        }

        void SetImagesbtnLotes(bool Check)
        {
            if (Check)
                this.btnLotes.Image = global::ACSMinCapture.Properties.Resources._1366855840_Control_Panel_Check;
            else
                this.btnLotes.Image = global::ACSMinCapture.Properties.Resources._1366855840_Control_Panel;
        }

        enum TypeImagebtnScan { Check, NotCheck, Stop }

        void SetImagesbtnScan(TypeImagebtnScan typeImage)
        {
            WFTranparentLoading.ShowLoading(Program.MainForm);
            if ((ACSConfig.SystemAction & ModeSystem.Scan) == ModeSystem.Scan)
            {
                if (typeImage == TypeImagebtnScan.Check)
                    this.btnScan.Image = global::ACSMinCapture.Properties.Resources._1366855784_Scanner1_check;
                else if (typeImage == TypeImagebtnScan.NotCheck)
                    this.btnScan.Image = global::ACSMinCapture.Properties.Resources._1366855784_Scanner1;
                else if (typeImage == TypeImagebtnScan.NotCheck)
                    this.btnScan.Image = global::ACSMinCapture.Properties.Resources._1366855784_Scanner1_stop;
            }
            else
            {
                if (typeImage == TypeImagebtnScan.Check)
                    this.btnScan.Image = global::ACSMinCapture.Properties.Resources._1389844340_document_import_checked;
                else if (typeImage == TypeImagebtnScan.NotCheck)
                    this.btnScan.Image = global::ACSMinCapture.Properties.Resources._1389844340_document_import;
                WFTranparentLoading.CloseLoading();
            }


        }

        void SetImagesbtnProcessar(bool Check)
        {

            if (ACSConfig.GetApp().User == ModeUser.Multi && ACSConfig.SystemAction != ModeSystem.Process)
            {
                if (Check)
                    this.btnProcessar.Image = global::ACSMinCapture.Properties.Resources._1407299872_network_wired_checked;
                else
                    this.btnProcessar.Image = global::ACSMinCapture.Properties.Resources._1407299872_network_wired;
            }
            else if (ACSConfig.GetApp().User == ModeUser.Mono || ACSConfig.SystemAction == ModeSystem.Process)
            {
                if (Check)
                    this.btnProcessar.Image = global::ACSMinCapture.Properties.Resources._1371251931_kservices_check;
                else
                    this.btnProcessar.Image = global::ACSMinCapture.Properties.Resources._1371251931_kservices;
            }

        }

        bool Clear(bool Confirm = true, bool Observation = false)
        {
            var dr = DialogResult.Yes;
            if (ACSGlobal.LoteSelecionado != null && iEscolhaCaptura == 0)
            {
                if (Confirm)
                    dr = WFMessageBox.Show("Existe um lote em processamento. Deseja continuar?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {

                    // this.ucImagesManipulation1.tvDocs.Nodes.Clear();                    
                    this.ucImagesManipulation1.Clear();

                    if (Directory.Exists(ACSGlobal.LoteSelecionado.DIRLOTEINBOX))
                        Directory.Delete(ACSGlobal.LoteSelecionado.DIRLOTEINBOX, true);

                    this.IndexImagesCapture = 0;
                    this.lbDescLote.Text = "...";
                    this.lblNomePaciente.Text = "...";

                    SetImagesbtnLotes(false);
                    SetImagesbtnScan(TypeImagebtnScan.NotCheck);
                    SetImagesbtnProcessar(false);

                    this.btnScan.Enabled = false;
                    this.btnProcessar.Enabled = false;

                    this.Lote = null;
                    ACSGlobal.LoteSelecionado = null;
                    WFLoading.CloseLoad();
                }
                else
                {
                    ACSGlobal.ContinuaLote = true;
                    return false;
                }

            }
            else if (iEscolhaCaptura != 0)
            {

                string msgAlerta = "";
                if (ACSGlobal.LoteSelecionado != null)
                    msgAlerta = "Existe um lote em processamento. Deseja continuar?";
                else
                    msgAlerta = "Deseja trocar o tipo da ação a ser executada?";

                if (Confirm)
                    dr = WFMessageBox.Show(msgAlerta, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {

                    if (iEscolhaCaptura == 1)
                    {
                        var driverValido = false;
                        var confValida = true;
                        string mensagem = "Verifique os seguinte problemas de configuração: ";
                        if (!String.IsNullOrEmpty(ACSConfig.GetScanner().Driver))
                        {
                            using (var tw = new Twain(this.Handle))
                            {


                                foreach (var device in tw.GetDevices())
                                {
                                    if (device.ProductName == ACSConfig.GetScanner().Driver)
                                    {
                                        driverValido = true;
                                        break;
                                    }
                                }
                            }
                        }

                        if ((ACSConfig.SystemAction & ModeSystem.Scan) == ModeSystem.Scan && !driverValido)
                        {
                            mensagem += "\n\tDriver de escâner não configurado";
                            confValida = false;
                        }


                        Global.ACSGlobal.configScanValida = confValida;
                        btnScan.Text = "(2) Digitalizar Documentos";
                        if (!confValida)
                        {
                            if (ACSGlobal.GrupoUsuario.GRP_FLAGPREFERENCIA == 0)
                            {
                                mensagem += "\nEntre em contato com o suporte";
                                WFMessageBox.Show(mensagem, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                this.Close();
                            }
                            else
                            {

                                WFMessageBox.Show(mensagem, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }

                        ACSConfig.SystemAction = ModeSystem.Scan;
                        Global.ACSGlobal.TipoCaptura = 1;

                        if (ACSConfig.GetApp().User == ModeUser.Mono)
                            ACSConfig.SystemAction = ACSConfig.SystemAction | ModeSystem.Process;

                        ucImagesManipulation1.btnDuplex.Visible = true;

                        if (ACSConfig.GetApp().DuplexInit == "YES")
                        {

                            tsmi1X1.Checked = false;
                            tsmi1X2.Checked = true;
                            ucImagesManipulation1.ucImages.Columns = 2;
                            ucImagesManipulation1.ucImages.Rows = 1;


                            Bitmap image1 = new Bitmap(ACSMinCapture.Properties.Resources.duplexYes);
                            ucImagesManipulation1.btnDuplex.Image = image1;
                            ACSGlobal.Duplex = true;
                        }
                        else
                        {
                            tsmi1X1.Checked = true;
                            tsmi1X2.Checked = false;
                            ucImagesManipulation1.ucImages.Columns = 1;
                            ucImagesManipulation1.ucImages.Rows = 1;

                            ACSGlobal.Duplex = false;
                            Bitmap image1 = new Bitmap(ACSMinCapture.Properties.Resources.duplexNot);
                            ucImagesManipulation1.btnDuplex.Image = image1;
                        }





                    }
                    else if (iEscolhaCaptura == 2)
                    {
                        btnScan.Text = "(2) Importar Documentos";
                        ACSConfig.SystemAction = ModeSystem.Import;
                        ucImagesManipulation1.btnDuplex.Visible = false;
                        Global.ACSGlobal.TipoCaptura = 2;

                        if (ACSConfig.GetApp().User == ModeUser.Mono)
                            ACSConfig.SystemAction = ACSConfig.SystemAction | ModeSystem.Process;

                    }
                    else if (iEscolhaCaptura == 3)
                    {
                        ACSConfig.SystemAction = ModeSystem.Process;
                        Global.ACSGlobal.TipoCaptura = 4;

                        if (ACSConfig.GetApp().User == ModeUser.Mono)
                            ACSConfig.SystemAction = ACSConfig.SystemAction | ModeSystem.Process;
                        try
                        {
                            var main = new WFMain();
                            var wfTAs = new WFAssinaNivel2();
                            wfTAs.WindowState = FormWindowState.Normal;
                            wfTAs.Dock = DockStyle.Fill;
                            wfTAs.TopMost = true;
                            wfTAs.MdiParent = main.MdiParent;
                            wfTAs.Show();
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }

                    }




                    iEscolhaCaptura = 0;
                    this.ucImagesManipulation1.Clear();

                    if (ACSGlobal.LoteSelecionado != null)
                        if (Directory.Exists(ACSGlobal.LoteSelecionado.DIRLOTEINBOX))
                            Directory.Delete(ACSGlobal.LoteSelecionado.DIRLOTEINBOX, true);

                    this.IndexImagesCapture = 0;
                    this.lbDescLote.Text = "...";
                    this.lblNomePaciente.Text = "...";

                    SetImagesbtnLotes(false);
                    SetImagesbtnScan(TypeImagebtnScan.NotCheck);
                    SetImagesbtnProcessar(false);

                    this.btnScan.Enabled = false;
                    this.btnProcessar.Enabled = false;

                    this.Lote = null;
                    ACSGlobal.LoteSelecionado = null;
                    WFLoading.CloseLoad();
                }
                else
                {
                    iEscolhaCaptura = 0;
                    ACSGlobal.ContinuaLote = true;
                    return false;
                }
            }
            return dr == DialogResult.Yes;
        }

        bool CopyImage(string FileName, string NewFileName)
        {
            File.Copy(FileName, NewFileName, true);
            return File.Exists(NewFileName);
        }

        bool CopySignature(string FileName, string NewFileName)
        {
            File.Copy(FileName, NewFileName, true);
            return File.Exists(NewFileName);
        }

        bool MoveImage(string FileName, string NewFileName)
        {
            using (Bitmap bmp = new Bitmap(FileName))
            {
                var pth = Path.GetDirectoryName(NewFileName);
                var nme = Path.GetFileNameWithoutExtension(NewFileName);
                var newfn = string.Concat(pth, @"\", nme, Path.GetExtension(NewFileName));
                bmp.Save(newfn);
                return File.Exists(NewFileName);
            }
            //File.Move(FileName,NewFileName);
            //return File.Exists(NewFileName);
        }

        bool MoveSignature(string FileName, string NewFileName)
        {
            using (Bitmap bmp = new Bitmap(FileName))
            {
                var pth = Path.GetDirectoryName(NewFileName);
                var nme = Path.GetFileNameWithoutExtension(NewFileName);
                var newfn = string.Concat(pth, @"\", nme, Path.GetExtension(NewFileName));
                bmp.Save(newfn);
                return File.Exists(NewFileName);
            }
            //File.Move(FileName,NewFileName);
            //return File.Exists(NewFileName);
        }

        string Aspas(string Value)
        {
            return string.Concat("\"", Value, "\"");
        }

        bool Process()
        {

            try
            {
                bool fAssinado = false;
                var pags = this.ucImagesManipulation1.Pages;
                var sb = new List<string>();
                var Output = string.Empty;
                var result = false;
                var Inclusao = ACSGlobal.LoteSelecionado.INCLUSAO;
                var OrderDoc = ACSGlobal.LoteSelecionado.MAX_ORDER;
                var idFormato = ACSDataBase.GetIdFormato(ACSConfig.GetImages().Format.ToString());
                var OutputIni = ACSConfig.GetStorage().Output;
                var MaskPageName = ACSConfig.GetImages().MaskPageName;
                var FormatImageIni = ACSConfig.GetImages().Format.ToString();
                var InsertDocuments = ACSConfig.GetApp().User == ModeUser.Mono || (ACSConfig.SystemAction & ModeSystem.Process) == ModeSystem.Process;

                var Docs = new List<GEDDOCUMENTOS>();

                foreach (var pag in pags)
                {
                    Inclusao++;
                    OrderDoc++;
                    var Doc = pag.Master;

                    WFTranparentLoading.Messege("Verificando " + Doc.Text + "...");

                    var NewName = OutputIni + "\\" +
                                 ACSGlobal.LoteSelecionado.PAS_REGISTRO + "\\" +
                                 ACSGlobal.LoteSelecionado.PAS_REGISTRO + Doc.BarCodes[0].DIV_CODIGOREDUZIDO + "\\" +
                                 ACSGlobal.LoteSelecionado.PAS_CODIGOPASSAGEM;
                    Output = NewName;


                    if (!Directory.Exists(Output))
                    {
                        try
                        {
                            Directory.CreateDirectory(Output);
                            if (!Directory.Exists(Output))
                            {
                                WFMessageBox.Show("Diretório não pode ser criado. Verifique! " + "\n" + Output, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                        catch (Exception e)
                        {
                            WFMessageBox.Show(e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }

                    NewName = string.Concat(NewName, "\\",
                        string.Format("{0:" + MaskPageName + "}", Inclusao), ".", FormatImageIni);

                    if (!MoveImage(pag.FileName, NewName))
                        return false;


                    string pathFileName = Path.GetDirectoryName(pag.FileName) + "\\" + Path.GetFileNameWithoutExtension(pag.FileName);
                    string pathNewName = Path.GetDirectoryName(NewName) + "\\" + Path.GetFileNameWithoutExtension(NewName);
                    string sExtession = Path.GetExtension(NewName);
                    if (File.Exists(pathFileName + "_N1.p7s"))
                    {
                        fAssinado = true;
                        if (!CopySignature(pathFileName + "_N1.p7s", pathNewName + "_N1.p7s"))
                            return false;
                    }


                    string dataEmission = "1900-01-01";
                    string dataValidity = "1900-01-01";

                    if (Doc.BarCodes[0].StartDateValidity.Value != DateTime.MinValue)
                        dataEmission = Doc.BarCodes[0].StartDateValidity.Value.Date.ToString("yyyy-MM-dd");

                    if (Doc.BarCodes[0].DateValidity.Value != DateTime.MinValue)
                        dataValidity = Doc.BarCodes[0].DateValidity.Value.Date.ToString("yyyy-MM-dd");

                    sb.Add(String.Concat(Aspas("1"), ";",
                                  Aspas("Orion_Capto01"), ";",
                                  Aspas(Path.GetFileName(NewName)), ";",
                                  Aspas(Doc.BarCodes[0].TPD_CODIGOBARRA), ";",
                                  Aspas(ACSGlobal.LoteSelecionado.PAS_CODIGOPASSAGEM), ";",
                                  Aspas(dataEmission), ";",
                                  Aspas(dataValidity)));



                    if (InsertDocuments)
                    {
                        var GedDoc = new GEDDOCUMENTOS();
                        GedDoc.DOC_IDVOLUME = 1;
                        GedDoc.DOC_IDDIVISAO = Doc.BarCodes[0].TPD_IDDIVISAO;
                        GedDoc.DOC_IDPASSAGEM = ACSGlobal.LoteSelecionado.PAS_IDPASSAGEM;
                        GedDoc.DOC_IDFORMATO = idFormato;
                        GedDoc.DOC_IDTIPODOCUMENTO = Doc.BarCodes[0].TPD_IDTIPODOCUMENTO;
                        GedDoc.DOC_NOMEARQUIVO = Path.GetFileNameWithoutExtension(NewName);
                        GedDoc.DOC_PATH = ACSGlobal.LoteSelecionado.PAS_REGISTRO;
                        GedDoc.DOC_IDSUBTIPODOCUMENTO = Doc.BarCodes[0].STD_IDSUBTIPOSDOCUMENTOS;
                        GedDoc.DOC_DATAVENCIMENTO = Convert.ToDateTime(dataValidity);
                        GedDoc.DOC_DATAEMISSAO = Convert.ToDateTime(dataEmission);
                        GedDoc.DOC_TEMPOVALIDADE = Doc.BarCodes[0].TPD_TEMPOVALIDADE;
                        GedDoc.DOC_DATAHORACADASTRO = DateTime.Now;
                        GedDoc.DOC_VENCIMENTORESOLVIDO = 0;
                        GedDoc.DOC_IDUSUARIOACSCAPTURE = (int)ACSGlobal.UsuarioLogado.USR_IDUSUARIO;
                        GedDoc.DOC_TIPOCAPTURA = ACSGlobal.TipoCaptura;
                        GedDoc.DOC_FLAGATIVO = 1;
                        GedDoc.DOC_ORDEM_VISUALIZACAO = OrderDoc;
                        GedDoc.DOC_EXTENSAONOMEARQUIVO = sExtession;
                        GedDoc.DOC_VERSAO = 1;
                        GedDoc.DOC_FLAGAPROVADO = ACSDataBase.VerificaDocumentoAprovar(Doc.BarCodes[0].STD_IDSUBTIPOSDOCUMENTOS);
                        GedDoc.DOC_FLAGDOCEXTERNO = (ACSGlobal.TipoCaptura == 1) ? 0 : 1;
                        GedDoc.DOC_FLAGREVISADO = 0;
                        GedDoc.DOC_FLAGPENDENTEUPLOAD = 0;
                        GedDoc.DOC_IDGRUPOUSUARIOCAPTURA = ACSGlobal.UsuarioLogado.USR_IDGRUPOUSUARIO.GetValueOrDefault();
                        GedDoc.DOC_VERSAOACS = "ACS - "+ Assembly.GetEntryAssembly().GetName().Version.ToString();
                        GedDoc.DOC_IDURLSTORAGE = 1;
			 


						if (fAssinado)
                        {

                            GedDoc.DOC_FLAGCERTIFICADO = 1;
                            GedDoc.DOC_IDUSUARIOASSINANIVEL1 = (int)ACSGlobal.UsuarioLogado.USR_IDUSUARIO;
                            GedDoc.DOC_DATAASSINANIVEL1 = DateTime.Now;
                            GedDoc.DOC_DETAIL_CERT = getInfoCertificado(ACSGlobal.UsuarioLogado.USR_SERIALNUMBERCERT);
                        }
                        else
                            GedDoc.DOC_FLAGCERTIFICADO = 0;


                        Docs.Add(GedDoc);
                    }

                    ACSGlobal.LoteSelecionado.INCLUSAO = Inclusao;

                }

                var DataLote = DateTime.Now;
                this.Lote.LTU_DATA = DataLote;
                this.Lote.LTU_PATHIMAGENS = Output;

                if (sb.Count() > 0)
                {

                    if (InsertDocuments)
                    {
                        result = ACSDataBase.InsertDocuments(Docs.ToArray());
                    
                        if (result)
                        {
                            ACSDataBase.SaveGedLotes(this.Lote, (int)StatusLote.Processado);
                        }
                    }
                    else
                    {
                        ACSDataBase.SaveGedLotes(this.Lote, (int)StatusLote.Capturado);
                        result = true;
                    }

                    string s = string.Concat(Output, "\\INDEX_" + DataLote.ToString("yyyyMMddHHmmss") + ".DAT");
                    StreamWriter sw = new StreamWriter(s, true);
                    sb.ForEach(l => { sw.WriteLine(l); });
                    sw.Close();
                    sw.Dispose();
                }
                else
                    result = true;

                if (result)
                    this.Clear(false);

                return result;
            }
            catch (Exception e)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, e);
                return false;
            }
        }

        public static string DecryptString(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(""));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Message);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }

        void AfterProcess(bool Processed)
        {
            WFTranparentLoading.CloseLoading();
            if (Processed)
            {
                WFMessageBox.Show("Processo Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                //if (ACSGlobal.UsuarioLogado.USR_FLAGASSINA == 1)
                //{
                //    if (WFMessageBox.Show("Deseja assinar o lote?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //    {
                //        //var exeSigner = @"C:\Users\William\Documents\Developer\Orion\ODSigner\Fontes\exe\ODSigner.exe ";
                //        var exeSigner = Path.GetDirectoryName(Application.ExecutablePath) + @"\ODSigner.exe ";

                //        var args = string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" ", ACSGlobal.UsuarioLogado.USR_LOGIN, ACSGlobal.PassLogin, Assinar.CodigoPassagem, Assinar.DataLote.ToString("yyyy-MM-dd"));
                //        System.Diagnostics.Process.Start(exeSigner, args);
                //    }
                //}
            }
            else
                WFMessageBox.Show("Erro no processo! Verifique Log.", MessageBoxButtons.OK, MessageBoxIcon.Error);

            this.Assinar = null;

        }

        new void CheckForIllegalCrossThreadCalls(bool value)
        {
            ImageBox.CheckForIllegalCrossThreadCalls = value;
            RichTextBox.CheckForIllegalCrossThreadCalls = value;
            DataGridView.CheckForIllegalCrossThreadCalls = value;
            Control.CheckForIllegalCrossThreadCalls = false;
            TreeView.CheckForIllegalCrossThreadCalls = false;
            Form.CheckForIllegalCrossThreadCalls = false;
        }

        LoteAssina Assinar;
        string sCertificado = "";
        private void btnProcessar_Click(object sender, EventArgs e)
        {
            this.Assinar = new LoteAssina();
            this.Assinar.CodigoPassagem = ACSGlobal.LoteSelecionado.PAS_CODIGOPASSAGEM;
            this.Assinar.DataLote = DateTime.Now;

            if ((ACSConfig.SystemAction & ModeSystem.Scan) == ModeSystem.Scan)
            {

                if (ACSGlobal.UsuarioLogado.USR_FLAGASSINA == 1 || ACSGlobal.UsuarioLogado.USR_FLAGASSINA == 2)
                {


                    WFMessageBox.Show("A assinatura nos documentos será realizada ao processar o lote", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // assinatura
                    try
                    {
                        if (String.IsNullOrEmpty(ACSGlobal.UsuarioLogado.USR_SERIALNUMBERCERT))
                        {
                            WFTranparentLoading.CloseLoading();

                            WFMessageBox.Show("Usuário sem certificado digital vinculado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (!Assinador.IsCetificadoComputador())
                        {
                            WFTranparentLoading.CloseLoading();

                            WFMessageBox.Show("Certificado do usuário não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }


                    //    var fAssina = ACSGlobal.UsuarioLogado.USR_FLAGASSINA;

                        sCertificado = ACSGlobal.UsuarioLogado.USR_SERIALNUMBERCERT;
                        string fileName = "";
                        var pags = this.ucImagesManipulation1.Pages;
                        foreach (var item in pags)
                        {
                            WFTranparentLoading.ShowLoading(this);

                            WFTranparentLoading.Messege("Assinando Documentos...");

                            fileName = item.FileName;

                            byte[] assinatura = Assinador.assinar(Assinador.ConvertDocToStream(fileName), sCertificado);
                            //if (assinatura == null)
                            //{
                            //    WFTranparentLoading.CloseLoading();
                            //    WFMessageBox.Show("Não foi possível assinar os documentos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    return;
                            //}
                            string pathNew = Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName);
                            System.IO.FileStream _FileStream = new System.IO.FileStream(pathNew + "_N1.p7s", System.IO.FileMode.Create, System.IO.FileAccess.Write);

                            _FileStream.Write(assinatura, 0, assinatura.Length);

                            // close file stream
                            _FileStream.Close();


                        }
                    }
                    catch (ExceptionCustom ex)
                    {
                        WFTranparentLoading.CloseLoading();

                        WFMessageBox.Show("Processamento cancelado. \n " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    WFMessageBox.Show("Documentos Assinados!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }



                WFTranparentLoading.CloseLoading();


            }

            CheckForIllegalCrossThreadCalls(false);
            WFTranparentLoading.ShowLoading(this);


            var tasks = new List<Task>();

            var taskProcess = Task.Factory.StartNew<bool>(() =>
                {
                    if (ACSConfig.GetNetworkAccesser().Valid)
                    {
                        try
                        {
                            using (var nsa = NetworkShareAccesser.Access(ACSConfig.GetStorage().Output,
                                                                         ACSConfig.GetNetworkAccesser().Domain,
                                                                         ACSConfig.GetNetworkAccesser().User,
                                                                         ACSConfig.GetNetworkAccesser().Password))
                            {
                                return this.Process();
                            }
                        }
                        catch (Exception g)
                        {
                            ACSLog.InsertLog(MessageBoxIcon.Error, g);
                            WFMessageBox.Show(g.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    else
                        return this.Process();
                });

            tasks.Add(taskProcess);
            Task.Factory.ContinueWhenAll(tasks.ToArray(), tk => AfterProcess((tk.First() as Task<bool>).Result));
        }


        private void WFCapture_AfterEndingScanEvent(object sender, bool Scanned)
        {
            if (Scanned)
            {
                SetImagesbtnScan(TypeImagebtnScan.Check);
            }

            this.ucImagesManipulation1.EnableAfterSelect = true;

            this.btnScan.Enabled = true;
            this.btnLotes.Enabled = true;
            try
            {
                this.ucImagesManipulation1.ZoomFitOnLoadBitmap = false;
                if (!this.ReScan)
                    this.ucImagesManipulation1.Pages.Last().Selected = true;
                else
                    this.LastDocumentScan.Selected = true;
            }
            catch
            {
            }
            WFTranparentLoading.CloseLoading();
            this.LastDocumentScan = null;
            this.ReScan = false;
            this.CountScan = 200;

        }

        public void WFCapture_AfterScanEvent(bool Scanning)
        {
            try
            {
                if (Scanning)
                {
                    this.ucImagesManipulation1.ZoomFitOnLoadBitmap = true;
                    SetImagesbtnScan(TypeImagebtnScan.Stop);
                    this.Lote.LTU_IDSTATUSLOTE = 2;
                    this.Lote.LTU_DATA = DateTime.Now;
                }
                else
                {
                    SetImagesbtnScan(TypeImagebtnScan.NotCheck);
                }
                this.btnScan.Enabled = true;
            }
            catch
            {
            }
            WFTranparentLoading.CloseLoading();
        }

        private void WFCapture_BeforeScanEvent(object sender)
        {
            // this.btnScan.Enabled = false;
            //      this.btnLotes.Enabled = false;
            WFTranparentLoading.ShowLoading(this);
        }

        private void WFCapture_FormClosing(object sender, FormClosingEventArgs e)
        {
            var idGrupoUsuario = ACSGlobal.UsuarioLogado.USR_IDGRUPOUSUARIO;
            var fPreferencia = ACSGlobal.GrupoUsuario.GRP_FLAGPREFERENCIA;
            var fSuporte = ACSGlobal.GrupoUsuario.GRP_FLAGSUPORTE;

            //SUPORTE
            if (fPreferencia == 1 && fSuporte == 1)
            {
                Application.Exit();
            }
            else
            {
                Clear(true);
                e.Cancel = true;

                this.tableLayoutPanel1.Visible = true;
                WFTranparentLoading.CloseLoading();

            }

        }

        private void WFCapture_ReScanTransferPictureEvent(ref Bitmap bitmap)
        {
            if (this.LastDocumentScan != null)
            {

                Page pag = null;

                try
                {
                    pag = this.LastDocumentScan.Pages.Where(pg => pg.Checked == true).First();
                }
                catch
                {
                }

                if (pag != null)
                {
                    AfterScanSaveBitmap(ref bitmap, pag.FileName);
                    pag.Checked = false;
                }
                else
                {
                    var FileName = AfterScanSaveBitmap(ref bitmap);
                    this.ucImagesManipulation1.AddPageInDocument(this.LastDocumentScan, FileName, bitmap);
                }

            }
        }

        string GetPathInbox()
        {
            return ACSConfig.GetStorage().Input + "\\" + ACSGlobal.LoteSelecionado.PAS_CODIGOPASSAGEM + "\\";
        }
        string AfterScanSaveBitmap(ref Bitmap bitmap, string FileName = "", bool incIndexImagesCapture = true)
        {

            var FormatImageIni = ACSConfig.GetImages().Format;
            var PathFileName = GetPathInbox();

            if (!Directory.Exists(PathFileName))
                Directory.CreateDirectory(PathFileName);

            if (incIndexImagesCapture)
                this.IndexImagesCapture++;

            if (FileName == string.Empty)
                FileName = PathFileName + this.IndexImagesCapture.ToString(ACSConfig.GetImages().MaskPageName) + "." + FormatImageIni.ToString();

            bitmap.Save(FileName, FormatImageIni);
            return FileName;
        }

        private void WFCapture_TransferPictureEvent(ref Bitmap bitmap)
        {

            if (ReScan)
            {
                WFCapture_ReScanTransferPictureEvent(ref bitmap);
                return;
            }

            var FileName = AfterScanSaveBitmap(ref bitmap);

            ///*
            if (this.LastDocumentScan == null)
            {
                this.LastDocumentScan = this.ucImagesManipulation1.AddDocument(FileName, bitmap);

                if (!ACSGlobal.Duplex)
                {
                    this.LastDocumentScan = null;
                }


            }
            else
            {
                this.ucImagesManipulation1.AddPageInDocument(this.LastDocumentScan, FileName, bitmap);
                this.LastDocumentScan = null;
            }

            //*/
            if (!ACSConfig.GetScanner().Driver.ToUpper().Contains("FUJITSU") && !ACSConfig.GetScanner().Driver.ToUpper().Contains("SP-1120"))
                Application.DoEvents();
        }

        private void excluirLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ucImagesManipulation1_Load(object sender, EventArgs e)
        {

        }


        // Retirado da documentação
        public const int CERT_SYSTEM_STORE_CURRENT_USER = 0;
        public string getInfoCertificado(string chave)
        {

            try
            {

                StringBuilder retorno = new StringBuilder();


                BRYSIGNERCOMLib.IRepositorio repositorio = new BRYSIGNERCOMLib.Repositorio();
                repositorio.inicialize("MY", CERT_SYSTEM_STORE_CURRENT_USER);
                int totalCertificados = repositorio.getCountCertificados();

                BRYSIGNERCOMLib.ICertificado certificado = null;


                for (int i = 0; i < totalCertificados; ++i)
                {
                    certificado = repositorio.getCertificado(i);

                    if (certificado.getIdCertificado() == chave)
                    {


                        retorno.Append("<Certs>  ");
                        retorno.Append("  <Cert1>  ");
                        retorno.Append("    <DataTerminio>" + certificado.getDataTermino() + "</DataTerminio>  ");
                        retorno.Append("    <IdCertificado>" + certificado.getIdCertificado() + "</IdCertificado>  ");
                        retorno.Append("    <Assunto>C = " + certificado.getAssuntoC() + "   O = " + certificado.getAssuntoO() + "  OU =  " + certificado.getAssuntoOU() + "  CN = " + certificado.getAssuntoCN() + "  </Assunto>  ");
                        retorno.Append("    <AssuntoCN>" + certificado.getAssuntoCN() + "</AssuntoCN>  ");
                        retorno.Append("    <AssuntoO>" + certificado.getAssuntoO() + " </AssuntoO>  ");
                        retorno.Append("    <AssuntoOU>" + certificado.getAssuntoOU() + "</AssuntoOU>  ");
                        retorno.Append("    <AssuntoL>" + certificado.getAssuntoL() + "</AssuntoL>  ");
                        retorno.Append("    <AssuntoS>" + certificado.getAssuntoS() + "</AssuntoS>  ");
                        retorno.Append("    <AssuntoC>" + certificado.getAssuntoC() + "</AssuntoC>  ");
                        retorno.Append("    <AssuntoE>" + certificado.getAssuntoE() + "</AssuntoE>  ");
                        retorno.Append("    <Emissor>C = " + certificado.getEmissorC() + "  O = " + certificado.getEmissorO() + "  OU = " + certificado.getEmissorOU() + "B  CN = " + certificado.getEmissorCN() + "  </Emissor>  ");
                        retorno.Append("    <EmissorCN>" + certificado.getEmissorCN() + "  </EmissorCN>  ");
                        retorno.Append("    <EmissorO>" + certificado.getEmissorO() + "  </EmissorO>  ");
                        retorno.Append("    <EmissorOU>" + certificado.getEmissorOU() + "  </EmissorOU>  ");
                        retorno.Append("    <EmissorL>" + certificado.getEmissorL() + "  </EmissorL>  ");
                        retorno.Append("    <EmissorS>" + certificado.getEmissorS() + "  </EmissorS>  ");
                        retorno.Append("    <EmissorC>" + certificado.getEmissorC() + "  </EmissorC>  ");
                        retorno.Append("    <EmissorE>" + certificado.getEmissorE() + "  </EmissorE>  ");
                        retorno.Append("    <NumeroSerial>" + certificado.getNumeroSerial() + "</NumeroSerial>  ");
                        retorno.Append("    <DataInicio>" + certificado.getDataInicio() + "  </DataInicio>  ");
                        retorno.Append("    <CPF>" + certificado.getCPF() + "  </CPF>  ");
                        retorno.Append("    <CPNJ>" + certificado.getCNPJ() + "  </CPNJ>  ");
                        retorno.Append("  </Cert1>  ");
                        retorno.Append(" </Certs>  ");



                        certificado.finalize();
                        break;
                    }

                }
                repositorio.finalize();

                return retorno.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ACSDataBase.StopSession();
            Application.ExitThread();
        }



        private void importarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iEscolhaCaptura = 2;

            this.Close();
        }

        private void digitalizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iEscolhaCaptura = 1;


            this.Close();
        }

        private void assinarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iEscolhaCaptura = 3;


            this.Close();
        }

        private void driverFujitsu_ScanToFile(object sender, AxFiScnLib._DFiScnEvents_ScanToFileEvent e)
        {
            var strFileName = e.fileName;
            Bitmap bitmap = (Bitmap)Image.FromFile(strFileName, true);

            var FileName = strFileName;

            ///*
            if (this.LastDocumentScan == null)
            {
                this.LastDocumentScan = this.ucImagesManipulation1.AddDocument(FileName, bitmap);

                if (!ACSGlobal.Duplex)
                {
                    this.LastDocumentScan = null;
                }


            }
            else
            {
                this.ucImagesManipulation1.AddPageInDocument(this.LastDocumentScan, FileName, bitmap);
                this.LastDocumentScan = null;
            }

            //*/
            if (!ACSConfig.GetScanner().Driver.ToUpper().Contains("FUJITSU") && !ACSConfig.GetScanner().Driver.ToUpper().Contains("SP-1120"))
                Application.DoEvents();
        }

    }

    class LoteAssina
    {
        public string CodigoPassagem { get; set; }
        public DateTime DataLote { get; set; }
    }
}

