//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ACSMinCapture.DataBase.Model
{
    public partial class GED_PROC_CodigosBarras_Result
    {
        #region Primitive Properties
    
        public string TPD_CODIGOBARRA
        {
            get;
            set;
        }
    
        public string DIV_CODIGOREDUZIDO
        {
            get;
            set;
        }
    
        public string TPD_DESCRICAO
        {
            get;
            set;
        }
    
        public int TPD_IDDIVISAO
        {
            get;
            set;
        }
    
        public int TPD_IDTIPODOCUMENTO
        {
            get;
            set;
        }
    
        public Nullable<int> STD_IDSUBTIPOSDOCUMENTOS
        {
            get;
            set;
        }
    
        public int STD_FLAGVENCIMENTOMANUAL
        {
            get;
            set;
        }
    
        public int STD_MESVENCIMENTOANUAL
        {
            get;
            set;
        }
    
        public int TPD_TEMPOVALIDADE
        {
            get;
            set;
        }
    
        public int REQUERDATAINICIOVALIDADE
        {
            get;
            set;
        }
    
        public Nullable<System.DateTime> StartDateValidity
        {
            get;
            set;
        }
    
        public Nullable<System.DateTime> DateValidity
        {
            get;
            set;
        }

        #endregion
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
