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
    
    public partial class GEDGLOBALSETTINGS
    {
        public decimal GLO_IDGLOBALSETTINGS { get; set; }
        public Nullable<decimal> GLO_IDUNIDADE { get; set; }
        public string GLO_SMTPSERVIDOR { get; set; }
        public Nullable<decimal> GLO_SMTPPORTA { get; set; }
        public string GLO_SMTPUUARIO { get; set; }
        public string GLO_SMTPSENHA { get; set; }
        public string GLO_SMTPEMAILSUPORTE { get; set; }
        public Nullable<decimal> GLO_INATIVIDADEMINUTOSCHECK { get; set; }
        public Nullable<decimal> GLO_INATIVIDADEMINUTOSPOPUP { get; set; }
        public Nullable<System.DateTime> GLO_DATAHORARIOVERAOINI { get; set; }
        public Nullable<System.DateTime> GLO_DATAHORARIOVERAOFIM { get; set; }
    
        public virtual GEDUNIDADES GEDUNIDADES { get; set; }
    }
}
