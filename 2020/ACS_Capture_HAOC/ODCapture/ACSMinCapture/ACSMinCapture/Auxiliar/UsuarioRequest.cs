using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACSMinCapture.Auxiliar
{
    public class UsuarioRequest
    {
        public string dataHoraCadastro { get; set; }
    }

    public class UsuarioResponse
    {
        public int? idPessoa { get; set; }

        public string nome { get; set; }

        public string usuario { get; set; }

        public string cpf { get; set; }

        public int grupoUsuario { get; set; }

        public string dataHoraCadastro { get; set; }
    }
}

