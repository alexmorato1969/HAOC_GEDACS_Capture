using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ACSMinCapture.Forms
{
    internal partial class WFObservation : ACSMinCapture.WFOk
    {
        public WFObservation()
        {
            InitializeComponent();
        }

        public static string GetObservation()
        {
            using (var wfOb = new WFObservation())
            {
                wfOb.TopMost = true;
                if (wfOb.ShowDialog() == DialogResult.OK)
                    return wfOb.tbObservation.Text.Trim();
                else
                    return string.Empty;
            }
        }

        private void WFObservation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.tbObservation.Text.Trim() == "")
            {
                WFMessageBox.Show("Observação obrigatória!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void WFObservation_BeforeExitEvent(object sender)
        {

        }
    }
}
