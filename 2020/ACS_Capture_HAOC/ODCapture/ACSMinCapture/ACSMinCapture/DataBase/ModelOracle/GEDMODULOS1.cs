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
    
    public partial class GEDMODULOS1
    {
        public GEDMODULOS1()
        {
            this.GEDOBJETOS = new HashSet<GEDOBJETOS1>();
        }
    
        public decimal MOD_IDMODULO { get; set; }
        public string MOD_DESCRICAO { get; set; }
        public decimal MOD_ORDEM { get; set; }
    
        public virtual ICollection<GEDOBJETOS1> GEDOBJETOS { get; set; }
    }
}
