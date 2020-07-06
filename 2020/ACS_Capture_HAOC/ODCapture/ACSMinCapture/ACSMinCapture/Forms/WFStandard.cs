using ACSMinCapture.Config;
using ACSMinCapture.DataBase;
using ACSMinCapture.Global;
using ACSMinCapture.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACSMinCapture
{

    public partial class WFStandard : Form
    {
        public bool IsShowning { get; set; }
        protected bool getCaptionControl = true;

        public delegate void BeforeExit_Event(object sender);
        public event BeforeExit_Event BeforeExitEvent;

        public WFStandard()
        {
            InitializeComponent();
        }

        public WFStandard(bool GetCaptionControl = true)
        {
            InitializeComponent();
            this.getCaptionControl = GetCaptionControl;
        }

        private void WFStandard_Enter(object sender, EventArgs e)
        {
            try
            {
                if (Program.MainForm != sender)
                {
                    
                    if (ACSGlobal.UsuarioLogado != null)
                    {
                        if (ACSGlobal.SetoresUsuario != null)
                        {
                            Program.MainForm.lbCaptionForm.Text = Application.ProductName + " - " + (sender as Form).Text + " - [Usuário: " + ACSGlobal.NomeLogado + "] - "; //+ ACSGlobal.SetorUsuario.SET_DESCRICAO;
                        }
                        else
                        {
                            Program.MainForm.lbCaptionForm.Text = Application.ProductName + " - " + (sender as Form).Text + " - [Usuário: " + ACSGlobal.NomeLogado + "]";
                        }
                    }
                    else
                        Program.MainForm.lbCaptionForm.Text = Application.ProductName + " - " + (sender as Form).Text;

                }
            }
            catch (Exception ex)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, ex, Program.MainForm.ToString());
            }
        }

        internal void SetttStandardAllControls()
        {
            this.ttStandard.ToolTipTitle = Application.ProductName;
            foreach (var ctrl in this.Controls.All())
            {
                this.ttStandard.SetToolTip(ctrl, string.Format("FormName= \"{0}\"\nComponentName= \"{1}\"", this.Name, ctrl.Name));
            }
        }

        internal void GetCaptionDataBase()
        {
            try
            {
                if (!ACSDataBase.FormInGEDCaptionControl(this.Name))
                    return;

                foreach (var ctrl in this.Controls.All())
                {
                    var ControlCaption = ACSDataBase.GetControlCaption(this.Name, ctrl.Name);
                    var ctroltype = ctrl.GetType();
                    var prop = ctroltype.GetProperty("Text");
                    if (ControlCaption != string.Empty)
                    {
                        if (prop != null)
                        {
                            prop.SetValue(ctrl, ControlCaption, null);
                            ctrl.Invalidate();
                        }
                    }
                }

            }
            catch (Exception e)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, e);
            }



        }

        Timer t1 = new Timer();
        public ToolTip ttStandard;

        private bool getDesignMode()
        {
            IDesignerHost host;
            if (this.Site != null)
            {
                host = this.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (host != null)
                {
                    return host.RootComponent.Site.DesignMode;
                }
            }
            return false;
        }

        public void WFStandard_Load(object sender, EventArgs e)
        {

            this.IsShowning = true;

            this.Opacity = 0;
            t1.Interval = 10;
            t1.Tick += new EventHandler(fadeIn);
            t1.Start();


            ttStandard = new ToolTip();

            Control.CheckForIllegalCrossThreadCalls = false;

            if (this.getCaptionControl && !getDesignMode() && !(this is WFMessageBox))
            {
                GetCaptionDataBase();
                if (ACSConfig.GetApp().Modo == ModeApp.Debug)
                    SetttStandardAllControls();
            }

        }

        void fadeIn(object sender, EventArgs e)
        {
            try
            {
                if (Opacity >= 1)
                    t1.Stop();
                else
                    Opacity += 0.05;
            }
            catch
            {
            }
        }

        void fadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0)
            {
                t1.Stop();
                Close();
            }
            else
                Opacity -= 0.05;
        }

        private void WFStandard_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (BeforeExitEvent != null)
                BeforeExitEvent(this);

            this.IsShowning = false;
        }

        private void WFStandard_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void ttStandard_Popup(object sender, PopupEventArgs e)
        {

        }


    }
}
