using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ACSMinCapture.Global;

namespace ACSMinCapture
{
    internal partial class WFSetTipoCliente : WFOk
    {
        public DataTable dtDocs = new DataTable();

        public WFSetTipoCliente()
        {
            InitializeComponent();

            this.dtDocs.Columns.Add("fDocs", typeof(string));
            this.dtDocs.Columns.Add("fBarCode", typeof(string));

        }


        private void WFSetValues_Load(object sender, EventArgs e)
        {
            btnCancel.Visible = false;
            btnCloseForm.Visible = false;
            btnOk.Visible = false;
            flowLayoutPanel1.Visible = false;
            var dataSource = ACSMinCapture.Global.ACSGlobal.ListaAreas;
            this.cbbAreas.DataSource = dataSource;
            this.cbbAreas.DisplayMember = "ARE_descricao";
            this.cbbAreas.ValueMember = "ARE_idArea";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {


        }

        private void tbInterDocs_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            int asc = (int)e.KeyChar;
            if (!char.IsDigit(e.KeyChar) && asc != 08 && !e.KeyChar.ToString().Equals("-"))
            {
                e.Handled = true;
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            var idAreaSelecionada = this.cbbAreas.SelectedValue;
            var id = (int)idAreaSelecionada;
            ACSGlobal.idAreaSelecionada = id;

            //get label generic
            var labels = ACSMinCapture.DataBase.ACSDataBase.GetLabelGenerico(id);

            var PF = (labels.LAB_FLAGCLIENTEPF == 0) ? false : true;
            var PJ = (labels.LAB_FLAGCLIENTEPJ == 0) ? false : true;
            ACSGlobal.FlagPF = (PF == null) ? false : (bool)PF;
            ACSGlobal.FlagPJ = (PJ == null) ? false : (bool)PJ;
            this.Close();
        }

    }
}
