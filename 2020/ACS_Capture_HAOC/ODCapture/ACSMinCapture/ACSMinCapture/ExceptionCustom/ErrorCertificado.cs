using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACSMinCapture
{
    public class ErrorCertificado
    {
        public string IdErro { get; set; }
        public string DescricaoErro { get; set; }

        public static IEnumerable<ErrorCertificado> ListaErroCertificado()
        {
            List<ErrorCertificado> ListaErro = new List<ErrorCertificado>();
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            int counter = 0;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("ExceptionCustom/ErrorCertificado.txt", encoding);
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                var split = line.Split(';');
                ListaErro.Add(new ErrorCertificado()
                {
                    DescricaoErro = split[2].ToString(),
                    IdErro = split[0].ToString()
                });

                counter++;
            }

            return ListaErro;
        }
    }
}
