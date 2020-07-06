using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACSMinCapture.Auxiliar
{
	public class AuthyCliente
	{


		public int AHC_Id { get; set; }

		public string AHC_NomeCliente { get; set; }

		public string AHC_CodigoCliente { get; set; }

		public DateTime? AHC_DataCadastro { get; set; }

		public bool? AHC_fDeletado { get; set; }

		public bool? AHC_fBloqueado { get; set; }

		public string AHC_MotivoBloqueio { get; set; }

		public DateTime? AHC_DataBloqueio { get; set; }
	}

	public class AuthyClienteAcesso
	{


		public int ACL_Id { get; set; }

		public int? ACL_idCliente { get; set; }

		public DateTime? ACL_DataAcesso { get; set; }

		public string ACL_Local { get; set; }

		public string ACL_sUsuario { get; set; }

		public int? ACL_idUsuarioAcesso { get; set; }

	}
}
