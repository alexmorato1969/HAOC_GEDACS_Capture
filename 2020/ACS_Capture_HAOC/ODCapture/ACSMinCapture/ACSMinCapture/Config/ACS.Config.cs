using ACSMinCapture.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Diagnostics;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ACSMinCapture.Config
{
    public enum ModeApp { Debug, Release };

    public enum ModeUser { Mono, Multi }

    [Flags]
    public enum ModeSystem { Scan = 1, Import = 2, Process = 4 }

    public static class ACSConfig
    {

        static ACSConfig()
        {

            try
            {
                if (IniFile == null)
                {
                    var pathIni = Path.GetDirectoryName(Application.ExecutablePath) + @"\Config.ypt";

                    if (!File.Exists(pathIni))
                    {
                        var msg = "Arquivo de configuração não encontrado. Avise o administrador do sistema!";
                        var e = new Exception(msg, new Exception(pathIni));
                        ACSLog.InsertLog(MessageBoxIcon.Error, e, "ACS.Config.cs: line 28");
                        WFMessageBox.Show(msg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Process.GetCurrentProcess().Kill();
                    }

                    IniFile = new IniParser(pathIni);
                }
            }
            catch (Exception ex)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, ex.ToString());
                WFMessageBox.Show(ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        static IniParser IniFile { get; set; }

        #region Statics

        public static UrlServices GetUrlServices()
        {
            return new UrlServices();
        }
        public static Connection GetConnection()
        {
            return new Connection();
        }
        public static App GetApp()
        {
            return new App();
        }
        public static ModeSystem SystemAction { get; set; }

        public static Images GetImages()
        {
            return new Images();
        }
        public static Storage GetStorage()
        {
            return new Storage();
        }
        public static Scanner GetScanner()
        {
            return new Scanner();
        }
        public static NetworkAccesser GetNetworkAccesser()
        {
            return new NetworkAccesser();
        }
        public static BarCodeSettings GetBarCodeSettings()
        {
            return new BarCodeSettings();
        }

        #endregion

        #region Class
        public class UrlServices
        {
            public string UrlAutenticationTasy
            {
                get
                {
                    return IniFile.GetSetting("SERVICESURL", "AUTENTICATIONTASY");
                }
                set
                {
                    IniFile.AddSetting("SERVICESURL", "AUTENTICATIONTASY", value);
                }

            }
            public string UrlDadosTasy
            {
                get
                {
                    return IniFile.GetSetting("SERVICESURL", "DADOSTASY");

                }
                set
                {
                    IniFile.AddSetting("SERVICESURL", "DADOSTASY", value);
                }

            }

        }

        public class Connection
        {
            public string ConnectioStringSQL
            {
                get
                {
                    EntityConnectionStringBuilder conStrIntegratedSecurity = new EntityConnectionStringBuilder()
                    {

                        Metadata = string.Concat("res://*/DataBase.Model.ACSModel.csdl|",
                                                 "res://*/DataBase.Model.ACSModel.ssdl|",
                                                 "res://*/DataBase.Model.ACSModel.msl"),

                        Provider = "System.Data.SqlClient",

                        ProviderConnectionString = IniFile.GetSetting("CONNECTION", "CONNECTIONSTRING")

                    };


                    return conStrIntegratedSecurity.ToString();
                }
                set
                {
                    IniFile.AddSetting("CONNECTION", "CONNECTIONSTRING", value);
                }

            }


            public string ConnectioStringOracle
            {
                get
                {
                    EntityConnectionStringBuilder conStrIntegratedSecurity = new EntityConnectionStringBuilder()
                    {
                        Metadata = string.Concat("res://*/DataBase.ModelOracle.Model1.csdl|",
                                                 "res://*/DataBase.ModelOracle.Model1.ssdl|",
                                                 "res://*/DataBase.ModelOracle.Model1.msl"),

                        Provider = "Oracle.DataAccess.Client",

                        ProviderConnectionString = IniFile.GetSetting("CONNECTION", "CONNECTIONSTRING")


                        //  ProviderConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SID=xe)));User Id=GEDTESTE;Password=1256171; PERSIST SECURITY INFO=True"

                        // ProviderConnectionString = "Password=APP_GED;User ID=APP_GED;Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = dtb-teste)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = dbteste)));Persist Security Info=True"
                        //"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SID=xe)));User Id=GEDTESTE;Password=1256171; PERSIST SECURITY INFO=True
                        // ProviderConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SID=xe)));User Id=GEDTESTE;Password=1256171; PERSIST SECURITY INFO=True"
                        //ProviderConnectionString = IniFile.GetSetting("CONNECTION", "CONNECTIONSTRING")

                    };


                    return conStrIntegratedSecurity.ToString();
                }
                set
                {
                    IniFile.AddSetting("CONNECTION", "CONNECTIONSTRING", value);
                }

            }

			public string CONNECTIONSTRINGDAPPERAUTH
			{

				get
				{
					return IniFile.GetSetting("CONNECTION", "CONNECTIONSTRINGDAPPERAUTH");
				}
				set
				{
					IniFile.AddSetting("CONNECTION", "CONNECTIONSTRINGDAPPERAUTH", value);
				}

			}
		}

        public class App
        {
            public string NIVELASSINA
            {
                get
                {
                    return IniFile.GetSetting("APP", "NIVELASSINA");
                }
                set
                {
                    IniFile.AddSetting("APP", "NIVELASSINA", value);
                }
            }
            public string TIPODOCASSINA
            {
                get
                {
                    return IniFile.GetSetting("APP", "TIPODOCASSINA");
                }
                set
                {
                    IniFile.AddSetting("APP", "TIPODOCASSINA", value);
                }
            }
            public string Logo
            {
                get
                {
                    return IniFile.GetSetting("APP", "LOGO");
                }
                set
                {
                    IniFile.AddSetting("APP", "LOGO", value);
                }
            }


            public string DuplexInit
            {
                get
                {
                    return IniFile.GetSetting("APP", "DUPLEXINIT");
                }
                set
                {
                    IniFile.AddSetting("APP", "DUPLEXINIT", value);
                }
            }

            public string CPFValidateCertificado
            {
                get
                {
                    return IniFile.GetSetting("APP", "CPFValidateCertificado");
                }
                set
                {
                    IniFile.AddSetting("APP", "CPFValidateCertificado", value);
                }
            }

            public ModeApp Modo
            {
                get
                {
                    var modo = IniFile.GetSetting("APP", "MODO");

                    if (modo.ToUpper() != null && modo.Equals(ModeApp.Debug.ToString().ToUpper()))
                        return ModeApp.Debug;
                    else
                        return ModeApp.Release;
                }
                set
                {
                    if (value == ModeApp.Debug)
                        IniFile.AddSetting("APP", "MODO", ModeApp.Debug.ToString());
                    else
                        IniFile.AddSetting("APP", "MODO", ModeApp.Release.ToString());

                }
            }

            public ModeUser User
            {
                get
                {
                    var user = IniFile.GetSetting("APP", "USER");

                    if (user.ToUpper() != null && user.Equals(ModeUser.Mono.ToString().ToUpper()))
                        return ModeUser.Mono;
                    else
                        return ModeUser.Multi;
                }
                set
                {
                    if (value == ModeUser.Mono)
                        IniFile.AddSetting("APP", "USER", ModeUser.Mono.ToString().ToUpper());
                    else
                        IniFile.AddSetting("APP", "USER", ModeUser.Multi.ToString().ToUpper());

                }
            }
			public string CODIGOCLIENTE
			{
				get
				{
					return IniFile.GetSetting("APP", "CODIGOCLIENTE");
				}
				set
				{
					IniFile.AddSetting("APP", "CODIGOCLIENTE", value);
				}
			}

		}
        public class Images
        {
            public float Contrast
            {
                get
                {
                    float result = 0;
                    float.TryParse(IniFile.GetSetting("IMAGENS", "CONTRASTE"), out result);
                    return result;
                }
                set
                {
                    IniFile.AddSetting("IMAGENS", "CONTRASTE", value.ToString());
                }
            }
            public float BrightnessReload
            {
                get
                {
                    float result = 0;
                    float.TryParse(IniFile.GetSetting("IMAGENS", "BRILHORELOAD"), out result);
                    return result;
                }
                set
                {
                    IniFile.AddSetting("IMAGENS", "BRILHORELOAD", value.ToString());
                }
            }
            public float Brightness
            {
                get
                {
                    float result = 0;
                    float.TryParse(IniFile.GetSetting("IMAGENS", "BRILHO"), out result);
                    return result;
                }
                set
                {
                    IniFile.AddSetting("IMAGENS", "BRILHO", value.ToString());
                }
            }
            public float Resolution
            {
                get
                {
                    float result = 0;
                    float.TryParse(IniFile.GetSetting("IMAGENS", "RESOLUCAO"), out result);
                    return result;
                }
                set
                {
                    IniFile.AddSetting("IMAGENS", "RESOLUCAO", value.ToString());
                }
            }
            public ImageFormat Format
            {
                get
                {
                    var result = IniFile.GetSetting("IMAGENS", "FORMATO");

                    try
                    {
                        if (result == string.Empty)
                            return ImageFormat.Jpeg;
                        else

                            return CUtils.GetImageFormat(result);
                    }
                    catch
                    {
                        return ImageFormat.Jpeg;
                    }

                }
                set
                {
                    IniFile.AddSetting("IMAGENS", "FORMATO", value.ToString().ToUpper());
                }
            }
            public string MaskDocumentName
            {
                get
                {
                    return IniFile.GetSetting("IMAGES", "MASKDOCUMENTNAME", "DOC");
                }
                set
                {
                    IniFile.AddSetting("IMAGENS", "MASKDOCUMENTNAME", value);
                }
            }
            public string MaskPageName
            {
                get
                {
                    return IniFile.GetSetting("IMAGES", "MASKPAGENAME", "00000000");
                }
                set
                {
                    IniFile.AddSetting("IMAGENS", "MASKPAGENAME", value);
                }
            }

        }
        public class Storage
        {
            public string Input
            {
                get
                {
                    return IniFile.GetSetting("STORAGE", "INPUT");
                }
                set
                {
                    IniFile.AddSetting("STORAGE", "INPUT", value);
                }
            }
            public string Output
            {
                get
                {
                    return IniFile.GetSetting("STORAGE", "OUTPUT");
                }
                set
                {
                    IniFile.AddSetting("STORAGE", "OUTPUT", value);
                }
            }
        }
        public class Scanner
        {
            public string Driver
            {
                get
                {
                    return IniFile.GetSetting("SCANNER", "DRIVER");
                }
                set
                {
                    IniFile.AddSetting("SCANNER", "DRIVER", value);
                }
            }
            public int ScanAs
            {
                get
                {
                    int result = 0;
                    int.TryParse(IniFile.GetSetting("SCANNER", "SCANAS"), out result);
                    return result;
                }
                set
                {
                    IniFile.AddSetting("SCANNER", "SCANAS", value.ToString());
                }
            }
            public int ScanCount
            {
                get
                {
                    int result = 0;
                    int.TryParse(IniFile.GetSetting("SCANNER", "SCANCOUNT"), out result);
                    if (result == 0)
                        result = 2;

                    return result;
                }
                set
                {
                    IniFile.AddSetting("SCANNER", "SCANCOUNT", value.ToString());
                }
            }

        }
        public class NetworkAccesser
        {
            public bool Valid
            {
                get
                {
                    bool result = false;
                    bool.TryParse(IniFile.GetSetting("NETWORKACCESSER", "VALID"), out result);
                    return result;
                }
                set
                {
                    IniFile.AddSetting("NETWORKACCESSER", "VALID", value.ToString());
                }
            }
            public string Domain
            {
                get
                {
                    return IniFile.GetSetting("NETWORKACCESSER", "DOMAIN");
                }
                set
                {
                    IniFile.AddSetting("NETWORKACCESSER", "DOMAIN", value);
                }
            }
            public string User
            {
                get
                {
                    return IniFile.GetSetting("NETWORKACCESSER", "USER");
                }
                set
                {
                    IniFile.AddSetting("NETWORKACCESSER", "USER", value);
                }
            }
            public string Password
            {
                get
                {
                    return IniFile.GetSetting("NETWORKACCESSER", "PASSWORD");
                }
                set
                {
                    IniFile.AddSetting("NETWORKACCESSER", "PASSWORD", value);
                }
            }
        }
        public class BarCodeSettings
        {
            public int MaxLength
            {
                get
                {
                    int result = 0;
                    int.TryParse(IniFile.GetSetting("CODIGOBARRAS", "MAXLENGTH"), out result);
                    return result;
                }
                set
                {
                    IniFile.AddSetting("CODIGOBARRAS", "MAXLENGTH", value.ToString());
                }
            }
        }

        #endregion

    }
}
