//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ACSMinCapture.DataBase.ModelOracle
{
    using System;
    using System.Collections.Generic;
    
    public partial class GEDCLIENTEPF
    {
        public decimal CPF_IDCLIENTEPF { get; set; }
        public string CPF_REGISTRO { get; set; }
        public string CPF_NOME { get; set; }
        public string CPF_NOMEPAI { get; set; }
        public string CPF_NOMEMAE { get; set; }
        public string CPF_CPF { get; set; }
        public string CPF_RG { get; set; }
        public Nullable<System.DateTime> CPF_DATANASCIMENTO { get; set; }
        public decimal CPF_IDSEXO { get; set; }
        public decimal CPF_IDUNIDADE { get; set; }
        public string CPF_TEL01 { get; set; }
        public string CPF_TEL02 { get; set; }
        public string CPF_CONTATO { get; set; }
        public string CPF_EMAIL { get; set; }
        public Nullable<System.DateTime> CPF_DATACADASTRO { get; set; }
        public string CPF_OBSERVACAO { get; set; }
        public Nullable<decimal> CPF_FLAGATIVO { get; set; }
        public decimal CPF_IDTIPOCONSELHO { get; set; }
        public string CPF_NUMEROCONSELHO { get; set; }
        public decimal CPF_FLAGCORPOCLINICO { get; set; }
        public string CPF_REGISTROOLD { get; set; }
        public Nullable<decimal> CPF_FLAGATUALIZARUNIFICACAO { get; set; }
    
        public virtual GEDSEXOS GEDSEXOS { get; set; }
        public virtual GEDUNIDADES GEDUNIDADES { get; set; }
    }
}