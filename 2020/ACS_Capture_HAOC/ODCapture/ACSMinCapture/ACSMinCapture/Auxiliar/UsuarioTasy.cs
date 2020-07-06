using ACSMinCapture.Config;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ACSMinCapture.Auxiliar
{
    public class UsuarioTasy
    {


        public string Execute(string user, string senha)
        {
            HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();

            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/""
                xmlns:wheb=""http://xfire.codehaus.org/WhebService"">                    
                    <soapenv:Body>
                        <wheb:validaUsuario>
                            <wheb:usuario>" + user + @"</wheb:usuario>
                            <wheb:senha>" + senha + @"</wheb:senha> 
                        </wheb:validaUsuario>
                    </soapenv:Body>
                </soapenv:Envelope>");



            try
            {
                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        string soapResult = rd.ReadToEnd();

                        Console.WriteLine("XML de retorno: ");
                        Console.WriteLine(soapResult);
                        Console.WriteLine("\n\n");
                        //XmlDocument document = new XmlDocument();
                        //document.LoadXml(soapResult);  //loading soap message as string 

                        XDocument xdoc = XDocument.Parse(soapResult);
                        XNamespace ns = "http://xfire.codehaus.org/WhebService";

                        XElement valida = xdoc.Descendants(ns + "validaUsuarioResponse").FirstOrDefault();

                        // neste ponto, verificar de valida é nulo.
                        // se for nulo, usuário não está autorizado para entrar
                        // se não for nulo, está autorizado


                        if (valida.Value == null)
                            return "null";

                        string valor = valida.Value;
                        //Console.WriteLine("Elemento validaUsuarioResponse: " + valida);
                        //Console.WriteLine("Valor de validaUsuarioResponse: " + valor);


                        return valor;

                    }
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.ToString();
                Console.WriteLine("Erro ao validar usuario");
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Create a soap webrequest to [Url]
        /// </summary>
        /// <returns></returns>
        public static HttpWebRequest CreateWebRequest()
        {
            string AutenticationTasy = ACSConfig.GetUrlServices().UrlAutenticationTasy;

            // HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"http://172.25.0.136:8080/WhebWS/ws/SistemaWS");
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(AutenticationTasy);
            webRequest.Headers.Add(@"SOAPENV:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }




        public void GetUsuarioTasy(string valor)
        {
            string DadosTasy = ACSConfig.GetUrlServices().UrlDadosTasy;

           // var client = new RestClient("http://172.25.0.170:7016");
            // Usuario           

            var client = new RestClient(DadosTasy);
            var requestUsuario = new RestRequest("IntegracaoGEDH/Usuario/ListarUltimos", Method.POST);
            requestUsuario.AddHeader("Content-type", "application/json");
            requestUsuario.AddJsonBody(new UsuarioRequest
            {
                dataHoraCadastro = valor
            });

            RestResponse responseUsuario = (RestResponse)client.Execute(requestUsuario);
            var contentUsuario = responseUsuario.Content; // raw content as string
            Console.WriteLine("Resposta WS Usuario:");
            Console.WriteLine(contentUsuario);
            Response resposta = JsonConvert.DeserializeObject<Response>(contentUsuario);
            resposta.retorno = JsonConvert.DeserializeObject<List<UsuarioResponse>>(resposta.retorno.ToString());
            //Console.WriteLine(((List<UsuarioResponse>)resposta.retorno).Count());
            foreach (var r in (List<UsuarioResponse>)resposta.retorno)
            {
                Console.WriteLine(r.usuario);
            }
        }







        public static string removerAcentos(string texto)
        {


            char[] array = texto.ToCharArray();

            // Loop through array.
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == Convert.ToChar('\''))
                {
                    // Get character from array.
                    array[i] = '´';

                }
            }
            texto = "";
            for (int i = 0; i < array.Length; i++)
            {

                texto = texto + array[i].ToString();
            }

            return texto;
        }
    }
}
