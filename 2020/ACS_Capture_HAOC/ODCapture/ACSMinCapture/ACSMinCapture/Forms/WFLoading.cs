using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACSMinCapture
{
    public partial class WFLoading : WFTitle
    {
        static WFLoading FWFLoading = null;

        public static void ShowLoad(bool GetCaptionControl = true,string Title = "", string Msg = "" )
        {
            try
            {
                if (FWFLoading == null)
                  FWFLoading = new WFLoading(GetCaptionControl);

                if (!Title.Equals(""))
                    FWFLoading.lbCaptionForm.Text = FWFLoading.lbCaptionForm.Text + " - " + Title;
                if (!Msg.Equals(""))
                    FWFLoading.lbAguarde.Text = Msg;

                Form.CheckForIllegalCrossThreadCalls = false;

                var tk = new Task(() =>
                {

                    try
                    {
                        FWFLoading.ShowDialog();
                        FWFLoading.Dispose();
                        FWFLoading = null;
                    }
                    catch
                    {
                    }
                    
                });
                tk.Start();
            
            }
            catch
            {
            }
        }

        public static void CloseLoad()
        {
            try
            {
                if (FWFLoading.IsShowning)
                    FWFLoading.Close();
            }
            catch
            {
            }
        }

        public WFLoading(bool GetCaptionControl = true)
        {
            InitializeComponent();
            this.btnCloseForm.Visible = false;
            this.btnMinimize.Visible = false;
            this.getCaptionControl = GetCaptionControl;
        }

        private void WFLoading_Load(object sender, EventArgs e)
        {

        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {

        }


    }
}
