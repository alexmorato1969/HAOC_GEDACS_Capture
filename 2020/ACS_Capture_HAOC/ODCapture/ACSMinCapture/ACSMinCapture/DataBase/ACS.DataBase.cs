using ACSMinCapture.Config;
using ACSMinCapture.DataBase.Model;
using ACSMinCapture.Log;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;
using System.Diagnostics;
using ACSMinCapture.Global;
using System.Net;
using ACSMinCapture.Forms;
using System.Threading.Tasks;
using System.Data;
using ACSMinCapture.DataBase.ModelOracle;
using System.Globalization;
using System.Data.Objects.SqlClient;
using ACSMinCapture.Auxiliar;
using Dapper;
using System.Data.SqlClient;

namespace ACSMinCapture.DataBase
{
	public static partial class ACSDataBase
	{

		static ACSDataBase()
		{

		}

		static string GetMd5Hash(MD5 md5Hash, string input)
		{

			// Convert the input string to a byte array and compute the hash.
			byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));


			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			StringBuilder sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data 
			// and format each one as a hexadecimal string.
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			// Return the hexadecimal string.
			return sBuilder.ToString();
		}

		static void ExceptionDB(Exception e)
		{
			var er = new Exception("Verifique a conexão com banco de dados: \n" + e.Message, e.InnerException);
			WFMessageBox.Show(er.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
			ACSLog.InsertLog(MessageBoxIcon.Error, er);
		}

		static TResult ExecuteCommand<TResult>(Func<object> action, bool ShowLoading = false)
		{
			if (ShowLoading)
				WFTranparentLoading.ShowLoading(Program.MainForm);

			var resultAction = action;

			var result = Task.Factory.StartNew<TResult>(() =>
				{
					try
					{
						return (TResult)resultAction();
					}
					catch (Exception e)
					{
						ExceptionDB(e);
						return default(TResult);
					}
				});

			while (!result.IsCompleted)
				if (!ACSConfig.GetScanner().Driver.ToUpper().Contains("FUJITSU") && !ACSConfig.GetScanner().Driver.ToUpper().Contains("SP-1120"))
					Application.DoEvents();
			if (ShowLoading)
				WFTranparentLoading.CloseLoading();
			return result.Result;

		}

		public static void ExecuteCommand(Action action, bool ShowLoading = false)
		{
			if (ShowLoading)
				WFTranparentLoading.ShowLoading(Program.MainForm);
			try
			{
				var result = Task.Factory.StartNew(() =>
					{
						try
						{
							action();
						}
						catch (DataException e)
						{
							throw e;
						}
						catch (Exception e)
						{
							throw e;
						}
					});
				while (!result.IsCompleted)
					if (!ACSConfig.GetScanner().Driver.ToUpper().Contains("FUJITSU") && !ACSConfig.GetScanner().Driver.ToUpper().Contains("SP-1120"))
						Application.DoEvents();
				WFTranparentLoading.CloseLoading();
			}
			catch (Exception e)
			{
				ExceptionDB(e);
				WFTranparentLoading.CloseLoading();
			}
		}

		public static bool DataBaseActive()
		{
			try
			{
				using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
				{

					DB.Connection.Open();
					DB.Connection.Close();
				}
				return true;
			}
			catch
			{
				throw new Exception("Erro ao conectar no banco de dados!");
			}

		}

		public static GEDLOGLOGIN StartSession(GEDLOGLOGIN gEDLogLogin)
		{
			try
			{
				return ExecuteCommand<GEDLOGLOGIN>(() =>
				{
					using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
					{
						DB.GEDLOGLOGIN.AddObject(gEDLogLogin);
						DB.SaveChanges();
						DB.Refresh(System.Data.Objects.RefreshMode.StoreWins, gEDLogLogin);
						return gEDLogLogin;

					}
				}
			);
			}
			catch (Exception ez)
			{

				throw;
			}

		}

		static string GetIPLocal()
		{
			string host = Dns.GetHostName();
			IPHostEntry ip = Dns.GetHostEntry(host);

			return ip.AddressList.Where(ad => ad.AddressFamily.ToString() == "InterNetwork").First().ToString();
		}



		public static GEDUSUARIOS GetGEDUsuarioOracle(string Login, string Senha)
		{
			return ExecuteCommand<GEDUSUARIOS>(() => getGEDUsuarioOracle(Login, Senha));
		}
		static GEDUSUARIOS getGEDUsuarioOracle(string Login, string Senha)
		{
			using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
			{


				var SenhaMD5 = string.Empty;
				using (var md5Hash = MD5.Create())
				{
					SenhaMD5 = GetMd5Hash(md5Hash, Senha);
				}

				var result = from users in DB.GEDUSUARIOS
							 join pes in DB.GEDPESSOAS on users.USR_IDPESSOA equals pes.PES_IDPESSOA
							 where users.USR_LOGIN.ToUpper() == Login.ToUpper() &&
									   users.USR_SENHA == SenhaMD5
							 select users;

				var newLog = new GEDLOGLOGIN();
				if (result.Count() <= 0)
				{
					Exception e = new Exception("Acesso Negado!", new Exception("Login: " + Login));

					ACSLog.InsertLog(MessageBoxIcon.Error, e, "ACS.DataBase.cs: line 180");
					newLog.LLG_DATAHORALOGIN = DateTime.Now;
					newLog.LLG_DATAHORALOGOUT = DateTime.Now;
					newLog.LLG_FLAGLOGIN = -1;
					newLog.LLG_IDUNIDADE = -1;
					newLog.LLG_IDUSUARIO = -1;
					newLog.LLG_IPESTACAO = GetIPLocal();
					newLog.LLG_OBSERVACAO = "Usuário Inexistente! - Login=" + Login + " Senha=" + SenhaMD5;
					newLog.LLG_TOKEN = string.Empty;
					StartSession(newLog);
					return new GEDUSUARIOS();
				}
				else
				{

					var usr = result.ToList().First();
					ExceptionCustom e = new ExceptionCustom("Acesso Liberado!", new Exception("Login: " + Login));
					ACSLog.InsertLog(MessageBoxIcon.Asterisk, e);

					newLog.LLG_DATAHORALOGIN = DateTime.Now;
					newLog.LLG_DATAHORALOGOUT = null;
					newLog.LLG_FLAGLOGIN = 1;
					newLog.LLG_IDUNIDADE = -1;
					newLog.LLG_IDUSUARIO = usr.USR_IDUSUARIO;
					newLog.LLG_IPESTACAO = GetIPLocal();
					newLog.LLG_OBSERVACAO = "Usuário Logado. - Login=" + Login + " Senha=" + SenhaMD5;
					newLog.LLG_TOKEN = string.Empty;

					var teste = usr.USR_FLAGASSINA;
					decimal aswq = (decimal)teste;


					var temp = DB.GEDGRUPOSUSUARIOS.Where(c => c.GRP_IDGRUPOUSUARIO == usr.USR_IDGRUPOUSUARIO).FirstOrDefault();
					ACSGlobal.GrupoUsuario = DB.GEDGRUPOSUSUARIOS.Where(c => c.GRP_IDGRUPOUSUARIO == usr.USR_IDGRUPOUSUARIO).FirstOrDefault();
					ACSGlobal.NomeLogado = DB.GEDPESSOAS.Where(c => c.PES_IDPESSOA == usr.USR_IDPESSOA).FirstOrDefault().PES_NOME;
					ACSGlobal.SetoresUsuario = (from c in DB.GEDSETOR
												join GSU in DB.GEDSETORXGRUPOUSUARIO on c.SET_IDSETOR equals GSU.SXG_IDSETOR
												where GSU.SXG_IDGRUPOUSUARIO == ACSGlobal.GrupoUsuario.GRP_IDGRUPOUSUARIO
												&& c.SET_FLAGATIVO == 1 && GSU.SXG_FLAGATIVO == 1
												select c.SET_IDSETOR).ToList();

					getGruposSetoresLogado(DB);

					DB.Dispose();


					StartSession(newLog);

					ACSGlobal.Session = newLog;
					return usr;
				}
			}

		}

		private static void getGruposSetoresLogado(EntitiesOracle DB)
		{
			// retornar grupos vinculados aos setores do usuário logado
			if (ACSGlobal.SetoresUsuario != null && ACSGlobal.SetoresUsuario.Count() > 0)
			{
				ACSGlobal.ListaGruposSetoresLogado = (from c in DB.GEDSETORXGRUPOUSUARIO
													  where ACSGlobal.SetoresUsuario.Contains((decimal)c.SXG_IDSETOR)
													   && c.SXG_FLAGATIVO == 1
													  select c.SXG_IDGRUPOUSUARIO).ToList().GroupBy(g => g).Select(grp => grp.FirstOrDefault()).ToList();
			}
		}


		public static GEDUSUARIOS GetGEDUsuarioOracleTasy(string Login)
		{
			return ExecuteCommand<GEDUSUARIOS>(() => getGEDUsuarioOracleTasy(Login));
		}
		static GEDUSUARIOS getGEDUsuarioOracleTasy(string Login)
		{
			using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
			{

				var result = from users in DB.GEDUSUARIOS
							 join pes in DB.GEDPESSOAS on users.USR_IDPESSOA equals pes.PES_IDPESSOA
							 where users.USR_LOGIN.ToUpper() == Login.ToUpper()
							 select users;

				var newLog = new GEDLOGLOGIN();
				if (result.Count() <= 0)
				{
					Exception e = new Exception("Acesso Negado!", new Exception("Login Tasy: " + Login));

					ACSLog.InsertLog(MessageBoxIcon.Error, e, "ACS.DataBase.cs: line 180");
					newLog.LLG_DATAHORALOGIN = DateTime.Now;
					newLog.LLG_DATAHORALOGOUT = DateTime.Now;
					newLog.LLG_FLAGLOGIN = -1;
					newLog.LLG_IDUNIDADE = -1;
					newLog.LLG_IDUSUARIO = -1;
					newLog.LLG_IPESTACAO = GetIPLocal();
					newLog.LLG_OBSERVACAO = "Usuário Inexistente! - " + Login;
					newLog.LLG_TOKEN = string.Empty;
					StartSession(newLog);
					return null;
				}
				else
				{

					var usr = result.ToList().First();
					ExceptionCustom e = new ExceptionCustom("Acesso Liberado!", new Exception("Login Tasy: " + Login));
					ACSLog.InsertLog(MessageBoxIcon.Asterisk, e);

					newLog.LLG_DATAHORALOGIN = DateTime.Now;
					newLog.LLG_DATAHORALOGOUT = null;
					newLog.LLG_FLAGLOGIN = 1;
					newLog.LLG_IDUNIDADE = -1;
					newLog.LLG_IDUSUARIO = usr.USR_IDUSUARIO;
					newLog.LLG_IPESTACAO = GetIPLocal();
					newLog.LLG_OBSERVACAO = "Usuário Logado. - " + Login;
					newLog.LLG_TOKEN = string.Empty;



					var temp = DB.GEDGRUPOSUSUARIOS.Where(c => c.GRP_IDGRUPOUSUARIO == usr.USR_IDGRUPOUSUARIO).FirstOrDefault();

					ACSGlobal.GrupoUsuario = DB.GEDGRUPOSUSUARIOS.Where(c => c.GRP_IDGRUPOUSUARIO == usr.USR_IDGRUPOUSUARIO).FirstOrDefault();

					ACSGlobal.NomeLogado = DB.GEDPESSOAS.Where(c => c.PES_IDPESSOA == usr.USR_IDPESSOA).FirstOrDefault().PES_NOME;





					ACSGlobal.SetoresUsuario = (from c in DB.GEDSETOR
												join GSU in DB.GEDSETORXGRUPOUSUARIO on c.SET_IDSETOR equals GSU.SXG_IDSETOR
												where GSU.SXG_IDGRUPOUSUARIO == usr.USR_IDGRUPOUSUARIO
												&& c.SET_FLAGATIVO == 1 && GSU.SXG_FLAGATIVO == 1
												select c.SET_IDSETOR).ToList();

					ACSLog.InsertLog(MessageBoxIcon.Asterisk, "ID USUARIO: " + usr.USR_IDUSUARIO.ToString());
					ACSLog.InsertLog(MessageBoxIcon.Asterisk, "Setores: " + ACSGlobal.SetoresUsuario.Count().ToString());

					getGruposSetoresLogado(DB);

					DB.Dispose();


					StartSession(newLog);

					ACSGlobal.Session = newLog;

					return usr;

				}
			}

		}






		//public static GEDUsuarios GetGEDUsuario(string Login, string Senha)
		//{
		//    return ExecuteCommand<GEDUsuarios>(() => getGEDUsuario(Login, Senha));
		//}

		//static GEDUsuarios getGEDUsuario(string Login, string Senha)
		//{
		//    using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
		//    {
		//        var SenhaMD5 = string.Empty;
		//        using (var md5Hash = MD5.Create())
		//        {
		//            SenhaMD5 = GetMd5Hash(md5Hash, Senha);
		//        }

		//        var result = from users in DB.GEDUSUARIOS
		//                     where users.USR_LOGIN == Login &&
		//                           users.USR_SENHA == SenhaMD5
		//                     select users;

		//        var newLog = new GEDLOGLOGIN();
		//        if (result.Count() <= 0)
		//        {
		//            Exception e = new Exception("Acesso Negado!", new Exception("Login: " + Login));

		//            ACSLog.InsertLog(MessageBoxIcon.Error, e, "ACS.DataBase.cs: line 180");
		//            newLog.LLG_DATAHORALOGIN = DateTime.Now;
		//            newLog.LLG_DATAHORALOGOUT = DateTime.Now;
		//            newLog.LLG_FLAGLOGIN = -1;
		//            newLog.LLG_IDUNIDADE = -1;
		//            newLog.LLG_IDUSUARIO = -1;
		//            newLog.LLG_IPESTACAO = GetIPLocal();
		//            newLog.LLG_OBSERVACAO = "Usuário Inexistente!";
		//            newLog.LLG_TOKEN = string.Empty;
		//            StartSession(newLog);
		//            return null;
		//        }
		//        else
		//        {
		//            var usr = result.ToList().First();
		//            ExceptionCustom e = new ExceptionCustom("Acesso Liberado!", new Exception("Login: " + Login));
		//            ACSLog.InsertLog(MessageBoxIcon.Asterisk, e);
		//            newLog.LLG_DATAHORALOGIN = DateTime.Now;
		//            newLog.LLG_DATAHORALOGOUT = null;
		//            newLog.LLG_FLAGLOGIN = 1;
		//            newLog.LLG_IDUNIDADE = -1;
		//            newLog.LLG_IDUSUARIO = usr.USR_IDUSUARIO;
		//            newLog.LLG_IPESTACAO = GetIPLocal();
		//            newLog.LLG_OBSERVACAO = "Usuário Logado.";
		//            newLog.LLG_TOKEN = string.Empty;
		//            StartSession(newLog);

		//            ACSGlobal.Session = newLog;
		//            return usr;
		//        }
		//    }

		//}

		public static void StopSession()
		{
			ExecuteCommand(() =>
				{
					if (ACSGlobal.UsuarioLogado != null)
					{
						using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
						{
							var result = DB.GEDLOGLOGIN.Where(gll => gll.LLG_IDUSUARIO == ACSGlobal.Session.LLG_IDUSUARIO).OrderByDescending(c => c.LLG_IDLOGLOGIN).ToList().FirstOrDefault();
							result.LLG_DATAHORALOGOUT = DateTime.Now;
							result.LLG_FLAGLOGOUT = 1;
							result.LLG_OBSERVACAO = "Usuário Deslogado";
							DB.SaveChanges();
						}
					}
				}
			);
		}

		//public static string consult()
		//{
		//    try
		//    {

		//        string connstring =
		//           "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SID=xe)));User Id=GEDTESTE;Password=1256171; ";





		//        string query = "SELECT object_name, object_type FROM user_objects WHERE object_type IN ('TABLE','VIEW')";
		//        string result = String.Empty;

		//        using (OracleConnection conn = new OracleConnection(connstring))
		//        {


		//            conn.Open();




		//            string teste = "";

		//            using (OracleCommand cmd = new OracleCommand())
		//            {
		//                cmd.Connection = conn;
		//                cmd.CommandText = query;
		//                cmd.CommandType = CommandType.Text;
		//                using (OracleDataReader dr = cmd.ExecuteReader())
		//                {

		//                    while (dr.Read())
		//                    {
		//                        teste += " - " + dr.GetString(0);

		//                    }

		//                }
		//                return teste;
		//            }

		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        return ex.ToString();
		//        throw ex;
		//    }
		//}

		public static bool FormInGEDCaptionControl(string FormName)
		{

			return ExecuteCommand<bool>(() =>
						{

							var exists = false;
							using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
							{
								try
								{

									var result = from t in DB.GEDCAPTIONCONTROL where t.GCC_FORMNAME == FormName select t;
									exists = result.Count() > 0;
									DB.Dispose();
								}
								catch (Exception ex)
								{
									ACSLog.InsertLog(MessageBoxIcon.Error, "2: " + ex.InnerException);
									WFMessageBox.Show(ex.InnerException + " ( GEDCAPTIONCONTROL 1)", MessageBoxButtons.OK, MessageBoxIcon.Error);
								}

							}
							return exists;

						});

		}

		public static string GetControlCaption(string FormName, string ControlName)
		{
			return ExecuteCommand<string>(() =>
					   {
						   var result = string.Empty;
						   using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
						   {
							   try
							   {
								   var qry = from t in DB.GEDCAPTIONCONTROL where t.GCC_FORMNAME == FormName && t.GCC_CONTROLNAME == ControlName select t;
								   if (qry.Count() > 0)
									   result = qry.First().GCC_CAPTION;
								   DB.Dispose();
							   }
							   catch (Exception ex)
							   {
								   ACSLog.InsertLog(MessageBoxIcon.Error, ex, "2: " + ex.Message);
								   WFMessageBox.Show(ex.Message + " ( GEDCAPTIONCONTROL 2)", MessageBoxButtons.OK, MessageBoxIcon.Error);
							   }

						   }
						   return result;
					   });
		}

		public static List<GEDTIPOSBUSCALOTESPJ> GetGEDTiposBuscaLotePJ()
		{
			return ExecuteCommand<List<GEDTIPOSBUSCALOTESPJ>>(() =>
					   {
						   using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
						   {
							   var result = from t in DB.GEDTIPOSBUSCALOTESPJ select t;
							   return result.ToList();
						   }
					   });
		}

		public static List<GEDTIPOSBUSCALOTES> GetGEDTiposBuscaLote()
		{

			return ExecuteCommand<List<GEDTIPOSBUSCALOTES>>(() =>
				{
					using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
					{
						var result = from t in DB.GEDTIPOSBUSCALOTES select t;
						return result.ToList();
					}
				});

		}

		public static List<GED_PROC_F_Lotes_Result> GetLotes(int TipoPessoa, int Tipo, string Value, DateTime Dtini, DateTime DtFin, int IdStatusLote)
		{
			using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
			{

				try
				{

					#region QUERY PROC SQL

					//                    SELECT DISTINCT
					//			PAS_IDPASSAGEM,
					//			PAS_CODIGOPASSAGEM,
					//			PAS_REGISTRO,
					//			CPF_NOME AS NOME,
					//			PAS_DATAHORAPASSAGEM,
					//			CPF_CPF AS CPF_CNPJ,
					//			CPF_FLAGATIVO AS CPF_CNPJ_FLAGATIVO,
					//			ISNULL(MAX(cast(DOC_nomeArquivo as int)) over (partition by DOC_IdPassagem),0) as INCLUSAO,
					//			CASE 
					//				WHEN ISNULL(MAX(DOC_Ordem_Visualizacao) over (partition by DOC_IdPassagem),0) = 0 THEN 
					//					Count(DOC_IdPassagem) over (PARTITION by DOC_IdPassagem +1)
					//				ELSE
					//					ISNULL(MAX(DOC_Ordem_Visualizacao) over (partition by DOC_IdPassagem),0)
					//			END MAX_ORDER 	
					//	 FROM GEDPASSAGENS
					//	 INNER JOIN GEDCLIENTEPF ON (CPF_REGISTRO = PAS_REGISTRO)
					//	 LEFT JOIN GEDDocumentos ON (DOC_idPassagem = PAS_idPassagem)
					//	 WHERE (@Tipo = 2 and Pas_Registro  like '%' + @Value + '%') 
					//		OR (@Tipo = 3 and PAS_codigoPassagem  like '%' + @Value + '%')
					//		OR (@Tipo = 1 and  PAS_DATAHORAPASSAGEM BETWEEN @DtIni AND @DtFin)
					//
					#endregion


					if (TipoPessoa == 0)
					{
						#region PESSOA FISICA


						var resultadoPF = (from Gpass in DB.GEDPASSAGENS
										   join Gcli in DB.GEDCLIENTEPF on Gpass.PAS_REGISTRO equals Gcli.CPF_REGISTRO
										   join Gdoc in DB.GEDDOCUMENTOS on Gpass.PAS_IDPASSAGEM equals Gdoc.DOC_IDPASSAGEM into _Gdoc
										   from Gdoc in _Gdoc.DefaultIfEmpty()

											   //where
										   where

											  (Tipo == 1 && Gpass.PAS_DATAHORAPASSAGEM >= Dtini && Gpass.PAS_DATAHORAPASSAGEM <= DtFin)
										   || (Tipo == 2 && Gpass.PAS_REGISTRO == Value)
										   || (Tipo == 3 && Gpass.PAS_CODIGOPASSAGEM == Value)

										   //ORDER
										   orderby Gdoc.DOC_NOMEARQUIVO descending

										   select new
										   {
											   PAS_IDPASSAGEM = (int)Gpass.PAS_IDPASSAGEM,
											   PAS_CODIGOPASSAGEM = Gpass.PAS_CODIGOPASSAGEM,
											   PAS_REGISTRO = Gpass.PAS_REGISTRO,
											   NOME = Gcli.CPF_NOME,
											   PAS_DATAHORAPASSAGEM = Gpass.PAS_DATAHORAPASSAGEM,
											   CPF_CNPJ = Gcli.CPF_CPF,
											   CPF_CNPJ_FLAGATIVO = (Gcli.CPF_FLAGATIVO == 1) ? true : false,
											   INCLUSAO = (Gdoc.DOC_NOMEARQUIVO != null || Gdoc.DOC_NOMEARQUIVO != "") ? Gdoc.DOC_NOMEARQUIVO : "0",
											   MAX_ORDER = (!string.IsNullOrEmpty(Gdoc.DOC_NOMEARQUIVO)) ? Gdoc.DOC_NOMEARQUIVO : "0",
											   DIRLOTEINBOX = "",
											   LTU_PathImagens = "",
											   NRO_PRONTUARIO = Gcli.CPF_REGISTRO,
											   DATA_NASCIMENTO = Gcli.CPF_DATANASCIMENTO,
											   TIPO_ATENDIMENTO = Gpass.PAS_TIPOATENDIMENTO
										   });


						List<GED_PROC_F_Lotes_Result> ListaPF = new List<GED_PROC_F_Lotes_Result>();
						foreach (var item in resultadoPF)
						{
							ListaPF.Add(new GED_PROC_F_Lotes_Result()
							{
								CPF_CNPJ_FLAGATIVO = item.CPF_CNPJ_FLAGATIVO,
								CPF_CNPJ = item.CPF_CNPJ,
								DIRLOTEINBOX = item.DIRLOTEINBOX,
								INCLUSAO = int.Parse(item.INCLUSAO),
								LTU_PathImagens = item.LTU_PathImagens,
								MAX_ORDER = int.Parse(item.MAX_ORDER),
								NOME = item.NOME,
								PAS_CODIGOPASSAGEM = item.PAS_CODIGOPASSAGEM,
								PAS_DATAHORAPASSAGEM = item.PAS_DATAHORAPASSAGEM,
								PAS_IDPASSAGEM = item.PAS_IDPASSAGEM,
								PAS_REGISTRO = item.PAS_REGISTRO,
								NRO_PRONTUARIO = item.NRO_PRONTUARIO,
								DATA_NASCIMENTO = item.DATA_NASCIMENTO,
								TIPO_ATENDIMENTO = item.TIPO_ATENDIMENTO

							});

							if (Tipo > 1)
							{ break; }
						}



						return ListaPF;
						#endregion
					}
					else
					{
						#region PESSOA JURIDICA
						var resultadoPJ = (from Gpass in DB.GEDPASSAGENS
										   join Gcli in DB.GEDCLIENTEPJ on Gpass.PAS_REGISTRO equals Gcli.CPJ_REGISTRO
										   join Gdoc in DB.GEDDOCUMENTOS on Gpass.PAS_IDPASSAGEM equals Gdoc.DOC_IDPASSAGEM into _Gdoc
										   from Gdoc in _Gdoc.DefaultIfEmpty()

											   //where
										   where

											  (Tipo == 1 && Gpass.PAS_DATAHORAPASSAGEM >= Dtini && Gpass.PAS_DATAHORAPASSAGEM <= DtFin)
										   || (Tipo == 2 && Gpass.PAS_REGISTRO == Value)
										   || (Tipo == 3 && Gpass.PAS_CODIGOPASSAGEM == Value)

										   //ORDER
										   orderby Gdoc.DOC_NOMEARQUIVO descending

										   select new
										   {
											   PAS_IDPASSAGEM = (int)Gpass.PAS_IDPASSAGEM,
											   PAS_CODIGOPASSAGEM = Gpass.PAS_CODIGOPASSAGEM,
											   PAS_REGISTRO = Gpass.PAS_REGISTRO,
											   NOME = Gcli.CPJ_RAZAOSOCIAL,
											   PAS_DATAHORAPASSAGEM = Gpass.PAS_DATAHORAPASSAGEM,
											   CPF_CNPJ = Gcli.CPJ_CNPJ,
											   CPF_CNPJ_FLAGATIVO = (Gcli.CPJ_FLAGATIVO == 1) ? true : false,
											   INCLUSAO = (Gdoc.DOC_NOMEARQUIVO != null || Gdoc.DOC_NOMEARQUIVO != "") ? Gdoc.DOC_NOMEARQUIVO : "0",
											   MAX_ORDER = (!string.IsNullOrEmpty(Gdoc.DOC_NOMEARQUIVO)) ? Gdoc.DOC_NOMEARQUIVO : "0",
											   DIRLOTEINBOX = "",
											   LTU_PathImagens = "",
											   NRO_PRONTUARIO = "",
											   DATA_NASCIMENTO = "",
											   TIPO_ATENDIMENTO = Gpass.PAS_TIPOATENDIMENTO
										   });

						List<GED_PROC_F_Lotes_Result> ListaPJ = new List<GED_PROC_F_Lotes_Result>();
						foreach (var item in resultadoPJ)
						{
							ListaPJ.Add(new GED_PROC_F_Lotes_Result()
							{
								CPF_CNPJ_FLAGATIVO = item.CPF_CNPJ_FLAGATIVO,
								CPF_CNPJ = item.CPF_CNPJ,
								DIRLOTEINBOX = item.DIRLOTEINBOX,
								INCLUSAO = int.Parse(item.INCLUSAO),
								LTU_PathImagens = item.LTU_PathImagens,
								MAX_ORDER = int.Parse(item.MAX_ORDER),
								NOME = item.NOME,
								PAS_CODIGOPASSAGEM = item.PAS_CODIGOPASSAGEM,
								PAS_DATAHORAPASSAGEM = item.PAS_DATAHORAPASSAGEM,
								PAS_IDPASSAGEM = item.PAS_IDPASSAGEM,
								PAS_REGISTRO = item.PAS_REGISTRO,
								NRO_PRONTUARIO = "",
								DATA_NASCIMENTO = null,
								TIPO_ATENDIMENTO = item.TIPO_ATENDIMENTO

							});

							if (Tipo > 1)
							{ break; }


						}



						return ListaPJ;
						#endregion
					}

				}
				catch (Exception)
				{

					throw;
				}

			}

		}

		public static List<GED_PROC_CodigosBarras_Result> GetGED_PROC_CodigosBarras(int tipo, int sub, string codigo = "")
		{


			StringBuilder strCodigo = new StringBuilder();

			using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
			{

				var BARCODE = from bar in
								  ((from DtDOc in DB.GEDTIPOSDOCUMENTOS
									where DtDOc.TPD_IDTIPODOCUMENTO > 0
									select new GED_PROC_CodigosBarras_Result
									{
										TPD_CODIGOBARRA = DtDOc.TPD_CODIGOBARRA,
										TPD_DESCRICAO = DtDOc.TPD_DESCRICAO,
										TPD_TEMPOVALIDADE = (int)DtDOc.TPD_TEMPOVALIDADE,
										TPD_IDDIVISAO = (int)DtDOc.TPD_IDDIVISAO,
										TPD_IDTIPODOCUMENTO = (int)DtDOc.TPD_IDTIPODOCUMENTO,
										STD_IDSUBTIPOSDOCUMENTOS = null,
										STD_FLAGVENCIMENTOMANUAL = 0,
										STD_MESVENCIMENTOANUAL = 0,
										DIV_CODIGOREDUZIDO = DtDOc.GEDDIVISOES.DIV_CODIGOREDUZIDO,
										TPD_FLAGTIPOCLIENTE = (int)DtDOc.TPD_FLAGTIPOCLIENTE

									}).Union(

									  from DstDoc in DB.GEDSUBTIPOSDOCUMENTOS

									  join subT in DB.GEDTIPOSDOCUMENTOS on DstDoc.STD_IDTIPOSDOCUMENTOS equals subT.TPD_IDTIPODOCUMENTO

									  where DstDoc.STD_IDSUBTIPOSDOCUMENTOS > 0 && DstDoc.STD_FLAGATIVO == 1
									  select new GED_PROC_CodigosBarras_Result
									  {
										  TPD_CODIGOBARRA = DstDoc.STD_CODIGOBARRA,
										  TPD_DESCRICAO = DstDoc.STD_DESCRICAO,
										  TPD_TEMPOVALIDADE = (int)DstDoc.STD_TEMPOVALIDADE,
										  TPD_IDDIVISAO = (int)subT.TPD_IDDIVISAO,
										  TPD_IDTIPODOCUMENTO = (int)subT.TPD_IDTIPODOCUMENTO,
										  STD_IDSUBTIPOSDOCUMENTOS = (int)DstDoc.STD_IDSUBTIPOSDOCUMENTOS,
										  STD_FLAGVENCIMENTOMANUAL = (int)DstDoc.STD_FLAGVENCIMENTOMANUAL,
										  STD_MESVENCIMENTOANUAL = (int)DstDoc.STD_FLAGVENCIMENTOANUAL,
										  DIV_CODIGOREDUZIDO = DstDoc.GEDTIPOSDOCUMENTOS.GEDDIVISOES.DIV_CODIGOREDUZIDO,
										  TPD_FLAGTIPOCLIENTE = (int)DstDoc.STD_FLAGTIPOCLIENTE
									  }))

							  join Gdiv in DB.GEDDIVISOES on bar.TPD_IDDIVISAO equals Gdiv.DIV_IDDIVISAO
							  where bar.STD_IDSUBTIPOSDOCUMENTOS != null
							  select bar;


				List<GED_PROC_CodigosBarras_Result> ListaReturn = new List<GED_PROC_CodigosBarras_Result>();
				List<GED_PROC_CodigosBarras_Result> ListaTipo = new List<GED_PROC_CodigosBarras_Result>();
				List<GED_PROC_CodigosBarras_Result> ListaSub = new List<GED_PROC_CodigosBarras_Result>();
				List<GED_PROC_CodigosBarras_Result> ListaCod = new List<GED_PROC_CodigosBarras_Result>();
				List<GED_PROC_CodigosBarras_Result> ListaCodSegunda = new List<GED_PROC_CodigosBarras_Result>();

				ListaReturn = BARCODE.ToList();
				ListaCodSegunda = BARCODE.ToList();

				if (tipo == 1 && sub == 1)
				{


					if (codigo == "")
					{
						foreach (var item in ListaReturn)
						{
							ListaCod.Add(new GED_PROC_CodigosBarras_Result()
							{
								STD_IDSUBTIPOSDOCUMENTOS = item.STD_IDSUBTIPOSDOCUMENTOS,
								TPD_IDDIVISAO = item.TPD_IDDIVISAO,
								DateValidity = null,
								DIV_CODIGOREDUZIDO = item.DIV_CODIGOREDUZIDO,
								REQUERDATAINICIOVALIDADE = (item.TPD_TEMPOVALIDADE > 0 || item.STD_MESVENCIMENTOANUAL > 0 || item.STD_FLAGVENCIMENTOMANUAL > 0) ? 1 : 0,
								StartDateValidity = null,
								STD_FLAGVENCIMENTOMANUAL = item.STD_FLAGVENCIMENTOMANUAL,
								STD_MESVENCIMENTOANUAL = item.STD_MESVENCIMENTOANUAL,
								TPD_CODIGOBARRA = item.TPD_CODIGOBARRA,
								TPD_DESCRICAO = item.TPD_DESCRICAO,
								TPD_IDTIPODOCUMENTO = item.TPD_IDTIPODOCUMENTO,
								TPD_TEMPOVALIDADE = item.TPD_TEMPOVALIDADE,
								TPD_FLAGTIPOCLIENTE = item.TPD_FLAGTIPOCLIENTE
							});
						}
					}
					else
					{

						foreach (var item in ListaReturn)
						{
							if (item.TPD_CODIGOBARRA == codigo)
							{
								ListaCod.Add(new GED_PROC_CodigosBarras_Result()
								{
									STD_IDSUBTIPOSDOCUMENTOS = item.STD_IDSUBTIPOSDOCUMENTOS,
									TPD_IDDIVISAO = item.TPD_IDDIVISAO,
									DateValidity = null,
									DIV_CODIGOREDUZIDO = item.DIV_CODIGOREDUZIDO,
									REQUERDATAINICIOVALIDADE = (item.TPD_TEMPOVALIDADE > 0 || item.STD_MESVENCIMENTOANUAL > 0 || item.STD_FLAGVENCIMENTOMANUAL > 0) ? 1 : 0,
									StartDateValidity = null,
									STD_FLAGVENCIMENTOMANUAL = item.STD_FLAGVENCIMENTOMANUAL,
									STD_MESVENCIMENTOANUAL = item.STD_MESVENCIMENTOANUAL,
									TPD_CODIGOBARRA = item.TPD_CODIGOBARRA,
									TPD_DESCRICAO = item.TPD_DESCRICAO,
									TPD_IDTIPODOCUMENTO = item.TPD_IDTIPODOCUMENTO,
									TPD_TEMPOVALIDADE = item.TPD_TEMPOVALIDADE,
									TPD_FLAGTIPOCLIENTE = item.TPD_FLAGTIPOCLIENTE
								});
							}
						}

					}
					ListaCod.ForEach(delegate (GED_PROC_CodigosBarras_Result i) { i.StartDateValidity = DateTime.MinValue; i.DateValidity = DateTime.MinValue; });
					return ListaCod;
				}
				else if (tipo == 1)
				{
					//    WHERE NOT  STD_IDSUBTIPOSDOCUMENTOS IS NULL
					foreach (var item in ListaReturn)
					{
						if (item.STD_IDSUBTIPOSDOCUMENTOS == null)
						{
							ListaTipo.Add(item);
						}
					}
					ListaCodSegunda.Clear();
					ListaCodSegunda.AddRange(ListaTipo);
				}
				else if (sub == 1)
				{
					//     WHERE STD_IDSUBTIPOSDOCUMENTOS IS NULL 
					foreach (var item in ListaReturn)
					{
						if (item.STD_IDSUBTIPOSDOCUMENTOS != null)
						{
							ListaSub.Add(item);
						}
					}
					ListaCodSegunda.Clear();
					ListaCodSegunda.AddRange(ListaSub);
				}

				if (codigo == "")
				{
					foreach (var item in ListaCodSegunda)
					{
						ListaCod.Add(new GED_PROC_CodigosBarras_Result()
						{
							STD_IDSUBTIPOSDOCUMENTOS = item.STD_IDSUBTIPOSDOCUMENTOS,
							TPD_IDDIVISAO = item.TPD_IDDIVISAO,
							DateValidity = null,
							DIV_CODIGOREDUZIDO = item.DIV_CODIGOREDUZIDO,
							REQUERDATAINICIOVALIDADE = (item.TPD_TEMPOVALIDADE > 0 || item.STD_MESVENCIMENTOANUAL > 0 || item.STD_FLAGVENCIMENTOMANUAL > 0) ? 1 : 0,
							StartDateValidity = null,
							STD_FLAGVENCIMENTOMANUAL = item.STD_FLAGVENCIMENTOMANUAL,
							STD_MESVENCIMENTOANUAL = item.STD_MESVENCIMENTOANUAL,
							TPD_CODIGOBARRA = item.TPD_CODIGOBARRA,
							TPD_DESCRICAO = item.TPD_DESCRICAO,
							TPD_IDTIPODOCUMENTO = item.TPD_IDTIPODOCUMENTO,
							TPD_TEMPOVALIDADE = item.TPD_TEMPOVALIDADE,
							TPD_FLAGTIPOCLIENTE = item.TPD_FLAGTIPOCLIENTE
						});
					}
				}
				else
				{

					foreach (var item in ListaCodSegunda)
					{
						if (item.TPD_CODIGOBARRA == codigo)
						{
							ListaCod.Add(new GED_PROC_CodigosBarras_Result()
							{
								STD_IDSUBTIPOSDOCUMENTOS = item.STD_IDSUBTIPOSDOCUMENTOS,
								TPD_IDDIVISAO = item.TPD_IDDIVISAO,
								DateValidity = null,
								DIV_CODIGOREDUZIDO = item.DIV_CODIGOREDUZIDO,
								REQUERDATAINICIOVALIDADE = (item.TPD_TEMPOVALIDADE > 0 || item.STD_MESVENCIMENTOANUAL > 0 || item.STD_FLAGVENCIMENTOMANUAL > 0) ? 1 : 0,
								StartDateValidity = null,
								STD_FLAGVENCIMENTOMANUAL = item.STD_FLAGVENCIMENTOMANUAL,
								STD_MESVENCIMENTOANUAL = item.STD_MESVENCIMENTOANUAL,
								TPD_CODIGOBARRA = item.TPD_CODIGOBARRA,
								TPD_DESCRICAO = item.TPD_DESCRICAO,
								TPD_IDTIPODOCUMENTO = item.TPD_IDTIPODOCUMENTO,
								TPD_TEMPOVALIDADE = item.TPD_TEMPOVALIDADE,
								TPD_FLAGTIPOCLIENTE = item.TPD_FLAGTIPOCLIENTE
							});
						}
					}

				}
				ListaCod.ForEach(delegate (GED_PROC_CodigosBarras_Result i) { i.StartDateValidity = DateTime.MinValue; i.DateValidity = DateTime.MinValue; });

				return ListaCod;



			}



		}

		public static Nullable<int> GetIdFormato(string sFormato)
		{
			return ExecuteCommand<Nullable<int>>(() =>
				{
					using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
					{
						var result = DB.GEDFORMATOS.Where(gf => gf.FMT_EXTENSAO.ToUpper() == sFormato.ToUpper()).First();

						if (result != null)
						{
							return (int)result.FMT_IDFORMATO;
						}
						else
						{
							return 2;
						}
					}
				});


		}

		public static bool InsertDocuments(GEDDOCUMENTOS[] Docs)
		{
			return ExecuteCommand<bool>(() =>
				{
					using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
					{
						try
						{
							foreach (var Doc in Docs)
							{
								WFTranparentLoading.Messege("Inserindo " + Doc.DOC_NOMEARQUIVO + "...");

								DB.GEDDOCUMENTOS.AddObject(Doc);

							}
							return DB.SaveChanges() > 0;
						}
						catch (Exception ex)
						{
							MessageBox.Show("ERRP: " + ex.InnerException.ToString());
							return false;
						}

					}
				});



		}

		public static GEDLOTESXUSUARIOS NewGEDLotesXUsuarios(decimal IdUsuario, string CodigoPassagem, int IdStatusLote)
		{
			var result = new GEDLOTESXUSUARIOS();
			result.LTU_CODIGOPASSAGEM = CodigoPassagem;
			result.LTU_IDUSUARIO = IdUsuario;
			SaveGedLotes(result, IdStatusLote);
			return result;
		}

		public static GEDLOTESXUSUARIOS SaveGedLotes(GEDLOTESXUSUARIOS gEDLotesXUsuarios, int IdStatusLote)
		{
			return ExecuteCommand<GEDLOTESXUSUARIOS>(() =>
			  {
				  using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
				  {
					  gEDLotesXUsuarios.LTU_IDSTATUSLOTE = IdStatusLote;
					  gEDLotesXUsuarios.LTU_DATA = DateTime.Now;
					  DB.GEDLOTESXUSUARIOS.AddObject(gEDLotesXUsuarios);
					  DB.SaveChanges();
					  return gEDLotesXUsuarios;
				  }
			  });

		}


		/////////////////////////////////////////////////LUCAS./////////////////////////


		public static bool UpdateCertificadoUsuario(int idPessoa, string Certificado)
		{
			return ExecuteCommand<bool>(() =>
			{
				int iNivellAcesso = 0;
				int flagCertificado = 0;
				if (!string.IsNullOrEmpty(Certificado))
				{
					flagCertificado = 1;

				}
				using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
				{

					var usuario = DB.GEDUSUARIOS.Where(c => c.USR_IDPESSOA == idPessoa).FirstOrDefault();
					usuario.USR_SERIALNUMBERCERT = Certificado;

					DB.SaveChanges();

					return true;
				}
			});
		}


		public static IEnumerable<GEDPESSOAS> GetAllGEDPessoas()
		{
			var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle);


			var listaPessoas = DB.GEDPESSOAS.Where(c => c.PES_FLAGATIVO == 1).AsEnumerable();
			return listaPessoas;


		}


		public static IEnumerable<GEDPESSOAS> GetAllGEDPessoasLike(string sNome)
		{
			var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle);



			var listaPessoas = DB.GEDPESSOAS.Where(c => c.PES_FLAGATIVO == 1 && c.PES_NOME.ToUpper().Contains(sNome.ToUpper()) && c.GEDUSUARIOS.Any()).AsEnumerable();
			return listaPessoas;


		}

		public static GEDCLIENTEPF GetClientePF(string sCPF, string sRegistro)
		{
			try
			{
				// MessageBox.Show(sCPF);
				///aqui eh ondele ele consulta por CPF ou  Registro
				var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle);

				var gedPf = DB.GEDCLIENTEPF.Where(c => c.CPF_CPF == sCPF).FirstOrDefault();

				//if (gedPf == null)
				//{

				//    MessageBox.Show("Não achou, ira consultar pelo Registro: " + sRegistro);
				//}
				//else
				//{
				//    MessageBox.Show("Achou com o CPF o nome: " + gedPf.CPF_NOME);
				//}
				if (gedPf == null)
					gedPf = DB.GEDCLIENTEPF.Where(c => c.CPF_REGISTRO == sRegistro).FirstOrDefault();

				return gedPf;

			}
			catch (Exception)
			{

				throw;
			}
		}



		public static GEDCLIENTEPF GetClientePFRegistros(string sRegistro, string sRegistroAntigo)
		{
			try
			{

				var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle);

				var gedReg = DB.GEDCLIENTEPF.Where(c => c.CPF_REGISTRO == sRegistro).FirstOrDefault();

				if (gedReg == null)
				{

					if (!string.IsNullOrEmpty(sRegistroAntigo))
					{
						if (sRegistroAntigo.Contains(","))
						{

							var splt = sRegistroAntigo.Split(',');

							//verifica nos registros
							foreach (var item in splt)
							{
								if (item != "0" && item != "")
								{
									gedReg = DB.GEDCLIENTEPF.Where(c => c.CPF_REGISTRO.Contains(item)).FirstOrDefault();
									if (gedReg != null) break;
								}
							}

							//verifica nos registros antigos
							if (gedReg == null)
								foreach (var item in splt)
								{
									if (item != "0" && item != "")
									{
										gedReg = DB.GEDCLIENTEPF.Where(c => c.CPF_REGISTROOLD.Contains(item)).FirstOrDefault();
										if (gedReg != null) break;

									}
								}

						}
						else
						{
							if (sRegistroAntigo != "0" && sRegistroAntigo != "")
							{
								gedReg = DB.GEDCLIENTEPF.Where(c => c.CPF_REGISTRO == sRegistroAntigo).FirstOrDefault();

								if (gedReg == null)
									gedReg = DB.GEDCLIENTEPF.Where(c => c.CPF_REGISTROOLD == sRegistroAntigo).FirstOrDefault();
							}
						}


					}


				}


				return gedReg;

			}
			catch (Exception)
			{

				throw;
			}
		}

		public static GEDPASSAGENS GetPassagemByCodPassagem(string sPassagem)
		{
			try
			{

				var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle);

				var gedPASS = DB.GEDPASSAGENS.Where(c => c.PAS_CODIGOPASSAGEM == sPassagem).FirstOrDefault();

				return gedPASS;

			}
			catch (Exception)
			{

				throw;
			}
		}

		public static GEDCLIENTEPF InsertGEDClientePF(GEDCLIENTEPF cliente)
		{
			try
			{

				var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle);

				var clientePF = cliente;
				DB.GEDCLIENTEPF.AddObject(clientePF);
				DB.SaveChanges();

				DB.Refresh(System.Data.Objects.RefreshMode.StoreWins, clientePF);
				return clientePF;

			}
			catch (Exception)
			{

				throw;
			}
		}


		public static GEDCLIENTEPF UpdateGEDClientePF(GEDCLIENTEPF cliente)
		{
			try
			{

				var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle);



				var clientePF = DB.GEDCLIENTEPF.Where(c => c.CPF_IDCLIENTEPF == cliente.CPF_IDCLIENTEPF).FirstOrDefault();

				clientePF.CPF_REGISTRO = cliente.CPF_REGISTRO;
				clientePF.CPF_REGISTROOLD = cliente.CPF_REGISTROOLD;
				clientePF.CPF_NOME = cliente.CPF_NOME;
				clientePF.CPF_FLAGATUALIZARUNIFICACAO = cliente.CPF_FLAGATUALIZARUNIFICACAO;


				DB.SaveChanges();

				return clientePF;

			}
			catch (Exception)
			{

				throw;
			}
		}

		public static GEDPASSAGENS InsertGEDPassagem(GEDPASSAGENS passagem)
		{
			try
			{
				return ExecuteCommand<GEDPASSAGENS>(() =>
				{
					using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
					{
						DB.AddObject("GEDPASSAGENS", passagem);
						DB.SaveChanges();
						DB.Refresh(RefreshMode.StoreWins, DB.ObjectStateManager.GetObjectStateEntries(EntityState.Added));

						return passagem;
					}
				});

			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public static GEDPASSAGENS UpdateGEDPassagem(GEDPASSAGENS passagem)
		{
			try
			{
				return ExecuteCommand<GEDPASSAGENS>(() =>
				{
					using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
					{


						var passagemUpdate = DB.GEDPASSAGENS.Where(c => c.PAS_IDPASSAGEM == passagem.PAS_IDPASSAGEM).FirstOrDefault();


						passagemUpdate.PAS_IDUNIDADE = passagem.PAS_IDUNIDADE;
						passagemUpdate.PAS_IDCONVENIO = 0;
						passagemUpdate.PAS_DATAHORAPASSAGEM = passagem.PAS_DATAHORAPASSAGEM;
						passagemUpdate.PAS_CODIGOPASSAGEM = passagem.PAS_CODIGOPASSAGEM;
						passagemUpdate.PAS_FLAGCLIENTEPF = 1;
						passagemUpdate.PAS_DATAHORAPASSAGEMFIM = new DateTime(1900, 01, 01);
						passagemUpdate.PAS_TIPOATENDIMENTO = passagem.PAS_TIPOATENDIMENTO;
						passagemUpdate.PAS_REGISTRO = passagem.PAS_REGISTRO;
						passagemUpdate.PAS_REGISTROOLD = passagem.PAS_REGISTROOLD;


						DB.SaveChanges();

						return passagem;
					}
				});

			}
			catch (Exception ex)
			{

				throw;
			}
		}


		//N2
		public static List<GEDDocumentosNivel2> GetDocumentosAssN2(string NumeroAtendimento, string dataAtendimento, bool fVerTodos, decimal idUsuarioAssina, decimal iNivelAssinaUsuario)
		{
			try
			{
				using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
				{

					DateTime dt;
					bool fVerifica = DateTime.TryParse(dataAtendimento, out dt);
					if (!fVerifica)
					{
						dataAtendimento = string.Empty;
					}
					else
					{
						dt = Convert.ToDateTime(dataAtendimento);
					}

					if (ACSGlobal.ListaGruposSetoresLogado == null)
						ACSGlobal.ListaGruposSetoresLogado = new List<decimal?>();

					if (fVerTodos)
					{
						var getSelect = (from gDoc in DB.GEDDOCUMENTOS

										 join Us in DB.GEDUSUARIOS on gDoc.DOC_IDUSUARIOACSCAPTURE equals Us.USR_IDUSUARIO
										 //join Gu in DB.GEDGRUPOSUSUARIOS on Us.USR_IDGRUPOUSUARIO equals Gu.GRP_IDGRUPOUSUARIO
										 join Gu in DB.GEDGRUPOSUSUARIOS on gDoc.DOC_IDGRUPOUSUARIOCAPTURA equals Gu.GRP_IDGRUPOUSUARIO
										 //join GuS in DB.GEDSETORXGRUPOUSUARIO on Gu.GRP_IDGRUPOUSUARIO equals GuS.SXG_IDGRUPOUSUARIO
										 //join Set in DB.GEDSETOR on GuS.SXG_IDSETOR equals Set.SET_IDSETOR

										 join gPass in DB.GEDPASSAGENS on gDoc.DOC_IDPASSAGEM equals gPass.PAS_IDPASSAGEM
										 join gCli in DB.GEDCLIENTEPF on gPass.PAS_REGISTRO equals gCli.CPF_REGISTRO
										 join gTipoDoc in DB.GEDTIPOSDOCUMENTOS on gDoc.DOC_IDTIPODOCUMENTO equals gTipoDoc.TPD_IDTIPODOCUMENTO
										 join gTipoSubDFoc in DB.GEDSUBTIPOSDOCUMENTOS on gDoc.DOC_IDSUBTIPODOCUMENTO equals gTipoSubDFoc.STD_IDSUBTIPOSDOCUMENTOS
										 join gDiv in DB.GEDDIVISOES on gTipoDoc.TPD_IDDIVISAO equals gDiv.DIV_IDDIVISAO



										 where gDoc.DOC_IDUSUARIOASSINANIVEL1 != null && gDoc.DOC_TIPOCAPTURA == 1 //1 - scanner e 2  - importado
																												   // Fernando - 01/02/2017
																												   // filtrar só pelos subtipos maiores que o nível de assinatura do usuário
										 && gTipoSubDFoc.STD_NIVELASSINA >= iNivelAssinaUsuario
										 // Fernando - 01/02/2017
										 // colocada ordenação por ID da passagem
										 &&
										 (
											(iNivelAssinaUsuario == 2 && gDoc.DOC_IDUSUARIOASSINANIVEL2 == null)
										 || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 == null)
										 || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != idUsuarioAssina && gDoc.DOC_IDUSUARIOASSINANIVEL3_2 == null && gTipoSubDFoc.STD_CODIGOBARRA == "CPSI")
										 )

										  &&
										  (
											 (iNivelAssinaUsuario == 2 && ACSGlobal.ListaGruposSetoresLogado.Contains(gDoc.DOC_IDGRUPOUSUARIOCAPTURA))
										   || (iNivelAssinaUsuario != 2 && gDoc.DOC_IDGRUPOUSUARIOCAPTURA != null)
										  )
										   &&
									   (DB.GEDDOCASSINANDO.Any(doa => doa.DOA_IDDOCUMENTO == gDoc.DOC_IDDOCUMENTO) == false)

										 orderby gPass.PAS_DATAHORAPASSAGEM ascending, gDoc.DOC_IDPASSAGEM, gDoc.DOC_IDDOCUMENTO
										 select new GEDDocumentosNivel2
										 {
											 CPF_NOME = gCli.CPF_NOME,
											 CPF_CPF = gCli.CPF_CPF,
											 CPF_IDCLIENTEPF = gCli.CPF_IDCLIENTEPF,
											 CPF_REGISTRO = gCli.CPF_REGISTRO,

											 DOC_EXTENSAONOMEARQUIVO = gDoc.DOC_EXTENSAONOMEARQUIVO,
											 DOC_IDDOCUMENTO = gDoc.DOC_IDDOCUMENTO,
											 DOC_IDPASSAGEM = gDoc.DOC_IDPASSAGEM,
											 DOC_IDUSUARIOASSINANIVEL1 = gDoc.DOC_IDUSUARIOASSINANIVEL1,
											 DOC_IDUSUARIOASSINANIVEL2 = gDoc.DOC_IDUSUARIOASSINANIVEL2,
											 DOC_IDUSUARIOASSINANIVEL3 = gDoc.DOC_IDUSUARIOASSINANIVEL3,
											 DOC_IDUSUARIOASSINANIVEL3_2 = gDoc.DOC_IDUSUARIOASSINANIVEL3_2,
											 DOC_NOMEARQUIVO = gDoc.DOC_NOMEARQUIVO,
											 DOC_PATH = gDoc.DOC_PATH,
											 DOC_DIVISAO = gDiv.DIV_CODIGOREDUZIDO,
											 DOC_DATAHORACADASTRO = gDoc.DOC_DATAHORACADASTRO,
											 DOC_IDGRUPOUSUARIOCAPTURA = gDoc.DOC_IDGRUPOUSUARIOCAPTURA,

											 PAS_CODIGOPASSAGEM = gPass.PAS_CODIGOPASSAGEM,
											 PAS_DATAHORAPASSAGEM = gPass.PAS_DATAHORAPASSAGEM,
											 PAS_REGISTRO = gPass.PAS_REGISTRO,

											 STD_NOME = gTipoSubDFoc.STD_DESCRICAO,
											 STD_NIVELASSINA = gTipoSubDFoc.STD_NIVELASSINA,
											 STD_CODIGO = gTipoSubDFoc.STD_CODIGOBARRA,

											 //SET_IDSETOR = Set.SET_IDSETOR

										 });

						//if (iNivelAssinaUsuario == 2)
						//{
						//    getSelect = getSelect.Where(e => e.DOC_IDUSUARIOASSINANIVEL2 == null);
						//}
						//else if (iNivelAssinaUsuario == 3)
						//{
						//    getSelect = getSelect.Where(e => 1 == 1 && (e.DOC_IDUSUARIOASSINANIVEL3 == null
						//        || (e.DOC_IDUSUARIOASSINANIVEL3 != null && e.STD_CODIGO == "CPSI" && e.DOC_IDUSUARIOASSINANIVEL3_2 == null && e.DOC_IDUSUARIOASSINANIVEL3 == idUsuarioAssina)));
						//}


						var getList = getSelect.Take(1000).ToList();
						//return getList;
						//nova funcionalidade... fazer um top e pegar os documentos do ultimo registro por completo.

						if (getList == null || getList.Count() == 0)
							return getList;



						// remover os registros da último atendimento
						// necessário para evitar que estejam faltando documentos do atendimento quando seleciona os top 3000
						var idUltimaPassagem = getList[getList.Count() - 1].DOC_IDPASSAGEM;

						var listaNova = getList.Where(c => c.DOC_IDPASSAGEM != idUltimaPassagem).ToList();

						// retornar os documentos do último atendimento removido
						getSelect = (from gDoc in DB.GEDDOCUMENTOS

									 join Us in DB.GEDUSUARIOS on gDoc.DOC_IDUSUARIOACSCAPTURE equals Us.USR_IDUSUARIO
									 //join Gu in DB.GEDGRUPOSUSUARIOS on Us.USR_IDGRUPOUSUARIO equals Gu.GRP_IDGRUPOUSUARIO
									 join Gu in DB.GEDGRUPOSUSUARIOS on gDoc.DOC_IDGRUPOUSUARIOCAPTURA equals Gu.GRP_IDGRUPOUSUARIO
									 //join GuS in DB.GEDSETORXGRUPOUSUARIO on Gu.GRP_IDGRUPOUSUARIO equals GuS.SXG_IDGRUPOUSUARIO
									 //join Set in DB.GEDSETOR on GuS.SXG_IDSETOR equals Set.SET_IDSETOR

									 join gPass in DB.GEDPASSAGENS on gDoc.DOC_IDPASSAGEM equals gPass.PAS_IDPASSAGEM
									 join gCli in DB.GEDCLIENTEPF on gPass.PAS_REGISTRO equals gCli.CPF_REGISTRO
									 join gTipoDoc in DB.GEDTIPOSDOCUMENTOS on gDoc.DOC_IDTIPODOCUMENTO equals gTipoDoc.TPD_IDTIPODOCUMENTO
									 join gTipoSubDFoc in DB.GEDSUBTIPOSDOCUMENTOS on gDoc.DOC_IDSUBTIPODOCUMENTO equals gTipoSubDFoc.STD_IDSUBTIPOSDOCUMENTOS
									 join gDiv in DB.GEDDIVISOES on gTipoDoc.TPD_IDDIVISAO equals gDiv.DIV_IDDIVISAO

									 where gDoc.DOC_IDUSUARIOASSINANIVEL1 != null && gDoc.DOC_TIPOCAPTURA == 1 && gDoc.DOC_IDPASSAGEM == idUltimaPassagem  //1 - scanner e 2  - importado
																																						   // Fernando - 01/02/2017
																																						   // filtrar só pelos subtipos maiores que o nível de assinatura do usuário
										 && gTipoSubDFoc.STD_NIVELASSINA >= iNivelAssinaUsuario

										  &&
										 (
											(iNivelAssinaUsuario == 2 && gDoc.DOC_IDUSUARIOASSINANIVEL2 == null)
										 || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 == null)
										 || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != idUsuarioAssina && gDoc.DOC_IDUSUARIOASSINANIVEL3_2 == null && gTipoSubDFoc.STD_CODIGOBARRA == "CPSI")
										 )
										 &&
									   (
										  (iNivelAssinaUsuario == 2 && ACSGlobal.ListaGruposSetoresLogado.Contains(gDoc.DOC_IDGRUPOUSUARIOCAPTURA))
									   || (iNivelAssinaUsuario != 2 && gDoc.DOC_IDGRUPOUSUARIOCAPTURA != null)
									   )
										&&
									   (DB.GEDDOCASSINANDO.Any(doa => doa.DOA_IDDOCUMENTO == gDoc.DOC_IDDOCUMENTO) == false)
									 // Fernando - 01/02/2017
									 // colocada ordenação por ID da passagem
									 orderby gPass.PAS_DATAHORAPASSAGEM ascending, gDoc.DOC_IDPASSAGEM, gDoc.DOC_IDDOCUMENTO
									 select new GEDDocumentosNivel2
									 {
										 CPF_NOME = gCli.CPF_NOME,
										 CPF_CPF = gCli.CPF_CPF,
										 CPF_IDCLIENTEPF = gCli.CPF_IDCLIENTEPF,
										 CPF_REGISTRO = gCli.CPF_REGISTRO,

										 DOC_EXTENSAONOMEARQUIVO = gDoc.DOC_EXTENSAONOMEARQUIVO,
										 DOC_IDDOCUMENTO = gDoc.DOC_IDDOCUMENTO,
										 DOC_IDPASSAGEM = gDoc.DOC_IDPASSAGEM,
										 DOC_IDUSUARIOASSINANIVEL1 = gDoc.DOC_IDUSUARIOASSINANIVEL1,
										 DOC_IDUSUARIOASSINANIVEL2 = gDoc.DOC_IDUSUARIOASSINANIVEL2,
										 DOC_IDUSUARIOASSINANIVEL3 = gDoc.DOC_IDUSUARIOASSINANIVEL3,
										 DOC_IDUSUARIOASSINANIVEL3_2 = gDoc.DOC_IDUSUARIOASSINANIVEL3_2,
										 DOC_NOMEARQUIVO = gDoc.DOC_NOMEARQUIVO,
										 DOC_PATH = gDoc.DOC_PATH,
										 DOC_DIVISAO = gDiv.DIV_CODIGOREDUZIDO,
										 DOC_DATAHORACADASTRO = gDoc.DOC_DATAHORACADASTRO,
										 DOC_IDGRUPOUSUARIOCAPTURA = gDoc.DOC_IDGRUPOUSUARIOCAPTURA,

										 PAS_CODIGOPASSAGEM = gPass.PAS_CODIGOPASSAGEM,
										 PAS_DATAHORAPASSAGEM = gPass.PAS_DATAHORAPASSAGEM,
										 PAS_REGISTRO = gPass.PAS_REGISTRO,

										 STD_NOME = gTipoSubDFoc.STD_DESCRICAO,
										 STD_NIVELASSINA = gTipoSubDFoc.STD_NIVELASSINA,
										 STD_CODIGO = gTipoSubDFoc.STD_CODIGOBARRA,

										 //SET_IDSETOR = Set.SET_IDSETOR

									 });

						//if (iNivelAssinaUsuario == 2)
						//{
						//    getSelect = getSelect.Where(e => e.DOC_IDUSUARIOASSINANIVEL2 == null);
						//}
						//else if (iNivelAssinaUsuario == 3)
						//{
						//    getSelect = getSelect.Where(e => 1 == 1 && (e.DOC_IDUSUARIOASSINANIVEL3 == null
						//        || (e.DOC_IDUSUARIOASSINANIVEL3 != null && e.STD_CODIGO == "CPSI" && e.DOC_IDUSUARIOASSINANIVEL3_2 == null && e.DOC_IDUSUARIOASSINANIVEL3 == idUsuarioAssina)));
						//}

						getList = getSelect.ToList();

						listaNova.AddRange(getList);


						//bloqueia os documentos consultados
						bloqueiaDocumentos(listaNova);
						return listaNova;

					}
					else
					{

						//verifica se vai usar os dois campos oou so 1 data e numero atendimento
						if (string.IsNullOrEmpty(NumeroAtendimento) && !string.IsNullOrEmpty(dataAtendimento))
						{
							//consulta por data
							var getList = (from gDoc in DB.GEDDOCUMENTOS


										   join Us in DB.GEDUSUARIOS on gDoc.DOC_IDUSUARIOACSCAPTURE equals Us.USR_IDUSUARIO
										   //join Gu in DB.GEDGRUPOSUSUARIOS on Us.USR_IDGRUPOUSUARIO equals Gu.GRP_IDGRUPOUSUARIO
										   join Gu in DB.GEDGRUPOSUSUARIOS on gDoc.DOC_IDGRUPOUSUARIOCAPTURA equals Gu.GRP_IDGRUPOUSUARIO
										   //join GuS in DB.GEDSETORXGRUPOUSUARIO on Gu.GRP_IDGRUPOUSUARIO equals GuS.SXG_IDGRUPOUSUARIO
										   //join Set in DB.GEDSETOR on GuS.SXG_IDSETOR equals Set.SET_IDSETOR

										   join gPass in DB.GEDPASSAGENS on gDoc.DOC_IDPASSAGEM equals gPass.PAS_IDPASSAGEM
										   join gCli in DB.GEDCLIENTEPF on gPass.PAS_REGISTRO equals gCli.CPF_REGISTRO
										   join gTipoDoc in DB.GEDTIPOSDOCUMENTOS on gDoc.DOC_IDTIPODOCUMENTO equals gTipoDoc.TPD_IDTIPODOCUMENTO
										   join gTipoSubDFoc in DB.GEDSUBTIPOSDOCUMENTOS on gDoc.DOC_IDSUBTIPODOCUMENTO equals gTipoSubDFoc.STD_IDSUBTIPOSDOCUMENTOS
										   join gDiv in DB.GEDDIVISOES on gTipoDoc.TPD_IDDIVISAO equals gDiv.DIV_IDDIVISAO


										   where gDoc.DOC_IDUSUARIOASSINANIVEL1 != null
											   // Fernando - 01/02/2017
											   // filtro pelo tipo de captura
											   && gDoc.DOC_TIPOCAPTURA == 1
											 && EntityFunctions.TruncateTime(gPass.PAS_DATAHORAPASSAGEM) == EntityFunctions.TruncateTime(dt)
									   // Fernando - 01/02/2017
									   // filtrar só pelos subtipos maiores que o nível de assinatura do usuário
									   && gTipoSubDFoc.STD_NIVELASSINA >= iNivelAssinaUsuario

										&&
									   (
										  (iNivelAssinaUsuario == 2 && gDoc.DOC_IDUSUARIOASSINANIVEL2 == null)
									   || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 == null)
									   || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != idUsuarioAssina && gDoc.DOC_IDUSUARIOASSINANIVEL3_2 == null && gTipoSubDFoc.STD_CODIGOBARRA == "CPSI")
									   )
									   &&
									   (
										  (iNivelAssinaUsuario == 2 && ACSGlobal.ListaGruposSetoresLogado.Contains(gDoc.DOC_IDGRUPOUSUARIOCAPTURA))
									   || (iNivelAssinaUsuario != 2 && gDoc.DOC_IDGRUPOUSUARIOCAPTURA != null)
									   )
									   &&
									   (DB.GEDDOCASSINANDO.Any(doa => doa.DOA_IDDOCUMENTO == gDoc.DOC_IDDOCUMENTO) == false)


										   // Fernando - 01/02/2017
										   // colocada ordenação por ID da passagem
										   orderby gPass.PAS_DATAHORAPASSAGEM ascending, gDoc.DOC_IDPASSAGEM, gDoc.DOC_IDDOCUMENTO

										   select new GEDDocumentosNivel2
										   {
											   CPF_NOME = gCli.CPF_NOME,
											   CPF_CPF = gCli.CPF_CPF,
											   CPF_IDCLIENTEPF = gCli.CPF_IDCLIENTEPF,
											   CPF_REGISTRO = gCli.CPF_REGISTRO,

											   DOC_EXTENSAONOMEARQUIVO = gDoc.DOC_EXTENSAONOMEARQUIVO,
											   DOC_IDDOCUMENTO = gDoc.DOC_IDDOCUMENTO,
											   DOC_IDPASSAGEM = gDoc.DOC_IDPASSAGEM,
											   DOC_IDUSUARIOASSINANIVEL1 = gDoc.DOC_IDUSUARIOASSINANIVEL1,
											   DOC_IDUSUARIOASSINANIVEL2 = gDoc.DOC_IDUSUARIOASSINANIVEL2,
											   DOC_IDUSUARIOASSINANIVEL3 = gDoc.DOC_IDUSUARIOASSINANIVEL3,
											   DOC_IDUSUARIOASSINANIVEL3_2 = gDoc.DOC_IDUSUARIOASSINANIVEL3_2,
											   DOC_NOMEARQUIVO = gDoc.DOC_NOMEARQUIVO,
											   DOC_PATH = gDoc.DOC_PATH,
											   DOC_DIVISAO = gDiv.DIV_CODIGOREDUZIDO,
											   DOC_DATAHORACADASTRO = gDoc.DOC_DATAHORACADASTRO,
											   DOC_IDGRUPOUSUARIOCAPTURA = gDoc.DOC_IDGRUPOUSUARIOCAPTURA,

											   PAS_CODIGOPASSAGEM = gPass.PAS_CODIGOPASSAGEM,
											   PAS_DATAHORAPASSAGEM = gPass.PAS_DATAHORAPASSAGEM,
											   PAS_REGISTRO = gPass.PAS_REGISTRO,

											   STD_NOME = gTipoSubDFoc.STD_DESCRICAO,
											   STD_NIVELASSINA = gTipoSubDFoc.STD_NIVELASSINA,
											   STD_CODIGO = gTipoSubDFoc.STD_CODIGOBARRA,


											   //SET_IDSETOR = Set.SET_IDSETOR
										   }).Take(1000).ToList();

							if (getList == null || getList.Count() == 0)
								return getList;

							// remover os registros da último atendimento
							// necessário para evitar que estejam faltando documentos do atendimento quando seleciona os top 3000
							var idUltimaPassagem = getList[getList.Count() - 1].DOC_IDPASSAGEM;

							var listaNova = getList.Where(c => c.DOC_IDPASSAGEM != idUltimaPassagem).ToList();

							// retornar os documentos do último atendimento removido
							getList = (from gDoc in DB.GEDDOCUMENTOS

									   join Us in DB.GEDUSUARIOS on gDoc.DOC_IDUSUARIOACSCAPTURE equals Us.USR_IDUSUARIO
									   //join Gu in DB.GEDGRUPOSUSUARIOS on Us.USR_IDGRUPOUSUARIO equals Gu.GRP_IDGRUPOUSUARIO
									   join Gu in DB.GEDGRUPOSUSUARIOS on gDoc.DOC_IDGRUPOUSUARIOCAPTURA equals Gu.GRP_IDGRUPOUSUARIO
									   //join GuS in DB.GEDSETORXGRUPOUSUARIO on Gu.GRP_IDGRUPOUSUARIO equals GuS.SXG_IDGRUPOUSUARIO
									   //join Set in DB.GEDSETOR on GuS.SXG_IDSETOR equals Set.SET_IDSETOR

									   join gPass in DB.GEDPASSAGENS on gDoc.DOC_IDPASSAGEM equals gPass.PAS_IDPASSAGEM
									   join gCli in DB.GEDCLIENTEPF on gPass.PAS_REGISTRO equals gCli.CPF_REGISTRO
									   join gTipoDoc in DB.GEDTIPOSDOCUMENTOS on gDoc.DOC_IDTIPODOCUMENTO equals gTipoDoc.TPD_IDTIPODOCUMENTO
									   join gTipoSubDFoc in DB.GEDSUBTIPOSDOCUMENTOS on gDoc.DOC_IDSUBTIPODOCUMENTO equals gTipoSubDFoc.STD_IDSUBTIPOSDOCUMENTOS
									   join gDiv in DB.GEDDIVISOES on gTipoDoc.TPD_IDDIVISAO equals gDiv.DIV_IDDIVISAO

									   where gDoc.DOC_IDUSUARIOASSINANIVEL1 != null
											   // Fernando - 01/02/2017
											   // filtro pelo tipo de captura
											   && gDoc.DOC_TIPOCAPTURA == 1
										 && EntityFunctions.TruncateTime(gPass.PAS_DATAHORAPASSAGEM) == EntityFunctions.TruncateTime(dt)
									   // Fernando - 01/02/2017
									   // filtrar só pelos subtipos maiores que o nível de assinatura do usuário
									   && gTipoSubDFoc.STD_NIVELASSINA >= iNivelAssinaUsuario
									   // Fernando - 01/02/2017
									   // filtrar pelo id da passagem removido
									   && gDoc.DOC_IDPASSAGEM == idUltimaPassagem
										&&
									   (
										  (iNivelAssinaUsuario == 2 && gDoc.DOC_IDUSUARIOASSINANIVEL2 == null)
									   || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 == null)
									   || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != idUsuarioAssina && gDoc.DOC_IDUSUARIOASSINANIVEL3_2 == null && gTipoSubDFoc.STD_CODIGOBARRA == "CPSI")
									   )
										 &&
										 (
											(iNivelAssinaUsuario == 2 && ACSGlobal.ListaGruposSetoresLogado.Contains(gDoc.DOC_IDGRUPOUSUARIOCAPTURA))
										 || (iNivelAssinaUsuario != 2 && gDoc.DOC_IDGRUPOUSUARIOCAPTURA != null)
										 )
										  &&
									   (DB.GEDDOCASSINANDO.Any(doa => doa.DOA_IDDOCUMENTO == gDoc.DOC_IDDOCUMENTO) == false)
									   // Fernando - 01/02/2017

									   orderby gPass.PAS_DATAHORAPASSAGEM ascending, gDoc.DOC_IDPASSAGEM, gDoc.DOC_IDDOCUMENTO

									   select new GEDDocumentosNivel2
									   {
										   CPF_NOME = gCli.CPF_NOME,
										   CPF_CPF = gCli.CPF_CPF,
										   CPF_IDCLIENTEPF = gCli.CPF_IDCLIENTEPF,
										   CPF_REGISTRO = gCli.CPF_REGISTRO,

										   DOC_EXTENSAONOMEARQUIVO = gDoc.DOC_EXTENSAONOMEARQUIVO,
										   DOC_IDDOCUMENTO = gDoc.DOC_IDDOCUMENTO,
										   DOC_IDPASSAGEM = gDoc.DOC_IDPASSAGEM,
										   DOC_IDUSUARIOASSINANIVEL1 = gDoc.DOC_IDUSUARIOASSINANIVEL1,
										   DOC_IDUSUARIOASSINANIVEL2 = gDoc.DOC_IDUSUARIOASSINANIVEL2,
										   DOC_IDUSUARIOASSINANIVEL3 = gDoc.DOC_IDUSUARIOASSINANIVEL3,
										   DOC_IDUSUARIOASSINANIVEL3_2 = gDoc.DOC_IDUSUARIOASSINANIVEL3_2,
										   DOC_NOMEARQUIVO = gDoc.DOC_NOMEARQUIVO,
										   DOC_PATH = gDoc.DOC_PATH,
										   DOC_DIVISAO = gDiv.DIV_CODIGOREDUZIDO,
										   DOC_DATAHORACADASTRO = gDoc.DOC_DATAHORACADASTRO,
										   DOC_IDGRUPOUSUARIOCAPTURA = gDoc.DOC_IDGRUPOUSUARIOCAPTURA,

										   PAS_CODIGOPASSAGEM = gPass.PAS_CODIGOPASSAGEM,
										   PAS_DATAHORAPASSAGEM = gPass.PAS_DATAHORAPASSAGEM,
										   PAS_REGISTRO = gPass.PAS_REGISTRO,

										   STD_NOME = gTipoSubDFoc.STD_DESCRICAO,
										   STD_NIVELASSINA = gTipoSubDFoc.STD_NIVELASSINA,
										   STD_CODIGO = gTipoSubDFoc.STD_CODIGOBARRA,

										   //SET_IDSETOR = Set.SET_IDSETOR

									   }).ToList();

							listaNova.AddRange(getList);


							//bloqueia os documentos consultados
							bloqueiaDocumentos(listaNova);

							return listaNova;

						}
						else if (!string.IsNullOrEmpty(NumeroAtendimento) && string.IsNullOrEmpty(dataAtendimento))
						{
							//consulta por numero atendimento
							var getList = (from gDoc in DB.GEDDOCUMENTOS

										   join Us in DB.GEDUSUARIOS on gDoc.DOC_IDUSUARIOACSCAPTURE equals Us.USR_IDUSUARIO
										   //join Gu in DB.GEDGRUPOSUSUARIOS on Us.USR_IDGRUPOUSUARIO equals Gu.GRP_IDGRUPOUSUARIO
										   join Gu in DB.GEDGRUPOSUSUARIOS on gDoc.DOC_IDGRUPOUSUARIOCAPTURA equals Gu.GRP_IDGRUPOUSUARIO
										   //join GuS in DB.GEDSETORXGRUPOUSUARIO on Gu.GRP_IDGRUPOUSUARIO equals GuS.SXG_IDGRUPOUSUARIO
										   //join Set in DB.GEDSETOR on GuS.SXG_IDSETOR equals Set.SET_IDSETOR

										   join gPass in DB.GEDPASSAGENS on gDoc.DOC_IDPASSAGEM equals gPass.PAS_IDPASSAGEM
										   join gCli in DB.GEDCLIENTEPF on gPass.PAS_REGISTRO equals gCli.CPF_REGISTRO
										   join gTipoDoc in DB.GEDTIPOSDOCUMENTOS on gDoc.DOC_IDTIPODOCUMENTO equals gTipoDoc.TPD_IDTIPODOCUMENTO
										   join gTipoSubDFoc in DB.GEDSUBTIPOSDOCUMENTOS on gDoc.DOC_IDSUBTIPODOCUMENTO equals gTipoSubDFoc.STD_IDSUBTIPOSDOCUMENTOS
										   join gDiv in DB.GEDDIVISOES on gTipoDoc.TPD_IDDIVISAO equals gDiv.DIV_IDDIVISAO


										   orderby gPass.PAS_DATAHORAPASSAGEM ascending, gDoc.DOC_IDDOCUMENTO

										   where gDoc.DOC_IDUSUARIOASSINANIVEL1 != null
											   // Fernando - 01/02/2017
											   // filtro pelo tipo de captura
											   && gDoc.DOC_TIPOCAPTURA == 1
									   // Fernando - 01/02/2017
									   // filtrar só pelos subtipos maiores que o nível de assinatura do usuário
									   && gTipoSubDFoc.STD_NIVELASSINA >= iNivelAssinaUsuario
										   && gPass.PAS_CODIGOPASSAGEM == NumeroAtendimento
											&&
									   (
										  (iNivelAssinaUsuario == 2 && gDoc.DOC_IDUSUARIOASSINANIVEL2 == null)
									   || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 == null)
									   || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != idUsuarioAssina && gDoc.DOC_IDUSUARIOASSINANIVEL3_2 == null && gTipoSubDFoc.STD_CODIGOBARRA == "CPSI")
									   )
										 &&
										 (
											(iNivelAssinaUsuario == 2 && ACSGlobal.ListaGruposSetoresLogado.Contains(gDoc.DOC_IDGRUPOUSUARIOCAPTURA))
										 || (iNivelAssinaUsuario != 2 && gDoc.DOC_IDGRUPOUSUARIOCAPTURA != null)
										 )
										   &&
									   (DB.GEDDOCASSINANDO.Any(doa => doa.DOA_IDDOCUMENTO == gDoc.DOC_IDDOCUMENTO) == false)
										   select new GEDDocumentosNivel2
										   {
											   CPF_NOME = gCli.CPF_NOME,
											   CPF_CPF = gCli.CPF_CPF,
											   CPF_IDCLIENTEPF = gCli.CPF_IDCLIENTEPF,
											   CPF_REGISTRO = gCli.CPF_REGISTRO,

											   DOC_EXTENSAONOMEARQUIVO = gDoc.DOC_EXTENSAONOMEARQUIVO,
											   DOC_IDDOCUMENTO = gDoc.DOC_IDDOCUMENTO,
											   DOC_IDPASSAGEM = gDoc.DOC_IDPASSAGEM,
											   DOC_IDUSUARIOASSINANIVEL1 = gDoc.DOC_IDUSUARIOASSINANIVEL1,
											   DOC_IDUSUARIOASSINANIVEL2 = gDoc.DOC_IDUSUARIOASSINANIVEL2,
											   DOC_IDUSUARIOASSINANIVEL3 = gDoc.DOC_IDUSUARIOASSINANIVEL3,
											   DOC_IDUSUARIOASSINANIVEL3_2 = gDoc.DOC_IDUSUARIOASSINANIVEL3_2,
											   DOC_NOMEARQUIVO = gDoc.DOC_NOMEARQUIVO,
											   DOC_PATH = gDoc.DOC_PATH,
											   DOC_DIVISAO = gDiv.DIV_CODIGOREDUZIDO,
											   DOC_DATAHORACADASTRO = gDoc.DOC_DATAHORACADASTRO,
											   DOC_IDGRUPOUSUARIOCAPTURA = gDoc.DOC_IDGRUPOUSUARIOCAPTURA,

											   PAS_CODIGOPASSAGEM = gPass.PAS_CODIGOPASSAGEM,
											   PAS_DATAHORAPASSAGEM = gPass.PAS_DATAHORAPASSAGEM,
											   PAS_REGISTRO = gPass.PAS_REGISTRO,

											   STD_NOME = gTipoSubDFoc.STD_DESCRICAO,
											   STD_NIVELASSINA = gTipoSubDFoc.STD_NIVELASSINA,
											   STD_CODIGO = gTipoSubDFoc.STD_CODIGOBARRA,
											   //SET_IDSETOR = Set.SET_IDSETOR

										   }).ToList();


							//bloqueia os documentos consultados
							bloqueiaDocumentos(getList);

							return getList;


						}
						else
						{
							//consulta por numero atendimento e data
							var getList = (from gDoc in DB.GEDDOCUMENTOS


										   join Us in DB.GEDUSUARIOS on gDoc.DOC_IDUSUARIOACSCAPTURE equals Us.USR_IDUSUARIO
										   //join Gu in DB.GEDGRUPOSUSUARIOS on Us.USR_IDGRUPOUSUARIO equals Gu.GRP_IDGRUPOUSUARIO
										   join Gu in DB.GEDGRUPOSUSUARIOS on gDoc.DOC_IDGRUPOUSUARIOCAPTURA equals Gu.GRP_IDGRUPOUSUARIO
										   //join GuS in DB.GEDSETORXGRUPOUSUARIO on Gu.GRP_IDGRUPOUSUARIO equals GuS.SXG_IDGRUPOUSUARIO
										   //join Set in DB.GEDSETOR on GuS.SXG_IDSETOR equals Set.SET_IDSETOR

										   join gPass in DB.GEDPASSAGENS on gDoc.DOC_IDPASSAGEM equals gPass.PAS_IDPASSAGEM
										   join gCli in DB.GEDCLIENTEPF on gPass.PAS_REGISTRO equals gCli.CPF_REGISTRO
										   join gTipoDoc in DB.GEDTIPOSDOCUMENTOS on gDoc.DOC_IDTIPODOCUMENTO equals gTipoDoc.TPD_IDTIPODOCUMENTO
										   join gTipoSubDFoc in DB.GEDSUBTIPOSDOCUMENTOS on gDoc.DOC_IDSUBTIPODOCUMENTO equals gTipoSubDFoc.STD_IDSUBTIPOSDOCUMENTOS
										   join gDiv in DB.GEDDIVISOES on gTipoDoc.TPD_IDDIVISAO equals gDiv.DIV_IDDIVISAO

										   orderby gPass.PAS_DATAHORAPASSAGEM ascending, gDoc.DOC_IDDOCUMENTO

										   where gDoc.DOC_IDUSUARIOASSINANIVEL1 != null
											   // Fernando - 01/02/2017
											   // filtro pelo tipo de captura
											   && gDoc.DOC_TIPOCAPTURA == 1
									   // Fernando - 01/02/2017
									   // filtrar só pelos subtipos maiores que o nível de assinatura do usuário
									   && gTipoSubDFoc.STD_NIVELASSINA >= iNivelAssinaUsuario
										   && gPass.PAS_CODIGOPASSAGEM == NumeroAtendimento
										   && EntityFunctions.TruncateTime(gPass.PAS_DATAHORAPASSAGEM) == EntityFunctions.TruncateTime(dt)
											&&
									   (
										  (iNivelAssinaUsuario == 2 && gDoc.DOC_IDUSUARIOASSINANIVEL2 == null)
									   || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 == null)
									   || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != idUsuarioAssina && gDoc.DOC_IDUSUARIOASSINANIVEL3_2 == null && gTipoSubDFoc.STD_CODIGOBARRA == "CPSI")
									   )
										 &&
										 (
											(iNivelAssinaUsuario == 2 && ACSGlobal.ListaGruposSetoresLogado.Contains(gDoc.DOC_IDGRUPOUSUARIOCAPTURA))
										 || (iNivelAssinaUsuario != 2 && gDoc.DOC_IDGRUPOUSUARIOCAPTURA != null)
										 )
										   &&
									   (DB.GEDDOCASSINANDO.Any(doa => doa.DOA_IDDOCUMENTO == gDoc.DOC_IDDOCUMENTO) == false)
										   select new GEDDocumentosNivel2
										   {
											   CPF_NOME = gCli.CPF_NOME,
											   CPF_CPF = gCli.CPF_CPF,
											   CPF_IDCLIENTEPF = gCli.CPF_IDCLIENTEPF,
											   CPF_REGISTRO = gCli.CPF_REGISTRO,

											   DOC_EXTENSAONOMEARQUIVO = gDoc.DOC_EXTENSAONOMEARQUIVO,
											   DOC_IDDOCUMENTO = gDoc.DOC_IDDOCUMENTO,
											   DOC_IDPASSAGEM = gDoc.DOC_IDPASSAGEM,
											   DOC_IDUSUARIOASSINANIVEL1 = gDoc.DOC_IDUSUARIOASSINANIVEL1,
											   DOC_IDUSUARIOASSINANIVEL2 = gDoc.DOC_IDUSUARIOASSINANIVEL2,
											   DOC_IDUSUARIOASSINANIVEL3 = gDoc.DOC_IDUSUARIOASSINANIVEL3,
											   DOC_IDUSUARIOASSINANIVEL3_2 = gDoc.DOC_IDUSUARIOASSINANIVEL3_2,
											   DOC_NOMEARQUIVO = gDoc.DOC_NOMEARQUIVO,
											   DOC_PATH = gDoc.DOC_PATH,
											   DOC_DIVISAO = gDiv.DIV_CODIGOREDUZIDO,
											   DOC_DATAHORACADASTRO = gDoc.DOC_DATAHORACADASTRO,
											   DOC_IDGRUPOUSUARIOCAPTURA = gDoc.DOC_IDGRUPOUSUARIOCAPTURA,

											   PAS_CODIGOPASSAGEM = gPass.PAS_CODIGOPASSAGEM,
											   PAS_DATAHORAPASSAGEM = gPass.PAS_DATAHORAPASSAGEM,
											   PAS_REGISTRO = gPass.PAS_REGISTRO,

											   STD_NOME = gTipoSubDFoc.STD_DESCRICAO,
											   STD_NIVELASSINA = gTipoSubDFoc.STD_NIVELASSINA,
											   STD_CODIGO = gTipoSubDFoc.STD_CODIGOBARRA,

											   //SET_IDSETOR = Set.SET_IDSETOR

										   }).ToList();


							//bloqueia os documentos consultados
							bloqueiaDocumentos(getList);
							return getList;


						}
					}

					return new List<GEDDocumentosNivel2>();
				}
			}
			catch (Exception ex)
			{

				Exception e = new Exception("Ocorreu um Erro!", ex);
				throw ex;
			}
		}



		//N3
		public static List<GEDDocumentosNivel2> GetDocumentosAssN3(string NumeroAtendimento, string dataAtendimento, bool fVerTodos, decimal idUsuarioAssina, decimal iNivelAssinaUsuario)
		{
			try
			{
				using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
				{

					DateTime dt;
					bool fVerifica = DateTime.TryParse(dataAtendimento, out dt);
					if (!fVerifica)
					{
						dataAtendimento = string.Empty;
					}
					else
					{
						dt = Convert.ToDateTime(dataAtendimento);
					}

					dt = Convert.ToDateTime("2018/11/01");

					if (ACSGlobal.ListaGruposSetoresLogado == null)
						ACSGlobal.ListaGruposSetoresLogado = new List<decimal?>();

					if (fVerTodos)
					{
						try
						{
							var getSelect = (from gDoc in DB.GEDDOCUMENTOS

											 join Us in DB.GEDUSUARIOS on gDoc.DOC_IDUSUARIOACSCAPTURE equals Us.USR_IDUSUARIO

											 join Gu in DB.GEDGRUPOSUSUARIOS on gDoc.DOC_IDGRUPOUSUARIOCAPTURA equals Gu.GRP_IDGRUPOUSUARIO

											 join gPass in DB.GEDPASSAGENS on gDoc.DOC_IDPASSAGEM equals gPass.PAS_IDPASSAGEM
											 join gCli in DB.GEDCLIENTEPF on gPass.PAS_REGISTRO equals gCli.CPF_REGISTRO
											 join gTipoDoc in DB.GEDTIPOSDOCUMENTOS on gDoc.DOC_IDTIPODOCUMENTO equals gTipoDoc.TPD_IDTIPODOCUMENTO
											 join gTipoSubDFoc in DB.GEDSUBTIPOSDOCUMENTOS on gDoc.DOC_IDSUBTIPODOCUMENTO equals gTipoSubDFoc.STD_IDSUBTIPOSDOCUMENTOS
											 join gDiv in DB.GEDDIVISOES on gTipoDoc.TPD_IDDIVISAO equals gDiv.DIV_IDDIVISAO

											 where gDoc.DOC_TIPOCAPTURA == 1
											 && gTipoSubDFoc.STD_NIVELASSINA >= iNivelAssinaUsuario

											 &&
											 (

											(iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL3 == null)
											|| (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL3 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != idUsuarioAssina && gDoc.DOC_IDUSUARIOASSINANIVEL3_2 == null)
											 ) &&
										   (DB.GEDDOCASSINANDO.Any(doa => doa.DOA_IDDOCUMENTO == gDoc.DOC_IDDOCUMENTO) == false)

											 orderby gDoc.DOC_IDDOCUMENTO
											 select new GEDDocumentosNivel2
											 {
												 CPF_NOME = gCli.CPF_NOME,
												 CPF_CPF = gCli.CPF_CPF,
												 CPF_IDCLIENTEPF = gCli.CPF_IDCLIENTEPF,
												 CPF_REGISTRO = gCli.CPF_REGISTRO,

												 DOC_EXTENSAONOMEARQUIVO = gDoc.DOC_EXTENSAONOMEARQUIVO,
												 DOC_IDDOCUMENTO = gDoc.DOC_IDDOCUMENTO,
												 DOC_IDPASSAGEM = gDoc.DOC_IDPASSAGEM,
												 DOC_IDUSUARIOASSINANIVEL1 = gDoc.DOC_IDUSUARIOASSINANIVEL1,
												 DOC_IDUSUARIOASSINANIVEL2 = gDoc.DOC_IDUSUARIOASSINANIVEL2,
												 DOC_IDUSUARIOASSINANIVEL3 = gDoc.DOC_IDUSUARIOASSINANIVEL3,
												 DOC_IDUSUARIOASSINANIVEL3_2 = gDoc.DOC_IDUSUARIOASSINANIVEL3_2,
												 DOC_NOMEARQUIVO = gDoc.DOC_NOMEARQUIVO,
												 DOC_PATH = gDoc.DOC_PATH,
												 DOC_DIVISAO = gDiv.DIV_CODIGOREDUZIDO,
												 DOC_DATAHORACADASTRO = gDoc.DOC_DATAHORACADASTRO,
												 DOC_IDGRUPOUSUARIOCAPTURA = gDoc.DOC_IDGRUPOUSUARIOCAPTURA,

												 PAS_CODIGOPASSAGEM = gPass.PAS_CODIGOPASSAGEM,
												 PAS_DATAHORAPASSAGEM = gPass.PAS_DATAHORAPASSAGEM,
												 PAS_REGISTRO = gPass.PAS_REGISTRO,

												 STD_NOME = gTipoSubDFoc.STD_DESCRICAO,
												 STD_NIVELASSINA = gTipoSubDFoc.STD_NIVELASSINA,
												 STD_CODIGO = gTipoSubDFoc.STD_CODIGOBARRA,

												 //SET_IDSETOR = Set.SET_IDSETOR

											 });

							//if (iNivelAssinaUsuario == 2)
							//{
							//    getSelect = getSelect.Where(e => e.DOC_IDUSUARIOASSINANIVEL2 == null);
							//}
							//else if (iNivelAssinaUsuario == 3)
							//{
							//    getSelect = getSelect.Where(e => 1 == 1 && (e.DOC_IDUSUARIOASSINANIVEL3 == null
							//        || (e.DOC_IDUSUARIOASSINANIVEL3 != null && e.STD_CODIGO == "CPSI" && e.DOC_IDUSUARIOASSINANIVEL3_2 == null && e.DOC_IDUSUARIOASSINANIVEL3 == idUsuarioAssina)));
							//}


							var getList = getSelect.Take(1000).AsEnumerable();

							// MessageBox.Show("passou listagem"); 

							//return getList;
							//nova funcionalidade... fazer um top e pegar os documentos do ultimo registro por completo.

							if (getList == null || getList.Count() == 0)
								return getList.ToList();

							// remover os registros da último atendimento
							// necessário para evitar que estejam faltando documentos do atendimento quando seleciona os top 3000
							var idUltimaPassagem = getList.ToList()[getList.Count() - 1].DOC_IDPASSAGEM;

							var listaNova = getList.Where(c => c.DOC_IDPASSAGEM != idUltimaPassagem).ToList();

							//  MessageBox.Show("ultima passagem = " + idUltimaPassagem);

							// retornar os documentos do último atendimento removido
							getSelect = (

								from gDoc in DB.GEDDOCUMENTOS

								join Us in DB.GEDUSUARIOS on gDoc.DOC_IDUSUARIOACSCAPTURE equals Us.USR_IDUSUARIO
								//join Gu in DB.GEDGRUPOSUSUARIOS on Us.USR_IDGRUPOUSUARIO equals Gu.GRP_IDGRUPOUSUARIO
								join Gu in DB.GEDGRUPOSUSUARIOS on gDoc.DOC_IDGRUPOUSUARIOCAPTURA equals Gu.GRP_IDGRUPOUSUARIO
								//join GuS in DB.GEDSETORXGRUPOUSUARIO on Gu.GRP_IDGRUPOUSUARIO equals GuS.SXG_IDGRUPOUSUARIO
								//join Set in DB.GEDSETOR on GuS.SXG_IDSETOR equals Set.SET_IDSETOR

								join gPass in DB.GEDPASSAGENS on gDoc.DOC_IDPASSAGEM equals gPass.PAS_IDPASSAGEM
								join gCli in DB.GEDCLIENTEPF on gPass.PAS_REGISTRO equals gCli.CPF_REGISTRO
								join gTipoDoc in DB.GEDTIPOSDOCUMENTOS on gDoc.DOC_IDTIPODOCUMENTO equals gTipoDoc.TPD_IDTIPODOCUMENTO
								join gTipoSubDFoc in DB.GEDSUBTIPOSDOCUMENTOS on gDoc.DOC_IDSUBTIPODOCUMENTO equals gTipoSubDFoc.STD_IDSUBTIPOSDOCUMENTOS
								join gDiv in DB.GEDDIVISOES on gTipoDoc.TPD_IDDIVISAO equals gDiv.DIV_IDDIVISAO



								where gDoc.DOC_TIPOCAPTURA == 1 && gDoc.DOC_IDPASSAGEM == idUltimaPassagem  //1 - scanner e 2  - importado

								// Fernando - 01/02/2017
								// filtrar só pelos subtipos maiores que o nível de assinatura do usuário
								&& gTipoSubDFoc.STD_NIVELASSINA >= iNivelAssinaUsuario
								// Fernando - 01/02/2017
								// colocada ordenação por ID da passagem
								&&
								(
							   //SOMENTE NIVEL >=3 assina LUCAS - 28/11/2018
							   //     (iNivelAssinaUsuario == 2 && gDoc.DOC_IDUSUARIOASSINANIVEL2 == null)
							   //   || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 == null)
							   // || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != idUsuarioAssina && gDoc.DOC_IDUSUARIOASSINANIVEL3_2 == null && gTipoSubDFoc.STD_CODIGOBARRA == "CPSI")
							   (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL3 == null)
							   || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL3 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != idUsuarioAssina && gDoc.DOC_IDUSUARIOASSINANIVEL3_2 == null)
								)

								  //&&
								  //(
								  //   (iNivelAssinaUsuario == 2 && ACSGlobal.ListaGruposSetoresLogado.Contains(gDoc.DOC_IDGRUPOUSUARIOCAPTURA))
								  // || (iNivelAssinaUsuario != 2 && gDoc.DOC_IDGRUPOUSUARIOCAPTURA != null)
								  //)
								  &&
							  (DB.GEDDOCASSINANDO.Any(doa => doa.DOA_IDDOCUMENTO == gDoc.DOC_IDDOCUMENTO) == false)


								// Fernando - 01/02/2017
								// colocada ordenação por ID da passagem
								orderby gPass.PAS_DATAHORAPASSAGEM ascending, gDoc.DOC_IDPASSAGEM, gDoc.DOC_IDDOCUMENTO
								select new GEDDocumentosNivel2
								{
									CPF_NOME = gCli.CPF_NOME,
									CPF_CPF = gCli.CPF_CPF,
									CPF_IDCLIENTEPF = gCli.CPF_IDCLIENTEPF,
									CPF_REGISTRO = gCli.CPF_REGISTRO,

									DOC_EXTENSAONOMEARQUIVO = gDoc.DOC_EXTENSAONOMEARQUIVO,
									DOC_IDDOCUMENTO = gDoc.DOC_IDDOCUMENTO,
									DOC_IDPASSAGEM = gDoc.DOC_IDPASSAGEM,
									DOC_IDUSUARIOASSINANIVEL1 = gDoc.DOC_IDUSUARIOASSINANIVEL1,
									DOC_IDUSUARIOASSINANIVEL2 = gDoc.DOC_IDUSUARIOASSINANIVEL2,
									DOC_IDUSUARIOASSINANIVEL3 = gDoc.DOC_IDUSUARIOASSINANIVEL3,
									DOC_IDUSUARIOASSINANIVEL3_2 = gDoc.DOC_IDUSUARIOASSINANIVEL3_2,
									DOC_NOMEARQUIVO = gDoc.DOC_NOMEARQUIVO,
									DOC_PATH = gDoc.DOC_PATH,
									DOC_DIVISAO = gDiv.DIV_CODIGOREDUZIDO,
									DOC_DATAHORACADASTRO = gDoc.DOC_DATAHORACADASTRO,
									DOC_IDGRUPOUSUARIOCAPTURA = gDoc.DOC_IDGRUPOUSUARIOCAPTURA,

									PAS_CODIGOPASSAGEM = gPass.PAS_CODIGOPASSAGEM,
									PAS_DATAHORAPASSAGEM = gPass.PAS_DATAHORAPASSAGEM,
									PAS_REGISTRO = gPass.PAS_REGISTRO,

									STD_NOME = gTipoSubDFoc.STD_DESCRICAO,
									STD_NIVELASSINA = gTipoSubDFoc.STD_NIVELASSINA,
									STD_CODIGO = gTipoSubDFoc.STD_CODIGOBARRA,

									//SET_IDSETOR = Set.SET_IDSETOR

								});

							//if (iNivelAssinaUsuario == 2)
							//{
							//    getSelect = getSelect.Where(e => e.DOC_IDUSUARIOASSINANIVEL2 == null);
							//}
							//else if (iNivelAssinaUsuario == 3)
							//{
							//    getSelect = getSelect.Where(e => 1 == 1 && (e.DOC_IDUSUARIOASSINANIVEL3 == null
							//        || (e.DOC_IDUSUARIOASSINANIVEL3 != null && e.STD_CODIGO == "CPSI" && e.DOC_IDUSUARIOASSINANIVEL3_2 == null && e.DOC_IDUSUARIOASSINANIVEL3 == idUsuarioAssina)));
							//}

							getList = getSelect;

							listaNova.AddRange(getList);


							//   MessageBox.Show("passou listagem 2");
							//bloqueia os documentos consultados
							bloqueiaDocumentos(listaNova);

							//   MessageBox.Show("Bloqueio");
							return listaNova;
						}
						catch (EntitySqlException ex)
						{

							MessageBox.Show(ex.Message);
							throw;
						}
						catch (Exception ex)
						{

							MessageBox.Show(ex.Message);
							throw;
						}

					}
					else
					{

						//verifica se vai usar os dois campos oou so 1 data e numero atendimento
						if (string.IsNullOrEmpty(NumeroAtendimento) && !string.IsNullOrEmpty(dataAtendimento))
						{
							//consulta por data
							var getList = (from gDoc in DB.GEDDOCUMENTOS


										   join Us in DB.GEDUSUARIOS on gDoc.DOC_IDUSUARIOACSCAPTURE equals Us.USR_IDUSUARIO
										   //join Gu in DB.GEDGRUPOSUSUARIOS on Us.USR_IDGRUPOUSUARIO equals Gu.GRP_IDGRUPOUSUARIO
										   join Gu in DB.GEDGRUPOSUSUARIOS on gDoc.DOC_IDGRUPOUSUARIOCAPTURA equals Gu.GRP_IDGRUPOUSUARIO
										   //join GuS in DB.GEDSETORXGRUPOUSUARIO on Gu.GRP_IDGRUPOUSUARIO equals GuS.SXG_IDGRUPOUSUARIO
										   //join Set in DB.GEDSETOR on GuS.SXG_IDSETOR equals Set.SET_IDSETOR

										   join gPass in DB.GEDPASSAGENS on gDoc.DOC_IDPASSAGEM equals gPass.PAS_IDPASSAGEM
										   join gCli in DB.GEDCLIENTEPF on gPass.PAS_REGISTRO equals gCli.CPF_REGISTRO
										   join gTipoDoc in DB.GEDTIPOSDOCUMENTOS on gDoc.DOC_IDTIPODOCUMENTO equals gTipoDoc.TPD_IDTIPODOCUMENTO
										   join gTipoSubDFoc in DB.GEDSUBTIPOSDOCUMENTOS on gDoc.DOC_IDSUBTIPODOCUMENTO equals gTipoSubDFoc.STD_IDSUBTIPOSDOCUMENTOS
										   join gDiv in DB.GEDDIVISOES on gTipoDoc.TPD_IDDIVISAO equals gDiv.DIV_IDDIVISAO


										   where gDoc.DOC_TIPOCAPTURA == 1
											 && EntityFunctions.TruncateTime(gPass.PAS_DATAHORAPASSAGEM) == EntityFunctions.TruncateTime(dt)
									   // Fernando - 01/02/2017
									   // filtrar só pelos subtipos maiores que o nível de assinatura do usuário
									   && gTipoSubDFoc.STD_NIVELASSINA >= iNivelAssinaUsuario

										&&
									   (
										//SOMENTE NIVEL >=3 assina LUCAS - 28/11/2018
										//     (iNivelAssinaUsuario == 2 && gDoc.DOC_IDUSUARIOASSINANIVEL2 == null)
										//   || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 == null)
										// || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != idUsuarioAssina && gDoc.DOC_IDUSUARIOASSINANIVEL3_2 == null && gTipoSubDFoc.STD_CODIGOBARRA == "CPSI")
										(iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL3 == null)
										|| (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL3 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != idUsuarioAssina && gDoc.DOC_IDUSUARIOASSINANIVEL3_2 == null
										&& gTipoSubDFoc.STD_CODIGOBARRA == "CPSI")
										 )

										   //&&
										   //(
										   //   (iNivelAssinaUsuario == 2 && ACSGlobal.ListaGruposSetoresLogado.Contains(gDoc.DOC_IDGRUPOUSUARIOCAPTURA))
										   // || (iNivelAssinaUsuario != 2 && gDoc.DOC_IDGRUPOUSUARIOCAPTURA != null)
										   //)
										   &&
									   (DB.GEDDOCASSINANDO.Any(doa => doa.DOA_IDDOCUMENTO == gDoc.DOC_IDDOCUMENTO) == false)


										   // Fernando - 01/02/2017
										   // colocada ordenação por ID da passagem
										   orderby gPass.PAS_DATAHORAPASSAGEM ascending, gDoc.DOC_IDPASSAGEM, gDoc.DOC_IDDOCUMENTO

										   select new GEDDocumentosNivel2
										   {
											   CPF_NOME = gCli.CPF_NOME,
											   CPF_CPF = gCli.CPF_CPF,
											   CPF_IDCLIENTEPF = gCli.CPF_IDCLIENTEPF,
											   CPF_REGISTRO = gCli.CPF_REGISTRO,

											   DOC_EXTENSAONOMEARQUIVO = gDoc.DOC_EXTENSAONOMEARQUIVO,
											   DOC_IDDOCUMENTO = gDoc.DOC_IDDOCUMENTO,
											   DOC_IDPASSAGEM = gDoc.DOC_IDPASSAGEM,
											   DOC_IDUSUARIOASSINANIVEL1 = gDoc.DOC_IDUSUARIOASSINANIVEL1,
											   DOC_IDUSUARIOASSINANIVEL2 = gDoc.DOC_IDUSUARIOASSINANIVEL2,
											   DOC_IDUSUARIOASSINANIVEL3 = gDoc.DOC_IDUSUARIOASSINANIVEL3,
											   DOC_IDUSUARIOASSINANIVEL3_2 = gDoc.DOC_IDUSUARIOASSINANIVEL3_2,
											   DOC_NOMEARQUIVO = gDoc.DOC_NOMEARQUIVO,
											   DOC_PATH = gDoc.DOC_PATH,
											   DOC_DIVISAO = gDiv.DIV_CODIGOREDUZIDO,
											   DOC_DATAHORACADASTRO = gDoc.DOC_DATAHORACADASTRO,
											   DOC_IDGRUPOUSUARIOCAPTURA = gDoc.DOC_IDGRUPOUSUARIOCAPTURA,

											   PAS_CODIGOPASSAGEM = gPass.PAS_CODIGOPASSAGEM,
											   PAS_DATAHORAPASSAGEM = gPass.PAS_DATAHORAPASSAGEM,
											   PAS_REGISTRO = gPass.PAS_REGISTRO,

											   STD_NOME = gTipoSubDFoc.STD_DESCRICAO,
											   STD_NIVELASSINA = gTipoSubDFoc.STD_NIVELASSINA,
											   STD_CODIGO = gTipoSubDFoc.STD_CODIGOBARRA,


											   //SET_IDSETOR = Set.SET_IDSETOR
										   }).Take(1000).ToList();

							if (getList == null || getList.Count() == 0)
								return getList;

							// remover os registros da último atendimento
							// necessário para evitar que estejam faltando documentos do atendimento quando seleciona os top 3000
							var idUltimaPassagem = getList[getList.Count() - 1].DOC_IDPASSAGEM;

							var listaNova = getList.Where(c => c.DOC_IDPASSAGEM != idUltimaPassagem).ToList();

							// retornar os documentos do último atendimento removido
							getList = (from gDoc in DB.GEDDOCUMENTOS

									   join Us in DB.GEDUSUARIOS on gDoc.DOC_IDUSUARIOACSCAPTURE equals Us.USR_IDUSUARIO
									   //join Gu in DB.GEDGRUPOSUSUARIOS on Us.USR_IDGRUPOUSUARIO equals Gu.GRP_IDGRUPOUSUARIO
									   join Gu in DB.GEDGRUPOSUSUARIOS on gDoc.DOC_IDGRUPOUSUARIOCAPTURA equals Gu.GRP_IDGRUPOUSUARIO
									   //join GuS in DB.GEDSETORXGRUPOUSUARIO on Gu.GRP_IDGRUPOUSUARIO equals GuS.SXG_IDGRUPOUSUARIO
									   //join Set in DB.GEDSETOR on GuS.SXG_IDSETOR equals Set.SET_IDSETOR

									   join gPass in DB.GEDPASSAGENS on gDoc.DOC_IDPASSAGEM equals gPass.PAS_IDPASSAGEM
									   join gCli in DB.GEDCLIENTEPF on gPass.PAS_REGISTRO equals gCli.CPF_REGISTRO
									   join gTipoDoc in DB.GEDTIPOSDOCUMENTOS on gDoc.DOC_IDTIPODOCUMENTO equals gTipoDoc.TPD_IDTIPODOCUMENTO
									   join gTipoSubDFoc in DB.GEDSUBTIPOSDOCUMENTOS on gDoc.DOC_IDSUBTIPODOCUMENTO equals gTipoSubDFoc.STD_IDSUBTIPOSDOCUMENTOS
									   join gDiv in DB.GEDDIVISOES on gTipoDoc.TPD_IDDIVISAO equals gDiv.DIV_IDDIVISAO

									   where gDoc.DOC_TIPOCAPTURA == 1
										 && EntityFunctions.TruncateTime(gPass.PAS_DATAHORAPASSAGEM) == EntityFunctions.TruncateTime(dt)
									   // Fernando - 01/02/2017
									   // filtrar só pelos subtipos maiores que o nível de assinatura do usuário
									   && gTipoSubDFoc.STD_NIVELASSINA >= iNivelAssinaUsuario
									   // Fernando - 01/02/2017
									   // filtrar pelo id da passagem removido
									   && gDoc.DOC_IDPASSAGEM == idUltimaPassagem
										&&
									   (
										//SOMENTE NIVEL >=3 assina LUCAS - 28/11/2018
										//     (iNivelAssinaUsuario == 2 && gDoc.DOC_IDUSUARIOASSINANIVEL2 == null)
										//   || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 == null)
										// || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != idUsuarioAssina && gDoc.DOC_IDUSUARIOASSINANIVEL3_2 == null && gTipoSubDFoc.STD_CODIGOBARRA == "CPSI")
										(iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL3 == null)
										|| (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL3 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != idUsuarioAssina && gDoc.DOC_IDUSUARIOASSINANIVEL3_2 == null
										&& gTipoSubDFoc.STD_CODIGOBARRA == "CPSI")
										 )

										   //&&
										   //(
										   //   (iNivelAssinaUsuario == 2 && ACSGlobal.ListaGruposSetoresLogado.Contains(gDoc.DOC_IDGRUPOUSUARIOCAPTURA))
										   // || (iNivelAssinaUsuario != 2 && gDoc.DOC_IDGRUPOUSUARIOCAPTURA != null)
										   //)
										   &&
									   (DB.GEDDOCASSINANDO.Any(doa => doa.DOA_IDDOCUMENTO == gDoc.DOC_IDDOCUMENTO) == false)
									   // Fernando - 01/02/2017

									   orderby gPass.PAS_DATAHORAPASSAGEM ascending, gDoc.DOC_IDPASSAGEM, gDoc.DOC_IDDOCUMENTO

									   select new GEDDocumentosNivel2
									   {
										   CPF_NOME = gCli.CPF_NOME,
										   CPF_CPF = gCli.CPF_CPF,
										   CPF_IDCLIENTEPF = gCli.CPF_IDCLIENTEPF,
										   CPF_REGISTRO = gCli.CPF_REGISTRO,

										   DOC_EXTENSAONOMEARQUIVO = gDoc.DOC_EXTENSAONOMEARQUIVO,
										   DOC_IDDOCUMENTO = gDoc.DOC_IDDOCUMENTO,
										   DOC_IDPASSAGEM = gDoc.DOC_IDPASSAGEM,
										   DOC_IDUSUARIOASSINANIVEL1 = gDoc.DOC_IDUSUARIOASSINANIVEL1,
										   DOC_IDUSUARIOASSINANIVEL2 = gDoc.DOC_IDUSUARIOASSINANIVEL2,
										   DOC_IDUSUARIOASSINANIVEL3 = gDoc.DOC_IDUSUARIOASSINANIVEL3,
										   DOC_IDUSUARIOASSINANIVEL3_2 = gDoc.DOC_IDUSUARIOASSINANIVEL3_2,
										   DOC_NOMEARQUIVO = gDoc.DOC_NOMEARQUIVO,
										   DOC_PATH = gDoc.DOC_PATH,
										   DOC_DIVISAO = gDiv.DIV_CODIGOREDUZIDO,
										   DOC_DATAHORACADASTRO = gDoc.DOC_DATAHORACADASTRO,
										   DOC_IDGRUPOUSUARIOCAPTURA = gDoc.DOC_IDGRUPOUSUARIOCAPTURA,

										   PAS_CODIGOPASSAGEM = gPass.PAS_CODIGOPASSAGEM,
										   PAS_DATAHORAPASSAGEM = gPass.PAS_DATAHORAPASSAGEM,
										   PAS_REGISTRO = gPass.PAS_REGISTRO,

										   STD_NOME = gTipoSubDFoc.STD_DESCRICAO,
										   STD_NIVELASSINA = gTipoSubDFoc.STD_NIVELASSINA,
										   STD_CODIGO = gTipoSubDFoc.STD_CODIGOBARRA,

										   //SET_IDSETOR = Set.SET_IDSETOR

									   }).ToList();

							listaNova.AddRange(getList);


							//bloqueia os documentos consultados
							bloqueiaDocumentos(listaNova);

							return listaNova;

						}
						else if (!string.IsNullOrEmpty(NumeroAtendimento) && string.IsNullOrEmpty(dataAtendimento))
						{
							//consulta por numero atendimento
							var getList = (from gDoc in DB.GEDDOCUMENTOS

										   join Us in DB.GEDUSUARIOS on gDoc.DOC_IDUSUARIOACSCAPTURE equals Us.USR_IDUSUARIO
										   //join Gu in DB.GEDGRUPOSUSUARIOS on Us.USR_IDGRUPOUSUARIO equals Gu.GRP_IDGRUPOUSUARIO
										   join Gu in DB.GEDGRUPOSUSUARIOS on gDoc.DOC_IDGRUPOUSUARIOCAPTURA equals Gu.GRP_IDGRUPOUSUARIO
										   //join GuS in DB.GEDSETORXGRUPOUSUARIO on Gu.GRP_IDGRUPOUSUARIO equals GuS.SXG_IDGRUPOUSUARIO
										   //join Set in DB.GEDSETOR on GuS.SXG_IDSETOR equals Set.SET_IDSETOR

										   join gPass in DB.GEDPASSAGENS on gDoc.DOC_IDPASSAGEM equals gPass.PAS_IDPASSAGEM
										   join gCli in DB.GEDCLIENTEPF on gPass.PAS_REGISTRO equals gCli.CPF_REGISTRO
										   join gTipoDoc in DB.GEDTIPOSDOCUMENTOS on gDoc.DOC_IDTIPODOCUMENTO equals gTipoDoc.TPD_IDTIPODOCUMENTO
										   join gTipoSubDFoc in DB.GEDSUBTIPOSDOCUMENTOS on gDoc.DOC_IDSUBTIPODOCUMENTO equals gTipoSubDFoc.STD_IDSUBTIPOSDOCUMENTOS
										   join gDiv in DB.GEDDIVISOES on gTipoDoc.TPD_IDDIVISAO equals gDiv.DIV_IDDIVISAO


										   orderby gPass.PAS_DATAHORAPASSAGEM ascending, gDoc.DOC_IDDOCUMENTO

										   where //gDoc.DOC_IDUSUARIOASSINANIVEL1 != null
												 // Fernando - 01/02/2017
												 // filtro pelo tipo de captura
												gDoc.DOC_TIPOCAPTURA == 1
									   // Fernando - 01/02/2017
									   // filtrar só pelos subtipos maiores que o nível de assinatura do usuário
									   && gTipoSubDFoc.STD_NIVELASSINA >= iNivelAssinaUsuario
										   && gPass.PAS_CODIGOPASSAGEM == NumeroAtendimento
											&&
									   (
										//SOMENTE NIVEL >=3 assina LUCAS - 28/11/2018
										//     (iNivelAssinaUsuario == 2 && gDoc.DOC_IDUSUARIOASSINANIVEL2 == null)
										//   || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 == null)
										// || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != idUsuarioAssina && gDoc.DOC_IDUSUARIOASSINANIVEL3_2 == null && gTipoSubDFoc.STD_CODIGOBARRA == "CPSI")
										(iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL3 == null)
										|| (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL3 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != idUsuarioAssina && gDoc.DOC_IDUSUARIOASSINANIVEL3_2 == null
										&& gTipoSubDFoc.STD_CODIGOBARRA == "CPSI")
										 )

										   //&&
										   //(
										   //   (iNivelAssinaUsuario == 2 && ACSGlobal.ListaGruposSetoresLogado.Contains(gDoc.DOC_IDGRUPOUSUARIOCAPTURA))
										   // || (iNivelAssinaUsuario != 2 && gDoc.DOC_IDGRUPOUSUARIOCAPTURA != null)
										   //)
										   &&
									   (DB.GEDDOCASSINANDO.Any(doa => doa.DOA_IDDOCUMENTO == gDoc.DOC_IDDOCUMENTO) == false)
										   select new GEDDocumentosNivel2
										   {
											   CPF_NOME = gCli.CPF_NOME,
											   CPF_CPF = gCli.CPF_CPF,
											   CPF_IDCLIENTEPF = gCli.CPF_IDCLIENTEPF,
											   CPF_REGISTRO = gCli.CPF_REGISTRO,

											   DOC_EXTENSAONOMEARQUIVO = gDoc.DOC_EXTENSAONOMEARQUIVO,
											   DOC_IDDOCUMENTO = gDoc.DOC_IDDOCUMENTO,
											   DOC_IDPASSAGEM = gDoc.DOC_IDPASSAGEM,
											   DOC_IDUSUARIOASSINANIVEL1 = gDoc.DOC_IDUSUARIOASSINANIVEL1,
											   DOC_IDUSUARIOASSINANIVEL2 = gDoc.DOC_IDUSUARIOASSINANIVEL2,
											   DOC_IDUSUARIOASSINANIVEL3 = gDoc.DOC_IDUSUARIOASSINANIVEL3,
											   DOC_IDUSUARIOASSINANIVEL3_2 = gDoc.DOC_IDUSUARIOASSINANIVEL3_2,
											   DOC_NOMEARQUIVO = gDoc.DOC_NOMEARQUIVO,
											   DOC_PATH = gDoc.DOC_PATH,
											   DOC_DIVISAO = gDiv.DIV_CODIGOREDUZIDO,
											   DOC_DATAHORACADASTRO = gDoc.DOC_DATAHORACADASTRO,
											   DOC_IDGRUPOUSUARIOCAPTURA = gDoc.DOC_IDGRUPOUSUARIOCAPTURA,

											   PAS_CODIGOPASSAGEM = gPass.PAS_CODIGOPASSAGEM,
											   PAS_DATAHORAPASSAGEM = gPass.PAS_DATAHORAPASSAGEM,
											   PAS_REGISTRO = gPass.PAS_REGISTRO,

											   STD_NOME = gTipoSubDFoc.STD_DESCRICAO,
											   STD_NIVELASSINA = gTipoSubDFoc.STD_NIVELASSINA,
											   STD_CODIGO = gTipoSubDFoc.STD_CODIGOBARRA,
											   //SET_IDSETOR = Set.SET_IDSETOR

										   }).ToList();


							//bloqueia os documentos consultados
							bloqueiaDocumentos(getList);

							return getList;


						}
						else
						{
							//consulta por numero atendimento e data
							var getList = (from gDoc in DB.GEDDOCUMENTOS


										   join Us in DB.GEDUSUARIOS on gDoc.DOC_IDUSUARIOACSCAPTURE equals Us.USR_IDUSUARIO
										   //join Gu in DB.GEDGRUPOSUSUARIOS on Us.USR_IDGRUPOUSUARIO equals Gu.GRP_IDGRUPOUSUARIO
										   join Gu in DB.GEDGRUPOSUSUARIOS on gDoc.DOC_IDGRUPOUSUARIOCAPTURA equals Gu.GRP_IDGRUPOUSUARIO
										   //join GuS in DB.GEDSETORXGRUPOUSUARIO on Gu.GRP_IDGRUPOUSUARIO equals GuS.SXG_IDGRUPOUSUARIO
										   //join Set in DB.GEDSETOR on GuS.SXG_IDSETOR equals Set.SET_IDSETOR

										   join gPass in DB.GEDPASSAGENS on gDoc.DOC_IDPASSAGEM equals gPass.PAS_IDPASSAGEM
										   join gCli in DB.GEDCLIENTEPF on gPass.PAS_REGISTRO equals gCli.CPF_REGISTRO
										   join gTipoDoc in DB.GEDTIPOSDOCUMENTOS on gDoc.DOC_IDTIPODOCUMENTO equals gTipoDoc.TPD_IDTIPODOCUMENTO
										   join gTipoSubDFoc in DB.GEDSUBTIPOSDOCUMENTOS on gDoc.DOC_IDSUBTIPODOCUMENTO equals gTipoSubDFoc.STD_IDSUBTIPOSDOCUMENTOS
										   join gDiv in DB.GEDDIVISOES on gTipoDoc.TPD_IDDIVISAO equals gDiv.DIV_IDDIVISAO

										   orderby gPass.PAS_DATAHORAPASSAGEM ascending, gDoc.DOC_IDDOCUMENTO

										   where gDoc.DOC_TIPOCAPTURA == 1
									   // Fernando - 01/02/2017
									   // filtrar só pelos subtipos maiores que o nível de assinatura do usuário
									   && gTipoSubDFoc.STD_NIVELASSINA >= iNivelAssinaUsuario
										   && gPass.PAS_CODIGOPASSAGEM == NumeroAtendimento
										   && EntityFunctions.TruncateTime(gPass.PAS_DATAHORAPASSAGEM) == EntityFunctions.TruncateTime(dt)
											&&
									   (
										//SOMENTE NIVEL >=3 assina LUCAS - 28/11/2018
										//     (iNivelAssinaUsuario == 2 && gDoc.DOC_IDUSUARIOASSINANIVEL2 == null)
										//   || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 == null)
										// || (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL2 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != idUsuarioAssina && gDoc.DOC_IDUSUARIOASSINANIVEL3_2 == null && gTipoSubDFoc.STD_CODIGOBARRA == "CPSI")
										(iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL3 == null)
										|| (iNivelAssinaUsuario == 3 && gDoc.DOC_IDUSUARIOASSINANIVEL3 != null && gDoc.DOC_IDUSUARIOASSINANIVEL3 != idUsuarioAssina && gDoc.DOC_IDUSUARIOASSINANIVEL3_2 == null
										&& gTipoSubDFoc.STD_CODIGOBARRA == "CPSI")
										 )

										   //&&
										   //(
										   //   (iNivelAssinaUsuario == 2 && ACSGlobal.ListaGruposSetoresLogado.Contains(gDoc.DOC_IDGRUPOUSUARIOCAPTURA))
										   // || (iNivelAssinaUsuario != 2 && gDoc.DOC_IDGRUPOUSUARIOCAPTURA != null)
										   //)
										   &&
									   (DB.GEDDOCASSINANDO.Any(doa => doa.DOA_IDDOCUMENTO == gDoc.DOC_IDDOCUMENTO) == false)
										   select new GEDDocumentosNivel2
										   {
											   CPF_NOME = gCli.CPF_NOME,
											   CPF_CPF = gCli.CPF_CPF,
											   CPF_IDCLIENTEPF = gCli.CPF_IDCLIENTEPF,
											   CPF_REGISTRO = gCli.CPF_REGISTRO,

											   DOC_EXTENSAONOMEARQUIVO = gDoc.DOC_EXTENSAONOMEARQUIVO,
											   DOC_IDDOCUMENTO = gDoc.DOC_IDDOCUMENTO,
											   DOC_IDPASSAGEM = gDoc.DOC_IDPASSAGEM,
											   DOC_IDUSUARIOASSINANIVEL1 = gDoc.DOC_IDUSUARIOASSINANIVEL1,
											   DOC_IDUSUARIOASSINANIVEL2 = gDoc.DOC_IDUSUARIOASSINANIVEL2,
											   DOC_IDUSUARIOASSINANIVEL3 = gDoc.DOC_IDUSUARIOASSINANIVEL3,
											   DOC_IDUSUARIOASSINANIVEL3_2 = gDoc.DOC_IDUSUARIOASSINANIVEL3_2,
											   DOC_NOMEARQUIVO = gDoc.DOC_NOMEARQUIVO,
											   DOC_PATH = gDoc.DOC_PATH,
											   DOC_DIVISAO = gDiv.DIV_CODIGOREDUZIDO,
											   DOC_DATAHORACADASTRO = gDoc.DOC_DATAHORACADASTRO,
											   DOC_IDGRUPOUSUARIOCAPTURA = gDoc.DOC_IDGRUPOUSUARIOCAPTURA,

											   PAS_CODIGOPASSAGEM = gPass.PAS_CODIGOPASSAGEM,
											   PAS_DATAHORAPASSAGEM = gPass.PAS_DATAHORAPASSAGEM,
											   PAS_REGISTRO = gPass.PAS_REGISTRO,

											   STD_NOME = gTipoSubDFoc.STD_DESCRICAO,
											   STD_NIVELASSINA = gTipoSubDFoc.STD_NIVELASSINA,
											   STD_CODIGO = gTipoSubDFoc.STD_CODIGOBARRA,

											   //SET_IDSETOR = Set.SET_IDSETOR

										   }).ToList();


							//bloqueia os documentos consultados
							bloqueiaDocumentos(getList);
							return getList;


						}
					}

					return new List<GEDDocumentosNivel2>();
				}
			}
			catch (Exception ex)
			{

				Exception e = new Exception("Ocorreu um Erro!", ex);
				throw ex;
			}
		}




		public static async void deleteBloqueiaDocumentosLote(int idUsuarioAssina, List<GEDDocumentosNivel2> listaPesquisa)
		{
			try
			{
				using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
				{
					foreach (var item in listaPesquisa)
					{

						var itemDocumento = DB.GEDDOCASSINANDO.Where(c => c.DOA_IDUSUARIOASSINA == idUsuarioAssina && c.DOA_IDDOCUMENTO == item.DOC_IDDOCUMENTO).FirstOrDefault();
						if (itemDocumento != null)
						{
							DB.GEDDOCASSINANDO.DeleteObject(itemDocumento);
							DB.SaveChanges();

						}


					}

				}
			}


			catch (Exception ex)
			{

				throw;
			}
		}


		public static async void deleteBloqueiaDocumentos(int idUsuarioAssina, int idDocumento)
		{
			try
			{
				using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
				{

					var itemDocumento = DB.GEDDOCASSINANDO.Where(c => c.DOA_IDUSUARIOASSINA == idUsuarioAssina && c.DOA_IDDOCUMENTO == idDocumento).FirstOrDefault();

					DB.GEDDOCASSINANDO.DeleteObject(itemDocumento);
					DB.SaveChanges();


				}
			}


			catch (Exception ex)
			{

				throw;
			}
		}


		public static async void bloqueiaDocumentos(List<GEDDocumentosNivel2> listaDocumentos)
		{
			try
			{
				using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
				{



					foreach (var item in listaDocumentos)
					{

						var docAssinando = new GEDDOCASSINANDO()
						{
							DOA_BLOQUEADO = 1,
							DOA_DATACONSULTA = DateTime.Now,
							DOA_IDDOCUMENTO = item.DOC_IDDOCUMENTO,
							DOA_IDUSUARIOASSINA = ACSGlobal.UsuarioLogado.USR_IDUSUARIO
						};

						DB.GEDDOCASSINANDO.AddObject(docAssinando);
						DB.SaveChanges();


					}



				}
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public static bool UpdateDocumentoAssinadoN2(decimal IdUsuarioN2, decimal IdDocumento, string sDetailsCert)
		{
			try
			{
				string Details = "";
				using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
				{

					var documento = DB.GEDDOCUMENTOS.Where(c => c.DOC_IDDOCUMENTO == IdDocumento).FirstOrDefault();

					Details += documento.DOC_DETAIL_CERT.Replace("</Certs>", "") + sDetailsCert;

					documento.DOC_IDUSUARIOASSINANIVEL2 = IdUsuarioN2;
					documento.DOC_DATAASSINANIVEL2 = DateTime.Now;
					documento.DOC_DETAIL_CERT = Details;

					DB.SaveChanges();




				}
				//delete boqueio de documento
				deleteBloqueiaDocumentos((int)IdUsuarioN2, (int)IdDocumento);
				return true;
			}
			catch (Exception ex)
			{
				//delete boqueio de documento
				deleteBloqueiaDocumentos((int)IdUsuarioN2, (int)IdDocumento);

				throw;
			}

		}

		public static bool UpdateDocumentoAssinadoN3(decimal IdUsuarioN3, decimal IdDocumento, string sDetailsCert, bool first = true)
		{
			try
			{
				string Details = "";
				using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
				{

					var documento = DB.GEDDOCUMENTOS.Where(c => c.DOC_IDDOCUMENTO == IdDocumento).FirstOrDefault();
					if (documento.DOC_DETAIL_CERT == null) documento.DOC_DETAIL_CERT = "";
					Details += documento.DOC_DETAIL_CERT.Replace("</Certs>", "") + sDetailsCert;
					if (first)
					{
						documento.DOC_IDUSUARIOASSINANIVEL3 = IdUsuarioN3;
						documento.DOC_DATAASSINANIVEL3 = DateTime.Now;
					}
					else
					{
						documento.DOC_IDUSUARIOASSINANIVEL3_2 = IdUsuarioN3;
						documento.DOC_DATAASSINANIVEL3_2 = DateTime.Now;
					}

					documento.DOC_DETAIL_CERT = Details;

					DB.SaveChanges();
				}


				//delete boqueio de documento
				deleteBloqueiaDocumentos((int)IdUsuarioN3, (int)IdDocumento);
				return true;

			}
			catch (Exception ex)
			{
				//delete boqueio de documento
				deleteBloqueiaDocumentos((int)IdUsuarioN3, (int)IdDocumento);
				throw;
			}
		}




		public static List<GEDAREAS> GetAreasUsuario(int IdUsuario)
		{
			return ExecuteCommand<List<GEDAREAS>>(() =>
			{
				using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
				{

					var areas = (from A in DB.GEDAREAS
								 join UA in DB.GEDUNIDADEXAREAS on A.ARE_IDAREA equals UA.UXA_IDAREA
								 join UUA in DB.GEDUSUARIOSXUNIDADESXAREAS on UA.UXA_IDUNIDADEXAREA equals UUA.UXU_IDUNIDADEXAREA
								 where UUA.UXU_IDUSUARIO == IdUsuario
								 select A).ToList();

					return areas;
				}
			});

		}


		public static List<GEDDIVISOES> GetDivisoesUsuario(int IdArea)
		{
			return ExecuteCommand<List<GEDDIVISOES>>(() =>
			{
				using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
				{

					var areas = (from A in DB.GEDDIVISOES

								 where A.DIV_IDAREA == IdArea
								 select A).ToList();

					return areas;
				}
			});

		}


		public static List<GEDTIPOCLIENTE> GetTipoClienteArea(int IdArea)
		{
			return ExecuteCommand<List<GEDTIPOCLIENTE>>(() =>
			{
				using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
				{

					var TiposClientes = (from A in DB.GEDTIPOCLIENTE

										 where A.TIC_IDAREA == IdArea
										 select A).ToList();

					return TiposClientes;
				}
			});

		}


		public static GEDLABELGENERICO GetLabelGenerico(int IdArea)
		{
			return ExecuteCommand<GEDLABELGENERICO>(() =>
			{
				using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
				{

					var labels = (from A in DB.GEDAREAS
								  join L in DB.GEDLABELGENERICO on A.ARE_IDLABELGENERICO equals L.LAB_IDLABELGENERICO
								  where A.ARE_IDAREA == IdArea
								  select L).FirstOrDefault();

					return labels;
				}
			});

		}





		public static decimal? VerificaDocumentoAprovar(int? idSubTipoDocumento)
		{

			using (var DB = new EntitiesOracle(ACSConfig.GetConnection().ConnectioStringOracle))
			{

				var subTD = (from A in DB.GEDSUBTIPOSDOCUMENTOS

							 where A.STD_IDSUBTIPOSDOCUMENTOS == idSubTipoDocumento
							 select A).FirstOrDefault();

				return (subTD.STD_FLAGAPROVADOC == 1) ? 0 : 1;
			}

		}

		public static AuthyCliente validaClienteAtivoNuvem(string codigoCliente)
		{
			try
			{

				using (IDbConnection _dbDapper = new SqlConnection(ACSConfig.GetConnection().CONNECTIONSTRINGDAPPERAUTH))
				{

					StringBuilder str = new StringBuilder();
					str.Append(" SELECT [AHC_Id] ,[AHC_NomeCliente] ,[AHC_CodigoCliente] ,[AHC_DataCadastro] ,[AHC_fDeletado] ,[AHC_DataBloqueio], AHC_fBloqueado , AHC_MotivoBloqueio");
					str.Append(" FROM  [AuthyCliente] where  AHC_CodigoCliente = AHC_CodigoCliente ");

					var retorno = _dbDapper.Query<AuthyCliente>(str.ToString(), new { AHC_CodigoCliente = codigoCliente }).FirstOrDefault();


					return retorno;
				}


			}
			catch (Exception ex)
			{
				ACSLog.InsertLog(MessageBoxIcon.Error, ex);
				return null;

			}
		}

		public static bool InsertLogClienteAtivoNuvem(AuthyCliente cliente, string sUsuario)
		{
			try
			{

				using (IDbConnection _dbDapper = new SqlConnection(ACSConfig.GetConnection().CONNECTIONSTRINGDAPPERAUTH))
				{

					StringBuilder str = new StringBuilder();
					str.Append(" INSERT INTO  [AuthyClienteAcesso] ");
					str.Append(" ([ACL_idCliente] ");
					str.Append(" ,[ACL_DataAcesso] ");
					str.Append(" ,[ACL_Local] ");
					str.Append(" ,[ACL_sUsuario]");
					str.Append(" ,[ACL_ClienteBloqueado]) ");
					str.Append(" VALUES ");
					str.Append(" (@ACL_idCliente ");
					str.Append(" ,getDate() ");
					str.Append(" ,@ACL_Local ");
					str.Append(" ,@ACL_sUsuario  ");
					str.Append(" ,@ACL_ClienteBloqueado ) ");

					var retorno = _dbDapper.Execute(str.ToString(), new
					{
						ACL_idCliente = cliente.AHC_Id,
						ACL_Local = "ACS - Capture",
						ACL_sUsuario = sUsuario,
						ACL_ClienteBloqueado = cliente.AHC_fBloqueado

					});


					return true;
				}


			}
			catch (Exception ex)
			{
				ACSLog.InsertLog(MessageBoxIcon.Error, ex);
				return false;

			}
		}
	}
}



