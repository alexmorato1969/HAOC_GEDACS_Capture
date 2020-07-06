using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using ACSMinCapture.DataBase.Model;
using ACSMinCapture.DataBase;
using System.Reflection;
using System.Linq;
using ACSMinCapture.Controls;
using ACSMinCapture.Global;

namespace ACSMinCapture
{
    internal partial class WFSearchBarCoce : WFOk
    {

        public GED_PROC_CodigosBarras_Result BarCodeSelected { get; set; }

        BindingSource eBin = null;
        public WFSearchBarCoce()
        {  
            //capturar quais divisoes minha area tem

            var flagTipoCliente = ACSGlobal.flagTipoCliente;
            var idArea = ACSGlobal.idAreaSelecionada;
            var listaDivisoes = ACSDataBase.GetDivisoesUsuario(idArea);
            List<GED_PROC_CodigosBarras_Result> ListDataSource = new List<GED_PROC_CodigosBarras_Result>();

            var listagem = ACSDataBase.GetGED_PROC_CodigosBarras(1, 1);

            foreach (var item in listagem)
            {
                if (listaDivisoes.Any(c => c.DIV_IDDIVISAO == item.TPD_IDDIVISAO && item.TPD_FLAGTIPOCLIENTE == flagTipoCliente))
                {
                    ListDataSource.Add(item);
                }
            }


            InitializeComponent();
            btnOk.Click += btnOk_Click1;

            eBin = new BindingSource();
            eBin.DataSource = ListDataSource;
            eBin.AllowNew = false;

            this.dgvBarCodes.DataSource = eBin;
            this.dgvBarCodes.DefaultCellStyle.Font = new Font("Arial", 11);

            this.dgvBarCodes.AutoGenerateColumns = false;
            this.dgvBarCodes.AllowUserToAddRows = false;

            foreach (DataGridViewColumn col in this.dgvBarCodes.Columns)
            {
                col.Visible = false;
            }

            this.dgvBarCodes.Columns["TPD_CODIGOBARRA"].DisplayIndex = 0;
            this.dgvBarCodes.Columns["TPD_CODIGOBARRA"].HeaderText = "Código";
            this.dgvBarCodes.Columns["TPD_CODIGOBARRA"].Visible = true;

            this.dgvBarCodes.Columns["TPD_TEMPOVALIDADE"].DisplayIndex = 1;
            this.dgvBarCodes.Columns["TPD_TEMPOVALIDADE"].HeaderText = "Validade";
            this.dgvBarCodes.Columns["TPD_TEMPOVALIDADE"].Visible = true;

            this.dgvBarCodes.Columns["DIV_CODIGOREDUZIDO"].DisplayIndex = 2;
            this.dgvBarCodes.Columns["DIV_CODIGOREDUZIDO"].HeaderText = "Divisão";
            this.dgvBarCodes.Columns["DIV_CODIGOREDUZIDO"].Visible = true;

            this.dgvBarCodes.Columns["TPD_DESCRICAO"].DisplayIndex = 3;
            this.dgvBarCodes.Columns["TPD_DESCRICAO"].HeaderText = "Descrição";
            this.dgvBarCodes.Columns["TPD_DESCRICAO"].Visible = true;
            this.dgvBarCodes.Columns["TPD_DESCRICAO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            
            this.tbSearch.Focus();            

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.lbTextBox.Text = (sender as RadioButton).Text;
            this.tbSearch.Clear();
        }

        BindingSource SearchBarCode = null;
        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            var list = eBin.List;
            var result = list.OfType<GED_PROC_CodigosBarras_Result>().Where(bc => bc.TPD_DESCRICAO.ToUpper().Contains((sender as TextBox).Text.ToUpper()) || bc.TPD_CODIGOBARRA.Contains((sender as TextBox).Text.ToUpper()));
            //bs.Filter = string.Format("TPD_DESCRICAO LIKE '%{0}%' OR TPD_CODIGOBARRA LIKE '%{1}%'", (sender as TextBox).Text, (sender as TextBox).Text);
            if (SearchBarCode != null)
            {
                SearchBarCode.Dispose();
            }
            
            SearchBarCode = new BindingSource(result,"");

            if ((sender as TextBox).Text.Equals(""))
            {
                this.dgvBarCodes.DataSource = eBin;
                if (SearchBarCode != null)
                {
                    SearchBarCode.Dispose();
                }
            
            }
            else
                this.dgvBarCodes.DataSource = SearchBarCode;

            this.dgvBarCodes.Refresh();
        }


        public void btnOk_Click1(object sender, EventArgs e)
        {
            if (dgvBarCodes.SelectedRows.Count < 0)
            {
                WFMessageBox.Show("Selecione um código", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            else
              DialogResult = DialogResult.OK;

        }

        private void dgvXML_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvBarCodes_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvBarCodes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           // WFMessageBox.Show("Double", MessageBoxButtons.OK, MessageBoxIcon.Error);
            btnOk.PerformClick();
        }
        private void dgvBarCodes_DoubleClick(object sender, EventArgs e)
        {
            btnOk.PerformClick();
        }


        private void dgvBarCodes_SelectionChanged(object sender, EventArgs e)
        {
            if ((sender as DataGridView).SelectedRows.Count <= 0)
                return;

            var row = (sender as DataGridView).SelectedRows[0];
            
            if (row != null)
            {
                this.BarCodeSelected = row.DataBoundItem as GED_PROC_CodigosBarras_Result;
            }
        }


       


   
        
    }
}
