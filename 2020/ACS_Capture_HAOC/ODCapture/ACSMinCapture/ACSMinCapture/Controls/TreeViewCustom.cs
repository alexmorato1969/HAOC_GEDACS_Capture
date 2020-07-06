using ACSMinCapture.Config;
using ACSMinCapture.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TreeViewDragDrop;

namespace ACSMinCapture.Controls
{
    public static class MyExtensions
    {
        public static IEnumerable<TreeNode> All(this TreeNodeCollection nodes)
        {
            foreach (TreeNode n in nodes)
            {
                yield return n;
                foreach (TreeNode child in n.Nodes.All())
                    yield return child;
            }
        }
    }

    public partial class TreeViewCustom : TreeView
    {
        #region Vars

        TreeNode dragNode, tempDropNode;
        ImageList imageListDrag = new ImageList();
        Timer timer = new Timer();
        bool dragging = false;
        int imageIndexDocumentValid = -1;
        int imageIndexDocumentNotValid = -1;
        int imageIndexDocumentOCR = -1;
        int imageIndexPageValid = -1;
        int imageIndexPageNotValid = -1;
        int imageIndexPageOCR = -1;


        #endregion

        public TreeViewCustom()
        {
            try
            {


                InitializeComponent();
                this.AllowDrop = true;

                this.timer.Interval = 200;
                this.timer.Tick += new EventHandler(timer_Tick);
            }
            catch (Exception e)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, e);
            }
        }
        #region Methods

        public Document NewDocument()
        {
            try
            {
                var DocName = (this.Nodes.Count + 1);

                var Doc = new Document(this.MaskDocumentName + DocName.ToString(),
                    this.ImageIndexDocumentValid,
                    this.ImageIndexDocumentNotValid,
                    this.ImageIndexDocumentOCR);

                if (Doc == null)
                    ACSLog.InsertLog(MessageBoxIcon.Error, "DOCUMENTO NULO: " + DocName);
                else
                    this.Nodes.Add(Doc);

                return Doc;
            }
            catch (Exception e)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, e);
                return null;
            }
        }

        //public Document NewDocument()
        //{
        //    try
        //    {

        //        var DocName = (this.Nodes.Count + 1);
        //        var Doc = new Document(this.MaskDocumentName + DocName.ToString(),
        //            this.ImageIndexDocumentValid,
        //            this.ImageIndexDocumentNotValid,
        //            this.ImageIndexDocumentOCR);

        //        this.Nodes.Add(Doc);
        //        return Doc;

        //    }
        //    catch (Exception e)
        //    {
        //        ACSLog.InsertLog(MessageBoxIcon.Error, e);
        //        var DocName = (this.Nodes.Count + 1);
        //        var Doc = new Document(this.MaskDocumentName + DocName.ToString(),
        //            this.ImageIndexDocumentValid,
        //            this.ImageIndexDocumentNotValid,
        //            this.ImageIndexDocumentOCR);

        //        this.Nodes.Add(Doc);
        //        return Doc;
        //    }
        //}

        #endregion

        #region Propertys

        public bool Draging
        {
            get
            {
                return this.dragging;
            }
        }
        [DefaultValue(-1)]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [RelatedImageList("TreeView.ImageList")]
        [TypeConverter(typeof(TreeViewImageIndexConverter))]
        public int ImageIndexDocumentValid
        {
            get
            {
                return this.imageIndexDocumentValid;
            }
            set
            {
                var Docs = this.Nodes.OfType<Document>();

                foreach (var Doc in Docs)
                {
                    Doc.ImageIndexValid = value;
                }

                this.imageIndexDocumentValid = value;
            }
        }
        [DefaultValue(-1)]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [RelatedImageList("TreeView.ImageList")]
        [TypeConverter(typeof(TreeViewImageIndexConverter))]
        public int ImageIndexDocumentNotValid
        {
            get
            {
                return this.imageIndexDocumentNotValid;
            }
            set
            {
                var Docs = this.Nodes.OfType<Document>();

                foreach (var Doc in Docs)
                {
                    Doc.ImageIndexNotValid = value;
                }

                this.imageIndexDocumentNotValid = value;
            }
        }
        [DefaultValue(-1)]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [RelatedImageList("TreeView.ImageList")]
        [TypeConverter(typeof(TreeViewImageIndexConverter))]
        public int ImageIndexPageValid
        {
            get
            {
                return this.imageIndexPageValid;
            }
            set
            {
                var Pages = this.Nodes.OfType<Page>();

                foreach (var pag in Pages)
                {
                    pag.ImageIndexValid = value;
                }

                this.imageIndexPageValid = value;
            }
        }
        [DefaultValue(-1)]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [RelatedImageList("TreeView.ImageList")]
        [TypeConverter(typeof(TreeViewImageIndexConverter))]
        public int ImageIndexPageNotValid
        {
            get
            {
                return this.imageIndexPageNotValid;
            }
            set
            {
                var Pages = this.Nodes.OfType<Page>();

                foreach (var pag in Pages)
                {
                    pag.ImageIndexNotValid = value;
                }

                this.imageIndexPageNotValid = value;
            }
        }
        [DefaultValue(-1)]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [RelatedImageList("TreeView.ImageList")]
        [TypeConverter(typeof(TreeViewImageIndexConverter))]
        public int ImageIndexDocumentOCR
        {
            get
            {
                return this.imageIndexDocumentOCR;
            }
            set
            {
                var Docs = this.Nodes.OfType<Document>();

                foreach (var doc in Docs)
                {
                    doc.ImageIndexOCR = value;
                }

                this.imageIndexDocumentOCR = value;
            }
        }
        [DefaultValue(-1)]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [RelatedImageList("TreeView.ImageList")]
        [TypeConverter(typeof(TreeViewImageIndexConverter))]
        public int ImageIndexPageOCR
        {
            get
            {
                return this.imageIndexPageOCR;
            }
            set
            {
                var Pages = this.Nodes.OfType<Page>();

                foreach (var pag in Pages)
                {
                    pag.ImageIndexOCR = value;
                }

                this.imageIndexPageOCR = value;
            }
        }

        [DefaultValue("DOC")]
        public string MaskDocumentName { get; set; }
        [DefaultValue("00000000")]
        public string MaskPageName { get; set; }

        #endregion

        #region Delegates

        public delegate void AfterOnDragDropEvent(TreeNodeCustom Node);

        #endregion

        #region Events

        public new event AfterOnDragDropEvent AfterOnDragDrop = null;

        #endregion

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                this.timer.Dispose();
                this.imageListDrag.Dispose();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public TreeViewCustom(IContainer container)
        {
            try
            {

                container.Add(this);

                InitializeComponent();
            }
            catch (Exception e)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, e);

            }
        }
        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            try
            {


                // Get drag node and select it
                this.dragNode = (TreeNode)e.Item;

                this.SelectedNode = this.dragNode;

                // Reset image list used for drag image
                this.imageListDrag.Images.Clear();
                this.imageListDrag.ImageSize = new Size(this.dragNode.Bounds.Size.Width + this.Indent, this.dragNode.Bounds.Height);

                // Create new bitmap
                // This bitmap will contain the tree node image to be dragged
                Bitmap bmp = new Bitmap(this.dragNode.Bounds.Width + this.Indent, this.dragNode.Bounds.Height);

                // Get graphics from bitmap
                Graphics gfx = Graphics.FromImage(bmp);

                // Draw node icon into the bitmap
                if (this.ImageList != null)
                {
                    this.imageListDrag.ColorDepth = this.ImageList.ColorDepth;

                    gfx.DrawImage(this.ImageList.Images[this.dragNode.ImageIndex], 0, 0);

                    // Draw node label into bitmap
                    gfx.DrawString(this.dragNode.Text,
                        this.Font,
                        new SolidBrush(this.ForeColor),
                        (float)this.Indent, 1.0f);
                }
                // Add bitmap to imagelist
                this.imageListDrag.Images.Add(bmp);

                // Get mouse position in client coordinates
                Point p = this.PointToClient(Control.MousePosition);

                // Compute delta between mouse position and node bounds
                int dx = p.X + this.Indent - this.dragNode.Bounds.Left;
                int dy = p.Y - this.dragNode.Bounds.Top;

                // Begin dragging image
                if (DragHelper.ImageList_BeginDrag(this.imageListDrag.Handle, 0, dx, dy))
                {
                    // Begin dragging
                    this.DoDragDrop(bmp, DragDropEffects.Move);
                    // End dragging image
                    DragHelper.ImageList_EndDrag();
                }

            }
            catch (Exception ex)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, ex);
            }
        }
        protected override void OnDragOver(DragEventArgs drgevent)
        {
            try
            {

                // Compute drag position and move image
                Point formP = this.PointToClient(new Point(drgevent.X, drgevent.Y));
                //DragHelper.ImageList_DragMove(formP.X - this.Left, formP.Y - this.Top);
                DragHelper.ImageList_DragMove(formP.X, formP.Y);

                // Get actual drop node
                TreeNode dropNode = this.GetNodeAt(this.PointToClient(new Point(drgevent.X, drgevent.Y)));
                if (dropNode == null)
                {
                    //Verificar
                    //drgevent.Effect = DragDropEffects.None;
                    return;
                }

                drgevent.Effect = DragDropEffects.Move;

                // if mouse is on a new node select it
                if (this.tempDropNode != dropNode)
                {
                    DragHelper.ImageList_DragShowNolock(false);
                    this.SelectedNode = dropNode;
                    DragHelper.ImageList_DragShowNolock(true);
                    tempDropNode = dropNode;
                }

                // Avoid that drop node is child of drag node 
                TreeNode tmpNode = dropNode;
                while (tmpNode.Parent != null)
                {
                    if (tmpNode.Parent == this.dragNode) drgevent.Effect = DragDropEffects.None;
                    tmpNode = tmpNode.Parent;
                }

            }
            catch (Exception e)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, e);

            }
        }
        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            try
            {


                DragHelper.ImageList_DragEnter(this.Handle, drgevent.X - this.Left,
                    drgevent.Y - this.Top);

                // Enable timer for scrolling dragged item
                this.timer.Enabled = true;

            }
            catch (Exception e)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, e);

            }
        }
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            try
            {

                this.dragging = false;

                // Unlock updates
                DragHelper.ImageList_DragLeave(this.Handle);

                // Get drop node
                TreeNode dropNode = this.GetNodeAt(this.PointToClient(new Point(drgevent.X, drgevent.Y)));

                if (dropNode == null && this.dragNode.Parent != null)
                {
                    dropNode = NewDocument();
                }

                var IndexAdd = -1;

                if (dropNode != null)
                {
                    if (dropNode.Parent != null)
                    {
                        IndexAdd = dropNode.Index;
                        dropNode = dropNode.Parent;
                    }

                    if (this.dragNode.Parent == null && dropNode.Parent == null)
                    {
                        this.Nodes.Remove(this.dragNode);
                        this.Nodes.Insert(dropNode.Index + 1, this.dragNode);

                        foreach (TreeNode Node in this.Nodes)
                        {
                            var Index = Node.Index + 1;
                            Node.Text = this.MaskDocumentName + Index.ToString();
                        }
                        // Disable scroll timer
                        this.timer.Enabled = false;

                        return;
                    }
                }

                // If drop node isn't equal to drag node, add drag node as child of drop node
                if (this.dragNode != dropNode)
                {
                    // Remove drag node from parent
                    if (this.dragNode.Parent == null)
                    {
                        this.Nodes.Remove(this.dragNode);
                    }
                    else
                    {
                        var ParentNode = this.dragNode.Parent;
                        ParentNode.Nodes.Remove(this.dragNode);

                        if (ParentNode.Nodes.Count == 0)
                        {
                            if (ParentNode.Parent == null)
                                this.Nodes.Remove(ParentNode);
                            else
                                ParentNode.Parent.Nodes.Remove(ParentNode);
                        }
                    }

                    // Add drag node to drop node
                    if (dropNode != null)
                    {
                        if (IndexAdd == -1)
                            dropNode.Nodes.Add(this.dragNode);
                        else
                            dropNode.Nodes.Insert(IndexAdd, this.dragNode);

                        dropNode.ExpandAll();

                        foreach (TreeNode Node in dropNode.Nodes)
                        {
                            var Index = Node.Index + 1;
                            Node.Text = Index.ToString(this.MaskPageName);
                        }

                        // Set drag node to null
                        this.dragNode = null;

                    }
                    else
                    {
                        this.Nodes.Add(this.dragNode);
                    }

                }

                if (AfterOnDragDrop != null)
                {
                    AfterOnDragDrop(this.dragNode == null ? (TreeNodeCustom)dropNode : (TreeNodeCustom)this.dragNode);
                }

                // Disable scroll timer
                this.timer.Enabled = false;

            }
            catch (Exception e)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, e);

            }
        }
        protected override void OnDragLeave(EventArgs e)
        {
            try
            {

                this.dragging = false;
                DragHelper.ImageList_DragLeave(this.Handle);

                // Disable timer for scrolling dragged item
                this.timer.Enabled = false;

            }
            catch (Exception ex)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, ex);


            }
        }
        protected override void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
        {
            try
            {


                if (gfbevent.Effect == DragDropEffects.Move)
                {
                    // Show pointer cursor while dragging
                    gfbevent.UseDefaultCursors = false;
                    this.Cursor = Cursors.Default;
                }
                else
                    gfbevent.UseDefaultCursors = true;
            }
            catch (Exception)
            {


            }
        }
        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {

            try
            {


                this.dragging = true;
                if (e.Node == null || String.IsNullOrEmpty(e.Node.Text)) return;

                var backColorSelected = SystemColors.Highlight;
                var backColor = System.Drawing.Color.Red;
                var foreColor = e.Node.ForeColor;

                
                if ((e.State & TreeNodeStates.Selected) != 0)
                {
                    using (SolidBrush brush = new SolidBrush(backColorSelected))
                    {
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }

                    foreColor = Color.White;

                    e.Graphics.DrawRectangle(SystemPens.Window, e.Bounds);

                    TextRenderer.DrawText(e.Graphics,
                                           e.Node.Text,
                                           e.Node.TreeView.Font,
                                           e.Node.Bounds,
                                           foreColor);
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(this.BackColor))
                    {
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }
                    TextRenderer.DrawText(e.Graphics,
                           e.Node.Text,
                           e.Node.TreeView.Font,
                           e.Node.Bounds,
                           foreColor);

                }
                this.dragging = false;
            }
            catch (Exception ex)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, ex);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {


                // get node at mouse position
                Point pt = PointToClient(Control.MousePosition);
                TreeNode node = this.GetNodeAt(pt);

                if (node == null) return;

                // if mouse is near to the top, scroll up
                if (pt.Y < 30)
                {
                    // set actual node to the upper one
                    if (node.PrevVisibleNode != null)
                    {
                        node = node.PrevVisibleNode;

                        // hide drag image
                        DragHelper.ImageList_DragShowNolock(false);
                        // scroll and refresh
                        node.EnsureVisible();
                        this.Refresh();
                        // show drag image
                        DragHelper.ImageList_DragShowNolock(true);

                    }
                }
                // if mouse is near to the bottom, scroll down
                else if (pt.Y > this.Size.Height - 30)
                {
                    if (node.NextVisibleNode != null)
                    {
                        node = node.NextVisibleNode;

                        DragHelper.ImageList_DragShowNolock(false);
                        node.EnsureVisible();
                        this.Refresh();
                        DragHelper.ImageList_DragShowNolock(true);
                    }
                }
            }
            catch (Exception ex)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, ex);
            }
        }


    }
}
