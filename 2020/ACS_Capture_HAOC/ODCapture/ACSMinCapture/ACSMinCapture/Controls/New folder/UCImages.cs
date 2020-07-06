using ACSMinCapture.Log;
using Cyotek.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace ACSMinCapture.Controls
{
    public enum ImageCommand
    {
        Clear = 0,
        Rotate = 1,
        CropStart = 2,
        CropStop = 3,
        Crop = 4,
        EraserStart = 5,
        EraserStop = 6,
        Eraser = 7,
        ZoomIn = 8,
        ZoomOut = 9,
        ZoomToFit = 10
    }


    public partial class ImageBoxCapture : ImageBox
    {
        #region Constructor
        public ImageBoxCapture(int Column, int Row)
        {
            this.column = Column;
            this.row = Row;

        }
        #endregion

        #region Properties

        int column = -1;
        public int Column
        {
            get
            {
                return this.column;
            }
        }

        int row = -1;
        public int Row
        {
            get
            {
                return this.row;
            }
        }


        #endregion

        #region Methods

        #endregion
    }

    public partial class UCImages : UserControl
    {
   
        #region Classes


        /*
        public sealed  class ImageCommand
        {
            public static ImageCommand Rotate 
            {
                get
                {
                    return (ImageCommand)Activator.CreateInstance(typeof(ImageCommand));
                }
            }
        }
        */
        #endregion

        #region Constructor
        public UCImages()
        {
            InitializeComponent();
            this.colorsUnSelected[0] = Color.Silver;
            this.colorsUnSelected[1] = Color.White;

            this.colorsSelected[0] = Color.Gainsboro;
            this.colorsSelected[1] = Color.White;

            this.Columns = 2;
            this.Rows = 1;
        }
        #endregion

        #region Properties

        int columns = 1;
        public int Columns
        {
            get
            {
                return this.columns;
            }
            set
            {
                try
                {
                    this.columns = value;
                    if (this.columns < 1)
                    {
                        this.columns = 1;
                    }

                    if (this.columns < this.tlpImages.ColumnCount)
                    {
                        do
                        {
                            RemoveLastColumn();                           
                        }
                        while (this.columns < this.tlpImages.ColumnCount);
                    }
                    else if (this.columns > this.tlpImages.ColumnCount)
                    {
                        do
                        {
                            this.tlpImages.ColumnCount += 1;
                            AddImageBox();
                        }
                        while (this.columns > this.tlpImages.ColumnCount);
                    }
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
        }

        int rows = 1;
        public int Rows
        {
            get
            {
                return this.rows;
            }
            set
            {
                try
                {
                    this.rows = value;
                    if (this.rows < 1)
                    {
                        this.rows = 1;
                    }

                    if (this.rows < this.tlpImages.RowCount)
                    {
                        do
                        {
                            RemoveLastRow();
                        }
                        while (this.rows < this.tlpImages.RowCount);
                    }
                    else if (this.rows > this.tlpImages.RowCount)
                    {
                        do
                        {
                            this.tlpImages.RowCount += 1;
                            AddImageBox();
                        }
                        while (this.rows > this.tlpImages.RowCount);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        ImageBoxCapture selected = null;
        public ImageBoxCapture Selected 
        {
            get
            {
                return this.selected;
            }
            set
            {
                this.selected = value;
            }
        }

        Color[] colorsSelected = new Color[2];
        public Color[] ColorsSelected
        {
            get
            {
                return this.colorsSelected;
            }
            set
            {
                if (value.Count() >= 2)
                {
                    this.colorsSelected[0] = value[0];
                    this.colorsSelected[1] = value[1];

                }
            }

        }

        Color[] colorsUnSelected = new Color[2];
        public Color[] ColorsUnSelected
        {
            get
            {
                return this.colorsUnSelected;
            }
            set
            {
                if (value.Count() >= 2)
                {
                    this.colorsUnSelected[0] = value[0];
                    this.colorsUnSelected[1] = value[1];
                }
            }

        }


        #endregion 

        #region Delegates

        public delegate void AfterImageClickEvent(object sender);
        public delegate void AfterImageKeyDownEvent(object sender, KeyEventArgs e);
        public delegate void AfterLoadFromFileEvent(object sender, ImageBoxCapture imageBoxCapture, string FileName);
        public delegate void AfterClearAllImagesEvent(object sender);

        #endregion

        #region Events

        public new event AfterImageClickEvent AfterImageClick = null;
        public new event AfterImageKeyDownEvent AfterImageKeyDown = null;
        public new event AfterLoadFromFileEvent AfterLoadFromFile = null;
        public new event AfterClearAllImagesEvent AfterClearAllImages = null;

        #endregion

        #region Methods

        #region Private

        void ImageBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.selected != null)
            {
                this.selected.GridColor = this.colorsUnSelected[0];
                this.selected.GridColorAlternate = this.colorsUnSelected[1];
            }

            this.selected = (ImageBoxCapture)sender;

            this.selected.GridColor = this.colorsSelected[0];
            this.selected.GridColorAlternate = this.colorsSelected[1];

            if (this.AfterImageClick != null)
            {
                this.AfterImageClick(this);
            }


        }

        void ImageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.AfterImageKeyDown != null)
            {
                this.AfterImageKeyDown(this,e);
            }
        }

        void ImageBox_ZoomChanged(object sender, EventArgs e)
        {
            foreach (var imb in this.AllImageBoxCapture())
            {
                if (imb.Zoom != (sender as ImageBox).Zoom)
                    imb.Zoom = (sender as ImageBox).Zoom;
            }
        }

        void ClearImage(int Column, int Row)
        {

            var result = (ImageBox)this.tlpImages.GetControlFromPosition(Column, Row);
            if (result != null)
            {
                result.Clear();                
            }
        }

        ImageBoxCapture NewImageBox(int Column, int Row)
        {
            var result = new ImageBoxCapture(Column, Row);
            
            result.Dock = DockStyle.Fill;                 
            result.GridDisplayMode = ImageBoxGridDisplayMode.Client;
            result.SaveImageEdition = true;
            result.GridColor = this.ColorsUnSelected[0];
            result.GridColorAlternate = this.ColorsUnSelected[1];
            result.AutoPan = true;
            result.AllowClickZoom = false;
            result.AllowZoom = true;


            result.MouseClick += this.ImageBox_MouseClick;
            result.KeyDown += this.ImageBox_KeyDown;
            result.ZoomChanged += this.ImageBox_ZoomChanged;

            return result;
        }

        void AddImageBox()
        {
            for (int rw = 0; rw <= this.tlpImages.RowCount - 1; rw++)
            {

                for (int col = 0; col <= this.tlpImages.ColumnCount - 1; col++)
                {
                    Control ctrol = this.tlpImages.GetControlFromPosition(col, rw);
                    if (ctrol == null)
                    {
                        this.tlpImages.Controls.Add(NewImageBox(col, rw), col, rw);
                        this.Invalidate();
                    }
                }
            }
            RefreshStyles();
        }

        void RemoveLastColumn()
        {
            var col = this.tlpImages.ColumnCount - 1;

            for (int rw = 0; rw <= this.tlpImages.RowCount - 1; rw++)
            {
                var ctrol = this.tlpImages.GetControlFromPosition(col, rw);
                if (ctrol != null)
                {
                    ctrol.Dispose();
                }
            }
            this.tlpImages.ColumnCount -= 1;
            RefreshStyles();
            this.Invalidate();
        }

        void RemoveLastRow()
        {
            var rw = this.tlpImages.RowCount - 1;

            for (int col = 0; col <= this.tlpImages.ColumnCount - 1; col++)
            {
                var ctrol = this.tlpImages.GetControlFromPosition(col, rw);
                if (ctrol != null)
                {
                    ctrol.Dispose();
                }
            }
            this.tlpImages.RowCount -= 1;
            RefreshStyles();
            this.Invalidate();
        }
        
        void RefreshStyles()
        {

            this.tlpImages.RowStyles.Clear();
            this.tlpImages.ColumnStyles.Clear();
            for (int rw = 0; rw <= this.tlpImages.RowCount - 1; rw++)
            {

                this.tlpImages.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / this.tlpImages.RowCount));

                for (int col = 0; col <= this.tlpImages.ColumnCount - 1; col++)
                {
                    this.tlpImages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / this.tlpImages.ColumnCount));
                }
            }
        }

        #endregion

        #region Public
        public ImageBoxCapture LoadFromFile(string FileName)
        {
            ImageBoxCapture result = null;
            bool BreakAll = false;
            for (int rw = 0; rw <= this.tlpImages.RowCount - 1; rw++)
            {
                for (int col = 0; col <= this.tlpImages.ColumnCount - 1; col++)
                {
                    result = (ImageBoxCapture)this.tlpImages.GetControlFromPosition(col, rw);
                    if (result != null)
                    {
                        if (result.Image == null)
                        {
                            result.LoadFromFile(FileName);
                            BreakAll = true;
                            break;
                        }
                    }
                }

                if (BreakAll)
                    break;
            }

            if (AfterLoadFromFile != null)
                AfterLoadFromFile(this, result, FileName);

            return result;
        }

        public ImageBoxCapture LoadFromBitmap(Bitmap bmp, string FileName)
        {

            var listImbAll = this.tlpImages.Controls.OfType<ImageBoxCapture>().ToList();
            var listImb = listImbAll.Where(imb => imb.Image == null);
            if (listImb.Count() > 0)
            {
                var imb = listImb.First();
                imb.Image = bmp;
                imb.fileName = FileName;
                imb.ZoomToFit();
                return imb;
            }
            else if (listImbAll.Count() > 0)
            {
                ClearAllImages();
                var imb = listImbAll.First();               
                imb.Image = bmp;
                imb.fileName = FileName;
                imb.ZoomToFit();
                return imb;
            }
            else
                return null;
        }

        public void ExecuteCommand(ImageCommand Command, int Column, int Row)
        {
            if (Column > this.Columns-1 || Column < 0 || Row > this.Rows-1 || Row < 0)
                return;

            //Column -= 1;
            //Row -= 1;

            try
            {
                var ImgBC = (ImageBoxCapture)this.tlpImages.GetControlFromPosition(Column, Row);

                if (ImgBC != null)
                {
                    switch (Command)
                    {
                        case ImageCommand.Rotate:
                            {
                                ImgBC.Rotate();
                                break;
                            }
                        case ImageCommand.Crop:
                            {
                                ImgBC.Crop(CropState.Crop);
                                break;
                            }
                        case ImageCommand.CropStart:
                            {
                                ImgBC.AutoPan = false;
                                ImgBC.AllowClickZoom = false;
                                ImgBC.AllowZoom = false;
                                ImgBC.Crop(CropState.Start);
                                break;
                            }
                        case ImageCommand.CropStop:
                            {
                                ImgBC.AutoPan = true;
                                ImgBC.AllowClickZoom = false;
                                ImgBC.AllowZoom = true;
                                ImgBC.Crop(CropState.Stop);
                                break;
                            }
                        case ImageCommand.Eraser:
                            {
                                ImgBC.Eraser(CropState.Crop);
                                break;
                            }
                        case ImageCommand.EraserStart:
                            {
                                ImgBC.AutoPan = false;
                                ImgBC.AllowClickZoom = false;
                                ImgBC.AllowZoom = false;
                                ImgBC.Eraser(CropState.Start);
                                break;
                            }
                        case ImageCommand.EraserStop:
                            {
                                ImgBC.AutoPan = true;
                                ImgBC.AllowClickZoom = false;
                                ImgBC.AllowZoom = true;
                                ImgBC.Eraser(CropState.Stop);
                                break;
                            }
                        case ImageCommand.ZoomIn:
                            {
                                ImgBC.ZoomIn();
                                break;
                            }
                        case ImageCommand.ZoomOut:
                            {
                                ImgBC.ZoomOut();
                                break;
                            }
                        case ImageCommand.ZoomToFit:
                            {
                                ImgBC.ZoomToFit();
                                break;
                            }
                        case ImageCommand.Clear:
                            {
                                ClearImage(Column, Row);
                                break;
                            }
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void ClearAllImages()
        {
            for (var row = 0; row <= Rows - 1;row++ )
            {
                var col = 0;
                ClearImage(col, row);
                for (col = 0; col <= Columns - 1; col++)
                {
                    ClearImage(col, row);
                }
            }

            if (AfterClearAllImages != null)
                AfterClearAllImages(this);
        }

        public IEnumerable<ImageBoxCapture> AllImageBoxCapture()
        {
            var Ctrls = this.Controls.All();
            var ImgsCtrl = Ctrls.OfType<ImageBoxCapture>();
            return ImgsCtrl;
        }
        
        #endregion

        

        #endregion

    }
}
