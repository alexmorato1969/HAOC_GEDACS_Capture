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
    
    public partial class GEDSUBTIPOSDOCUMENTOS1
    {
        public GEDSUBTIPOSDOCUMENTOS1()
        {
            this.GEDSUBTIPODOCXGRUPUSER = new HashSet<GEDSUBTIPODOCXGRUPUSER1>();
        }
    
        public decimal STD_IDSUBTIPOSDOCUMENTOS { get; set; }
        public decimal STD_IDTIPOSDOCUMENTOS { get; set; }
        public string STD_DESCRICAO { get; set; }
        public string STD_CODIGOBARRA { get; set; }
        public Nullable<decimal> STD_ORDEM { get; set; }
        public Nullable<decimal> STD_TEMPOVALIDADE { get; set; }
        public Nullable<decimal> STD_OBRIGATORIO { get; set; }
        public Nullable<decimal> STD_TEMPOGUARDA { get; set; }
        public Nullable<decimal> STD_COBRANCADIAS { get; set; }
        public Nullable<decimal> STD_FLAGIMPRESSAO { get; set; }
        public Nullable<decimal> STD_FLAGVENCIMENTOANUAL { get; set; }
        public Nullable<decimal> STD_FLAGVENCIMENTOMANUAL { get; set; }
        public Nullable<decimal> STD_MESVENCIMENTOANUAL { get; set; }
        public decimal STD_FLAGTIPOCLIENTE { get; set; }
        public Nullable<decimal> STD_NIVELASSINA { get; set; }
        public Nullable<decimal> STD_FLAGAPROVADOC { get; set; }
        public Nullable<decimal> STD_TEMPOPROCESSO { get; set; }
    
        public virtual ICollection<GEDSUBTIPODOCXGRUPUSER1> GEDSUBTIPODOCXGRUPUSER { get; set; }
        public virtual GEDTIPOSDOCUMENTOS1 GEDTIPOSDOCUMENTOS { get; set; }
    }
}