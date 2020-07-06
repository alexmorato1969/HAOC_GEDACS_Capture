using ACSMinCapture.Config;
using ACSMinCapture.Log;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACSMinCapture.Auxiliar
{
    public class AtendimentoTasy
    {

        public  AtendimentoResponse  GetAtendimento(string numeroAtendimento)
        {
            try
            {
                string atendimentoTasy = ACSConfig.GetUrlServices().UrlDadosTasy;
                var client = new RestClient(atendimentoTasy);
                var requestAtendimento = new RestRequest("IntegracaoGEDH/Atendimento/Pesquisar", Method.POST);
                requestAtendimento.AddHeader("Content-type", "application/json");

                requestAtendimento.AddJsonBody(new AtendimentoRequest
                {
                    numeroAtendimento = numeroAtendimento
                });

                RestResponse responseAtendimento = (RestResponse)client.Execute(requestAtendimento);
                var contentAtendimento = responseAtendimento.Content; // raw content as string
        
                 
                Response respostaAtendimento = JsonConvert.DeserializeObject<Response>(contentAtendimento);
                if (respostaAtendimento.flgConfirmacao == "N" && respostaAtendimento.codMotivo != 1)
                {
                    ACSLog.InsertLog(MessageBoxIcon.Error, "Erro ao retornar os dados do serviço. " + respostaAtendimento.codMotivo + " - " + respostaAtendimento.descMotivo);
                    throw new Exception("Não foi possível retornar os atendimentos. " + respostaAtendimento.descMotivo);

                }
                respostaAtendimento.retorno = JsonConvert.DeserializeObject<AtendimentoResponse>(respostaAtendimento.retorno.ToString());

                if (respostaAtendimento.codMotivo == 5)
                    return null;


                return (AtendimentoResponse)respostaAtendimento.retorno;
            }
            catch (Exception ex)
            { 
                return null;
                throw;
            }
           

        }

    }
}
