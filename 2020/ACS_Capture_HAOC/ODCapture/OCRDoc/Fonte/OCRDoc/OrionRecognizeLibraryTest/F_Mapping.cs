using OrionRecognizeLibraryTest.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrionRecognizeLibraryTest
{
    public partial class F_Mapping : Form
    {
        public F_Mapping()
        {
            InitializeComponent();
        }

        int DocId = 1;

        int MapIndex = 1;
 
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.LoadZones(DocId, MapIndex, ofd.FileName);
                }
                ofd.Dispose();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.imageBoxRecognize1.AllowZone = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.imageBoxRecognize1.ZoomIn();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.imageBoxRecognize1.ZoomOut();

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.imageBoxRecognize1.ZoomToFit();

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            var Zones = this.imageBoxRecognize1.RecognizeForZones();

            foreach(var Zone in Zones)
            {
                var item = this.listView1.Items.Add(Zone.Name);
                item.SubItems.Add(Zone.Value);
            }
        }

        void LoadZones(int DocId, int MapIndex, string path = "")
        {
            var da = new DataAccess();
            var Zones = da.GetZones(DocId,MapIndex);

            if (Zones.Count() > 0)
            {
                var map = da.Get_OCR_Mapping(Zones[0].ZON_MAP_Id);

                this.listView1.Items.Clear();
                this.imageBoxRecognize1.OrionRecognize.DeleteAllZone();
                this.imageBoxRecognize1.Clear();


                if (!File.Exists(path))
                {
                    path = ".//Templates//" + 
                        map.MAP_Width.Value.ToString() + "_" +
                        map.MAP_Height.Value.ToString() + "_" + 
                        MapIndex.ToString()+
                        ".bmp";

                    if (!File.Exists(path))
                    {
                        using (var bmp = new Bitmap(map.MAP_Width.Value, map.MAP_Height.Value))
                        {

                            Directory.CreateDirectory(Path.GetDirectoryName(path));

                            var e = Graphics.FromImage(bmp);
                            e.FillRectangle(Brushes.White, 0, 0, map.MAP_Width.Value, map.MAP_Height.Value);
                            e.Save();
                            bmp.Save(path);

                        }
                    }
                }

                this.imageBoxRecognize1.LoadFromFile(path);

                foreach (var zone in Zones)
                {
                    this.imageBoxRecognize1.OrionRecognize.AddZone(new RectangleF()
                    {
                        X = zone.ZON_Left,
                        Y = zone.ZON_Top,
                        Width = zone.ZON_Width,
                        Height = zone.ZON_Rigth

                    }, zone.ZON_Id, zone.ZON_Name);
                }

            }
            else
            {
                this.imageBoxRecognize1.OrionRecognize.DeleteAllZone();
                this.imageBoxRecognize1.Invalidate();

                if (File.Exists(path))
                    this.imageBoxRecognize1.LoadFromFile(path);

                return;
            }

        }

        void Save(int DocId, int MapIndex)
        {
            var da = new DataAccess();
            var zones = this.imageBoxRecognize1.OrionRecognize.GetZones();
            foreach (var zone in zones)
            {
                var newOMZ = da.Get_OCR_MappingZone(zone.ID);

                if (newOMZ == null)
                {
                    newOMZ = new OCR_MappingZone();
                    newOMZ.ZON_Id = zone.ID;
                }

                newOMZ.ZON_Name = zone.Name;
                newOMZ.ZON_Left = zone.X;
                newOMZ.ZON_Top = zone.Y;
                newOMZ.ZON_Width = zone.Width;
                newOMZ.ZON_Rigth = zone.Height;

                da.OCR_MappingZone_Update(newOMZ, MapIndex, this.imageBoxRecognize1.Image.Width, this.imageBoxRecognize1.Image.Height, DocId);

                zone.ID = newOMZ.ZON_Id;
                zone.Name = newOMZ.ZON_Name;

                zone.Update();
            }
            
        }

        void Save()
        {

            if (this.imageBoxRecognize1.OrionRecognize.GetZones().Count() <= 0)
                return;

            Save(DocId, MapIndex);

            MessageBox.Show("As informações foram salvas com sucesso!",Application.ProductName,MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void F_Mapping_Load(object sender, EventArgs e)
        {
            this.LoadZones(DocId, MapIndex);
        }

        void SaveCheckedChanged(object sender)
        {
            if (!(sender as RadioButton).Checked)
                return;

            this.Save();

            if (this.rbCNH.Checked)
            {
                this.DocId = 1;
            }
            else if (this.rbCPF.Checked)
            {
                this.DocId = 2;
            }
            else if (this.rbRG.Checked)
            {
                this.DocId = 3;
            }

            if (this.rbMask1.Checked)
            {
                this.MapIndex = 1;
            }
            else if (this.rbMask2.Checked)
            {
                this.MapIndex = 2;
            }
            else if (this.rbMask3.Checked)
            {
                this.MapIndex = 3;
            }

            this.LoadZones(this.DocId, this.MapIndex);
        }

        private void rbCNH_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveCheckedChanged(sender);
        }

        private void rbCPF_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveCheckedChanged(sender);
        }

        private void rbRG_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveCheckedChanged(sender);
        }

        private void rbMask1_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveCheckedChanged(sender);
        }

        private void rbMask2_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveCheckedChanged(sender);
        }

        private void rbMask3_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveCheckedChanged(sender);
        }
    }
}
