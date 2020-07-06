using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Crom.Controls.Docking;

namespace OrionRecognizeLibrary
{
    public partial class ImageBoxRecognizeManager : UserControl
    {
        F_ImageBoxRecognize _F_ImageBoxRecognize;
        public ImageBoxRecognizeManager()
        {
            InitializeComponent();
            
            this._F_ImageBoxRecognize = new F_ImageBoxRecognize();
            this._F_ImageBoxRecognize.TopLevel = false;
            //this.Controls.Add(this._F_ImageBoxRecognize);
            var info = this.dcMaster.Add(this._F_ImageBoxRecognize, zAllowedDock.Fill,new Guid("AB0A87DF-1C46-47AD-AD8A-D71BAFAB5A46"));
            //this.dcMaster.DockForm(info,DockStyle.Fill,zDockMode.Outer);
        }
        
    }
}
