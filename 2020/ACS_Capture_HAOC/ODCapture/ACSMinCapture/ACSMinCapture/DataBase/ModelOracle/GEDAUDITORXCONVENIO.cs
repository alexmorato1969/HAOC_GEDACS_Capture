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
    
    public partial class GEDAUDITORXCONVENIO
    {
        public decimal AUC_IDAUDITORXCONVENIO { get; set; }
        public decimal AUC_IDTIPOAUDITOR { get; set; }
        public decimal AUC_IDCONVENIO { get; set; }
        public Nullable<System.DateTime> AUC_DATAHORAINI { get; set; }
        public Nullable<System.DateTime> AUC_DATAHORAFIM { get; set; }
        public string AUC_OBSERVACAO { get; set; }
        public Nullable<decimal> AUC_FLAGATIVO { get; set; }
    
        public virtual GEDCONVENIO GEDCONVENIO { get; set; }
        public virtual GEDTIPOAUDITOR GEDTIPOAUDITOR { get; set; }
    }
}
