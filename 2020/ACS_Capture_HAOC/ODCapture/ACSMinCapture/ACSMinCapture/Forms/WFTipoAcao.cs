using ACSMinCapture.Auxiliar;
using ACSMinCapture.Config;
using ACSMinCapture.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TwainLib;

namespace ACSMinCapture
{
    public partial class WFTipoAcao : WFMDIChild
    {
        public WFTipoAcao()
        {
            InitializeComponent();
            btnEscanear.Visible = true;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton1_Click(object sender, EventArgs e)
        {

        }

        private void rbEscanear_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                WFMessageBox.Show(ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }
        }

        private void rbImportar_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void WFTipoAcao_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if ((!this.rbEscanear.Checked) && (!this.rbImportar.Checked) && (!this.rbProcessar.Checked) && (!this.rbAssinar.Checked))
            //{
            //    WFMessageBox.Show("Selecione uma opção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    e.Cancel = true;
            //}
        }

        private void WFTipoAcao_Load(object sender, EventArgs e)
        {

            try
            {


                if (this.btnEscanear.Visible && this.btnImportar.Visible && this.btnAssinar.Visible)
                {
                    this.tableLayoutPanel2.ColumnCount = 3;

                    this.tableLayoutPanel2.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 33);
                    this.tableLayoutPanel2.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 33);
                    this.tableLayoutPanel2.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 33);
                }

                if (this.btnEscanear.Visible && this.btnImportar.Visible && !this.btnAssinar.Visible)
                {
                    this.tableLayoutPanel2.ColumnCount = 2;
                    this.tableLayoutPanel2.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 50);
                    this.tableLayoutPanel2.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 50);
                }

                if (this.btnEscanear.Visible && !this.btnImportar.Visible && this.btnAssinar.Visible)
                {
                    this.tableLayoutPanel2.ColumnCount = 3;

                    this.tableLayoutPanel2.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 50);
                    this.tableLayoutPanel2.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 50);
                }

                if (!this.btnEscanear.Visible && this.btnImportar.Visible && this.btnAssinar.Visible)
                {
                    this.tableLayoutPanel2.ColumnCount = 2;
                    this.tableLayoutPanel2.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 50);
                    this.tableLayoutPanel2.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 50);
                }
                bool istasy = ACSGlobal.isTasy;

                //if (istasy)
                //    lblTipoUsuario.Text = "Usuário Tasy";
                //else
                //    lblTipoUsuario.Text = "Usuário Orion";


                //verificar o tipo divisao se tem PF ou PJ e mostrar 


                if (ACSMinCapture.Global.ACSGlobal.ListaAreas != null && ACSMinCapture.Global.ACSGlobal.ListaAreas.Count > 1)
                {
                    //abrir view para escolha

                    WFSetArea wfSV = new WFSetArea();
                    wfSV.ShowInTaskbar = false;
                    wfSV.ShowDialog(this);
                }
                else
                {
                    foreach (var item in ACSMinCapture.Global.ACSGlobal.ListaAreas)
                    {
                        ACSMinCapture.Global.ACSGlobal.idAreaSelecionada = (int)item.ARE_IDAREA;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void rbImportar_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void rbAssinar_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnEscanear_Click(object sender, EventArgs e)
        {

            // verifica se interação é do tipo escaner e se tem escaner configurado

            // verificar se existe o driver no config

            var driverValido = false;
            var confValida = true;
            string mensagem = "Verifique os seguinte problemas de configuração: ";
            if (!String.IsNullOrEmpty(ACSConfig.GetScanner().Driver))
            {
                using (var tw = new Twain(this.Handle))
                {


                    foreach (var device in tw.GetDevices())
                    {
                        if (device.ProductName == ACSConfig.GetScanner().Driver)
                        {
                            driverValido = true;
                            break;
                        }
                    }

                    //cbDrivers.Text = ACSConfig.GetScanner().Driver;
                }



            }

            if ((ACSConfig.SystemAction & ModeSystem.Scan) == ModeSystem.Scan && !driverValido)
            {
                //WFMessageBox.Show("Driver de escâner não configurado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                mensagem += "\n\tDriver de escâner não configurado";
                confValida = false;
            }


            //  if (String.IsNullOrEmpty(ACSGlobal.UsuarioLogado.USR_SERIALNUMBERCERT))
            //  {
            //      mensagem += "\n\tUsuário sem certificado digital vinculado";
            //      confValida = false;
            //  }

            //  if (!Assinador.IsCetificadoComputador())
            //  {
            //      mensagem += "\n\tCertificado do usuário não encontrado";
            //      confValida = false;
            //  }

            Global.ACSGlobal.configScanValida = confValida;
            if (!confValida)
            {
                if (ACSGlobal.GrupoUsuario.GRP_FLAGPREFERENCIA == 0)
                {
                    mensagem += "\nEntre em contato com o suporte";
                    WFMessageBox.Show(mensagem, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                }
                else
                {

                    WFMessageBox.Show(mensagem, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            ACSConfig.SystemAction = ModeSystem.Scan;
            Global.ACSGlobal.TipoCaptura = 1;

            if (ACSConfig.GetApp().User == ModeUser.Mono)
                ACSConfig.SystemAction = ACSConfig.SystemAction | ModeSystem.Process;

            this.Close();
        }

        private void btnAssinar_Click(object sender, EventArgs e)
        {
            var nivelAssinaCondifg = ACSConfig.GetApp().NIVELASSINA;

            if (nivelAssinaCondifg.ToUpper() == "ALL")
            {
                ACSConfig.SystemAction = ModeSystem.Process;
                Global.ACSGlobal.TipoCaptura = 4;

                if (ACSConfig.GetApp().User == ModeUser.Mono)
                    ACSConfig.SystemAction = ACSConfig.SystemAction | ModeSystem.Process;

                this.Close();
            }
            else
            {
                var fNivelValid = false;
                var splitNivel = nivelAssinaCondifg.Split(',');
                foreach (var item in splitNivel)
                {
                    if (!string.IsNullOrEmpty(item))
                        if (ACSGlobal.UsuarioLogado.USR_NIVELASSINA != null && (int)ACSGlobal.UsuarioLogado.USR_NIVELASSINA == int.Parse(item))
                        {
                            fNivelValid = true;
                        }
                }
                

                if(fNivelValid==true)
                {
                    ACSConfig.SystemAction = ModeSystem.Process;
                    Global.ACSGlobal.TipoCaptura = 4;

                    if (ACSConfig.GetApp().User == ModeUser.Mono)
                        ACSConfig.SystemAction = ACSConfig.SystemAction | ModeSystem.Process;

                    this.Close();
                }
                else
                {
                   var mensagem  = "Nível não incluido no perfil ou usuário não tem permissão para assinar no nível configurado";
                    WFMessageBox.Show(mensagem, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }


            }



        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            ACSConfig.SystemAction = ModeSystem.Import;
            Global.ACSGlobal.TipoCaptura = 2;

            if (ACSConfig.GetApp().User == ModeUser.Mono)
                ACSConfig.SystemAction = ACSConfig.SystemAction | ModeSystem.Process;

            this.Close();
        }


        private void btnProcessar_Click(object sender, EventArgs e)
        {
            ACSConfig.SystemAction = ModeSystem.Process;
            Global.ACSGlobal.TipoCaptura = 1;

            if (ACSConfig.GetApp().User == ModeUser.Mono)
                ACSConfig.SystemAction = ACSConfig.SystemAction | ModeSystem.Process;

            this.Close();
        }
    }
}
