using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OCRTools;
using ACSMinCapture.DataBase;
using ACSMinCapture.DataBase.Model;
using ACSMinCapture.Forms;
using ACSMinCapture.Log;
using ConversorPDF;
using ACSMinCapture.Config;
using ACSMinCapture.Global;
using RestSharp;
using OrionDigital.Assinador.ViewModel.Response;
using OrionDigital.Assinador.ViewModel;
using OrionDigital.Framework;
using System.Web.UI.WebControls;
using ACSMinCapture.Auxiliar;

namespace ACSMinCapture.Controls
{
    partial class UCImagesManipulation : UserControl
    {


        // Retirado da documentação

        static List<string> ListaUrlRemota = new List<string>();
        WFCapture _wfCapture;

        public UCImagesManipulation()
        {

        }
        public UCImagesManipulation(WFCapture wfCapture)
        {
            InitializeComponent();
            _wfCapture = wfCapture;
            this.EnableAfterSelect = true;
            this.OCRInImage = true;

            if (ACSGlobal.Duplex)
            {
                Bitmap image1 = new Bitmap(ACSMinCapture.Properties.Resources.duplexYes);


                btnDuplex.Image = image1;

                ACSGlobal.Duplex = true;

            }
            else
            {
                ACSGlobal.Duplex = false;

                Bitmap image1 = new Bitmap(ACSMinCapture.Properties.Resources.duplexNot);


                btnDuplex.Image = image1;
            }

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            if (ucImages.Selected != null)
            {
                //ExecuteCommandAllColumnsRows(ImageCommand.ZoomIn);
                this.ucImages.Selected.ZoomIn();
            }
        }

        private void brnZoomOut_Click(object sender, EventArgs e)
        {
            if (ucImages.Selected != null)
            {
                //ExecuteCommandAllColumnsRows(ImageCommand.ZoomOut);
                this.ucImages.Selected.ZoomOut();
            }
        }

        private void btnQuad_Click(object sender, EventArgs e)
        {
            if (ucImages.Selected != null)
            {
                ExecuteCommandAllColumnsRows(ImageCommand.ZoomToFit);
            }
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            if (ucImages.Selected != null)
            {
                ucImages.ExecuteCommand(ImageCommand.Rotate, ucImages.Selected.Column, ucImages.Selected.Row);
            }
        }

        private void btnEraser_CheckedChanged(object sender, EventArgs e)
        {
            if (ucImages.Selected != null)
            {
                if ((sender as System.Windows.Forms.CheckBox).Checked)
                {
                    this.ExecuteCommandAllColumnsRows(ImageCommand.EraserStart);
                    //ucImages.ExecuteCommand(ImageCommand.EraserStart, ucImages.Selected.Column, ucImages.Selected.Row);
                }
                else if (!(sender as System.Windows.Forms.CheckBox).Checked)
                {
                    this.ExecuteCommandAllColumnsRows(ImageCommand.EraserStop);
                    //ucImages.ExecuteCommand(ImageCommand.EraserStop, ucImages.Selected.Column, ucImages.Selected.Row);
                }
            }
        }

        private void btnCrop_CheckedChanged(object sender, EventArgs e)
        {
            if (ucImages.Selected != null)
            {
                if ((sender as System.Windows.Forms.CheckBox).Checked)
                {
                    this.ExecuteCommandAllColumnsRows(ImageCommand.CropStart);
                    //ucImages.ExecuteCommand(ImageCommand.CropStart, ucImages.Selected.Column, ucImages.Selected.Row);
                }
                else if (!(sender as System.Windows.Forms.CheckBox).Checked)
                {
                    this.ExecuteCommandAllColumnsRows(ImageCommand.CropStop);
                    //ucImages.ExecuteCommand(ImageCommand.CropStop, ucImages.Selected.Column, ucImages.Selected.Row);
                }
            }
        }

        private void ucImages_AfterImageKeyDown(object sender, KeyEventArgs e)
        {
            if ((sender as UCImages).Selected != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (btnCrop.Checked)
                    {
                        (sender as UCImages).ExecuteCommand(
                            ImageCommand.Crop,
                            (sender as UCImages).Selected.Column,
                            (sender as UCImages).Selected.Row);

                        btnCrop.Checked = false;
                    }

                    if (btnEraser.Checked)
                    {
                        (sender as UCImages).ExecuteCommand(
                            ImageCommand.Eraser,
                            (sender as UCImages).Selected.Column,
                            (sender as UCImages).Selected.Row);

                    }
                }
                else if (e.KeyData == Keys.Delete)
                {
                    var Doc = FileNameExistIntvDocs((sender as UCImages).Selected.FileName);
                    if (Doc != null)
                    {
                        DeleteDocument(Doc, true);
                    }
                }
                else if ((e.KeyData == Keys.Up) || (e.KeyData == Keys.Left))
                {
                    PreviewImage();
                }
                else if ((e.KeyData == Keys.Right) || (e.KeyData == Keys.Down))
                {
                    NextImage();
                }
            }
        }

        #region Vars

        bool ImageClicled = false;
        bool IsCleaing = false;

        #endregion

        #region Delegates

        public delegate void AfterAllDocumentsValidEvent(bool Valids);

        #endregion

        #region Events

        public new event AfterAllDocumentsValidEvent AfterAllDocumentsValid = null;

        #endregion

        #region Methods

        void AfterOCRImage(Guid GuidDocument, List<GED_PROC_CodigosBarras_Result> ListBarCode)
        {

            try
            {
                var Node = (TreeNodeCustom)this.tvDocs.Nodes.All().Where(d => (d as TreeNodeCustom).Guid == GuidDocument).First();

                if (Node == null)
                    return;

                var Doc = Node.Master;

                if (Doc != null)
                {
                    if (Doc.OCR != null)
                    {
                        Doc.OCR = null;
                    }
                    if (Doc.BarCodes == null)
                    {
                        Doc.BarCodes = new List<GED_PROC_CodigosBarras_Result>();
                    }

                    if (ListBarCode.Count <= 0)
                    {
                        Doc.Observation = "";
                        DocumentIsValid(Doc);
                        return;
                    }

                    foreach (var bc in ListBarCode)
                    {
                        AddBarCodeInDocument(Doc, bc.TPD_CODIGOBARRA, false);
                    }

                    if (!ACSConfig.GetScanner().Driver.ToUpper().Contains("FUJITSU") && !ACSConfig.GetScanner().Driver.ToUpper().Contains("SP-1120"))
                        Application.DoEvents();
                }
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        void AfterOCRImageReload(Guid GuidDocument, List<GED_PROC_CodigosBarras_Result> ListBarCode)
        {

            try
            {
                var Node = (TreeNodeCustom)this.tvDocs.Nodes.All().Where(d => (d as TreeNodeCustom).Guid == GuidDocument).First();

                if (Node == null)
                    return;

                var Doc = Node.Master;

                if (Doc != null)
                {
                    if (Doc.OCR != null)
                    {
                        Doc.OCR = null;
                    }
                    if (Doc.BarCodes == null)
                    {
                        Doc.BarCodes = new List<GED_PROC_CodigosBarras_Result>();
                    }

                    if (ListBarCode.Count <= 0)
                    {
                        Doc.Observation = "";
                        DocumentIsValid(Doc);
                        return;
                    }

                    foreach (var bc in ListBarCode)
                    {
                        AddBarCodeInDocument(Doc, bc.TPD_CODIGOBARRA, false);
                    }

                    if (!ACSConfig.GetScanner().Driver.ToUpper().Contains("FUJITSU") && !ACSConfig.GetScanner().Driver.ToUpper().Contains("SP-1120"))
                        Application.DoEvents();
                }
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        void BeforeRemovePage(Page page)
        {
            try
            {
                var imguc = this.ucImages.AllImageBoxCapture().Where(uc => uc.FileName == page.FileName).ToList().First();
                imguc.Clear();
            }
            catch (Exception a)
            {
                if (1 == 2)
                    ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        void AddBarCodeInDocument(Document Doc, GED_PROC_CodigosBarras_Result bc, bool fValida = true)
        {
            try
            {
                if (Doc.BarCodes == null)
                    Doc.BarCodes = new List<GED_PROC_CodigosBarras_Result>();

                if (fValida)
                {
                    if (Doc.BarCodes.Count >= 1)
                    {

                        WFMessageBox.Show("Impossível adicionar mais de um código de barras por documento!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    if (Doc.BarCodes.Count >= 1)
                    { 
                        return;
                    }
                }

                if (Doc.BarCodes.Where(b => b.TPD_CODIGOBARRA == bc.TPD_CODIGOBARRA).Count() <= 0)
                {
                    Doc.BarCodes.Add(bc);
                }

                DocumentIsValid(Doc);
                EnabledcodigoDeBarrasToolStripMenuItem(Doc);

            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        void AddBarCodeInDocument(Document Doc, string Code, bool fValida = true)
        {
            try
            {

                if (this.BarCodes == null)
                    return;

                //var Barcodes = ACSDataBase.GetGED_PROC_CodigosBarras(1, 1, Code);

                //   var BCode = this.BarCodes.Where(bc => bc.TPD_CODIGOBARRA == Code);

                var flagTipoCliente = ACSGlobal.flagTipoCliente;
                var idArea = ACSGlobal.idAreaSelecionada;
                var listaDivisoes = ACSDataBase.GetDivisoesUsuario(idArea);
                List<GED_PROC_CodigosBarras_Result> ListDataSource = new List<GED_PROC_CodigosBarras_Result>();

                var listagem = ACSDataBase.GetGED_PROC_CodigosBarras(1, 1);

                foreach (var item in listagem)
                {
                    if (listaDivisoes.Any(c => c.DIV_IDDIVISAO == item.TPD_IDDIVISAO))
                    {
                        ListDataSource.Add(item);
                    }
                }
                var BCode = ListDataSource.Where(bc => bc.TPD_CODIGOBARRA == Code);


                if (BCode.Count() > 0 && BCode != null)
                {
                    var temp = BCode.First();
                    var newBCode = new GED_PROC_CodigosBarras_Result()
                    {
                        DateValidity = temp.DateValidity,
                        DIV_CODIGOREDUZIDO = temp.DIV_CODIGOREDUZIDO,
                        REQUERDATAINICIOVALIDADE = temp.REQUERDATAINICIOVALIDADE,
                        StartDateValidity = temp.StartDateValidity,
                        STD_FLAGVENCIMENTOMANUAL = temp.STD_FLAGVENCIMENTOMANUAL,
                        STD_IDSUBTIPOSDOCUMENTOS = temp.STD_IDSUBTIPOSDOCUMENTOS,
                        STD_MESVENCIMENTOANUAL = temp.STD_MESVENCIMENTOANUAL,
                        TPD_CODIGOBARRA = temp.TPD_CODIGOBARRA,
                        TPD_DESCRICAO = temp.TPD_DESCRICAO,
                        TPD_IDDIVISAO = temp.TPD_IDDIVISAO,
                        TPD_IDTIPODOCUMENTO = temp.TPD_IDTIPODOCUMENTO,
                        TPD_TEMPOVALIDADE = temp.TPD_TEMPOVALIDADE
                    };
                    AddBarCodeInDocument(Doc, newBCode, false);

                }
                else
                {
                    DocumentIsValid(Doc);
                }
                //AddBarCodeInDocument(Doc, (GED_PROC_CodigosBarras_Result)Barcode.Clone());
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        public bool RefreshBarCodes()
        {
            try
            {

                this.BarCodes = ACSDataBase.GetGED_PROC_CodigosBarras(1, 1);

                return this.BarCodes != null;

            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);
                return false;
            }
        }

        public Page AddPageInDocument(Document Doc, string FileName, Bitmap bitmap)
        {
            try
            {


                this.ucImages.LoadFromBitmap(bitmap, FileName);
                return AddPageInDocument(Doc, FileName);
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);
                return null;
            }
        }

        public Page AddPageInDocument(Document Doc, string FileName)
        {
            try
            {


                var pag = Doc.NewPage(FileName);
                pag.BeforeRemovePageEvent += BeforeRemovePage;

                if (OCRInImage)
                {
                    Doc.OCR = new OCRorion(FileName, pag.Guid);
                    Doc.OCR.AfterOCREvent += AfterOCRImage;
                    Doc.OCR.TimeOut = 10;
                    Doc.OCR.StartOCR();

                    ListaUrlRemota.Add(FileName);
                }

                return pag;
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);
                return null;

            }
        }


        public void ReoloadDocument(Document Doc, string FileName)
        {
            try
            {


                Doc.OCR = new OCRorion(FileName, Doc.Guid);
                Doc.OCR.AfterOCREvent += AfterOCRImageReload;
                Doc.OCR.TimeOut = 10;
                Doc.OCR.StartOCR();





            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);


            }
        }

        public Document AddDocument(string FileName, Bitmap bitmap)
        {
            try
            {


                this.ucImages.LoadFromBitmap(bitmap, FileName);
                return AddDocument(FileName);
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);
                return null;
            }
        }

        public Document AddDocument(string FileName)
        {
            try
            {

                this.richTextBox1.Clear();
                var result = this.tvDocs.NewDocument();

                if (Path.GetExtension(FileName).ToUpper() == ".PDF")
                    AddPDF(result, FileName);
                else
                    AddPageInDocument(result, FileName);

                result.Observation = "Aguarde uns instantes estamos verificando se existem Códigos de Barras...";
                result.ExpandAll();
                result.TreeView.Invalidate();

                if (!ACSConfig.GetScanner().Driver.ToUpper().Contains("FUJITSU") && !ACSConfig.GetScanner().Driver.ToUpper().Contains("SP-1120"))
                    Application.DoEvents();
                DisplayCountDocs();
                return result;
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);
                return null;
            }
        }

        public Document AddDocument(string[] FileNames)
        {
            try
            {


                Document result = null;

                ListaUrlRemota.Clear();
                foreach (var FileName in FileNames)
                {
                    result = AddDocument(FileName);
                }

                return result;
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);
                return null;
            }
        }

        public Document AddPDF(Document Doc, string FileName)
        {
            try
            {


                #region Convertendo PDF

                using (PDFWrapperX PDF = new PDFWrapperX())
                {
                    if (PDF.LoadPDF(FileName))
                    {
                        PDF.RenderDPI = ACSConfig.GetImages().Resolution;

                        string[] arquivos;
                        while (PDF.CurrentPage <= PDF.PageCount)
                        {
                            var img = PDF.ToBitmap(PDF.CurrentPage);

                            string NewPath = ACSConfig.GetStorage().Input + "\\" + ACSGlobal.LoteSelecionado.PAS_CODIGOPASSAGEM + "\\";

                            if (!Directory.Exists(NewPath))
                                Directory.CreateDirectory(NewPath);

                            arquivos = Directory.GetFiles(NewPath);
                            Random random = new Random();
                            var verificaExistencia = false;

                            foreach (var item in arquivos)
                            {
                                var newP = NewPath + Path.GetFileNameWithoutExtension(FileName) + PDF.CurrentPage.ToString() + "." + ACSConfig.GetImages().Format.ToString();
                                if (item == newP)
                                    verificaExistencia = true;
                            }

                            if (verificaExistencia)
                                NewPath = NewPath + Path.GetFileNameWithoutExtension(FileName) + PDF.CurrentPage.ToString() + "_" + random.Next(1, 1000) + "." + ACSConfig.GetImages().Format.ToString();
                            else
                                NewPath = NewPath + Path.GetFileNameWithoutExtension(FileName) + PDF.CurrentPage.ToString() + "." + ACSConfig.GetImages().Format.ToString();

                            img.Save(NewPath, ACSConfig.GetImages().Format);
                            img.Dispose();
                            img = null;

                            var Child = AddPageInDocument(Doc, NewPath);
                            Child.Text = PDF.CurrentPage.ToString("00000000");
                            Child.Name = Child.Text;

                            if (PDF.CurrentPage == PDF.PageCount)
                                break;





                            PDF.NextPage();

                        }

                    }

                }
                #endregion

                ListaUrlRemota.Clear();

                foreach (var item in Doc.Pages)
                {
                    //adiciona a url na listagem
                    ListaUrlRemota.Add(item.FileName);
                }
                return Doc;
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);
                return null;
            }
        }

        public void ClearUCImages()
        {
            this.ucImages.ClearAllImages();
        }

        bool DocumentIsValid(Document Doc)
        {
            try
            {


                var result = false;
                if (Doc.BarCodes == null || Doc.BarCodes.Count() <= 0)
                {
                    Doc.Valid = false;
                    Doc.Observation = "Não existe Código de Barras.";
                    goto gotoFin;
                }
                else if (Doc.BarCodes.Count() > 1)
                {
                    Doc.Valid = false;
                    Doc.Observation = "Foram encontrados mais de um Código de Barras. Verifique!";
                    goto gotoFin;
                }
                else
                    result = true;

                Doc.Observation = string.Empty;
                Doc.Observation = Doc.Observation + "Nome: " + Doc.Text + "\n";
                Doc.Observation = Doc.Observation + "Quantidade Imagens: " + Doc.Nodes.OfType<Page>().Count().ToString() + "\n";

                foreach (var bc in Doc.BarCodes)
                {
                    if ((bc.REQUERDATAINICIOVALIDADE == 1 && bc.StartDateValidity == DateTime.MinValue) || (bc.STD_FLAGVENCIMENTOMANUAL == 1 && bc.DateValidity.Value == DateTime.MinValue))
                    {
                        result = false;
                        Doc.Observation = Doc.Observation + "Código: " + bc.TPD_CODIGOBARRA + "\nAviso: Informe Data Vencimento.";
                    }
                    else if (result)
                        result = true;
                }

                Doc.Valid = result;

                if (result)
                {

                    foreach (var bc in Doc.BarCodes)
                    {
                        Doc.Observation = Doc.Observation + "Código: " + bc.TPD_CODIGOBARRA + "\n";
                        Doc.Observation = Doc.Observation + "Descrição: " + bc.TPD_DESCRICAO + "\n";
                        if (bc.StartDateValidity.Value != DateTime.MinValue)
                            Doc.Observation = Doc.Observation + "Início Vigência: " + bc.StartDateValidity.Value.ToString("dd/MM/yyyy");
                        if (bc.DateValidity.Value != DateTime.MinValue)
                            Doc.Observation = Doc.Observation + " Fim Vigência: " + bc.DateValidity.Value.ToString("dd/MM/yyyy");
                    }
                }

            gotoFin:

                if (Doc.TreeView.SelectedNode != null && (Doc.TreeView.SelectedNode as TreeNodeCustom).Master == Doc)
                {
                    SetDataSourceInDataGrid(Doc.BarCodes);
                    SetTextrichTextBox1(Doc.Observation);
                }

                if (this.AfterAllDocumentsValid != null)
                    AfterAllDocumentsValid(AllDocumentsValid());

                return result;
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);
                return false;
            }
        }

        void SetTextrichTextBox1(string value)
        {
            try
            {


                this.richTextBox1.Text = value;
                var index = this.richTextBox1.Text.IndexOf("Aviso");
                if (index >= 0)
                {
                    this.richTextBox1.SelectionStart = index;
                    this.richTextBox1.SelectionLength = this.richTextBox1.Text.Length;
                    this.richTextBox1.SelectionColor = Color.Red;
                }
                else
                {
                    this.richTextBox1.SelectionColor = this.richTextBox1.ForeColor;
                }

            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        bool AllDocumentsValid()
        {
            try
            {
                return tvDocs.Nodes.OfType<Document>().Where(d => d.Valid == false).Count() <= 0;

            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);
                return false;
            }
        }

        void DisplayCountDocs()
        {
            try
            {


                var Docs = this.CountDocuments;
                var Pags = this.CountPages;
                this.label1.Text = "Documentos: " + Docs.ToString() + "\n" + "Imagens: " + Pags.ToString();
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        ImageBoxCapture PageExistsInucImages(Page pag)
        {
            try
            {


                var ImgCtrls = ucImages.AllImageBoxCapture().Where(img => img.FileName == pag.FileName);
                ImageBoxCapture ImgCtrl = null;
                if (ImgCtrls.Count() > 0)
                    ImgCtrl = ImgCtrls.First();

                return ImgCtrl;
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);
                return null;
            }
        }

        Page FileNameExistIntvDocs(string FileName)
        {
            try
            {


                Page result = null;
                var pages = this.tvDocs.Nodes.All().OfType<Page>().Where(pag => pag.FileName == FileName);
                if (pages.Count() > 0)
                {
                    result = pages.First();
                }
                return result;
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);
                return null;
            }
        }

        ImageBoxCapture LoadImageUCImages(Page pag, bool Select = true)
        {
            try
            {


                var ImgCtrl = PageExistsInucImages(pag);
                if (ImgCtrl == null)
                {
                    if (ucImages.AllImageBoxCapture().Where(img => img.FileName == "").Count() > 0)
                        ImgCtrl = ucImages.LoadFromFile(pag.FileName, Select);

                }
                else
                    ImgCtrl.Selected = Select;

                return ImgCtrl;
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);
                return null;
            }
        }

        void EnabledButtons(bool e)
        {
            try
            {

                this.btnCrop.Enabled = e;
                this.btnEraser.Enabled = e;
                this.btnNext.Enabled = e;
                this.btnZoomOut.Enabled = e;
                this.btnPreview.Enabled = e;
                this.btnQuad.Enabled = e;
                this.btnReplicate.Enabled = e;
                this.btnRotate.Enabled = e;
                this.btnZoomIn.Enabled = e;

                //verifica se tem flag e codigo para assinar
                //string chaveAssinatura = ACSGlobal.UsuarioLogado.USR_SERIALNUMBERCERT;
                //if (!string.IsNullOrEmpty(chaveAssinatura))
                //    this.btnAssinador.Enabled = e; 
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        void ExecuteCommandAllColumnsRows(ImageCommand Command)
        {
            try
            {


                for (int rw = 0; rw <= this.ucImages.Rows - 1; rw++)
                {
                    for (int col = 0; col <= this.ucImages.Columns - 1; col++)
                    {
                        this.ucImages.ExecuteCommand(Command, col, rw);
                    }
                }
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        void DeleteDocument(System.Windows.Forms.TreeNode Node)
        {
            try
            {

                if (Node is Page)
                {
                    (Node as Page).Remove();
                }
                else if (Node is Document)
                {
                    (Node as Document).Remove();
                }
                DisplayCountDocs();
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        void DeleteDocument(System.Windows.Forms.TreeNode Node, bool Confirm)
        {
            try
            {

                var isConfirmed = Confirm;
                if (Confirm)
                {
                    Confirm = WFMessageBox.Show("Confirma exclusão?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                }

                if (Confirm)
                {
                    DeleteDocument(Node);
                    if (isConfirmed)
                    {

                        try
                        {
                            WFTranparentLoading.ShowLoading(this);
                            foreach (var nd in this.tvDocs.Nodes)
                            {
                                if (nd is Document)
                                    DocumentIsValid((Document)nd);
                            }

                            seleçãoMultiplaToolStripMenuItem.Checked = false;
                            WFTranparentLoading.CloseLoading();
                        }
                        catch
                        {
                            WFTranparentLoading.CloseLoading();
                        }
                    }

                    this.tvDocs.Invalidate();
                    this.ucImages.Invalidate();
                }
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        void PreviewImage()
        {
            try
            {


                if (this.tvDocs.SelectedNode == null)
                    return;

                if (this.tvDocs.SelectedNode.Nodes.Count > 0)
                {
                    this.tvDocs.SelectedNode = this.tvDocs.SelectedNode.LastNode;
                }
                else
                {
                    var PrevNode = this.tvDocs.SelectedNode.PrevNode;

                    if (PrevNode == null)
                    {
                        PrevNode = this.tvDocs.SelectedNode.Parent.PrevNode;
                        if (PrevNode != null)
                            PrevNode = PrevNode.LastNode;
                    }

                    if (PrevNode != null)
                        this.tvDocs.SelectedNode = PrevNode;
                }
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        void NextImage()
        {
            try
            {


                if (this.tvDocs.SelectedNode == null)
                    return;

                if (this.tvDocs.SelectedNode.Nodes.Count > 0)
                {
                    this.tvDocs.SelectedNode = this.tvDocs.SelectedNode.FirstNode;
                }
                else
                {
                    var NextNode = this.tvDocs.SelectedNode.NextNode;

                    if (NextNode == null)
                    {
                        NextNode = this.tvDocs.SelectedNode.Parent.NextNode;

                        if (NextNode != null)
                            NextNode = NextNode.FirstNode;
                    }

                    if (NextNode != null)
                        this.tvDocs.SelectedNode = NextNode;
                }
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        public void Clear()
        {
            try
            {


                this.IsCleaing = true;
                this.richTextBox1.Text = string.Empty;
                this.dataGridView1.DataSource = null;
                this.richTextBox1.Invalidate();
                this.dataGridView1.Invalidate();
                this.EnabledButtons(false);
                while (this.tvDocs.Nodes.Count > 0)
                {
                    var Doc = (Document)this.tvDocs.Nodes[this.tvDocs.Nodes.Count - 1];
                    WFTranparentLoading.Messege("Deletando " + Doc.Text + "...");
                    DeleteDocument(Doc);
                }
                this.LastDocSelected = null;
                this.IsCleaing = false;
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        #endregion

        #region Propertys

        public int CountDocuments
        {
            get
            {
                return this.tvDocs.Nodes.OfType<Document>().Count();
            }
        }
        public int CountPages
        {
            get
            {
                return this.tvDocs.Nodes.All().OfType<Page>().Count();
            }
        }
        [Browsable(false)]
        public Page[] Pages
        {
            get
            {
                return this.tvDocs.Nodes.All().OfType<Page>().ToArray();
            }
        }
        [Browsable(false)]
        public Document[] Documents
        {
            get
            {
                return this.tvDocs.Nodes.All().OfType<Document>().ToArray();
            }
        }

        public string MaskDocumentName
        {
            get
            {
                return this.tvDocs.MaskDocumentName;
            }
            set
            {
                this.tvDocs.MaskDocumentName = value;
            }
        }

        public string MaskPageName
        {
            get
            {
                return this.tvDocs.MaskPageName;
            }
            set
            {
                this.tvDocs.MaskPageName = value;
            }
        }

        [Browsable(false)]
        public bool ZoomFitOnLoadBitmap
        {
            get
            {
                return this.ucImages.ZoomFitOnLoadBitmap;
            }
            set
            {
                this.ucImages.ZoomFitOnLoadBitmap = value;
            }
        }

        [Browsable(false)]
        public Document LastDocSelected { get; set; }

        [Browsable(false)]
        [DefaultValue(true)]
        public bool EnableAfterSelect { get; set; }

        [Browsable(false)]
        public List<GED_PROC_CodigosBarras_Result> BarCodes { get; set; }

        [Browsable(false)]
        public bool OCRInImage { get; set; }

        #endregion

        void EnabledcodigoDeBarrasToolStripMenuItem(Document Doc)
        {
            try
            {


                foreach (var bc in Doc.BarCodes)
                {
                    if (bc.REQUERDATAINICIOVALIDADE == 1)
                    {
                        this.dataValidadeToolStripMenuItem.Enabled = true;
                        break;
                    }
                    else
                        this.dataValidadeToolStripMenuItem.Enabled = false;
                }
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        void SetDataSourceInDataGrid(object DataSource)
        {
            try
            {

                BindingSource eBin = null;
                if (this.dataGridView1.DataSource == null)
                {
                    eBin = new BindingSource();
                    eBin.DataSource = DataSource;
                    eBin.AllowNew = false;
                    this.dataGridView1.DataSource = eBin;
                    this.dataGridView1.AutoGenerateColumns = true;
                    this.dataGridView1.AllowUserToAddRows = true;
                    this.dataGridView1.Invalidate();
                }
                else
                {
                    eBin = (BindingSource)this.dataGridView1.DataSource;
                    eBin.DataSource = DataSource;
                    eBin.ResetBindings(false);
                }

                if (DataSource is List<GED_PROC_CodigosBarras_Result>)
                {
                    var lbc = (List<GED_PROC_CodigosBarras_Result>)DataSource;
                    this.dataGridView1.Columns["StartDateValidity"].Visible = lbc.Where(b => b.REQUERDATAINICIOVALIDADE == 1).Count() > 0;
                    this.dataGridView1.Columns["DateValidity"].Visible = lbc.Where(b => b.REQUERDATAINICIOVALIDADE == 1).Count() > 0;
                }

                if (this.dataGridView1.Columns.Count > 0 && this.dataGridView1.Columns.OfType<DataGridViewButtonColumn>().Count() <= 0)
                {
                    var btnDeleteRow = new DataGridViewButtonColumn();
                    btnDeleteRow.HeaderText = "";
                    btnDeleteRow.Text = "Remover";
                    btnDeleteRow.UseColumnTextForButtonValue = true;

                    this.dataGridView1.Columns.Insert(this.dataGridView1.Columns.Count, btnDeleteRow);
                }

                DisplayCountDocs();
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        private void ucImages_AfterLoadFromFile(object sender, ImageBoxCapture imageBoxCapture, string FileName)
        {
            EnabledButtons(imageBoxCapture != null);
        }

        private void ucImages_AfterClearAllImages(object sender)
        {
            EnabledButtons(false);
        }

        private void tvDocs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                DeleteDocument((sender as System.Windows.Forms.TreeView).SelectedNode, true);
                seleçãoMultiplaToolStripMenuItem.Checked = false;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NextImage();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            PreviewImage();
        }

        private void ucImages_AfterImageClick(object sender)
        {
            this.ImageClicled = true;
            try
            {
                if ((sender as UCImages).Selected != null &&
                    (sender as UCImages).Selected.FileName != string.Empty)
                {
                    var Doc = FileNameExistIntvDocs((sender as UCImages).Selected.FileName);
                    if (Doc != null)
                    {
                        this.tvDocs.SelectedNode = Doc;
                    }
                }
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
            this.ImageClicled = false;
        }

        private void tvDocs_Click(object sender, EventArgs e)
        {
            this.ImageClicled = false;
        }

        void DeleteRowGrid(int Index)
        {
            try
            {


                if (this.dataGridView1.DataSource == null)
                    return;

                var row = this.dataGridView1.Rows[Index];
                if (row != null)
                {
                    var drv = row.DataBoundItem as GED_PROC_CodigosBarras_Result;
                    var eBin = (BindingSource)this.dataGridView1.DataSource;
                    var dt = (List<GED_PROC_CodigosBarras_Result>)eBin.DataSource;

                    dt.Remove(drv);
                    eBin.ResetBindings(false);
                }
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {


                var Col = (sender as DataGridView).Columns[e.ColumnIndex];

                if (Col is DataGridViewButtonColumn)
                {
                    DeleteRowGrid(e.RowIndex);

                    if (this.tvDocs.SelectedNode != null)
                    {
                        Document Doc = (this.tvDocs.SelectedNode as TreeNodeCustom).Master;

                        if (Doc != null)
                        {
                            DocumentIsValid(Doc);
                        }
                    }
                }
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }

        }

        private void codigoDeBarrasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                using (var wfSBC = new WFSetBarCode())
                {
                    if (wfSBC.ShowDialog() == DialogResult.OK)
                    {
                        Document Doc = null;
                        if (this.tvDocs.SelectedNode.Parent == null)
                            Doc = (Document)this.tvDocs.SelectedNode;
                        else
                            Doc = (Document)this.tvDocs.SelectedNode.Parent;


                        AddBarCodeInDocument(Doc, wfSBC.BarCodeSelected);
                    }
                }
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (!this.tvDocs.CheckBoxes && this.tvDocs.SelectedNode != null)
                {
                    DeleteDocument(this.tvDocs.SelectedNode, true);
                }
                else if (this.tvDocs.CheckBoxes)
                {

                    var firstnd = true;
                    try
                    {
                        var NodesCheckeds = this.tvDocs.Nodes.All().Where(n => n != null && n.Checked);
                        for (var i = NodesCheckeds.Count() - 1; i >= 0; i--)
                        {
                            var nd = NodesCheckeds.ElementAt<System.Windows.Forms.TreeNode>(i);
                            if (nd == null)
                                continue;

                            if (nd.Checked)
                            {
                                if (firstnd)
                                {
                                    if (WFMessageBox.Show("Confirma exclusão?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        DeleteDocument(nd);
                                        firstnd = false;
                                    }
                                    else
                                        break;
                                }
                                else
                                {
                                    DeleteDocument(nd);

                                }
                            }
                        }

                    }
                    catch (Exception a)
                    {
                        ACSLog.InsertLog(MessageBoxIcon.Error, a);

                    }

                    if (this.tvDocs.SelectedNode != null)
                    {
                        var Doc = (this.tvDocs.SelectedNode as TreeNodeCustom).Master;
                        DocumentIsValid(Doc);
                    }

                    this.tvDocs.CheckBoxes = false;
                    seleçãoMultiplaToolStripMenuItem.Checked = false;
                    expandirTodosToolStripMenuItem.Checked = false;

                    expandirTodosToolStripMenuItem.PerformClick();
                }
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        private void dataValidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                using (var wfsdv = new WFSetDateValidityDocument())
                {
                    if (this.tvDocs.SelectedNode != null)
                    {
                        Document Doc = (this.tvDocs.SelectedNode as TreeNodeCustom).Master;

                        wfsdv.tbDateValidity.Enabled = false;

                        foreach (var bc in Doc.BarCodes)
                        {
                            if (bc.STD_FLAGVENCIMENTOMANUAL == 1)
                            {
                                wfsdv.tbDateValidity.Enabled = true;
                                break;
                            }
                        }

                        wfsdv.btnCalendar2.Enabled = wfsdv.tbDateValidity.Enabled;

                        if (wfsdv.ShowDialog() == DialogResult.OK)
                        {
                            var dt = DateTime.MinValue;

                            foreach (var bc in Doc.BarCodes)
                            {
                                try
                                {
                                    DateTime.TryParse(wfsdv.tbStartDate.Text, out dt);
                                    bc.StartDateValidity = dt;

                                    if (bc.TPD_TEMPOVALIDADE > 0 && bc.StartDateValidity.Value > DateTime.MinValue)
                                    {
                                        bc.DateValidity = bc.StartDateValidity.Value.AddMonths(bc.TPD_TEMPOVALIDADE);
                                    }
                                    else if (bc.STD_FLAGVENCIMENTOMANUAL == 1 && bc.StartDateValidity.Value > DateTime.MinValue)
                                    {
                                        bc.StartDateValidity = bc.StartDateValidity.Value;
                                        DateTime.TryParse(wfsdv.tbDateValidity.Text, out dt);
                                        bc.DateValidity = dt;
                                    }
                                    else if (bc.STD_MESVENCIMENTOANUAL > 0 && bc.StartDateValidity.Value > DateTime.MinValue)
                                    {
                                        var Ini = bc.StartDateValidity.Value.AddYears(1);
                                        var s = Ini.ToString("yyyy") + "-" +
                                                                   bc.STD_MESVENCIMENTOANUAL.ToString("00") + "-" +
                                                                   Ini.ToString("dd");
                                        bc.DateValidity = DateTime.Parse(s);
                                    }
                                }
                                catch (Exception er)
                                {
                                    WFMessageBox.Show("Erro ao busca Código de Barras.\n" + er.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                        }

                        DocumentIsValid(Doc);

                    }
                }
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        private void seleçãoMultiplaToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            this.tvDocs.CheckBoxes = (sender as ToolStripMenuItem).Checked;

            if (expandirTodosToolStripMenuItem.Checked == true && (sender as ToolStripMenuItem).Checked == false)
            {
                expandirTodosToolStripMenuItem.Checked = false;

                expandirTodosToolStripMenuItem.PerformClick();
            }
        }

        class ReplicateDocs : object
        {
            public string BarCode { get; set; }
            public List<int[]> Intervals = new List<int[]>();
        }

        private void btnReplicate_Click(object sender, EventArgs e)
        {
            try
            {


                if (tvDocs.GetNodeCount(true) <= 0) return;

                WFSetValues wfSV = new WFSetValues();
                wfSV.ShowInTaskbar = false;
                wfSV.ShowDialog(this);
                if (wfSV.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        WFTranparentLoading.ShowLoading(this);
                        if (wfSV.dtDocs.Rows.Count > 0)
                        {
                            int[] Intervalo = new int[] { 0, 0 };
                            string s = string.Empty;

                            List<ReplicateDocs> listReplicateDocs = new List<ReplicateDocs>();
                            #region Varrendo DataSet Intervalos

                            foreach (DataRow dr in wfSV.dtDocs.Rows)
                            {
                                ReplicateDocs rd = new ReplicateDocs();
                                rd.BarCode = (string)dr["fBarCode"];
                                rd.Intervals.Clear();
                                int Index = 0;

                                #region Montando Intervalo
                                int Length = (dr["fDocs"] as string).Length;
                                int IndexLength = 0;

                                foreach (char c in (string)dr["fDocs"])
                                {
                                    IndexLength++;
                                    if (char.IsNumber(c))
                                    {
                                        s = s + c;
                                        if (IndexLength == Length)
                                        {
                                            goto Master;
                                        }
                                        continue;
                                    }

                                Master:

                                    Intervalo[Index] = int.Parse(s);
                                    s = string.Empty;
                                    if (Index == 1)
                                    {
                                        rd.Intervals.Add(Intervalo);
                                        listReplicateDocs.Add(rd);
                                        Index = 0;
                                        Intervalo = new int[] { 0, 0 };
                                        continue;

                                    }
                                    Index = 1;
                                }
                                #endregion
                            }
                            #endregion

                            foreach (ReplicateDocs rd in listReplicateDocs)
                            {
                                for (int Index = 0; Index <= rd.Intervals.Count - 1; Index++)
                                {
                                    for (int i = rd.Intervals[Index][0]; i <= rd.Intervals[Index][1]; i++)
                                    {
                                        var get = tvDocs.Nodes.IndexOfKey("DOC" + i.ToString());
                                        if (get >= 0)
                                        {
                                            var doc = (Document)tvDocs.Nodes[get];
                                            if (doc.BarCodes == null)
                                            {
                                                doc.BarCodes = new List<GED_PROC_CodigosBarras_Result>();
                                            }
                                            doc.BarCodes.Clear();
                                            AddBarCodeInDocument(doc, rd.BarCode);
                                            WFTranparentLoading.Messege("Modificando " + doc.Text + "...");

                                            if (!ACSConfig.GetScanner().Driver.ToUpper().Contains("FUJITSU") && !ACSConfig.GetScanner().Driver.ToUpper().Contains("SP-1120"))
                                                Application.DoEvents();
                                        }
                                    }
                                }
                            }


                        }
                    }
                    catch
                    {
                        WFTranparentLoading.CloseLoading();
                        WFMessageBox.Show("Você executou uma operação ilégal!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                WFTranparentLoading.CloseLoading();
                wfSV.Dispose();
                wfSV = null;
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        private void tvDocs_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void tvDocs_AfterOnDragDrop(TreeNodeCustom Node)
        {
            try
            {
                var Doc = Node.Master;
                DocumentIsValid(Doc);
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        private void expandirTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                if ((sender as ToolStripMenuItem).CheckState == CheckState.Checked)
                    this.tvDocs.ExpandAll();
                else if ((sender as ToolStripMenuItem).CheckState == CheckState.Unchecked)
                    this.tvDocs.CollapseAll();

            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        private void tvDocs_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {

        }

        private void tvDocs_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            try
            {


                if (!this.EnableAfterSelect)
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }

        private void tvDocs_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (this.IsCleaing)
                    return;

                if (this.ImageClicled || (sender as TreeViewCustom).Draging)
                    return;

                if (e.Node == null) return;

                var PagesucI = (ucImages.Rows * ucImages.Columns);
                var PageIndex = 0;

                Page pag = null;
                if (e.Node is Page)
                {
                    pag = (Page)e.Node;
                    PageIndex = pag.Index;
                }

                Document Doc = (e.Node as TreeNodeCustom).Master;

                var x = 0;
                var pgLast = Doc.Pages.OfType<Page>().ToList().Last();
                var docCount = Doc.Pages.Count();

                if (PageIndex == pgLast.Index && pag != null && LastDocSelected != Doc)
                {
                    PageIndex = pgLast.Index - (PagesucI - 1);
                    if (PageIndex < 0)
                        PageIndex = 0;
                }

                if (pag == null && this.LastDocSelected == Doc)
                    return;


                for (var i = PageIndex; i <= Doc.Pages.Count() - 1; i++)
                {
                    var pg = Doc.Pages.ElementAt<Page>(i);
                    if (PageExistsInucImages(pg) == null)
                    {
                        ucImages.ClearAllImages();
                        break;
                    }
                }

                for (var i = PageIndex; i <= (docCount - 1); i++)
                {
                    var pg = Doc.Pages.ElementAt(i);
                    LoadImageUCImages(pg, pag == pg);

                    if (x >= PagesucI - 1)
                        break;
                    x++;
                }

                SetDataSourceInDataGrid(Doc.BarCodes);
                SetTextrichTextBox1(Doc.Observation);
                EnabledcodigoDeBarrasToolStripMenuItem(Doc);
                EnabledButtons(true);

                if (!Doc.IsExpanded)
                    this.expandirTodosToolStripMenuItem.CheckState = CheckState.Indeterminate;

            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }

            try
            {
                LastDocSelected = (e.Node as TreeNodeCustom).Master;
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);


                LastDocSelected = null;
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void cmsImages_Opening(object sender, CancelEventArgs e)
        {

        }

        private void ucImages_Load(object sender, EventArgs e)
        {
            try
            {

                if (ACSGlobal.TipoCaptura == 2)
                    btnDuplex.Visible = false;

                if (!ACSGlobal.Duplex)
                {
                    Bitmap image1 = new Bitmap(ACSMinCapture.Properties.Resources.duplexNot);


                    btnDuplex.Image = image1;

                    ACSGlobal.Duplex = false;

                }
                else
                {
                    ACSGlobal.Duplex = true;

                    Bitmap image1 = new Bitmap(ACSMinCapture.Properties.Resources.duplexYes);


                    btnDuplex.Image = image1;
                }
            }
            catch (Exception a)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, a);

            }
        }


        #region assinador digital -  22/10/2015 L,F

        protected System.Web.UI.HtmlControls.HtmlInputButton ListarCertificados;

        private void btnDuplex_Click(object sender, EventArgs e)
        {
            if (!ACSGlobal.Duplex)
            {
                Bitmap image1 = new Bitmap(ACSMinCapture.Properties.Resources.duplexYes);


                btnDuplex.Image = image1;

                ACSGlobal.Duplex = true;

                _wfCapture.tsmi1X1.Checked = false;
                _wfCapture.tsmi1X2.Checked = true;
                this.ucImages.Columns = 2;
                this.ucImages.Rows = 1;

            }
            else
            {
                ACSGlobal.Duplex = false;

                Bitmap image1 = new Bitmap(ACSMinCapture.Properties.Resources.duplexNot);



                btnDuplex.Image = image1;
                _wfCapture.tsmi1X1.Checked = true;
                _wfCapture.tsmi1X2.Checked = false;
                this.ucImages.Columns = 1;
                this.ucImages.Rows = 1;

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }




        //private byte[] assinar(Stream fileToSign)
        //{
        //    byte[] retorno = null;

        //    try
        //    {
        //        BRYSIGNERCOMLib.IAssinador assinador = new BRYSIGNERCOMLib.Assinador();
        //        String dataToSign = BitConverter.ToString(lerArquivo(fileToSign)).Replace("-", string.Empty);

        //        String idCertificado = sCertificado;
        //        assinador.setFormatoDadosMemoria(FORMATO_ARQUIVO_HEXADECIMAL);

        //        String assinaturaHex = "";
        //        int statusAssinatura = -1;


        //        statusAssinatura = assinador.assineMemDetached(dataToSign, "Orion Digital - 2016 - ACS Capture", idCertificado, ref assinaturaHex);

        //        var teste = statusAssinatura;
        //        retorno = StringToByteArray(assinaturaHex);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return retorno;
        //}

        //protected Byte[] lerArquivo(Stream arq)
        //{
        //    byte[] buffer;

        //    try
        //    {
        //        int length = (int)arq.Length;
        //        buffer = new byte[length];

        //        arq.Read(buffer, 0, length);
        //    }
        //    finally
        //    {
        //        arq.Close();
        //    }

        //    return buffer;
        //}

        //public static byte[] StringToByteArray(String hex)
        //{
        //    int NumberChars = hex.Length;
        //    byte[] bytes = new byte[NumberChars / 2];
        //    for (int i = 0; i < NumberChars; i += 2)
        //        bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        //    return bytes;
        //}

        //public Stream ConvertDocToStream(string urlDoc)
        //{
        //    try
        //    {
        //        string sFile = urlDoc; //Path

        //        Stream imageStreamSource = new FileStream(urlDoc, FileMode.Open, FileAccess.Read, FileShare.Read);


        //        return imageStreamSource;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public bool InitialCertificado()
        //{
        //    try
        //    {


        //        BRYSIGNERCOMLib.IRepositorio repositorio = new BRYSIGNERCOMLib.Repositorio();
        //        repositorio.inicialize("MY", CERT_SYSTEM_STORE_CURRENT_USER);
        //        int totalCertificados = repositorio.getCountCertificados();

        //        BRYSIGNERCOMLib.ICertificado certificado = null;

        //        this.listaCertificados = new System.Web.UI.HtmlControls.HtmlSelect();

        //        for (int i = 0; i < totalCertificados; ++i)
        //        {
        //            certificado = repositorio.getCertificado(i);
        //            ListItem item = new ListItem(certificado.getAssuntoCN(), certificado.getIdCertificado());
        //            this.listaCertificados.Items.Add(item);


        //            //achou o certificado na maquina
        //            if (certificado.getIdCertificado() == sCertificado)
        //                return true;


        //            certificado.finalize();


        //        }
        //        repositorio.finalize();

        //        return false;

        //    }
        //    catch (Exception ex)
        //    {

        //        return false;
        //        throw;
        //    }


        //}

        #endregion
    }
}
