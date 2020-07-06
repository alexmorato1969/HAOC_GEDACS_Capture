using ACSMinCapture.Config;
using ACSMinCapture.DataBase.Model;
using ACSMinCapture.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACSMinCapture.Controls
{


    public class TreeNodeCustom : TreeNode
    {
        public TreeNodeCustom(string Name, int ImageIndexValid, int ImageIndexNotValid, int ImageIndexOCR)
        {
            this.ImageIndex = ImageIndexNotValid;
            this.ImageIndexNotValid = ImageIndexNotValid;
            this.ImageIndexValid = ImageIndexValid;
            this.ImageIndexOCR = ImageIndexOCR;
            this.Name = Name;
            this.Text = Name;
            this.Observation = string.Empty;
            this.guid = Guid.NewGuid();
            this.Valid = false;
        }

        Guid guid;
        bool valid = false;

        public bool Valid
        {
            get
            {
                return this.valid;
            }
            set
            {
                this.valid = value;

                this.ImageIndex = value ? this.ImageIndexValid : this.ImageIndexNotValid;
                this.StateImageIndex = value ? this.ImageIndexValid : this.ImageIndexNotValid;
                this.SelectedImageIndex = value ? this.ImageIndexValid : this.ImageIndexNotValid;

                foreach (TreeNodeCustom c in this.Nodes)
                {
                    c.valid = value;
                }
            }
        }

        public List<GED_PROC_CodigosBarras_Result> BarCodes { get; set; }
        public Guid Guid
        {
            get
            {
                return this.guid;
            }
        }

        public int ImageIndexValid { get; set; }
        public int ImageIndexNotValid { get; set; }
        public int ImageIndexOCR { get; set; }

        string observation = string.Empty;
        public string Observation
        {
            get
            {
                return this.observation;
            }
            set
            {
                this.observation = value;
                this.ToolTipText = value;
            }
        }

        public Document Master
        {
            get
            {
                return this is Document ? (Document)this : (Document)this.Parent;
            }
        }

        OCRorion oCR = null;
        public OCRorion OCR
        {
            get
            {
                return this.oCR;
            }
            set
            {
                this.oCR = value;
                if (value != null)
                {
                    this.ImageIndex = this.ImageIndexOCR;
                    this.StateImageIndex = this.ImageIndexOCR;
                    this.SelectedImageIndex = this.ImageIndexOCR;
                }
            }
        }

        public bool IsOCR
        {
            get
            {
                return this.OCR != null;
            }

        }

        public bool Selected
        {
            get
            {
                return this.TreeView.SelectedNode == this;
            }
            set
            {
                if (value)
                    this.TreeView.SelectedNode = this;
                else if (this.TreeView.SelectedNode == this)
                    this.TreeView.SelectedNode = null;
            }
        }
    }

    public class Page : TreeNodeCustom
    {
        public Page(string Name, string FileName, int ImageIndexValid, int ImageIndexNotValid, int ImageIndexOCR)
            : base(Name, ImageIndexValid, ImageIndexNotValid, ImageIndexOCR)
        {
            this.FileName = FileName;
            this.StateImageIndex = this.ImageIndexNotValid;
            this.ImageIndex = this.ImageIndexNotValid;
            this.SelectedImageIndex = this.ImageIndexNotValid;
        }

        public string FileName { get; set; }

        public delegate void BeforeRemovePage_Event(Page sender);
        public event BeforeRemovePage_Event BeforeRemovePageEvent = null;

        public new void Remove()
        {
            try
            {
                var Doc = (Document)this.Parent;

                if (BeforeRemovePageEvent != null)
                    BeforeRemovePageEvent(this);

                if (File.Exists(this.FileName))
                    File.Delete(this.FileName);

                if (!File.Exists(this.FileName))
                    base.Remove();

                if (Doc.Pages.Count() <= 0)
                    Doc.Remove();
            }
            catch (Exception e)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, e);
                WFMessageBox.Show(e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

    }

    public class Document : TreeNodeCustom
    {
        public Document(string Name, int ImageIndexValid, int ImageIndexNotValid, int ImageIndexOCR)
            : base(Name, ImageIndexValid, ImageIndexNotValid, ImageIndexOCR)
        {
        }

        public IEnumerable<Page> Pages
        {
            get
            {
                foreach (Page pg in Nodes)
                    yield return pg;

            }
        }

        public new void Remove()
        {
            for (var i = this.Nodes.Count - 1; i >= 0; i--)
            {
                try
                {
                    (this.Nodes[i] as Page).Remove();
                }
                catch (Exception e)
                {
                    ACSLog.InsertLog(MessageBoxIcon.Error, e);

                    return;
                }   
            }

            if (this.DirInfo != null)
            {
                if (this.DirInfo.GetFiles().Count() <= 0)
                {
                    this.DirInfo.Delete();
                    this.DirInfo = null;
                }
            }

            base.Remove();
        }

        public Page NewPage(string FileName)
        {
            try
            {


                if (this.DirInfo == null)
                    this.DirInfo = new DirectoryInfo(Path.GetDirectoryName(FileName));

                var ChilName = (this.Nodes.Count + 1);
                var result = new Page(ChilName.ToString((this.TreeView as TreeViewCustom).MaskPageName),
                    FileName, (this.TreeView as TreeViewCustom).ImageIndexPageValid,
                    (this.TreeView as TreeViewCustom).ImageIndexPageNotValid,
                    (this.TreeView as TreeViewCustom).ImageIndexPageOCR);
                this.Nodes.Add(result);

                return result;
            }
            catch (Exception e)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, e);

                return null;
            }
        }

        DirectoryInfo DirInfo;
        public DirectoryInfo PathDir { get { return this.DirInfo; } }

    }
}
