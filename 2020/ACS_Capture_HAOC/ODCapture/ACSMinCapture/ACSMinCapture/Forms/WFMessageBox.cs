using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ACSMinCapture
{
    public partial class WFMessageBox : WFTitle
    {
        public static DialogResult Show(string Message, MessageBoxButtons MessageButtons, MessageBoxIcon MessageIcon)
        {
            using (var WFMsg = new WFMessageBox())
            {
                WFMsg.btnMinimize.Visible = false;
                WFMsg.plOk.Visible = MessageButtons == MessageBoxButtons.OK;
                WFMsg.plSimNao.Visible = MessageButtons == MessageBoxButtons.YesNo;
                WFMsg.TopMost = true;

                if (WFMsg.plSimNao.Visible)
                {
                    WFMsg.btnSim.Focus();
                    WFMsg.btnSim.Invalidate();
                }

                if (WFMsg.plOk.Visible)
                {
                    WFMsg.btnOk.Focus();
                    WFMsg.btnOk.Invalidate();
                }

                WFMsg.lbMessage.Text = Message;

                if (MessageIcon == MessageBoxIcon.Error)
                    WFMsg.pbImage.Image = global::ACSMinCapture.Properties.Resources._1363302145_button_cancel;

                if (MessageIcon == MessageBoxIcon.Exclamation)
                    WFMsg.pbImage.Image = global::ACSMinCapture.Properties.Resources._1363302123_clean; 

                if (MessageIcon == MessageBoxIcon.Question)
                    WFMsg.pbImage.Image = global::ACSMinCapture.Properties.Resources._1363302250_FAQ; 

                if (MessageIcon == MessageBoxIcon.Warning)
                    WFMsg.pbImage.Image = global::ACSMinCapture.Properties.Resources._1363302172_quick_restart;

                WFMsg.BringToFront();
                //WFMsg.Left = (Program.MainForm.Width / 2) - (WFMsg.Width / 2);
                //WFMsg.Top = (Program.MainForm.Height / 2) - (WFMsg.Height / 2);

                var result = WFMsg.ShowDialog();
                return result;
            }
        }

        public WFMessageBox()
        {
            InitializeComponent();
        }

        private void plSimNao_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;            
        }
    }
}
