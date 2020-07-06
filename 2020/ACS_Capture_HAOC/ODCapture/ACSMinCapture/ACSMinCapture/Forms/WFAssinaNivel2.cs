using ACSMinCapture.Config;
using ACSMinCapture.DataBase;
using ACSMinCapture.DataBase.ModelOracle;
using ACSMinCapture.Forms;
using ACSMinCapture.Global;
using ACSMinCapture.Log;
using ACSMinCapture.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Web.UI.WebControls;
using NewACSMinCapture;
using System.Threading;
using ACSMinCapture.Auxiliar;

namespace ACSMinCapture
{
    public partial class WFAssinaNivel2 : WFMDIChild
    {
        private List<int> ListIdSelecionados;
        private System.Drawing.Drawing2D.GraphicsPath mousePath;
        private int iVerifica = 0; //0-> SEM DADOS, 1-> Fechado , 2->Aberto
        private string sCaminhoIMGZoom = "";
        public WFAssinaNivel2()
        {
            InitializeComponent();
            mousePath = new System.Drawing.Drawing2D.GraphicsPath();


            ListIdSelecionados = new List<int>();

        }

        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    if (keyData == (Keys.ControlKey | Keys.Alt | Keys.F4))
        //    {
        //        return true;
        //    }

        //    return base.ProcessCmdKey(ref msg, keyData);
        //}


        private void rbImportar_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void WFTipoAcao_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (listaPesquisa != null)
                ACSDataBase.deleteBloqueiaDocumentosLote((int)ACSGlobal.UsuarioLogado.USR_IDUSUARIO, listaPesquisa);


            this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.WFTipoAcao_FormClosing);
            this.Close();
        }

        private void WFTipoAcao_Load(object sender, EventArgs e)
        {
            try
            {
                //   MemoryStream ms = new MemoryStream(Resources.iconTeste);
                //   pctSelected.Cursor = new Cursor(ms);
                pctSelected.Image = Resources.documentNull;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private List<GEDDocumentosNivel2> listaPesquisa;
        private List<GEDDocumentosNivel2> listaPesquisaNova;
        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                iVerifica = 1;
                if (listaPesquisa != null)
                    ACSDataBase.deleteBloqueiaDocumentosLote((int)ACSGlobal.UsuarioLogado.USR_IDUSUARIO, listaPesquisa);


                WFLoading.ShowLoad(true, "Assinatura de Documentos", "Buscando Documentos à serem assinados...");

                string numeroAtendimento = txtNumeroAtendimento.Text;
                string dataAtendimento = txtDataAtendimento.Text;
                bool fVerTodos = chkVerTodos.Checked;
                pctSelected.Image = Resources.documentNull;

                btnCoAssinador.Enabled = true;
                lblInformativo.Text = "";

                var iNivelAssinaUsuario = ACSGlobal.UsuarioLogado.USR_NIVELASSINA;

                var nivelAssinaCondifg = ACSConfig.GetApp().NIVELASSINA;

                if (nivelAssinaCondifg.ToUpper() == "ALL")
                    listaPesquisa = ACSDataBase.GetDocumentosAssN2(numeroAtendimento, dataAtendimento, fVerTodos, ACSGlobal.UsuarioLogado.USR_IDUSUARIO, iNivelAssinaUsuario.GetValueOrDefault());
                else
                    listaPesquisa = ACSDataBase.GetDocumentosAssN3(numeroAtendimento, dataAtendimento, fVerTodos, ACSGlobal.UsuarioLogado.USR_IDUSUARIO, iNivelAssinaUsuario.GetValueOrDefault());

                listaPesquisaNova = new List<GEDDocumentosNivel2>();


                if (iNivelAssinaUsuario == 2)
                {
                    if (ACSGlobal.ListaGruposSetoresLogado == null || ACSGlobal.ListaGruposSetoresLogado.Count == 0)
                    {

                        listaPesquisa = new List<GEDDocumentosNivel2>();
                    }
                    //else
                    //{

                    //    listaPesquisa = listaPesquisa.Where(c => ACSGlobal.ListaGruposSetoresLogado.Contains(c.DOC_IDGRUPOUSUARIOCAPTURA)).ToList();

                    //}
                }

                //  MessageBox.Show("1");
                if (nivelAssinaCondifg.ToUpper() != "ALL")
                {
                    //     MessageBox.Show("ALL");
                    foreach (var item in listaPesquisa)
                    {
                        //ACSLog.InsertLog(MessageBoxIcon.Information, "STD_NIVELASSINA: " + item.STD_NIVELASSINA);
                        //ACSLog.InsertLog(MessageBoxIcon.Information, "iNivelAssinaUsuario: " + iNivelAssinaUsuario);
                        if (item.STD_NIVELASSINA >= iNivelAssinaUsuario)
                        {
                            if ((item.STD_NIVELASSINA == 4 || item.STD_NIVELASSINA == 3) && iNivelAssinaUsuario == 3)
                            {
                                //verifica se foi assinado no segundo nivel para passar para o terceiro
                                if (item.DOC_IDUSUARIOASSINANIVEL3 == null)
                                {
                                    item.DOC_ASSINATURA = "-";
                                    listaPesquisaNova.Add(item);

                                }
                                //verifica se foi assinado no terceiro nivel para passar para o terceiro_2(Vulgo 4)
                                if (item.DOC_IDUSUARIOASSINANIVEL3 != null
                                    && item.DOC_IDUSUARIOASSINANIVEL3_2 == null
                                    && item.DOC_IDUSUARIOASSINANIVEL3 != ACSGlobal.UsuarioLogado.USR_IDUSUARIO
                                    && item.STD_CODIGO == "CPSI")
                                {
                                    item.DOC_ASSINATURA = "Nível 3";
                                    listaPesquisaNova.Add(item);

                                }
                            }

                        }
                    }


                    // MessageBox.Show("FINISH FOR");
                }
                else
                {
                    foreach (var item in listaPesquisa)
                    {
                        //ACSLog.InsertLog(MessageBoxIcon.Information, "STD_NIVELASSINA: " + item.STD_NIVELASSINA);
                        //ACSLog.InsertLog(MessageBoxIcon.Information, "iNivelAssinaUsuario: " + iNivelAssinaUsuario);
                        if (item.STD_NIVELASSINA >= iNivelAssinaUsuario)
                        {
                            if ((item.STD_NIVELASSINA == 4 || item.STD_NIVELASSINA == 3) && iNivelAssinaUsuario == 3)
                            {
                                //verifica se foi assinado no segundo nivel para passar para o terceiro
                                if (item.DOC_IDUSUARIOASSINANIVEL2 != null && item.DOC_IDUSUARIOASSINANIVEL3 == null)
                                {
                                    item.DOC_ASSINATURA = "Nível 2";
                                    listaPesquisaNova.Add(item);

                                }
                                //verifica se foi assinado no terceiro nivel para passar para o terceiro_2(Vulgo 4)
                                if (item.DOC_IDUSUARIOASSINANIVEL2 != null
                                    && item.DOC_IDUSUARIOASSINANIVEL3 != null
                                    && item.DOC_IDUSUARIOASSINANIVEL3_2 == null
                                    && item.DOC_IDUSUARIOASSINANIVEL3 != ACSGlobal.UsuarioLogado.USR_IDUSUARIO
                                    && item.STD_CODIGO == "CPSI")
                                {
                                    item.DOC_ASSINATURA = "Nível 3";
                                    listaPesquisaNova.Add(item);

                                }
                            }
                            else if ((item.STD_NIVELASSINA == 4 || item.STD_NIVELASSINA == 3) && iNivelAssinaUsuario == 2)
                            {
                                //verifica se foi assinado no primeiro nivel para passar para o segundo
                                if (item.DOC_IDUSUARIOASSINANIVEL2 == null)
                                {
                                    item.DOC_ASSINATURA = "Nível 1";
                                    listaPesquisaNova.Add(item);

                                }
                            }
                            else
                                if (item.STD_NIVELASSINA == 2 && iNivelAssinaUsuario == 2)
                                {
                                    //verifica se foi assinado no primeiro nivel para passar para o segundo
                                    if (item.DOC_IDUSUARIOASSINANIVEL2 == null)
                                    {
                                        item.DOC_ASSINATURA = "Nível 1";
                                        listaPesquisaNova.Add(item);

                                    }
                                }
                                else
                                    listaPesquisaNova.Add(item);
                        }
                    }

                }



                //   MessageBox.Show("2");

                //MessageBox.Show(listaPesquisaNova.Count.ToString());

                lblNumeroDocumentos.Text = listaPesquisaNova.Count().ToString();
                lblNumeroDocumentosSelecionados.Text = "0";

                if (listaPesquisaNova.Count > 0)
                {
                    pnBtnAll.Visible = true;
                    pnAllInfo.Visible = true;
                    btnSelecionarTodos.Visible = true;

                    dtgv_DocsN2.AutoGenerateColumns = false;
                    dtgv_DocsN2.DataSource = listaPesquisaNova;
                    btnCoAssinador.Visible = true;
                    this.dtgv_DocsN2.Visible = true;

                }
                else
                {
                    pnAllInfo.Visible = false;
                    pnBtnAll.Visible = false;
                    btnSelecionarTodos.Visible = false;
                    this.dtgv_DocsN2.Visible = false;
                    lblNumeroDocumentos.Text = "0";
                    lblNumeroDocumentosSelecionados.Text = "0";

                    dtgv_DocsN2.DataSource = null;
                    txtDataAtendimento.Text = string.Empty;
                    txtNumeroAtendimento.Text = string.Empty;
                    btnCoAssinador.Visible = false;
                    WFLoading.CloseLoad();
                    if ((ACSGlobal.ListaGruposSetoresLogado != null && ACSGlobal.ListaGruposSetoresLogado.Count > 0) || iNivelAssinaUsuario == 4 || iNivelAssinaUsuario == 3)
                    {
                        if (chkVerTodos.Checked)
                            WFMessageBox.Show("Não possui mais documentos a serem assinados!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            WFMessageBox.Show("Nenhum item encontrado com as informações passadas!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {

                        WFMessageBox.Show("Grupo do usuário não está vinculado a nenhum setor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }

                tableLayoutPanel1.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
                tableLayoutPanel1.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
                btnVoltar.Visible = false;
                WFLoading.CloseLoad();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void chkVerTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVerTodos.Checked)
            {
                txtDataAtendimento.Text = string.Empty;
                txtNumeroAtendimento.Text = string.Empty;

                txtDataAtendimento.Enabled = false;
                txtNumeroAtendimento.Enabled = false;
            }
            else
            {
                txtDataAtendimento.Text = string.Empty;
                txtNumeroAtendimento.Text = string.Empty;

                txtDataAtendimento.Enabled = true;
                txtNumeroAtendimento.Enabled = true;
            }
        }

        private void txtDataAtendimento_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {

        }

        private void txtDataAtendimento_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDataAtendimento_Validated(object sender, EventArgs e)
        {

        }

        private void txtDataAtendimento_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                int iLength = txtDataAtendimento.TextLength;

                if (iLength == 10)
                {
                    DateTime dt;
                    bool fVerifica = DateTime.TryParse(txtDataAtendimento.Text, out  dt);
                    if (!fVerifica)
                    {
                        txtDataAtendimento.Text = string.Empty;
                        WFMessageBox.Show("Data Inválida! Digite uma data válida", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        string GetPathOutput()
        {
            return ACSConfig.GetStorage().Output;
        }
        private void btnCoAssinador_Click(object sender, EventArgs e)
        {
            Thread threadAction = new Thread(() =>
            {

                fnBtnAssinar();
            });
            threadAction.Start();

        }

        private void fnBtnAssinar()
        {
            btnCoAssinador.Enabled = false;
            lblInformativo.Text = "Trabalhando em sua requisição...";
            if (ACSConfig.GetNetworkAccesser().Valid)
            {
                try
                {
                    using (var nsa = NetworkShareAccesser.Access(ACSConfig.GetStorage().Output,
                                                                 ACSConfig.GetNetworkAccesser().Domain,
                                                                 ACSConfig.GetNetworkAccesser().User,
                                                                 ACSConfig.GetNetworkAccesser().Password))
                    {
                        fnCoassinar();
                    }
                }
                catch (Exception g)
                {
                    ACSLog.InsertLog(MessageBoxIcon.Error, g);
                    WFMessageBox.Show(g.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnCoAssinador.Enabled = true;
                }
            }
            else
                fnCoassinar();
        }

        private void fnCoassinar()
        {
            try
            {


                if (string.IsNullOrEmpty(ACSGlobal.UsuarioLogado.USR_SERIALNUMBERCERT))
                {
                    btnCoAssinador.Enabled = true;
                    lblInformativo.Text = " ";
                    WFMessageBox.Show("Este usuário não possui certificado atribuído! ", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    //deleta os itens na tabela de documento assinando
                    if (listaPesquisa != null)
                        ACSDataBase.deleteBloqueiaDocumentosLote((int)ACSGlobal.UsuarioLogado.USR_IDUSUARIO, listaPesquisa);


                }
                else
                {


                    bool fCheck = false;
                    List<GEDDocumentosNivel2> listaPesquisaNovaAssinar = new List<GEDDocumentosNivel2>(); ;
                    foreach (DataGridViewRow r in dtgv_DocsN2.SelectedRows)
                    {
                        fCheck = true;
                        //Pegas as linhas selecionadas

                        var idDocument = (decimal)r.Cells["DOC_IDDOCUMENTO"].Value;
                        listaPesquisaNovaAssinar.Add(listaPesquisaNova.Find(c => c.DOC_IDDOCUMENTO == (int)idDocument));

                    }

                    if (fCheck)
                    {


                        WFLoading.ShowLoad(true, "CoAssinatura", "Assinando documentos...");

                        //var NewName = OutputIni + "\\" +
                        //            ACSGlobal.LoteSelecionado.PAS_REGISTRO + "\\" +
                        //            ACSGlobal.LoteSelecionado.PAS_REGISTRO + Doc.BarCodes[0].DIV_CODIGOREDUZIDO + "\\" +
                        //            ACSGlobal.LoteSelecionado.PAS_CODIGOPASSAGEM;


                        int i = 0;

                        foreach (var item in listaPesquisaNovaAssinar)
                        {
                            var nivelAssinaCondifg = ACSConfig.GetApp().NIVELASSINA;

                            if (nivelAssinaCondifg.ToUpper() == "ALL")
                            {
                                //WFTranparentLoading.Messege("Verificando " + Doc.Text + "...");
                                lblInformativo.Text = "Assinando documento " + (++i) + "... (restam " + (listaPesquisaNovaAssinar.Count() - i) + ")";
                                if (ACSGlobal.UsuarioLogado.USR_NIVELASSINA == 2)
                                {
                                    CoAssinarN2(item);
                                }

                                if (ACSGlobal.UsuarioLogado.USR_NIVELASSINA == 3)
                                {
                                    CoAssinarN3(item);
                                }

                            }
                            else
                            {

                                lblInformativo.Text = "Assinando documento " + (++i) + "... (restam " + (listaPesquisaNovaAssinar.Count() - i) + ")";

                                // CoAssinarN3(item);
                                if (ACSGlobal.UsuarioLogado.USR_NIVELASSINA == 3)
                                {
                                    AssinaN3First(item);
                                }
                                //else
                                //{
                                //    AssinaN3_2First(item); 
                                //}
                            }


                        }
                        WFLoading.CloseLoad();

                        WFMessageBox.Show("Documentos Co-Assinados com Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        lblInformativo.Text = "Requisição Finalizada";
                        dtgv_DocsN2.DataSource = null;
                        txtDataAtendimento.Text = string.Empty;
                        txtNumeroAtendimento.Text = string.Empty;
                        btnCoAssinador.Visible = false;

                        pnAllInfo.Visible = false;
                        pnBtnAll.Visible = false;
                        btnSelecionarTodos.Visible = false;
                        lblNumeroDocumentos.Text = "0";
                        lblNumeroDocumentosSelecionados.Text = "0";



                        //deleta os itens na tabela de documento assinando
                        if (listaPesquisa != null)
                            ACSDataBase.deleteBloqueiaDocumentosLote((int)ACSGlobal.UsuarioLogado.USR_IDUSUARIO, listaPesquisa);



                    }
                    else
                    {
                        WFMessageBox.Show("Selecione ao menos um documento a ser assinado! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblInformativo.Text = " ";
                        btnCoAssinador.Enabled = true;

                    }

                }
            }
            catch (Exception ex)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, "Não foi possível assinar o documento. Motivo: " + ex.InnerException);
                throw new ExceptionCustom("Não foi possível assinar o documento. Motivo: " + ex.InnerException);
            }
        }

        private bool AssinaN3_2First(GEDDocumentosNivel2 item)
        {
            string caminhoDocOriginal = "";
            string caminhoDocAssinadoN1 = "";
            caminhoDocOriginal = GetPathOutput() + "\\" + item.PAS_REGISTRO + "\\" + item.PAS_REGISTRO + item.DOC_DIVISAO + "\\" + item.PAS_CODIGOPASSAGEM + "\\" + item.DOC_NOMEARQUIVO + ".jpeg";
            caminhoDocAssinadoN1 = GetPathOutput() + "\\" + item.PAS_REGISTRO + "\\" + item.PAS_REGISTRO + item.DOC_DIVISAO + "\\" + item.PAS_CODIGOPASSAGEM + "\\" + item.DOC_NOMEARQUIVO + "_N3.p7s";


            byte[] retorno = null;

            Stream fileToSignDocOriginal = ConvertDocToStream(caminhoDocOriginal);
            Stream fileToSignDocAssinadoN1 = ConvertDocToStream(caminhoDocAssinadoN1);

            BRYSIGNERCOMLib.IAssinador assinador = new BRYSIGNERCOMLib.Assinador();

            String docOriginal = BitConverter.ToString(lerArquivo(fileToSignDocOriginal)).Replace("-", string.Empty);
            String docAssinadoN1 = BitConverter.ToString(lerArquivo(fileToSignDocAssinadoN1)).Replace("-", string.Empty);

            String idCertificado = ACSGlobal.UsuarioLogado.USR_SERIALNUMBERCERT;
            assinador.setFormatoDadosMemoria(FORMATO_ARQUIVO_HEXADECIMAL);

            String assinaturaHex = "";
            int statusAssinatura = -1;

            statusAssinatura = assinador.coAssineMemDetached(docAssinadoN1, docOriginal, "Orion Digital - 2016 - ACS Capture", idCertificado, ref assinaturaHex);

            if (statusAssinatura == 1)
            {
                retorno = StringToByteArray(assinaturaHex);

                string pathNew = GetPathOutput() + "\\" + item.PAS_REGISTRO + "\\" + item.PAS_REGISTRO + item.DOC_DIVISAO + "\\" + item.PAS_CODIGOPASSAGEM + "\\" + item.DOC_NOMEARQUIVO;
                System.IO.FileStream _FileStream = new System.IO.FileStream(pathNew + "_N3_2.p7s", System.IO.FileMode.Create, System.IO.FileAccess.Write);

                _FileStream.Write(retorno, 0, retorno.Length);

                // close file stream
                _FileStream.Close();

                ACSDataBase.UpdateDocumentoAssinadoN2((decimal)ACSGlobal.UsuarioLogado.USR_IDUSUARIO, (decimal)item.DOC_IDDOCUMENTO, getInfoCertificado(idCertificado, "2"));

                return true;
            }
            else
            {



                var error = ErrorCertificado.ListaErroCertificado().Where(c => c.IdErro == statusAssinatura.ToString().Replace("-", "")).FirstOrDefault().DescricaoErro;
                if (error == ".LLLLLLLLLLLLLLLLÇMNK1")
                    error = "Ocorreu um erro na validação do certificado digital!";

                ACSLog.InsertLog(MessageBoxIcon.Error, "Não foi possível assinar o documento. Motivo: " + statusAssinatura);
                throw new ExceptionCustom("Não foi possível assinar o documento. Motivo: " + error);
                return false;

            }
        }

        private bool CoAssinarN2(GEDDocumentosNivel2 item)
        {
            string caminhoDocOriginal = "";
            string caminhoDocAssinadoN1 = "";
            try
            {


                caminhoDocOriginal = GetPathOutput() + "\\" + item.PAS_REGISTRO + "\\" + item.PAS_REGISTRO + item.DOC_DIVISAO + "\\" + item.PAS_CODIGOPASSAGEM + "\\" + item.DOC_NOMEARQUIVO + ".jpeg";
                caminhoDocAssinadoN1 = GetPathOutput() + "\\" + item.PAS_REGISTRO + "\\" + item.PAS_REGISTRO + item.DOC_DIVISAO + "\\" + item.PAS_CODIGOPASSAGEM + "\\" + item.DOC_NOMEARQUIVO + "_N1.p7s";






                byte[] retorno = null;

                Stream fileToSignDocOriginal = ConvertDocToStream(caminhoDocOriginal);
                Stream fileToSignDocAssinadoN1 = ConvertDocToStream(caminhoDocAssinadoN1);

                BRYSIGNERCOMLib.IAssinador assinador = new BRYSIGNERCOMLib.Assinador();

                String docOriginal = BitConverter.ToString(lerArquivo(fileToSignDocOriginal)).Replace("-", string.Empty);
                String docAssinadoN1 = BitConverter.ToString(lerArquivo(fileToSignDocAssinadoN1)).Replace("-", string.Empty);

                String idCertificado = ACSGlobal.UsuarioLogado.USR_SERIALNUMBERCERT;
                assinador.setFormatoDadosMemoria(FORMATO_ARQUIVO_HEXADECIMAL);

                String assinaturaHex = "";
                int statusAssinatura = -1;

                statusAssinatura = assinador.coAssineMemDetached(docAssinadoN1, docOriginal, "Orion Digital - 2016 - ACS Capture", idCertificado, ref assinaturaHex);

                if (statusAssinatura == 1)
                {
                    retorno = StringToByteArray(assinaturaHex);

                    string pathNew = GetPathOutput() + "\\" + item.PAS_REGISTRO + "\\" + item.PAS_REGISTRO + item.DOC_DIVISAO + "\\" + item.PAS_CODIGOPASSAGEM + "\\" + item.DOC_NOMEARQUIVO;
                    System.IO.FileStream _FileStream = new System.IO.FileStream(pathNew + "_N2.p7s", System.IO.FileMode.Create, System.IO.FileAccess.Write);

                    _FileStream.Write(retorno, 0, retorno.Length);

                    // close file stream
                    _FileStream.Close();

                    ACSDataBase.UpdateDocumentoAssinadoN2((decimal)ACSGlobal.UsuarioLogado.USR_IDUSUARIO, (decimal)item.DOC_IDDOCUMENTO, getInfoCertificado(idCertificado, "2"));

                    return true;
                }
                else
                {



                    var error = ErrorCertificado.ListaErroCertificado().Where(c => c.IdErro == statusAssinatura.ToString().Replace("-", "")).FirstOrDefault().DescricaoErro;
                    if (error == ".LLLLLLLLLLLLLLLLÇMNK1")
                        error = "Ocorreu um erro na validação do certificado digital!";

                    ACSLog.InsertLog(MessageBoxIcon.Error, "Não foi possível assinar o documento. Motivo: " + statusAssinatura);
                    throw new ExceptionCustom("Não foi possível assinar o documento. Motivo: " + error);
                    return false;

                }
            }
            catch (Exception ex)
            {

                ACSLog.InsertLog(MessageBoxIcon.Error, "Erro ao assinar o documento: " + caminhoDocOriginal);

                ACSLog.InsertLog(MessageBoxIcon.Error, "Não foi possível assinar o documento. Motivo: " + ex.InnerException);

                throw;
            }
        }

        private bool CoAssinarN3(GEDDocumentosNivel2 item)
        {
            string caminhoDocOriginal = "";
            string caminhoDocAssinadoN2 = "";
            try
            { 
             
                bool first = false;

                if (item.DOC_IDUSUARIOASSINANIVEL3 == null)
                {
                    first = true;
                    caminhoDocOriginal = GetPathOutput() + "\\" + item.PAS_REGISTRO + "\\" + item.PAS_REGISTRO + item.DOC_DIVISAO + "\\" + item.PAS_CODIGOPASSAGEM + "\\" + item.DOC_NOMEARQUIVO + ".jpeg";
                    caminhoDocAssinadoN2 = GetPathOutput() + "\\" + item.PAS_REGISTRO + "\\" + item.PAS_REGISTRO + item.DOC_DIVISAO + "\\" + item.PAS_CODIGOPASSAGEM + "\\" + item.DOC_NOMEARQUIVO + "_N2.p7s";
                }
                else
                {
                    caminhoDocOriginal = GetPathOutput() + "\\" + item.PAS_REGISTRO + "\\" + item.PAS_REGISTRO + item.DOC_DIVISAO + "\\" + item.PAS_CODIGOPASSAGEM + "\\" + item.DOC_NOMEARQUIVO + ".jpeg";
                    caminhoDocAssinadoN2 = GetPathOutput() + "\\" + item.PAS_REGISTRO + "\\" + item.PAS_REGISTRO + item.DOC_DIVISAO + "\\" + item.PAS_CODIGOPASSAGEM + "\\" + item.DOC_NOMEARQUIVO + "_N3.p7s";

                }
                 
                byte[] retorno = null;

                Stream fileToSignDocOriginal = ConvertDocToStream(caminhoDocOriginal);
                Stream fileToSignDocAssinadoN1 = ConvertDocToStream(caminhoDocAssinadoN2);

                BRYSIGNERCOMLib.IAssinador assinador = new BRYSIGNERCOMLib.Assinador();

                String docOriginal = BitConverter.ToString(lerArquivo(fileToSignDocOriginal)).Replace("-", string.Empty);
                String docAssinadoN1 = BitConverter.ToString(lerArquivo(fileToSignDocAssinadoN1)).Replace("-", string.Empty);

                String idCertificado = ACSGlobal.UsuarioLogado.USR_SERIALNUMBERCERT;
                assinador.setFormatoDadosMemoria(FORMATO_ARQUIVO_HEXADECIMAL);

                String assinaturaHex = "";
                int statusAssinatura = -1;

                statusAssinatura = assinador.coAssineMemDetached(docAssinadoN1, docOriginal, "Orion Digital - 2016 - ACS Capture", idCertificado, ref assinaturaHex);

                if (statusAssinatura == 1)
                {
                    retorno = StringToByteArray(assinaturaHex);

                    string pathNew = GetPathOutput() + "\\" + item.PAS_REGISTRO + "\\" + item.PAS_REGISTRO + item.DOC_DIVISAO + "\\" + item.PAS_CODIGOPASSAGEM + "\\" + item.DOC_NOMEARQUIVO;
                    if (first)
                    {
                        System.IO.FileStream _FileStream = new System.IO.FileStream(pathNew + "_N3.p7s", System.IO.FileMode.Create, System.IO.FileAccess.Write);

                        _FileStream.Write(retorno, 0, retorno.Length);

                        // close file stream
                        _FileStream.Close();
                        ACSDataBase.UpdateDocumentoAssinadoN3((decimal)ACSGlobal.UsuarioLogado.USR_IDUSUARIO, (decimal)item.DOC_IDDOCUMENTO, getInfoCertificado(idCertificado, "3"));

                    }
                    else
                    {
                        System.IO.FileStream _FileStream = new System.IO.FileStream(pathNew + "_N3_2.p7s", System.IO.FileMode.Create, System.IO.FileAccess.Write);

                        _FileStream.Write(retorno, 0, retorno.Length);

                        // close file stream
                        _FileStream.Close();
                        ACSDataBase.UpdateDocumentoAssinadoN3((decimal)ACSGlobal.UsuarioLogado.USR_IDUSUARIO, (decimal)item.DOC_IDDOCUMENTO, getInfoCertificado(idCertificado, "3_2"), false);

                    }

                    return true;

                }
                else
                {



                    var error = ErrorCertificado.ListaErroCertificado().Where(c => c.IdErro == statusAssinatura.ToString().Replace("-", "")).FirstOrDefault().DescricaoErro;
                    ACSLog.InsertLog(MessageBoxIcon.Error, "Não foi possível assinar o documento. Motivo: " + statusAssinatura);
                    throw new ExceptionCustom("Não foi possível assinar o documento. Motivo: " + error);
                    return false;

                }
            }
            catch (Exception ex)
            {

                ACSLog.InsertLog(MessageBoxIcon.Error, "Erro ao assinar o documento: " + caminhoDocOriginal);

                ACSLog.InsertLog(MessageBoxIcon.Error, "Não foi possível assinar o documento. Motivo: " + ex.InnerException);

                throw;
            }
        }

        private bool AssinaN3First(GEDDocumentosNivel2 item)
        {
            string caminhoDocOriginal = "";
            string caminhoDocAssinadoN1 = "";
            try
            {
                if (item.DOC_IDUSUARIOASSINANIVEL2 != null || item.DOC_IDUSUARIOASSINANIVEL3 != null)
                {
                    return CoAssinarN3(item);
                }
                else
                {


                    ACSLog.InsertLog(MessageBoxIcon.Error, "-----------------------------------  Entrou para assinar  ----------------------------------- " );


                    caminhoDocOriginal = GetPathOutput() + "\\" + item.PAS_REGISTRO + "\\" + item.PAS_REGISTRO + item.DOC_DIVISAO + "\\" + item.PAS_CODIGOPASSAGEM + "\\" + item.DOC_NOMEARQUIVO + ".jpeg";
                    //  caminhoDocAssinadoN1 = GetPathOutput() + "\\" + item.PAS_REGISTRO + "\\" + item.PAS_REGISTRO + item.DOC_DIVISAO + "\\" + item.PAS_CODIGOPASSAGEM + "\\" + item.DOC_NOMEARQUIVO + "_N1.p7s";



                    ACSLog.InsertLog(MessageBoxIcon.Error, "Montou um caminho: " + caminhoDocOriginal);

                    var sCertificado = ACSGlobal.UsuarioLogado.USR_SERIALNUMBERCERT;
                    byte[] assinatura = Assinador.assinar(Assinador.ConvertDocToStream(caminhoDocOriginal), sCertificado);
               

                    string pathNew = Path.GetDirectoryName(caminhoDocOriginal) + "\\" + Path.GetFileNameWithoutExtension(caminhoDocOriginal);
                    System.IO.FileStream _FileStream = new System.IO.FileStream(pathNew + "_N3.p7s", System.IO.FileMode.Create, System.IO.FileAccess.Write);


                    ACSLog.InsertLog(MessageBoxIcon.Error, "Geraou o caminho para o _N3.p7s: " + pathNew);

                    _FileStream.Write(assinatura, 0, assinatura.Length);

                    // close file stream
                    _FileStream.Close();
                    ACSDataBase.UpdateDocumentoAssinadoN3((decimal)ACSGlobal.UsuarioLogado.USR_IDUSUARIO, (decimal)item.DOC_IDDOCUMENTO, getInfoCertificado(sCertificado, "3"));

                    ACSLog.InsertLog(MessageBoxIcon.Error, "Salvou a alteração na base de dados");

                    ACSLog.InsertLog(MessageBoxIcon.Error, "-----------------------------------  Finalizou assinar  ----------------------------------- ");
                    return true;

                }

            }
            catch (Exception ex)
            {

                ACSLog.InsertLog(MessageBoxIcon.Error, "Erro ao assinar o documento: " + caminhoDocOriginal);

                ACSLog.InsertLog(MessageBoxIcon.Error, "Não foi possível assinar o documento. Motivo: " + ex.InnerException);
                ACSLog.InsertLog(MessageBoxIcon.Error, "Não foi possível assinar o documento. Motivo w: " + ex.Message);
                ACSLog.InsertLog(MessageBoxIcon.Error, "Não foi possível assinar o documento. Motivo e: " + ex.InnerException);
                throw new ExceptionCustom("Não foi possível assinar o documento. Motivo e: " + ex.InnerException);
                throw new ExceptionCustom("Não foi possível assinar o documento. Motivo w: " + ex.Message);
                return false;
            }

        }


        #region  Parte CoAssinador

        // Retirado da documentação

        public const int FORMATO_ARQUIVO_HEXADECIMAL = 0;
        public const int CERT_SYSTEM_STORE_CURRENT_USER = 0;

        public string getInfoCertificado(string chave, string sNivel)
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


                        //  retorno.Append("<Certs>  ");

                        retorno.Append("  ");
                        retorno.Append("  <Cert" + sNivel + ">  ");
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
                        retorno.Append("  </Cert" + sNivel + ">  ");
                        retorno.Append(" </Certs>  ");



                        certificado.finalize();
                        break;
                    }

                }
                repositorio.finalize();

                return retorno.ToString();
            }
            catch (Exception ex)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, "Não foi possível assinar o documento. Motivo w: " + ex.Message);
                ACSLog.InsertLog(MessageBoxIcon.Error, "Não foi possível assinar o documento. Motivo e: " + ex.InnerException);
                throw new ExceptionCustom("Não foi possível assinar o documento. Motivo e: " + ex.InnerException);
                throw new ExceptionCustom("Não foi possível assinar o documento. Motivo w: " + ex.Message);
                throw;
            }
        }


        protected Byte[] lerArquivo(Stream arq)
        {
            byte[] buffer;

            try
            {
                int length = (int)arq.Length;
                buffer = new byte[length];

                arq.Read(buffer, 0, length);
            }
            finally
            {
                arq.Close();
            }

            return buffer;
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public Stream ConvertDocToStream(string urlDoc)
        {
            try
            {
                string sFile = urlDoc; //Path

                Stream imageStreamSource = new FileStream(urlDoc, FileMode.Open, FileAccess.Read, FileShare.Read);


                return imageStreamSource;
            }
            catch (Exception ex)
            {
                return null;

            }
        }


        #endregion

        private void WFAssinaNivel2_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void WFAssinaNivel2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void dtgv_DocsN2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                        getPictureDoubleClick();
                    }
                }
                catch (Exception g)
                {
                    ACSLog.InsertLog(MessageBoxIcon.Error, g);
                    WFMessageBox.Show(g.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
                getPictureDoubleClick();



        }

        private void getPictureDoubleClick()
        {
            var d = dtgv_DocsN2.CurrentRow;

            if (d == null)
                throw new Exception("Selecione um registro!");

            string PAS_REGISTRO = (string)d.Cells["PAS_REGISTRO"].Value;
            string DOC_DIVISAO = (string)d.Cells["DOC_DIVISAO"].Value;
            string PAS_CODIGOPASSAGEM = (string)d.Cells["PAS_CODIGOPASSAGEM"].Value;
            string DOC_NOMEARQUIVO = (string)d.Cells["DOC_NOMEARQUIVO"].Value;
            string DOC_EXTENSAONOMEARQUIVO = (string)d.Cells["DOC_EXTENSAONOMEARQUIVO"].Value;
            if (PAS_REGISTRO != null)
            {
                if (DOC_EXTENSAONOMEARQUIVO.Contains("_"))
                    DOC_EXTENSAONOMEARQUIVO = ".Jpeg";

                GEDDocumentosNivel2 obj;
                var OutputIni = ACSConfig.GetStorage().Output;
                var NewName = OutputIni + "\\" +
                            PAS_REGISTRO + "\\" +
                            PAS_REGISTRO + DOC_DIVISAO + "\\" +
                            PAS_CODIGOPASSAGEM + "\\" + DOC_NOMEARQUIVO + DOC_EXTENSAONOMEARQUIVO;
                if (!System.IO.File.Exists(NewName))
                {
                    WFMessageBox.Show("Arquivo Inexistente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pctSelected.Image = Resources.documentNull;
                    // pctSelected.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    Bitmap a = new Bitmap(System.Drawing.Image.FromFile(NewName));
                    pctSelected.Image = a;
                    pctSelected.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
                    sCaminhoIMGZoom = NewName;
                }
            }
        }

        private void dtgv_DocsN2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dtgv_DocsN2.Rows[0].Selected = false;
        }

        private void dtgv_DocsN2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (ACSConfig.GetNetworkAccesser().Valid)
            {
                try
                {
                    using (var nsa = NetworkShareAccesser.Access(ACSConfig.GetStorage().Output,
                                                                 ACSConfig.GetNetworkAccesser().Domain,
                                                                 ACSConfig.GetNetworkAccesser().User,
                                                                 ACSConfig.GetNetworkAccesser().Password))
                    {
                        getPictureInGrid(e, senderGrid);
                    }
                }
                catch (Exception g)
                {
                    ACSLog.InsertLog(MessageBoxIcon.Error, g);
                    WFMessageBox.Show(g.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
                getPictureInGrid(e, senderGrid);




        }

        private void getPictureInGrid(DataGridViewCellEventArgs e, DataGridView senderGrid)
        {

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {

                var d = dtgv_DocsN2.CurrentRow;

                if (d == null)
                    throw new Exception("Selecione um registro!");

                string PAS_REGISTRO = (string)d.Cells["PAS_REGISTRO"].Value;
                string DOC_DIVISAO = (string)d.Cells["DOC_DIVISAO"].Value;
                string PAS_CODIGOPASSAGEM = (string)d.Cells["PAS_CODIGOPASSAGEM"].Value;
                string DOC_NOMEARQUIVO = (string)d.Cells["DOC_NOMEARQUIVO"].Value;
                string DOC_EXTENSAONOMEARQUIVO = (string)d.Cells["DOC_EXTENSAONOMEARQUIVO"].Value;

                if (PAS_REGISTRO != null)
                {
                    if (DOC_EXTENSAONOMEARQUIVO.Contains("_"))
                        DOC_EXTENSAONOMEARQUIVO = ".Jpeg";

                    GEDDocumentosNivel2 obj;
                    var OutputIni = ACSConfig.GetStorage().Output;
                    var NewName = OutputIni + "\\" +
                                PAS_REGISTRO + "\\" +
                                PAS_REGISTRO + DOC_DIVISAO + "\\" +
                                PAS_CODIGOPASSAGEM + "\\" + DOC_NOMEARQUIVO + DOC_EXTENSAONOMEARQUIVO;

                    sCaminhoIMGZoom = NewName;

                    if (!System.IO.File.Exists(NewName))
                    {
                        WFMessageBox.Show("Arquivo Inexistente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        pctSelected.Image = Resources.documentNull;
                        // pctSelected.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        ImagePanel panelImage = new ImagePanel();
                        panelImage.displayScrollbar();
                        panelImage.setScrollbarValues();
                        Bitmap a = new Bitmap(System.Drawing.Image.FromFile(NewName));
                        pctSelected.Image = a;
                        pctSelected.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                        tableLayoutPanel1.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
                        tableLayoutPanel1.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);
                        btnVoltar.Visible = true;
                        // pctSelected.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }


            }
            else
            {

                //criar lista 
            }

        }

        private void dtgv_DocsN2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (listaPesquisa != null)
                ACSDataBase.deleteBloqueiaDocumentosLote((int)ACSGlobal.UsuarioLogado.USR_IDUSUARIO, listaPesquisa);

            Application.ExitThread();
        }


        private void btnVoltar_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
            tableLayoutPanel1.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
            btnVoltar.Visible = false;
        }

        private void pctSelected_Load(object sender, EventArgs e)
        {

        }

        private void dtgv_DocsN2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int incremento = 0;
                var row = (DataGridView)sender;

                for (int i = 0; i < row.Rows.Count; i++)
                {
                    if (row.Rows[i].Selected == true)
                    {
                        incremento += 1;
                    }
                }


                lblNumeroDocumentosSelecionados.Text = (incremento).ToString();

            }
            catch (Exception)
            {

                throw;
            }
        }


        private void btnSelecionarTodos_Click(object sender, EventArgs e)
        {
            try
            {
                var grid = dtgv_DocsN2.Rows;

                for (int i = 0; i < grid.Count; i++)
                {
                    grid[i].Selected = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}