using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cyotek.Windows.Forms;

namespace OrionRecognizeLibrary
{
    public partial class ImageBoxRecognize : ImageBox
    {
        bool _AllowZone;
        OrionRecognize _OrionRecognize;
        ContextMenuStrip _MenuStrip;
        ToolStripItem TSI_Config;
        ToolStripItem TSI_Delete;
        OrionRecognizeZone ZoneSelected;
        
        public ImageBoxRecognize()
        {
            InitializeComponent();

            this.AllowClickZoom = false;
            this.AutoPan = false;
            this._AllowZone = false;
            this.GridDisplayMode = ImageBoxGridDisplayMode.Client;

            this._OrionRecognize = new OrionRecognize();
            this._MenuStrip = new ContextMenuStrip();

            this.TSI_Config = this._MenuStrip.Items.Add("Configurar");
            this._MenuStrip.Items.Add("-");
            this.TSI_Delete = this._MenuStrip.Items.Add("Deletar");

            this.TSI_Delete.Click += TSI_Delete_Click;
            this.TSI_Config.Click += TSI_Config_Click;

        }

        void TSI_Config_Click(object sender, EventArgs e)
        {

            using (var fg = new F_ConfigZone(this, ZoneSelected))
            {
                fg.ShowInTaskbar = false;
                fg.FormClosed += fg_FormClosed;
                fg.ShowDialog();
            }
        }

        void fg_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Invalidate();
            this.ZoneSelected = null;
        }

        void TSI_Delete_Click(object sender, EventArgs e)
        {
            this._OrionRecognize.DeleteZone(this.ZoneSelected);
            this.Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            this._OrionRecognize.Dispose();
            this._MenuStrip.Dispose();
        }
        
        public bool AllowZone 
        { 
            get
            {
                return this._AllowZone;
            } 
            set
            {
                this._AllowZone = value;
                this.AutoPan = !value;

                if (this._AllowZone)
                {
                    this.SelectionMode = ImageBoxSelectionMode.Rectangle;
                }
                else
                {
                    this.SelectionMode = ImageBoxSelectionMode.None;
                }
            } 
        }

        public void Recognize()
        {
            this._OrionRecognize.Recognize();
        }

        public OrionRecognize OrionRecognize
        {
            get
            {
                return this._OrionRecognize;
            }
        }
        public OrionRecognizeZone[] RecognizeForZones()
        {
            return this._OrionRecognize.RecognizeAllZone();
        }

        protected  void DrawSelection(PaintEventArgs e, RectangleF rectReg)
        {
            RectangleF rect;

            e.Graphics.SetClip(this.GetInsideViewPort(true));

            rect = this.GetOffsetRectangle(rectReg);

            using (Brush brush = new SolidBrush(Color.FromArgb(64, this.SelectionColor)))
                e.Graphics.FillRectangle(brush, rect);

            using (Pen pen = new Pen(this.SelectionColor))
                e.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);

            e.Graphics.ResetClip();

        }

        PointF MousePositionInImage(MouseEventArgs e)
        {

            var _startMousePosition = e.Location;

            float x;
            float y;

            Point imageOffset;

            imageOffset = this.GetImageViewPort().Location;

            if (e.X < _startMousePosition.X)
            {
                x = e.X;
            }
            else
            {
                x = _startMousePosition.X;
            }

            if (e.Y < _startMousePosition.Y)
            {
                y = e.Y;
            }
            else
            {
                y = _startMousePosition.Y;
            }

            x = x - imageOffset.X - this.AutoScrollPosition.X;
            y = y - imageOffset.Y - this.AutoScrollPosition.Y;

            x = x / (float)this.ZoomFactor;
            y = y / (float)this.ZoomFactor;

            return new PointF(x, y);

        }

        public override void LoadFromFile(string FileName, bool ZoomToFit = true)
        {
            this._OrionRecognize.DeleteAllZone();

            this._OrionRecognize.OpenFile(FileName);
            base.LoadFromFile(FileName, ZoomToFit);
            
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (this._AllowZone)
            {
                this._OrionRecognize.AddZone(this.SelectionRegion);
                this.SelectionMode = ImageBoxSelectionMode.None;
                this.SelectNone();
                this._AllowZone = false;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                var pos = MousePositionInImage(e);
                //var pos = e.Location;

                int index = -1;
                var Zones = this._OrionRecognize.GetZones();
                foreach (var Zone in Zones)
                {
                    index++;
                    if (pos.X >= Zone.X && pos.Y >= Zone.Y && pos.X <= (Zone.X + Zone.Width) && pos.Y <= (Zone.Y + Zone.Height))
                    {
                        this.ZoneSelected = Zone;
                        this._MenuStrip.Show(Control.MousePosition.X, Control.MousePosition.Y);
                        break;
                    }
                }
                
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var Zones = this._OrionRecognize.GetZones();
            foreach(OrionRecognizeZone Zone in Zones)
            {
                this.DrawSelection(e, new RectangleF(Zone.X, Zone.Y, Zone.Width, Zone.Height));
            }
        }

    }
}
