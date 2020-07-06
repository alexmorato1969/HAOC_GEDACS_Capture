using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACSMinCapture.DataBase.ModelOracle.bkps
{
    public partial class GED_PROC_CodigosBarras_Result_BKP
    {
        #region Primitive Properties

        public int TPD_FLAGTIPOCLIENTE
        {
            get;
            set;
        }
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
