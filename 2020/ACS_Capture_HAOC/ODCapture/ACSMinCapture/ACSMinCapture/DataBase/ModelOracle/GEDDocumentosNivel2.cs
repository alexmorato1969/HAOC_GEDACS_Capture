using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACSMinCapture.DataBase.ModelOracle
{
    public class GEDDocumentosNivel2
    {

        //DOC
        public decimal? DOC_IDDOCUMENTO { get; set; }

        public string DOC_PATH { get; set; }

        public string DOC_NOMEARQUIVO { get; set; }

        public string DOC_EXTENSAONOMEARQUIVO { get; set; }

        public decimal? DOC_IDUSUARIOASSINANIVEL1 { get; set; }

        public decimal? DOC_IDUSUARIOASSINANIVEL2 { get; set; }

        public decimal? DOC_IDUSUARIOASSINANIVEL3 { get; set; }

        public decimal? DOC_IDUSUARIOASSINANIVEL3_2 { get; set; }

        public string DOC_ASSINATURA { get; set; }

        public decimal? DOC_IDPASSAGEM { get; set; }

        public string DOC_DIVISAO { get; set; }

        public decimal? DOC_IDGRUPOUSUARIOCAPTURA { get; set; }

        //PASSAGEM
        public DateTime? PAS_DATAHORAPASSAGEM { get; set; }

        public string PAS_CODIGOPASSAGEM { get; set; }

        public string PAS_REGISTRO { get; set; }

       //ClientePF

        public decimal? CPF_IDCLIENTEPF { get; set; }

        public string CPF_REGISTRO { get; set; }

        public string CPF_CPF { get; set; }

        public string CPF_NOME { get; set; }


        //SUBTIPODOCUMENT
        public string STD_NOME { get; set; }
        public decimal? STD_NIVELASSINA { get; set; }

        public DateTime? DOC_DATAHORACADASTRO { get; set; }

        public string STD_CODIGO { get; set; }

        //public decimal SET_IDSETOR { get; set; }
    }
}
