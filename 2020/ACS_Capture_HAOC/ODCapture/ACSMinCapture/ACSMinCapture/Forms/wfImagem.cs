using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACSMinCapture.Forms
{
    public partial class wfImagem : Form
    {
      private static string scaminhoImagem = "";
        public wfImagem(string sCaminho)
        {
            scaminhoImagem = sCaminho;
            InitializeComponent();
        }

        private void wfImagem_Load(object sender, EventArgs e)
        {
            Bitmap   a = new Bitmap(Image.FromFile(scaminhoImagem));
            imagePanel3AA.Image =a ; 
        }
    }
}
