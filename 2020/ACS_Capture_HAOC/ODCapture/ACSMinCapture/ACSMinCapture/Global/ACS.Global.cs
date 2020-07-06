using ACSMinCapture.DataBase;
using ACSMinCapture.DataBase.Model;
using ACSMinCapture.DataBase.ModelOracle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACSMinCapture.Global
{

    public static class ACSGlobal
    {
        public static string ConnectionString { get; set; }
        public static string PassLogin { get; set; }
        public static string NomeLogado { get; set; }
        public static GEDUSUARIOS UsuarioLogado { get; set; }
        public static GEDLOGLOGIN Session { get; set; }
        public static GEDGRUPOSUSUARIOS GrupoUsuario { get; set; }
        public static List<decimal?> ListaGruposSetoresLogado { get; set; }
        public static List<decimal> SetoresUsuario { get; set; }
        //public static GEDSETOR SetorUsuario { get; set; }
        public static GED_PROC_F_Lotes_Result LoteSelecionado { get; set; }
        public static int TipoCaptura { get; set; }
        public static bool isTasy { get; set; }

        public static bool FlagPF { get; set; }
        public static bool FlagPJ { get; set; }
        public static List<GEDAREAS> ListaAreas { get; set; }
        public static int idAreaSelecionada { get; set; } 
        public static int flagTipoCliente { get; set; } 
        public static bool configScanValida { get; set; } 
        public static bool Duplex { get; set; } 
        public static bool ContinuaLote { get; set; }
    }

}
