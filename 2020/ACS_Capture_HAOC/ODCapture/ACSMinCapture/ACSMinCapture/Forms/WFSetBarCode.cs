using ACSMinCapture.Controls;
using ACSMinCapture.DataBase;
using ACSMinCapture.DataBase.Model;
using ACSMinCapture.Global;
using ACSMinCapture.Log;
using OCRTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACSMinCapture
{
    partial class WFSetBarCode : WFOk
    {
        public GED_PROC_CodigosBarras_Result BarCodeSelected { get; set; }

        public WFSetBarCode()
        {
            InitializeComponent();
            tbText.Focus();
            tbText.SelectAll();

            this.btnOk.Click += btnOk_Click1;
        }

        public void btnOk_Click1(object sender, EventArgs e)
        {
            try
            {
                if (this.tbText.Text.Trim() == string.Empty)
                {
                    WFMessageBox.Show("Informe Código de Barras!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.DialogResult = DialogResult.None;
                    return;
                }

                var flagTipoCliente = ACSGlobal.flagTipoCliente;
                var idArea = ACSGlobal.idAreaSelecionada;
                var listaDivisoes = ACSDataBase.GetDivisoesUsuario(idArea);
                var listaTipoCliente = ACSDataBase.GetTipoClienteArea(idArea);

                List<GED_PROC_CodigosBarras_Result> ListDataSource = new List<GED_PROC_CodigosBarras_Result>();

                var result = ACSDataBase.GetGED_PROC_CodigosBarras(1, 1, this.tbText.Text.Trim()).ToList();




                foreach (var item in result)
                {
                    if (listaDivisoes.Any(c => c.DIV_IDDIVISAO == item.TPD_IDDIVISAO && item.TPD_FLAGTIPOCLIENTE == flagTipoCliente))
                    {
                        ListDataSource.Add(item);
                    }
                }



                if (ListDataSource.Count() <= 0)
                {
                    WFMessageBox.Show("Código de Barras não localizado!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.DialogResult = DialogResult.None;
                }
                else
                {
                    this.BarCodeSelected = (GED_PROC_CodigosBarras_Result)result.First().Clone();
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception err)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, err);
            }

        }

        private void tbText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnOk.PerformClick();
            }
        }

        private void btnSearchBarCode_Click(object sender, EventArgs e)
        {
            using (var wfSBC = new WFSearchBarCoce())
            {
                if (wfSBC.ShowDialog() == DialogResult.OK)
                {
                    this.BarCodeSelected = (GED_PROC_CodigosBarras_Result)wfSBC.BarCodeSelected.Clone();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void WFSetBarCode_Load(object sender, EventArgs e)
        {
            var idArea = ACSGlobal.idAreaSelecionada;
            var listaDivisaoArea = ACSDataBase.GetDivisoesUsuario(idArea);

            var listaTipoCliente = ACSDataBase.GetTipoClienteArea(idArea);


            bool fFisico = false;
            bool fJuridico = false;
            rbFisica.Enabled = false;
            rbJuridico.Enabled = false;

            foreach (var item in listaTipoCliente)
            {
                if (item.TIC_FLAGTIPOCLIENTE == 1)
                    fFisico = true;
                if (item.TIC_FLAGTIPOCLIENTE == 2)
                    fJuridico = true;
            }

            //verificava por divisão
            // foreach (var item in listaDivisaoArea)
            // {
            //     if (item.DIV_FLAGTIPOCLIENTE == 1)
            //         fFisico = true;
            //     if (item.DIV_FLAGTIPOCLIENTE == 2)
            //         fJuridico = true;
            // }

            if (fFisico && fJuridico)
            {
                rbFisica.Enabled = true;
                rbJuridico.Enabled = true;
                rbFisica.Checked = true;
                ACSGlobal.FlagPF = true;
                ACSGlobal.flagTipoCliente = 1;
            }
            else
                if (fFisico && !fJuridico)
                {
                    rbFisica.Enabled = true;
                    rbFisica.Checked = true;
                    ACSGlobal.FlagPF = true;
                    ACSGlobal.flagTipoCliente = 1;
                    gpPFpj.Visible = false;
                }
                else if (!fFisico && fJuridico)
                {
                    rbJuridico.Enabled = true;
                    rbJuridico.Checked = true;
                    ACSGlobal.FlagPJ = true;
                    ACSGlobal.flagTipoCliente = 2;
                    gpPFpj.Visible = false;
                }
        }

        private void tbText_TextChanged(object sender, EventArgs e)
        {

        }

        private void rbFisica_CheckedChanged(object sender, EventArgs e)
        {

            ACSGlobal.FlagPF = true;
            ACSGlobal.FlagPJ = false;
            ACSGlobal.flagTipoCliente = 1;
        }

        private void rbJuridico_CheckedChanged(object sender, EventArgs e)
        {
            ACSGlobal.FlagPF = false;
            ACSGlobal.FlagPJ = true;
            ACSGlobal.flagTipoCliente = 2;

        }




    }
}
