using ACSMinCapture.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using ACSMinCapture.Global;
using ACSMinCapture.Config;
using ACSMinCapture.Forms;
using TwainLib;
namespace ACSMinCapture
{
    public partial class WFMain : WFTitle
    {

        public WFMain()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        void BeforeExitwfCapture(object sender)
        {
            Global.ACSGlobal.TipoCaptura = 0;
            if (ACSGlobal.UsuarioLogado.USR_FLAGDIGITALIZACAO == 1)
                ACSConfig.SystemAction = ModeSystem.Scan;

            if (ACSGlobal.UsuarioLogado.USR_FLAGIMPORTACAO == 1)
                ACSConfig.SystemAction = ACSConfig.SystemAction | ModeSystem.Import;

            if (ACSGlobal.UsuarioLogado.USR_FLAGPROCESSAMENTO == 1 || ACSConfig.GetApp().User == ModeUser.Mono)
                ACSConfig.SystemAction = ACSConfig.SystemAction | ModeSystem.Process;



            if (((ACSConfig.SystemAction == (ModeSystem.Scan | ModeSystem.Import | ModeSystem.Process)) ||
               (ACSConfig.SystemAction == (ModeSystem.Scan | ModeSystem.Import)) ||
               (ACSConfig.SystemAction == (ModeSystem.Scan | ModeSystem.Process)) ||
               (ACSConfig.SystemAction == (ModeSystem.Import | ModeSystem.Process)))
               && ACSGlobal.UsuarioLogado.USR_NIVELASSINA > 1
                )
            {
                var wfTA = new WFTipoAcao();
                wfTA.btnEscanear.Visible = true;
                //
                wfTA.btnEscanear.Visible = ((ACSConfig.SystemAction & (ModeSystem.Scan)) == ModeSystem.Scan);
                wfTA.btnImportar.Visible = ((ACSConfig.SystemAction & (ModeSystem.Import)) == ModeSystem.Import);
                wfTA.btnProcessar.Visible = ((ACSConfig.SystemAction & (ModeSystem.Process)) == ModeSystem.Process) && (ACSConfig.GetApp().User == ModeUser.Multi);
                wfTA.MdiParent = this;
                wfTA.WindowState = FormWindowState.Normal;
                wfTA.BeforeExitEvent += BeforeExitTipoAcao;
                wfTA.Dock = DockStyle.Fill;
                wfTA.Show();


            }
            else
                if (ACSGlobal.UsuarioLogado.USR_NIVELASSINA > 1)
                {
                    var wfTAs = new WFAssinaNivel2();
                    wfTAs.MdiParent = this;
                    wfTAs.WindowState = FormWindowState.Normal;
                    wfTAs.Dock = DockStyle.Fill;
                    wfTAs.BeforeExitEvent += BeforeExitwfAssinatura;
                    wfTAs.Show();

                }
                else if ((ACSConfig.SystemAction == (ModeSystem.Scan | ModeSystem.Import | ModeSystem.Process)) ||
                   (ACSConfig.SystemAction == (ModeSystem.Scan | ModeSystem.Import)) ||
                   (ACSConfig.SystemAction == (ModeSystem.Scan | ModeSystem.Process)) ||
                   (ACSConfig.SystemAction == (ModeSystem.Import | ModeSystem.Process)))
                {
                    var wfTA = new WFTipoAcao();

                    wfTA.btnAssinar.Visible = false;
                    wfTA.btnEscanear.Visible = ((ACSConfig.SystemAction & (ModeSystem.Scan)) == ModeSystem.Scan);
                    wfTA.btnImportar.Visible = ((ACSConfig.SystemAction & (ModeSystem.Import)) == ModeSystem.Import);
                    wfTA.btnProcessar.Visible = ((ACSConfig.SystemAction & (ModeSystem.Process)) == ModeSystem.Process) && (ACSConfig.GetApp().User == ModeUser.Multi);
                    wfTA.MdiParent = this;
                    wfTA.WindowState = FormWindowState.Normal;
                    wfTA.BeforeExitEvent += BeforeExitTipoAcao;
                    wfTA.Dock = DockStyle.Fill;
                    if (!ACSConfig.GetScanner().Driver.ToUpper().Contains("FUJITSU") && !ACSConfig.GetScanner().Driver.ToUpper().Contains("SP-1120"))
                        Application.DoEvents(); 
                    wfTA.Show();
                }
        }

        public void BeforeExitwfAssinatura(object sender)
        {
            Application.Exit();
        }

       public void BeforeExitTipoAcao(object sender)
        {
            if (Global.ACSGlobal.TipoCaptura == 0)
            {
                Application.Exit();
            }

            if (Global.ACSGlobal.TipoCaptura == 4)
            {
                var wfTAs = new WFAssinaNivel2();
                wfTAs.MdiParent = this;
                wfTAs.WindowState = FormWindowState.Normal;
                wfTAs.BeforeExitEvent += BeforeExitwfAssinatura;
                wfTAs.Dock = DockStyle.Fill;
                wfTAs.Show();

            }
            else
            {

                var wfCapture = new WFCapture();
                wfCapture.MdiParent = this;
                wfCapture.WindowState = FormWindowState.Normal;
                wfCapture.Dock = DockStyle.Fill;
                wfCapture.BeforeExitEvent += BeforeExitwfCapture;
                wfCapture.Show();
            }
        }

        void BeforeExitLogin(object sender)
        {


            //teste nova tela assinador 05012016

            (sender as WFLogin).Hide();
            if (ACSGlobal.UsuarioLogado == null || ACSGlobal.UsuarioLogado.USR_IDUSUARIO == 0)
            {
                Application.Exit();

            }
            else
            {
                int iQntAcao = 0;

                if (ACSGlobal.UsuarioLogado.USR_FLAGDIGITALIZACAO == 1)
                {
                    ACSConfig.SystemAction = ModeSystem.Scan;
                    iQntAcao++;
                }

                if (ACSGlobal.UsuarioLogado.USR_FLAGIMPORTACAO == 1)
                {
                    ACSConfig.SystemAction = ACSConfig.SystemAction | ModeSystem.Import;
                    iQntAcao++;
                }

                if (ACSGlobal.UsuarioLogado.USR_FLAGPROCESSAMENTO == 1 || ACSConfig.GetApp().User == ModeUser.Mono)
                    ACSConfig.SystemAction = ACSConfig.SystemAction | ModeSystem.Process;



                if (((ACSConfig.SystemAction == (ModeSystem.Scan | ModeSystem.Import | ModeSystem.Process)) ||
                   (ACSConfig.SystemAction == (ModeSystem.Scan | ModeSystem.Import)) ||
                   (ACSConfig.SystemAction == (ModeSystem.Scan | ModeSystem.Process)) ||
                   (ACSConfig.SystemAction == (ModeSystem.Import | ModeSystem.Process)))
                   && ACSGlobal.UsuarioLogado.USR_NIVELASSINA > 1
                    )
                {
                    iQntAcao++;
                    var wfTA = new WFTipoAcao();
                    wfTA.btnEscanear.Visible = true;
                    //
                    wfTA.btnEscanear.Visible = ((ACSConfig.SystemAction & (ModeSystem.Scan)) == ModeSystem.Scan);
                    wfTA.btnImportar.Visible = ((ACSConfig.SystemAction & (ModeSystem.Import)) == ModeSystem.Import);
                    wfTA.btnProcessar.Visible = ((ACSConfig.SystemAction & (ModeSystem.Process)) == ModeSystem.Process) && (ACSConfig.GetApp().User == ModeUser.Multi);
                    wfTA.btnAssinar.Visible = true;
                    wfTA.MdiParent = this;
                    wfTA.WindowState = FormWindowState.Normal;
                    wfTA.BeforeExitEvent += BeforeExitTipoAcao;
                    wfTA.Dock = DockStyle.Fill;
                    if (iQntAcao > 1)
                    {
                        wfTA.Show();
                    }
                    else
                    {
                        if (ACSGlobal.UsuarioLogado.USR_FLAGDIGITALIZACAO == 1)
                        {
                            changeScanOneAction();
                            attrAreas();
                            BeforeExitTipoAcao(sender);
                        }


                        if (ACSGlobal.UsuarioLogado.USR_FLAGIMPORTACAO == 1)
                        {
                            ACSConfig.SystemAction = ModeSystem.Import;
                            Global.ACSGlobal.TipoCaptura = 2;

                            if (ACSConfig.GetApp().User == ModeUser.Mono)
                                ACSConfig.SystemAction = ACSConfig.SystemAction | ModeSystem.Process;

                            attrAreas();
                            BeforeExitTipoAcao(sender);
                        }
                    }


                }
                else
                    if (ACSGlobal.UsuarioLogado.USR_NIVELASSINA > 1)
                    {
                        var wfTAs = new WFAssinaNivel2();
                        wfTAs.MdiParent = this;
                        wfTAs.WindowState = FormWindowState.Normal;
                        wfTAs.Dock = DockStyle.Fill;
                        wfTAs.BeforeExitEvent += BeforeExitwfAssinatura;
                        wfTAs.Show();

                    }
                    else if ((ACSConfig.SystemAction == (ModeSystem.Scan | ModeSystem.Import | ModeSystem.Process)) ||
                       (ACSConfig.SystemAction == (ModeSystem.Scan | ModeSystem.Import)) ||
                       (ACSConfig.SystemAction == (ModeSystem.Scan | ModeSystem.Process)) ||
                       (ACSConfig.SystemAction == (ModeSystem.Import | ModeSystem.Process)))
                    {
                        var wfTA = new WFTipoAcao();

                        wfTA.btnAssinar.Visible = false;
                        wfTA.btnEscanear.Visible = ((ACSConfig.SystemAction & (ModeSystem.Scan)) == ModeSystem.Scan);
                        wfTA.btnImportar.Visible = ((ACSConfig.SystemAction & (ModeSystem.Import)) == ModeSystem.Import);
                        wfTA.btnProcessar.Visible = ((ACSConfig.SystemAction & (ModeSystem.Process)) == ModeSystem.Process) && (ACSConfig.GetApp().User == ModeUser.Multi);
                        wfTA.MdiParent = this;
                        wfTA.WindowState = FormWindowState.Normal;
                        wfTA.BeforeExitEvent += BeforeExitTipoAcao;
                        wfTA.Dock = DockStyle.Fill;
                        if (!ACSConfig.GetScanner().Driver.ToUpper().Contains("FUJITSU") && !ACSConfig.GetScanner().Driver.ToUpper().Contains("SP-1120"))
                            Application.DoEvents(); 
                        if (iQntAcao > 1)
                        {
                            wfTA.Show();
                        }
                        else
                        {
                            if (ACSGlobal.UsuarioLogado.USR_FLAGDIGITALIZACAO == 1)
                            {
                                changeScanOneAction();
                                attrAreas();
                                BeforeExitTipoAcao(sender);
                            }


                            if (ACSGlobal.UsuarioLogado.USR_FLAGIMPORTACAO == 1)
                            {
                                ACSConfig.SystemAction = ModeSystem.Import;
                                Global.ACSGlobal.TipoCaptura = 2;

                                if (ACSConfig.GetApp().User == ModeUser.Mono)
                                    ACSConfig.SystemAction = ACSConfig.SystemAction | ModeSystem.Process;

                                attrAreas();
                                BeforeExitTipoAcao(sender);
                            }
                        }
                    }
                    else
                    {
                        WFMessageBox.Show("Usuário sem Acesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ACSDataBase.StopSession();
                        Application.ExitThread();
                    }
            }
        }

        private void WFMain_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.BackColor = Color.White;
            this.Invalidate();

            this.IsMdiContainer = true;
            var wfLogin = new WFLogin();
            wfLogin.MdiParent = this;
            wfLogin.WindowState = FormWindowState.Normal;
            wfLogin.Dock = DockStyle.Fill;
            wfLogin.BeforeExitEvent += BeforeExitLogin;
            wfLogin.Show();
            this.Show();
        }

        private void WFMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ACSDataBase.StopSession();
            Application.ExitThread();
        }


        public void changeScanOneAction()
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
                    return;
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
        }

        public void attrAreas()
        {
            if (ACSMinCapture.Global.ACSGlobal.ListaAreas != null && ACSMinCapture.Global.ACSGlobal.ListaAreas.Count > 1)
            {

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
    }
}
