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
    
    public partial class GEDCLIENTEPJ1
    {
        public decimal CPJ_IDCLIENTEPJ { get; set; }
        public string CPJ_RAZAOSOCIAL { get; set; }
        public string CPJ_REGISTRO { get; set; }
        public string CPJ_CNPJ { get; set; }
        public string CPJ_TEL01 { get; set; }
        public string CPJ_TEL02 { get; set; }
        public string CPJ_CONTATO { get; set; }
        public string CPJ_EMAIL { get; set; }
        public Nullable<System.DateTime> CPJ_DATACADASTRO { get; set; }
        public Nullable<decimal> CPJ_IDUNIDADE { get; set; }
        public string CPJ_OBSERVACAO { get; set; }
        public Nullable<decimal> CPJ_FLAGATIVO { get; set; }
    
        public virtual GEDUNIDADES1 GEDUNIDADES { get; set; }
    }
}
