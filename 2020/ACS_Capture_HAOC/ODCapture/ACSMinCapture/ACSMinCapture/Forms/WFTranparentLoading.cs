using ACSMinCapture.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ACSMinCapture.Forms
{
    public partial class WFTranparentLoading : Form
    {
        public static WFTranparentLoading wFTranparentLoading { get; set; }

        static void OpacityMainForm(int value, bool brintToFront)
        {
            Program.MainForm.Opacity = Convert.ToDouble(value / 100.0f);
            Program.MainForm.Invalidate();
            if (brintToFront)
            {
                Program.MainForm.Enabled = true;
                Program.MainForm.BringToFront();
            }
            else
            {
                Program.MainForm.Enabled = false;
            }
        }

        public static void ShowLoading(Control Frame)
        {
            try
            {

                if (wFTranparentLoading == null)
                {
                    Point p = Frame.PointToScreen(Point.Empty);
                    wFTranparentLoading = new WFTranparentLoading();
                    wFTranparentLoading.Left = (int)(p.X + ((Frame.Width / 2) - (wFTranparentLoading.Width / 2)));
                    wFTranparentLoading.Top = (int)(p.Y + ((Frame.Height / 2) - (wFTranparentLoading.Height / 2)));
                    wFTranparentLoading.Show();
                    wFTranparentLoading.Invalidate();
                    wFTranparentLoading.pictureBox1.Invalidate();

                    OpacityMainForm(90, false); 
                    if (!ACSConfig.GetScanner().Driver.ToUpper().Contains("FUJITSU") && !ACSConfig.GetScanner().Driver.ToUpper().Contains("SP-1120"))
                        Application.DoEvents(); 
                }

            }
            catch
            {
                try
                {
                    CloseLoading();
                }
                catch
                {
                    OpacityMainForm(100,true); 
                }

            }
        }
        public static void CloseLoading()
        {
            try
            {
                if (wFTranparentLoading != null)
                {
                    wFTranparentLoading.Close();
                    wFTranparentLoading.Dispose();
                    wFTranparentLoading = null;
                    OpacityMainForm(100, true);
                }
            }
            catch
            {
                OpacityMainForm(100, true); 
            }
        }
        public static void Messege(string Msg)
        {
           if (wFTranparentLoading !=  null)
           {
               wFTranparentLoading.lbAguarde.Text = Msg;
               wFTranparentLoading.lbAguarde.Invalidate();
           }
        }

        public WFTranparentLoading()
        {
            InitializeComponent();
        }
    }
}

