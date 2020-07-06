using ACSMinCapture.Auxiliar;
using ACSMinCapture.Config;
using ACSMinCapture.DataBase;
using ACSMinCapture.Global;
using ACSMinCapture.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ACSMinCapture
{
    public partial class WFLogin : WFMDIChild
    {
        public WFLogin()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Thread FirstThread = new Thread(() =>
            {
                fnBtnLogin();
            });
            FirstThread.Start();


        }
		private void fnBtnLogin()
		{
			try
			{
				btnOk.Enabled = false;
				WFLoading.ShowLoad(true, "Login", "Autenticando usuário Tasy");

				ACSGlobal.UsuarioLogado = null;
				//validar no tasy 

				UsuarioTasy tasy = new UsuarioTasy();
				string retornoUsuario = tasy.Execute(tbLogin.Text, tbPass.Text);


				//string retornoUsuario = "null";
				bool fUsuarioTasy = false;

				if (retornoUsuario == "null")
					fUsuarioTasy = false;
				else
					if (retornoUsuario == "")
					fUsuarioTasy = true;
				else
						if (retornoUsuario.Contains("Error") || retornoUsuario.Contains("Erro"))
					fUsuarioTasy = false;

				//consulta usuario na base orion.
				//se o usuario nao tiver no tasy mas o grupo for 0 (orion consultoria) deixa logar
				//se nao for tasy e nem grupo 0 . usuario sem acesso.

				if (fUsuarioTasy)
				{
					ACSGlobal.UsuarioLogado = ACSDataBase.GetGEDUsuarioOracleTasy(tbLogin.Text);
					ACSGlobal.PassLogin = tbPass.Text.Trim();
					ACSGlobal.ListaAreas = ACSDataBase.GetAreasUsuario((int)ACSGlobal.UsuarioLogado.USR_IDUSUARIO);
					ACSGlobal.Duplex = true;

					if (ACSGlobal.UsuarioLogado != null && ACSGlobal.UsuarioLogado.USR_IDUSUARIO > 0)
					{

						if (ACSGlobal.SetoresUsuario == null)
						{
							WFLoading.CloseLoad();
							WFMessageBox.Show("Acesso Negado! Usuário não pertence a nenhum Grupo de Usuário e/ou Setor", MessageBoxButtons.OK, MessageBoxIcon.Stop);
							btnOk.Enabled = true;

						}
						else
						{
							ACSGlobal.isTasy = true;
							// WFMessageBox.Show("Usuário Tasy!", MessageBoxButtons.OK, MessageBoxIcon.Information);
							WFLoading.CloseLoad();
							this.Close();

						}

						//verifica se o usuario tem a chave de assinatura
						//bool fPin = VerificaCertificaXusuairo(ACSGlobal.UsuarioLogado.USR_SERIALNUMBERCERT);
						//if (fPin == false)
						//{
						//    DialogResult resultPin = WFMessageBox.Show("Você não possui chave de assinatura e/ou não consta na máquina!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						//    if (resultPin == System.Windows.Forms.DialogResult.OK)
						//    {

						//        Application.Exit();
						//    }
						//    else
						//    {
						//        Application.Exit();
						//    }
						//}
						//else
						//{

						//    ACSGlobal.isTasy = true;
						//    // WFMessageBox.Show("Usuário Tasy!", MessageBoxButtons.OK, MessageBoxIcon.Information);
						//    thCloseis.();


						//}

					}
					else
					{

						WFLoading.CloseLoad();
						WFMessageBox.Show("Acesso Negado!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						btnOk.Enabled = true;

					}
				}
				else
				{
					ACSGlobal.UsuarioLogado = ACSDataBase.GetGEDUsuarioOracle(tbLogin.Text, tbPass.Text);
					ACSGlobal.PassLogin = tbPass.Text.Trim();
					ACSGlobal.ListaAreas = ACSDataBase.GetAreasUsuario((int)ACSGlobal.UsuarioLogado.USR_IDUSUARIO);
					ACSGlobal.Duplex = true;

					if (ACSGlobal.UsuarioLogado != null && ACSGlobal.UsuarioLogado.USR_IDUSUARIO > 0)
					{

						if (ACSGlobal.UsuarioLogado.USR_IDGRUPOUSUARIO >= 0)
						{

							ACSGlobal.isTasy = false;
							//WFMessageBox.Show("Usuário Orion!", MessageBoxButtons.OK, MessageBoxIcon.Information);
							WFLoading.CloseLoad();
							this.Close();


						}
						else
						{
							if (ACSGlobal.SetoresUsuario == null)
							{
								WFLoading.CloseLoad();
								WFMessageBox.Show("Acesso Negado! Usuário não pertence a nenhum Grupo de Usuário e/ou Setor", MessageBoxButtons.OK, MessageBoxIcon.Stop);
								btnOk.Enabled = true;

							}
							else
							{
								WFLoading.CloseLoad();
								WFMessageBox.Show("Acesso Negado!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
								btnOk.Enabled = true;
							}
						}
					}
					else
					{
						WFLoading.CloseLoad();
						WFMessageBox.Show("Acesso Negado!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						btnOk.Enabled = true;
					}

				}


			}
			catch (Exception ex)
			{
				WFLoading.CloseLoad();
				ACSLog.InsertLog(MessageBoxIcon.Error, ex.Message);
				WFMessageBox.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnOk.Enabled = true;
			}
		}
		private void fnBtnLogin_()
        {
            try
			{
				string codigoCliente = ACSConfig.GetApp().CODIGOCLIENTE;

				var Authy_Cliente = ACSDataBase.validaClienteAtivoNuvem(codigoCliente);


				if (Authy_Cliente == null)
				{
					WFMessageBox.Show("Cliente com código " + codigoCliente + " não foi encontrado!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
				else if (Authy_Cliente.AHC_fBloqueado == true)
				{
					ACSDataBase.InsertLogClienteAtivoNuvem(Authy_Cliente, tbLogin.Text);

					WFMessageBox.Show("Cliente com código " + codigoCliente + " está bloqueado. Motivo: " + Authy_Cliente.AHC_MotivoBloqueio, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
				else
				{
					btnOk.Enabled = false;
					WFLoading.ShowLoad(true, "Login", "Autenticando usuário Tasy");

					ACSGlobal.UsuarioLogado = null;
					//validar no tasy 

					UsuarioTasy tasy = new UsuarioTasy();
					string retornoUsuario = tasy.Execute(tbLogin.Text, tbPass.Text);


					//string retornoUsuario = "null";
					bool fUsuarioTasy = false;

					if (retornoUsuario == "null")
						fUsuarioTasy = false;
					else
						if (retornoUsuario == "")
						fUsuarioTasy = true;
					else
							if (retornoUsuario.Contains("Error") || retornoUsuario.Contains("Erro"))
						fUsuarioTasy = false;

					//consulta usuario na base orion.
					//se o usuario nao tiver no tasy mas o grupo for 0 (orion consultoria) deixa logar
					//se nao for tasy e nem grupo 0 . usuario sem acesso.

					if (fUsuarioTasy)
					{
						ACSGlobal.UsuarioLogado = ACSDataBase.GetGEDUsuarioOracleTasy(tbLogin.Text);
						ACSGlobal.PassLogin = tbPass.Text.Trim();
						ACSGlobal.ListaAreas = ACSDataBase.GetAreasUsuario((int)ACSGlobal.UsuarioLogado.USR_IDUSUARIO);
						ACSGlobal.Duplex = true;

						if (ACSGlobal.UsuarioLogado != null && ACSGlobal.UsuarioLogado.USR_IDUSUARIO > 0)
						{

							if (ACSGlobal.SetoresUsuario == null)
							{
								WFLoading.CloseLoad();
								WFMessageBox.Show("Acesso Negado! Usuário não pertence a nenhum Grupo de Usuário e/ou Setor", MessageBoxButtons.OK, MessageBoxIcon.Stop);
								btnOk.Enabled = true;

							}
							else
							{
								ACSGlobal.isTasy = true;
								// WFMessageBox.Show("Usuário Tasy!", MessageBoxButtons.OK, MessageBoxIcon.Information);
								WFLoading.CloseLoad();
								this.Close();

							}

							//verifica se o usuario tem a chave de assinatura
							//bool fPin = VerificaCertificaXusuairo(ACSGlobal.UsuarioLogado.USR_SERIALNUMBERCERT);
							//if (fPin == false)
							//{
							//    DialogResult resultPin = WFMessageBox.Show("Você não possui chave de assinatura e/ou não consta na máquina!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
							//    if (resultPin == System.Windows.Forms.DialogResult.OK)
							//    {

							//        Application.Exit();
							//    }
							//    else
							//    {
							//        Application.Exit();
							//    }
							//}
							//else
							//{

							//    ACSGlobal.isTasy = true;
							//    // WFMessageBox.Show("Usuário Tasy!", MessageBoxButtons.OK, MessageBoxIcon.Information);
							//    thCloseis.();


							//}

						}
						else
						{

							WFLoading.CloseLoad();
							WFMessageBox.Show("Acesso Negado!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
							btnOk.Enabled = true;

						}
					}
					else
					{
						ACSGlobal.UsuarioLogado = ACSDataBase.GetGEDUsuarioOracle(tbLogin.Text, tbPass.Text);
						ACSGlobal.PassLogin = tbPass.Text.Trim();
						ACSGlobal.ListaAreas = ACSDataBase.GetAreasUsuario((int)ACSGlobal.UsuarioLogado.USR_IDUSUARIO);
						ACSGlobal.Duplex = true;

						if (ACSGlobal.UsuarioLogado != null && ACSGlobal.UsuarioLogado.USR_IDUSUARIO > 0)
						{

							if (ACSGlobal.UsuarioLogado.USR_IDGRUPOUSUARIO >= 0)
							{

								ACSGlobal.isTasy = false;
								//WFMessageBox.Show("Usuário Orion!", MessageBoxButtons.OK, MessageBoxIcon.Information);
								WFLoading.CloseLoad();
								this.Close();


							}
							else
							{
								if (ACSGlobal.SetoresUsuario == null)
								{
									WFLoading.CloseLoad();
									WFMessageBox.Show("Acesso Negado! Usuário não pertence a nenhum Grupo de Usuário e/ou Setor", MessageBoxButtons.OK, MessageBoxIcon.Stop);
									btnOk.Enabled = true;

								}
								else
								{
									WFLoading.CloseLoad();
									WFMessageBox.Show("Acesso Negado!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
									btnOk.Enabled = true;
								}
							}
						}
						else
						{
							WFLoading.CloseLoad();
							WFMessageBox.Show("Acesso Negado!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
							btnOk.Enabled = true;
						}

					}
				}

            }
            catch (Exception ex)
            {
                WFLoading.CloseLoad();
                ACSLog.InsertLog(MessageBoxIcon.Error, ex.Message);
                WFMessageBox.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnOk.Enabled = true;
            }
        }

        private void WFLogin_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Application.ExitThread();
            this.Close();
        }

        private void WFLogin_Load(object sender, EventArgs e)
        {


          

             

            lbversao.Text = "Versão " + Assembly.GetEntryAssembly().GetName().Version.ToString();
            tbLogin.Focus();
            if (ACSConfig.GetApp().Logo.ToUpper() == "GEDPES")
            {
                pbLogo.Image = global::ACSMinCapture.Properties.Resources.GEDPES;
            }
            else if (ACSConfig.GetApp().Logo.ToUpper() == "GEDH")
            {
                pbLogo.Image = global::ACSMinCapture.Properties.Resources.GEDH;
            }
        }

        private void tbPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                btnOk.PerformClick();
        }

        private void WFLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ACSGlobal.UsuarioLogado == null)
                Application.Exit();
        }




        // Retirado da documentação
        public const int CERT_SYSTEM_STORE_CURRENT_USER = 0;

        public List<Certificado> BuscaCertificados()
        {
            try
            {
                List<Certificado> ListaRetorno = new List<Certificado>();

                BRYSIGNERCOMLib.IRepositorio repositorio = new BRYSIGNERCOMLib.Repositorio();
                repositorio.inicialize("MY", CERT_SYSTEM_STORE_CURRENT_USER);
                int totalCertificados = repositorio.getCountCertificados();

                BRYSIGNERCOMLib.ICertificado certificado = null;


                for (int i = 0; i < totalCertificados; ++i)
                {
                    certificado = repositorio.getCertificado(i);

                    if (certificado.verificarValidade() == 1)
                    {
                        ListaRetorno.Add(new Certificado()
                        {  
                            Chave = certificado.getIdCertificado(),
                            Nome = certificado.getAssuntoCN()
                        });
                        certificado.finalize();
                    }

                }
                repositorio.finalize();

                return ListaRetorno;

            }
            catch (Exception ex)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, ex.Message);
                throw;
            }


        }

        public bool VerificaCertificaXusuairo(string certificadoUsuario)
        {
            foreach (var item in BuscaCertificados())
            {
                if (item.Chave == certificadoUsuario)
                {
                    return true;
                    break;
                }
            }

            return false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
