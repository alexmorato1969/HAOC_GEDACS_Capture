using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ACSMinCapture
{
    internal partial class WFSetValues : WFOk
    {
        public DataTable dtDocs = new DataTable();

        public WFSetValues()
        {
            InitializeComponent();

            this.dtDocs.Columns.Add("fDocs", typeof(string));
            this.dtDocs.Columns.Add("fBarCode", typeof(string));

        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (var wfSBC = new WFSetBarCode())
            {
                wfSBC.tbText.Focus();
                wfSBC.ShowDialog(this);
                if (wfSBC.DialogResult == DialogResult.OK)
                {
                    this.tbBarCode.Focus();
                    this.tbBarCode.Text = wfSBC.BarCodeSelected.TPD_CODIGOBARRA;
                    SendKeys.Send("{ENTER}");
                }
            }

        }

        private void tbBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                object[] row = { tbInterDocs.Text, tbBarCode.Text };
                dtDocs.Rows.Add(row);
                dataGridView1.DataSource = dtDocs;
                dataGridView1.Refresh();
                tbBarCode.Clear();
                tbInterDocs.Clear();
                tbInterDocs.Focus();
            }
        }

        private void tbBarCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void WFSetValues_Load(object sender, EventArgs e)
        {
            tbInterDocs.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

        }

        private void tbInterDocs_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            int asc = (int)e.KeyChar;
            if (!char.IsDigit(e.KeyChar) && asc != 08 && !e.KeyChar.ToString().Equals("-"))
            {
                e.Handled = true;
            }
        }

    }
}
