using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ACSMinCapture.Forms
{
    internal partial class WFSetDateValidityDocument : ACSMinCapture.WFOk
    {
        public WFSetDateValidityDocument()
        {
            InitializeComponent();
            btnCalendar2.CheckedChanged += btnCalendar_CheckedChanged;
            this.btnOk.Click += btnOk_Click1;
        }

        private void mcCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            (sender as MonthCalendar).Visible = false;
            if (btnCalendar.Checked)
            {
                tbStartDate.Text = (sender as MonthCalendar).SelectionRange.Start.Date.ToString();
                btnCalendar.Checked = false;
                tbStartDate.Focus();
            }
            else if (btnCalendar2.Checked)
            {
                tbDateValidity.Text = (sender as MonthCalendar).SelectionRange.Start.Date.ToString();
                btnCalendar.Checked = false;
                tbDateValidity.Focus();
            }
        }

        private void btnCalendar_CheckedChanged(object sender, EventArgs e)
        {
            mcCalendar.Top = (sender as CheckBox).Top;
            mcCalendar.Visible = (sender as CheckBox).Checked;
            if (sender == btnCalendar)
            {
                mcCalendar.MaxDate = DateTime.Now.Date;
                mcCalendar.MinDate = DateTime.MinValue;
            }
            else if (sender == btnCalendar2)
            {
                mcCalendar.MaxDate = DateTime.MaxValue;
                mcCalendar.MinDate = DateTime.Now.Date;
            }
        }

        private void btnOk_Click1(object sender, EventArgs e)
        {
            DateTime dt;
            bool result = false;

            if (!DateTime.TryParse(tbStartDate.Text, out dt))
            {
                WFMessageBox.Show("Data inválida!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
                return;
            }
            else
            {
                result = true;
            }

            if (tbDateValidity.Enabled)
            {
                if (!DateTime.TryParse(tbDateValidity.Text, out dt))
                {
                    WFMessageBox.Show("Data inválida!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                    return;
                }
                else
                {
                    result = true;
                }
            }

            if (result)
                this.DialogResult = DialogResult.OK;
            else
                this.DialogResult = DialogResult.None;

        }

    }
}
