using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using ACSMinCapture.DataBase;
using ACSMinCapture.DataBase.Model;
using ACSMinCapture.Global;
using ACSMinCapture.Config;
using ACSMinCapture.Auxiliar;
using ACSMinCapture.DataBase.ModelOracle;
using ACSMinCapture.Forms;
using System.IO;

namespace ACSMinCapture
{

	public partial class WFWorkList : WFMDIChild
	{

		public WFWorkList()
		{
			InitializeComponent();
			this.WindowState = FormWindowState.Maximized;
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		int GetTipoPessoaSetTipoBusca(bool SetDefault)
		{
			var result = 0;
			foreach (RadioButton r in flpTipoPessoa.Controls)
			{
				if (r.Checked)
				{
					result = int.Parse(r.Tag.ToString());
					break;
				}
			}

			if (!SetDefault)
				return result;

			flpTipos.Controls.Clear();
			switch (result)
			{
				case 0:
					var Lotes = ACSDataBase.GetGEDTiposBuscaLote();

					foreach (var PF in Lotes)
					{
						var r = new RadioButton();

						r.Text = PF.TBL_DESCRICAO;
						r.Tag = PF;

						if (r.Text == "Número Atendimento")
						{
							r.Width = 200;
							flpTipos.Controls.Add(r);
							r.Checked = (PF.TBL_DEFAULT == 0) ? false : true;
						}

					}
					break;

				case 1:
					var LotesPJ = ACSDataBase.GetGEDTiposBuscaLotePJ();

					foreach (var PJ in LotesPJ)
					{
						var r = new RadioButton();

						r.Text = PJ.TPJ_DESCRICAO;
						r.Tag = PJ;
						flpTipos.Controls.Add(r);

						r.Checked = PJ.TPJ_DEFAULT.HasValue;

					}
					break;

			}

			return result;
		}

		[STAThread]
		private void button1_Click(object sender, EventArgs e)
		{
			WFLoading.ShowLoad(true, "Aguarde um momento", "Pesquisando Atendimento");

			btnPesquisa.Enabled = false;

			lbTotalRegistros.Text = string.Empty;

			var Tipo = 0;
			var TipoPessoa = 0;

			if (this.rbPF.Checked)
				TipoPessoa = 0;
			else
				TipoPessoa = 1;

			try
			{
				GetTipoPessoaSetTipoBusca(false);
			}
			catch (Exception ex)
			{
				WFMessageBox.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnPesquisa.Enabled = true;
				return;
			}

			switch (TipoPessoa)
			{

				case 0:
					foreach (RadioButton r in flpTipos.Controls)
					{
						if (r.Checked)
						{
							Tipo = (int)(r.Tag as GEDTIPOSBUSCALOTES).TBL_ID;
							break;
						}
					}
					break;

				case 1:
					foreach (RadioButton r in flpTipos.Controls)
					{
						if (r.Checked)
						{
							Tipo = (int)(r.Tag as GEDTIPOSBUSCALOTESPJ).TPJ_ID;
							break;
						}
					}
					break;
			}

			try
			{

				if (Tipo == 0)
				{
					btnPesquisa.Enabled = true;
					throw new Exception("Selecione Tipo de Busca!");
				}

				if (Tipo == 1)
					this.tbValue.Clear();

				if ((Tipo > 1) && (tbValue.Text.Trim() == string.Empty) && (ACSConfig.SystemAction != ModeSystem.Process))
				{
					WFMessageBox.Show("Informe um valor para pesquisa!", MessageBoxButtons.OK, MessageBoxIcon.Error);
					tbValue.Focus();
					btnPesquisa.Enabled = true;
					WFLoading.CloseLoad();
					return;
				}

				var IdStatusLote = 0;
				if (ACSConfig.SystemAction == ModeSystem.Process)
					IdStatusLote = (int)StatusLote.Capturado;

				var lotes = ACSDataBase.GetLotes(TipoPessoa, Tipo, tbValue.Text.Trim(), dateTimePicker1.Value.Date, dateTimePicker2.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59), IdStatusLote).ToList();

				var count = lotes.Count();

				if (count <= 0)
				{

					//pesquisar no Tasy rest
					AtendimentoTasy atendimentoTasy = new AtendimentoTasy();
					AtendimentoResponse objTasy1 = atendimentoTasy.GetAtendimento(tbValue.Text.Trim());


					if (objTasy1 == null)
					{
						WFLoading.CloseLoad();
						btnPesquisa.Enabled = true;
						throw new Exception("Nenhum registro encontrado!");
					}
					else
					{
						if (string.IsNullOrEmpty(objTasy1.dataNascimento))
						{
							WFLoading.CloseLoad();
							btnPesquisa.Enabled = true;
							throw new Exception("Impossível Prosseguir, Paciente não possui Data de Nascimento cadastrado no TASY!");
						}
						else if (string.IsNullOrEmpty(objTasy1.dataHoraAtendimento))
						{
							WFLoading.CloseLoad();
							btnPesquisa.Enabled = true;
							throw new Exception("Impossível Prosseguir, Paciente não possui Data de Atendimento cadastrado no TASY!");
						}
						else
						{



							btnPesquisa.Enabled = false;

							//------------------------------------------------CLIENTE PF------------------------------------------------------//

							///PROCURA CLIENTE PELO CPF
							GEDCLIENTEPF cliente = ACSDataBase.GetClientePF(objTasy1.cpf, objTasy1.registroProntuario);

							//CASO NAO ENCONTRE, PASSAR PELO NOVO METODO PROCURANDO PELOS PRONTUARIOS UNIFICADOS
							if (cliente == null)
								cliente = ACSDataBase.GetClientePFRegistros(objTasy1.registroProntuario, objTasy1.registroProntuarioHist);

							//CASO NAO ACHE DE NENHUMA FORMA ACIMA, O CLIENTE NÃO EXISTE NA BASE. CRIAR NOVO CLIENTEPF
							if (cliente == null)
							{

								if (string.IsNullOrEmpty(objTasy1.dataHoraCadastroPaciente))
									objTasy1.dataHoraCadastroPaciente = DateTime.Now.ToString();

								GEDCLIENTEPF clientePF = new GEDCLIENTEPF()
								{
									CPF_IDCLIENTEPF = 0,
									CPF_REGISTRO = objTasy1.registroProntuario,
									CPF_REGISTROOLD = (objTasy1.registroProntuarioHist == "0") ? "" : objTasy1.registroProntuarioHist,
									CPF_NOME = objTasy1.nome,
									CPF_DATANASCIMENTO = Convert.ToDateTime(objTasy1.dataNascimento),
									CPF_CPF = objTasy1.cpf,
									CPF_DATACADASTRO = Convert.ToDateTime(objTasy1.dataHoraCadastroPaciente),
									CPF_NOMEPAI = "",
									CPF_NOMEMAE = "",
									CPF_RG = "",
									CPF_IDSEXO = 3,
									CPF_IDUNIDADE = objTasy1.idEstabelecimento,
									CPF_TEL01 = "",
									CPF_TEL02 = "",
									CPF_CONTATO = "",
									CPF_EMAIL = "",
									CPF_OBSERVACAO = "",
									CPF_FLAGATIVO = 1,
									CPF_IDTIPOCONSELHO = 0,
									CPF_NUMEROCONSELHO = "",
									CPF_FLAGCORPOCLINICO = 0,
									CPF_FLAGATUALIZARUNIFICACAO = 0


								};

								clientePF = ACSDataBase.InsertGEDClientePF(clientePF);

							}
							else
							{
								//nova regra... CASO EXISTA
								// verificar se o prontuario esta diferente do paciente, se estiver, atualizar a clientePF com o novo registro. PODE TER ENCONTRADO PELO CPF OU PELOS REGISTROS ANTIGOS



								if (cliente.CPF_REGISTRO != objTasy1.registroProntuario)
								{

									var registroOLDNow = cliente.CPF_REGISTRO;

									cliente.CPF_IDCLIENTEPF = cliente.CPF_IDCLIENTEPF;
									cliente.CPF_REGISTRO = objTasy1.registroProntuario;
									cliente.CPF_REGISTROOLD = cliente.CPF_REGISTROOLD + "," + registroOLDNow;
									cliente.CPF_NOME = cliente.CPF_NOME;

									//seta flag unificado para ROBO web unificar os atendimentosmessagem
									cliente.CPF_FLAGATUALIZARUNIFICACAO = 1;

									cliente = ACSDataBase.UpdateGEDClientePF(cliente);


								}
							}
							//------------------------------------------------CLIENTE PF FIM------------------------------------------------------//



							//------------------------------------------------PASSAGEM----------------------------------------------------------//

							//////REGRA NOVA - VERIFICAR SE A PASSAGEM JA EXISTE PELO NUMERO DE ATENDIMENTO
							GEDPASSAGENS passagens = new GEDPASSAGENS();
							passagens = ACSDataBase.GetPassagemByCodPassagem(objTasy1.numeroAtendimento);

							//CASO NAO EXISTA A PASSAGEM COM O NUMERO DE ATENDIMENTO, CRIAR UMA NOVA
							if (passagens == null)
							{

								passagens = new GEDPASSAGENS()
								{
									PAS_IDUNIDADE = objTasy1.idEstabelecimento,
									PAS_IDCONVENIO = 0,
									PAS_DATAHORAPASSAGEM = Convert.ToDateTime(objTasy1.dataHoraAtendimento),
									PAS_CODIGOPASSAGEM = objTasy1.numeroAtendimento,
									PAS_REGISTRO = objTasy1.registroProntuario,
									PAS_REGISTROOLD = objTasy1.registroProntuarioHist,
									PAS_FLAGCLIENTEPF = 1,
									PAS_DATAHORAPASSAGEMFIM = new DateTime(1900, 01, 01),
									PAS_TIPOATENDIMENTO = string.IsNullOrEmpty(objTasy1.tipoAtendimento) ? " - " : objTasy1.tipoAtendimento
								};

								passagens = ACSDataBase.InsertGEDPassagem(passagens);

								if (passagens.PAS_IDPASSAGEM == 0)
								{
									passagens = ACSDataBase.GetPassagemByCodPassagem(passagens.PAS_CODIGOPASSAGEM);
								}

							}
							else
							{

								//nova regra...
								// verificar se o prontuario esta diferente do paciente, se estiver, atualizar a passagem com o novo registro
								if (passagens.PAS_REGISTRO != cliente.CPF_REGISTRO)
								{
									var registroOLDNow = passagens.PAS_REGISTRO;
									passagens.PAS_REGISTRO = cliente.CPF_REGISTRO;
									passagens.PAS_REGISTROOLD = passagens.PAS_REGISTROOLD + "," + registroOLDNow;

									passagens = ACSDataBase.UpdateGEDPassagem(passagens);



									// leva as imagens da passagem/registro anterior para a nova pasta/Registro

									var OutputIni = ACSConfig.GetStorage().Output;
									var OldFolderReg = OutputIni + "\\" + registroOLDNow;


									string registro = registroOLDNow;
									string newReg = cliente.CPF_REGISTRO;
									string passagem = passagens.PAS_CODIGOPASSAGEM;// "111111";



									var newFolderReg = OutputIni + "\\" + newReg;

									foreach (var firstFolder in Directory.GetDirectories(OldFolderReg))
									{
										var splitName = firstFolder.Split('\\');

										var folderN = splitName[splitName.Length - 1].ToString();
										var strDiv = folderN.Substring(folderN.Length - 2, 2);
										int parseInt;

										string folderCreate = newFolderReg;

										if (!int.TryParse(strDiv, out parseInt))
										{
											folderCreate += "\\" + newReg + strDiv;
										}
										else
										{
											folderCreate += "\\" + newReg;
										}


										if (splitName[splitName.Length - 1].ToString().Contains(registro))
										{
											var OldFolderRegSecondFolder = firstFolder;

											foreach (var secondFolder in Directory.GetDirectories(OldFolderRegSecondFolder))
											{
												var splitNameAtendimento = secondFolder.Split('\\');

												//verifica se a pasta é a mesma do atendimento mencionando na pesquisa
												if (splitNameAtendimento[splitNameAtendimento.Length - 1].ToString() == passagem)
												{


													//caso seja o atendimento, copiar para a nova pasta

													folderCreate += "\\" + splitNameAtendimento[splitNameAtendimento.Length - 1].ToString();
													if (!Directory.Exists(folderCreate))
													{
														Directory.CreateDirectory(folderCreate);
													}


													DirectoryInfo dir = new DirectoryInfo(secondFolder);
													FileInfo[] files = dir.GetFiles();
													foreach (FileInfo file in files)
													{
														string temppath = Path.Combine(folderCreate, file.Name);
														file.CopyTo(temppath, true);
													}


												}

											}

										}
									}




								}
							}

							var maxorder = 0;
							var inclusao = 0;
							var lotesMerge = ACSDataBase.GetLotes(TipoPessoa, Tipo, passagens.PAS_CODIGOPASSAGEM, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59), IdStatusLote).FirstOrDefault();

							if (lotesMerge != null)
							{
								inclusao = lotesMerge.INCLUSAO;
								maxorder = lotesMerge.MAX_ORDER;
							}

							GED_PROC_F_Lotes_Result loteTasy = new GED_PROC_F_Lotes_Result()
							{

								CPF_CNPJ = objTasy1.cpf,
								NOME = objTasy1.nome,
								CPF_CNPJ_FLAGATIVO = true,
								INCLUSAO = inclusao,
								PAS_CODIGOPASSAGEM = objTasy1.numeroAtendimento,
								PAS_DATAHORAPASSAGEM = Convert.ToDateTime(objTasy1.dataHoraAtendimento),
								TIPO_ATENDIMENTO = objTasy1.tipoAtendimento,
								NRO_PRONTUARIO = objTasy1.registroProntuario,
								DATA_NASCIMENTO = Convert.ToDateTime(objTasy1.dataNascimento),
								PAS_REGISTRO = objTasy1.registroProntuario,
								PAS_IDPASSAGEM = (int)passagens.PAS_IDPASSAGEM,
								MAX_ORDER = maxorder

							};


							List<GED_PROC_F_Lotes_Result> lista1 = new List<GED_PROC_F_Lotes_Result>();
							lista1.Add(loteTasy);

							lbTotalRegistros.Text = "Total de Registros: 0";




							dataGridView1.Font = new System.Drawing.Font("Tahoma", 12);
							dataGridView1.DataSource = lista1;

							foreach (DataGridViewColumn c in dataGridView1.Columns)
							{
								c.Visible = false;
								c.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
							}

							dataGridView1.Columns["PAS_DATAHORAPASSAGEM"].HeaderText = "Data";
							dataGridView1.Columns["PAS_DATAHORAPASSAGEM"].Visible = true;

							dataGridView1.Columns["PAS_CODIGOPASSAGEM"].HeaderText = "Atendimento";
							dataGridView1.Columns["PAS_CODIGOPASSAGEM"].Visible = true;

							dataGridView1.Columns["TIPO_ATENDIMENTO"].HeaderText = "Tipo de Atendimento";
							dataGridView1.Columns["TIPO_ATENDIMENTO"].Visible = true;

							dataGridView1.Columns["NOME"].HeaderText = "Nome";
							dataGridView1.Columns["NOME"].Visible = true;

							dataGridView1.Columns["DATA_NASCIMENTO"].HeaderText = "Data de Nascimento";
							dataGridView1.Columns["DATA_NASCIMENTO"].Visible = true;

							dataGridView1.Columns["CPF_CNPJ"].HeaderText = "CPF";
							dataGridView1.Columns["CPF_CNPJ"].Visible = true;

							dataGridView1.Columns["NRO_PRONTUARIO"].HeaderText = "Prontuário";
							dataGridView1.Columns["NRO_PRONTUARIO"].Visible = true;

							dataGridView1.Columns["INCLUSAO"].HeaderText = "Quantidade de Imagens";
							dataGridView1.Columns["INCLUSAO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
							dataGridView1.Columns["INCLUSAO"].Visible = true;


						}
					}
					WFLoading.CloseLoad();
					btnPesquisa.Enabled = true;

				}
				else
				{

					lbTotalRegistros.Text = "Total de Registros: " + count.ToString();

					dataGridView1.Font = new System.Drawing.Font("Tahoma", 12);
					dataGridView1.DataSource = lotes;


					//3
					// criar passagem

					foreach (DataGridViewColumn c in dataGridView1.Columns)
					{
						c.Visible = false;
						c.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
					}

					dataGridView1.Columns["PAS_DATAHORAPASSAGEM"].HeaderText = "Data";
					dataGridView1.Columns["PAS_DATAHORAPASSAGEM"].Visible = true;

					dataGridView1.Columns["PAS_CODIGOPASSAGEM"].HeaderText = "Atendimento";
					dataGridView1.Columns["PAS_CODIGOPASSAGEM"].Visible = true;

					dataGridView1.Columns["TIPO_ATENDIMENTO"].HeaderText = "Tipo de Atendimento";
					dataGridView1.Columns["TIPO_ATENDIMENTO"].Visible = true;

					dataGridView1.Columns["NOME"].HeaderText = "Nome";
					dataGridView1.Columns["NOME"].Visible = true;

					if (TipoPessoa == 0)
					{
						dataGridView1.Columns["DATA_NASCIMENTO"].HeaderText = "Data de Nascimento";
						dataGridView1.Columns["DATA_NASCIMENTO"].Visible = true;

						dataGridView1.Columns["NRO_PRONTUARIO"].HeaderText = "Prontuário";
						dataGridView1.Columns["NRO_PRONTUARIO"].Visible = true;
					}

					switch (TipoPessoa)
					{
						case 0:
							dataGridView1.Columns["CPF_CNPJ"].HeaderText = "CPF";
							break;
						case 1:
							dataGridView1.Columns["CPF_CNPJ"].HeaderText = "CNPJ";
							break;
					}

					dataGridView1.Columns["CPF_CNPJ"].Visible = true;
					dataGridView1.Columns["INCLUSAO"].HeaderText = "Quantidade de Imagens";
					dataGridView1.Columns["INCLUSAO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
					dataGridView1.Columns["INCLUSAO"].Visible = true;

					WFLoading.CloseLoad();
					btnPesquisa.Enabled = true;
				}


			}
			catch (Exception d)
			{
				WFMessageBox.Show(d.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnPesquisa.Enabled = true;
				dataGridView1.DataSource = null;
			}

			dataGridView1.Invalidate();

		}

		//private static void InsertNewClientePF(AtendimentoResponse atendimento)
		//{


		//    //GEDCLIENTEPF cliente = ACSDataBase.GetClientePF("318339489810", "383239489810");
		//    atendimento = new AtendimentoResponse
		//           {
		//               nome = "Lucas Augusto ",
		//               registroProntuario = "1234567",
		//               idPessoa = 0,
		//               numeroAtendimento = "12345678",
		//               dataNascimento = "2000-08-01",
		//               cpf = "09560799633",
		//               dataHoraCadastroPaciente = "2014-07-25 10:11:12",
		//               tipoAtendimento = "Exame",
		//               dataHoraAtendimento = "2015-08-25 10:11:12"
		//           };

		//    GEDCLIENTEPF clientePF = new GEDCLIENTEPF()
		//    {
		//        CPF_IDCLIENTEPF = 0,
		//        CPF_REGISTRO = atendimento.registroProntuario,
		//        CPF_NOME = atendimento.nome,
		//        CPF_DATANASCIMENTO = Convert.ToDateTime(atendimento.dataNascimento),
		//        CPF_CPF = atendimento.cpf,
		//        CPF_DATACADASTRO = Convert.ToDateTime(atendimento.dataHoraCadastroPaciente),
		//        CPF_NOMEPAI = "",
		//        CPF_NOMEMAE = "",
		//        CPF_RG = "",
		//        CPF_IDSEXO = 3,
		//        CPF_IDUNIDADE = 1,
		//        CPF_TEL01 = "",
		//        CPF_TEL02 = "",
		//        CPF_CONTATO = "",
		//        CPF_EMAIL = "",
		//        CPF_OBSERVACAO = "",
		//        CPF_FLAGATIVO = 1,
		//        CPF_IDTIPOCONSELHO = 0,
		//        CPF_NUMEROCONSELHO = "",
		//        CPF_FLAGCORPOCLINICO = 0
		//    };

		//    clientePF = ACSDataBase.InsertGEDClientePF(clientePF);


		//    GEDPASSAGENS passagens = new GEDPASSAGENS()
		//    {
		//        PAS_IDPASSAGEM = 0,
		//        PAS_IDUNIDADE = 1,
		//        PAS_IDCONVENIO = 0,
		//        PAS_DATAHORAPASSAGEM = Convert.ToDateTime(atendimento.dataHoraAtendimento),
		//        PAS_CODIGOPASSAGEM = atendimento.numeroAtendimento,
		//        PAS_REGISTRO = atendimento.registroProntuario,
		//        PAS_FLAGCLIENTEPF = 1,
		//        PAS_DATAHORAPASSAGEMFIM = new DateTime(1900, 01, 01)
		//    };

		//    passagens = ACSDataBase.InsertGEDPassagem(passagens);




		//}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				var d = dataGridView1.CurrentRow;

				if (d == null)
					throw new Exception("Selecione um registro!");

				var v = (int)d.Cells["PAS_IDPASSAGEM"].Value;

				ACSGlobal.LoteSelecionado = (GED_PROC_F_Lotes_Result)d.DataBoundItem;

				if (ACSGlobal.LoteSelecionado != null)
					this.Close();
				else
					throw new Exception("Selecione um registro!");
			}
			catch (Exception x)
			{
				WFMessageBox.Show(x.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);

			}
		}

		private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			btnOk.PerformClick();
		}

		private void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
				btnPesquisa.PerformClick();
		}

		private void dataGridView1_DoubleClick(object sender, EventArgs e)
		{
			btnOk.PerformClick();
		}

		private void WFWorkList_Load(object sender, EventArgs e)
		{
			WFTranparentLoading.CloseLoading();
			try
			{
				GetTipoPessoaSetTipoBusca(true);
			}
			catch (Exception ex)
			{
				WFMessageBox.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
				return;
			}
			tbValue.Focus();
			tbValue.Focus();
		}

		private void rbPF_CheckedChanged(object sender, EventArgs e)
		{
			GetTipoPessoaSetTipoBusca(true);
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
