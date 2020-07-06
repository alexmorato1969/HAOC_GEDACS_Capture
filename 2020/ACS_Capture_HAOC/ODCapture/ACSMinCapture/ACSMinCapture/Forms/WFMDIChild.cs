using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ACSMinCapture
{
    public partial class WFMDIChild : WFStandard
    {

        public WFMDIChild()
        {
            InitializeComponent();
        }

        private void WFMDIChild_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }

    }
}
