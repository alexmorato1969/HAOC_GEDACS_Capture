using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACSMinCapture.Auxiliar
{
    public class AtendimentoRequest
    {
        public int? idPessoa { get; set; }

        public string nome { get; set; }

        public string registroProntuario { get; set; }

        public string dataNascimento { get; set; }

        public string cpf { get; set; }

        public string numeroAtendimento { get; set; }

        public string dataHoraAtendimento { get; set; }

    }
    public class AtendimentoResponse
    {
        public int? idPessoa { get; set; }
        public string  idPessoaHist { get; set; }

        public string nome { get; set; }

        public string registroProntuario { get; set; }
        public string registroProntuarioHist { get; set; }

        public string dataNascimento { get; set; }

        public string cpf { get; set; }

        public string dataHoraCadastroPaciente { get; set; }

        public string numeroAtendimento { get; set; }

        public string tipoAtendimento { get; set; }

        public string dataHoraAtendimento { get; set; }

        public int idEstabelecimento { get; set; }

        public string nomeEstabelecimento { get; set; }
    }

    public class Response
    {
        public object retorno { get; set; }

        public string flgConfirmacao { get; set; }

        public int codMotivo { get; set; }

        public string descMotivo { get; set; }

    }
}
