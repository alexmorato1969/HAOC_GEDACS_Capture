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
    
    public partial class GEDLOTESXUSUARIOS1
    {
        public decimal LTU_ID { get; set; }
        public decimal LTU_IDUSUARIO { get; set; }
        public string LTU_CODIGOPASSAGEM { get; set; }
        public decimal LTU_IDSTATUSLOTE { get; set; }
        public Nullable<System.DateTime> LTU_DATA { get; set; }
        public string LTU_OBSERVACAO { get; set; }
        public string LTU_PATHIMAGENS { get; set; }
    
        public virtual GEDSTATUSLOTE1 GEDSTATUSLOTE { get; set; }
        public virtual GEDUSUARIOS1 GEDUSUARIOS { get; set; }
    }
}
