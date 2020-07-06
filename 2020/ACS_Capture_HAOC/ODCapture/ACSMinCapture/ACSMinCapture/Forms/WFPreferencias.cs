using ACSMinCapture.Auxiliar;
using ACSMinCapture.Config;
using ACSMinCapture.DataBase.ModelOracle;
using ACSMinCapture.Forms;
using ACSMinCapture.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwainLib;

namespace ACSMinCapture
{
    public partial class WFPreferencias : WFTwain
    {

        IEnumerable<GEDPESSOAS> ListaPessoas;
        public WFPreferencias()
        {

            InitializeComponent();



        }

        private bool SaveSettings()
        {
            List<string> UsuariosInvalidos = new List<string>();


            ///GRID ASSINATURAS
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    bool fInsert = true;
                    DataGridViewTextBoxCell txtNome = (DataGridViewTextBoxCell)(row.Cells[0]);
                    DataGridViewComboBoxCell combo = (DataGridViewComboBoxCell)(row.Cells[1]);

                    if (txtNome.Tag != null)
                    {
                        var id = ((GEDPESSOAS)txtNome.Tag).PES_IDPESSOA;
                        int idPessoa = (int)id;
                        var sNome = txtNome.Value;
                        string chave = (combo.Value == null) ? "" : combo.Value.ToString();

                        if (chave != "")
                        {
                            string sCpfCertificado = Assinador.BuscaCPFCertificadosValidos(chave);
                            string sVefica = ACSConfig.GetApp().CPFValidateCertificado;

                            if (sVefica == "YES")
                            {
                                if (sCpfCertificado != "" && ((GEDPESSOAS)txtNome.Tag).PES_CPF != sCpfCertificado)
                                {
                                    fInsert = false;
                                    UsuariosInvalidos.Add("Usuário " + sNome + ", possui um CPF diferente ao do Certificado. Impossível fazer a vinculação do Certificado");


                                }
                            }
                        }

                        if (fInsert)
                        {
                            var fVerifica = DataBase.ACSDataBase.UpdateCertificadoUsuario(idPessoa, chave);
                            if (idPessoa == ACSGlobal.UsuarioLogado.USR_IDPESSOA)
                            {
                                ACSGlobal.UsuarioLogado.USR_SERIALNUMBERCERT = chave;
                            }

                        }
                    }


                }
            }

            ////GRID ASSINATURAS





            ACSConfig.GetStorage().Input = tbInput.Text;
            ACSConfig.GetStorage().Output = tbOutput.Text;


            if (rbJPG.Checked)
                ACSConfig.GetImages().Format = ImageFormat.Jpeg;
            if (rbPNG.Checked)
                ACSConfig.GetImages().Format = ImageFormat.Png;
            if (rbTIF.Checked)
                ACSConfig.GetImages().Format = ImageFormat.Tiff;

            ACSConfig.GetImages().Resolution = float.Parse(dudResolution.SelectedItem.ToString());

            ACSConfig.GetBarCodeSettings().MaxLength = int.Parse(tbMaxLength.Text);

            ACSConfig.GetScanner().Driver = cbDrivers.Text;

            if (cbDrivers.Text.Contains("Lexmark"))
            {
                ACSMinCapture.Controls.UCImagesManipulation ucImagem = new Controls.UCImagesManipulation(null);
                ucImagem.btnDuplex.Visible = true;
                ucImagem.btnDuplex.Refresh();
            }
            else
            {
                ACSMinCapture.Controls.UCImagesManipulation ucImagem = new Controls.UCImagesManipulation(null);
                ucImagem.btnDuplex.Visible = false;
                ucImagem.btnDuplex.Refresh();

            }

            if (rbPretoBranco.Checked)
                ACSConfig.GetScanner().ScanAs = 0;

            if (rbTonsCinza.Checked)
                ACSConfig.GetScanner().ScanAs = 1;

            if (rbColorido.Checked)
                ACSConfig.GetScanner().ScanAs = 2;


            ACSConfig.GetImages().Brightness = (int)tbBrightness.Value;
            ACSConfig.GetImages().BrightnessReload = (int)tbBrightness.Value;
            ACSConfig.GetImages().Contrast = (int)tbContrast.Value;

            ACSConfig.GetNetworkAccesser().Valid = cbAutentica.Checked;
            ACSConfig.GetNetworkAccesser().Domain = tbDominio.Text;
            ACSConfig.GetNetworkAccesser().User = tbUsuario.Text;
            ACSConfig.GetNetworkAccesser().Password = tbSenha.Text;
            if (UsuariosInvalidos.Count > 0)
            {

                foreach (var item in UsuariosInvalidos)
                {
                    WFMessageBox.Show(item, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;

            }
            return true;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (SaveSettings())
                this.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveSettings();
            }
            catch (Exception ex)
            {
                WFMessageBox.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void tbBrightness_ValueChanged(object sender, EventArgs e)
        {
            nudBrightness.Value = (sender as TrackBar).Value;
            ACSConfig.GetImages().Brightness = (float)nudBrightness.Value;


        }

        private void tbContrast_ValueChanged(object sender, EventArgs e)
        {
            nudContrast.Value = (sender as TrackBar).Value;
            ACSConfig.GetImages().Contrast = (float)nudContrast.Value;


        }

        private void nudBrightness_ValueChanged(object sender, EventArgs e)
        {
            tbBrightness.Value = (int)(sender as NumericUpDown).Value;
        }

        private void nudContrast_ValueChanged(object sender, EventArgs e)
        {
            tbContrast.Value = (int)(sender as NumericUpDown).Value;
        }

        private void cbAutentica_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender as CheckBox).Checked)
            {
                tbDominio.Clear();
                tbUsuario.Clear();
                tbSenha.Clear();
            }
        }

        private void WFPreferencias_Load(object sender, EventArgs e)
        {
            tbInput.Text = ACSConfig.GetStorage().Input;
            tbOutput.Text = ACSConfig.GetStorage().Output;

            rbJPG.Checked = ACSConfig.GetImages().Format == ImageFormat.Jpeg;
            rbPNG.Checked = ACSConfig.GetImages().Format == ImageFormat.Png;
            rbTIF.Checked = ACSConfig.GetImages().Format == ImageFormat.Tiff;

            dudResolution.SelectedIndex = dudResolution.Items.IndexOf(ACSConfig.GetImages().Resolution.ToString());

            tbMaxLength.Text = ACSConfig.GetBarCodeSettings().MaxLength.ToString();

            try
            {

                using (var tw = new Twain(this.Handle))
                {

                    this.cbDrivers.Items.Clear();

                    foreach (var device in tw.GetDevices())
                    {
                        this.cbDrivers.Items.Add(device.ProductName);
                    }

                    cbDrivers.Text = ACSConfig.GetScanner().Driver;
                }

            }
            catch (Exception)
            {

                WFMessageBox.Show("Reinicie a aplicação para realizar alguma alteração!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            switch (ACSConfig.GetScanner().ScanAs)
            {
                case 0:
                    {
                        rbPretoBranco.Checked = true;
                        break;
                    }
                case 1:
                    {
                        rbTonsCinza.Checked = true;
                        break;
                    }
                case 2:
                    {
                        rbColorido.Checked = true;
                        break;
                    }
            }

            nudBrightness.Maximum = tbBrightness.Maximum;
            nudBrightness.Minimum = tbBrightness.Minimum;

            nudBrightnessReload.Maximum = tbBrightnessReload.Maximum;
            nudBrightnessReload.Minimum = tbBrightnessReload.Minimum;

            nudContrast.Maximum = tbContrast.Maximum;
            nudContrast.Minimum = tbContrast.Minimum;

            tbBrightness.Value = (int)ACSConfig.GetImages().Brightness;
            tbBrightnessReload.Value = (int)ACSConfig.GetImages().BrightnessReload;
            tbContrast.Value = (int)ACSConfig.GetImages().Contrast;

            nudBrightness.Value = tbBrightness.Value;
            nudBrightnessReload.Value = tbBrightnessReload.Value;
            nudContrast.Value = tbContrast.Value;

            cbAutentica.Checked = ACSConfig.GetNetworkAccesser().Valid;
            tbDominio.Text = ACSConfig.GetNetworkAccesser().Domain;
            tbUsuario.Text = ACSConfig.GetNetworkAccesser().User;
            tbSenha.Text = ACSConfig.GetNetworkAccesser().Password;

            this.imageBox1.GridDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.Client;
            this.imageBox1.Invalidate();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!this.IsScanning())
            {
                WFTranparentLoading.ShowLoading(this.imageBox1);
                SaveSettings();
                this.Scan(1);
            }
        }

        private void WFPreferencias_TransferPictureEvent(ref Bitmap bitmap)
        {
            if (this.imageBox1.Image != null)
                this.imageBox1.Image.Dispose();

            this.imageBox1.Image = bitmap;
            this.imageBox1.ZoomToFit();
            this.imageBox1.Invalidate();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void WFPreferencias_AfterEndingScanEvent(object sender, bool Scanned)
        {
            WFTranparentLoading.CloseLoading();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


        }



        // Retirado da documentação
        public const int CERT_SYSTEM_STORE_CURRENT_USER = 0;



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 4)
            {

            }
        }

        public void removeRows()
        {
            dataGridView1.Rows.Clear();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            removeRows();

            WFTranparentLoading.ShowLoading(Program.MainForm);



            List<Certificado> ListCertificados = Assinador.BuscaCertificadosValidos();

            dataGridView1.AllowUserToAddRows = false;

            string sNome = txtFilterName.Text;
            txtFilterName.Text = "";

            ListaPessoas = DataBase.ACSDataBase.GetAllGEDPessoasLike(sNome);

            List<string> ListaCertificadosUsuario = new List<string>();


            foreach (var item in ListaPessoas)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Height = 30;

                row.CreateCells(dataGridView1);
                row.Cells[0].Value = item.PES_NOME;
                row.Cells[0].Tag = item;

                foreach (var certs in item.GEDUSUARIOS)
                {
                    ListaCertificadosUsuario.Add(certs.USR_SERIALNUMBERCERT);
                }

                dataGridView1.Rows.Add(row);
            }

            int iPosition = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewTextBoxCell txtNome = (DataGridViewTextBoxCell)(row.Cells[0]);
                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)(row.Cells[1]);
                cell.DataSource = ListCertificados;

                cell.DisplayMember = "Nome";
                cell.ValueMember = "Chave";

                string certificadoRow = ListaCertificadosUsuario[iPosition];
                bool fVerificaCertificado = false;

                foreach (var item in ListCertificados)
                {

                    if (item.Chave == ListaCertificadosUsuario[iPosition])
                        fVerificaCertificado = true;
                }

                if (fVerificaCertificado)
                    cell.Value = certificadoRow;

                iPosition++;
            }

            WFTranparentLoading.CloseLoading();
        }

       
         
        private void tbBrightnessReload_ValueChanged(object sender, EventArgs e)
        {

            nudBrightnessReload.Value = (sender as TrackBar).Value;
            ACSConfig.GetImages().BrightnessReload = (float)nudBrightnessReload.Value;
        }

        
        private void nudBrightnessReload_ValueChanged(object sender, EventArgs e)
        {
            tbBrightnessReload.Value = (int)(sender as NumericUpDown).Value;
        }


    }

}
