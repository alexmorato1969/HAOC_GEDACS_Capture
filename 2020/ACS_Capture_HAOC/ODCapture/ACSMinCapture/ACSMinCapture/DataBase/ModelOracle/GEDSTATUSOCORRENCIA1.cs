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
    
    public partial class GEDSTATUSOCORRENCIA1
    {
        public GEDSTATUSOCORRENCIA1()
        {
            this.GEDLOGOCORRENCIA = new HashSet<GEDLOGOCORRENCIA1>();
            this.GEDOCORRENCIAS = new HashSet<GEDOCORRENCIAS1>();
        }
    
        public decimal STO_IDSTATUSOCORRENCIA { get; set; }
        public string STO_DESCRICAO { get; set; }
    
        public virtual ICollection<GEDLOGOCORRENCIA1> GEDLOGOCORRENCIA { get; set; }
        public virtual ICollection<GEDOCORRENCIAS1> GEDOCORRENCIAS { get; set; }
    }
}