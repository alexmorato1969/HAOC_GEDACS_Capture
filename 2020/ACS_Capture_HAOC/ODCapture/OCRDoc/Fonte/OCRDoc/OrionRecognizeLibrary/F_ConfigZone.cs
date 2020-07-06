using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrionRecognizeLibrary
{
    public partial class F_ConfigZone : Form
    {
        OrionRecognizeZone _OrionRecognizeZone;
        ImageBoxRecognize _ImageBoxRecognize;
        public F_ConfigZone(ImageBoxRecognize ImageBoxRecognize, OrionRecognizeZone OrionRecognizeZone)
        {
            InitializeComponent();
            this._OrionRecognizeZone = OrionRecognizeZone;
            this._ImageBoxRecognize = ImageBoxRecognize;

            this.tbName.DataBindings.Add("Text", this._OrionRecognizeZone, "Name");
            this.nudX.DataBindings.Add("Value", this._OrionRecognizeZone, "X");
            this.nudY.DataBindings.Add("Value", this._OrionRecognizeZone, "Y");
            this.nudWidth.DataBindings.Add("Value", this._OrionRecognizeZone, "Width");
            this.nudHeight.DataBindings.Add("Value", this._OrionRecognizeZone, "Height");

            this.nudX.ValueChanged += nudX_ValueChanged;
            this.nudY.ValueChanged += nudX_ValueChanged;
            this.nudWidth.ValueChanged += nudX_ValueChanged;
            this.nudHeight.ValueChanged += nudX_ValueChanged;
        }

        void nudX_ValueChanged(object sender, EventArgs e)
        {
            (sender as NumericUpDown).Parent.Focus();
            (sender as NumericUpDown).Focus();
            this._OrionRecognizeZone.Update();
            this._ImageBoxRecognize.Invalidate();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this._OrionRecognizeZone.Update();
            this.Close();
        }

        private void tbName_Validating(object sender, CancelEventArgs e)
        {
            (sender as TextBox).Text = (sender as TextBox).Text.Trim().ToUpper().Replace(" ", "_");
        }
    }
}
