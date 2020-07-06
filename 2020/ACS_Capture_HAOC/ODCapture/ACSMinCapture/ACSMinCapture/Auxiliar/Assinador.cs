using ACSMinCapture.Global;
using ACSMinCapture.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace ACSMinCapture.Auxiliar
{
    public class Assinador
    {
        public const int CERT_SYSTEM_STORE_CURRENT_USER = 0;
        public const int CERT_SYSTEM_STORE_LOCAL_MACHINE = 1;
        public const int FORMATO_ARQUIVO_HEXADECIMAL = 0;
        public static byte[] assinar(Stream fileToSign, string sCertificado)
        {
            byte[] retorno = null;

            try
            {
                BRYSIGNERCOMLib.IAssinador assinador = new BRYSIGNERCOMLib.Assinador();
                String dataToSign = BitConverter.ToString(lerArquivo(fileToSign)).Replace("-", string.Empty);

                String idCertificado = sCertificado;
                assinador.setFormatoDadosMemoria(FORMATO_ARQUIVO_HEXADECIMAL);

                String assinaturaHex = "";
                int statusAssinatura = -1;


                statusAssinatura = assinador.assineMemDetached(dataToSign, "Orion Digital - 2016 - ACS Capture", idCertificado, ref assinaturaHex);

                if (statusAssinatura != 1)
                {


                    var error = ErrorCertificado.ListaErroCertificado().Where(c => c.IdErro == statusAssinatura.ToString().Replace("-", "")).FirstOrDefault().DescricaoErro;
                    if (error.Contains("ÇMNK1"))
                    {
                        error = "Ocorreu um erro na validação do certificado digital (" + statusAssinatura + ")";
                    }
                    else
                        error = error + " (" + statusAssinatura + ")";
                
                    ACSLog.InsertLog(MessageBoxIcon.Error, "Não foi possível assinar o documento. Motivo: " + statusAssinatura);
                    throw new ExceptionCustom("Não foi possível assinar o documento. Motivo: " + error);
                }
                retorno = StringToByteArray(assinaturaHex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }

        //public static bool InitialCertificado(string sCertificado)
        //{
        //    try
        //    {


        //        BRYSIGNERCOMLib.IRepositorio repositorio = new BRYSIGNERCOMLib.Repositorio();
        //        repositorio.inicialize("MY", CERT_SYSTEM_STORE_CURRENT_USER);
        //        int totalCertificados = repositorio.getCountCertificados();

        //        BRYSIGNERCOMLib.ICertificado certificado = null;

        //        System.Web.UI.HtmlControls.HtmlSelect listaCertificados = new System.Web.UI.HtmlControls.HtmlSelect();

        //        for (int i = 0; i < totalCertificados; ++i)
        //        {
        //            certificado = repositorio.getCertificado(i);
        //            ListItem item = new ListItem(certificado.getAssuntoCN(), certificado.getIdCertificado());
        //            listaCertificados.Items.Add(item);


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

        private static Byte[] lerArquivo(Stream arq)
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

        private static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static Stream ConvertDocToStream(string urlDoc)
        {
            try
            {
                string sFile = urlDoc; //Path

                Stream imageStreamSource = new FileStream(urlDoc, FileMode.Open, FileAccess.Read, FileShare.Read);

                return imageStreamSource;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static List<Certificado> BuscaCertificadosValidos()
        {
            try
            {
                List<Certificado> ListaRetorno = new List<Certificado>();
                ListaRetorno.Add(new Certificado()
                    {
                        Chave = "0",
                        Nome = ""
                    });
                BRYSIGNERCOMLib.IRepositorio repositorio = new BRYSIGNERCOMLib.Repositorio();
                repositorio.inicialize("MY", CERT_SYSTEM_STORE_CURRENT_USER);
                int totalCertificados = repositorio.getCountCertificados();

                BRYSIGNERCOMLib.ICertificado certificado = null;


                for (int i = 0; i < totalCertificados; ++i)
                {
                    certificado = repositorio.getCertificado(i);
                    string ste = certificado.getAssunto();
                    var teste = ste;
                    if (certificado.verificarValidade() == 1)
                    {
                        ListaRetorno.Add(new Certificado()
                        {
                            Chave = certificado.getIdCertificado(),
                            Nome = certificado.getAssuntoCN(),
                            CPF = certificado.getCPF()
                        });
                        certificado.finalize();
                    }
                }
                repositorio.finalize();

                return ListaRetorno;

            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public static string BuscaCPFCertificadosValidos(string sChave)
        {
            try
            { 
                BRYSIGNERCOMLib.IRepositorio repositorio = new BRYSIGNERCOMLib.Repositorio();
                repositorio.inicialize("MY", CERT_SYSTEM_STORE_CURRENT_USER);
                int totalCertificados = repositorio.getCountCertificados();

                BRYSIGNERCOMLib.ICertificado certificado = null;


                for (int i = 0; i < totalCertificados; ++i)
                {
                    certificado = repositorio.getCertificado(i);
                    string ste = certificado.getAssunto();
                    var teste = ste;
                    if (certificado.verificarValidade() == 1)
                    {
                        if (sChave == certificado.getIdCertificado())
                        {
                            string sCpf = certificado.getCPF();

                            certificado.finalize();
                            return sCpf;
                        } 
                           
                        certificado.finalize();
                    }
                }
                repositorio.finalize();

                return "";

            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public static bool IsCetificadoComputador()
        {
            List<Certificado> certificados = BuscaCertificadosValidos();
            certificados = certificados.Where(e => e.Chave == ACSGlobal.UsuarioLogado.USR_SERIALNUMBERCERT).ToList();

            if (certificados == null || certificados.Count() == 0)
            {
                return false;
            }
            return true;
        }
    }
}
