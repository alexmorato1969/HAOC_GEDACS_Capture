using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ConversorPDF
{
    class PDFWrapperX : PDFLibNet.PDFWrapper
    {
        public PDFWrapperX()
        {
            this.FileName = string.Empty;
        }
        public string FileName { get; set; }
        public Bitmap ToBitmap(int page)
        {
            using (var box = new PictureBox())
            {
                // have to give the document a handle to render into
                this.RenderPage(box.Handle);

                // create an image to draw the page into
                var buffer = new Bitmap(this.PageWidth, this.PageHeight);
                this.ClientBounds = new Rectangle(0, 0, this.PageWidth, this.PageHeight);
                using (var g = Graphics.FromImage(buffer))
                {
                    var hdc = g.GetHdc();
                    try
                    {
                        this.DrawPageHDC(hdc);
                    }
                    finally
                    {
                        g.ReleaseHdc();
                    }
                }

                return buffer;
            }
        }
    }

    class PDFUtils
    {
        PDFWrapperX PDF;

        PDFUtils()
        {
            PDF = new PDFWrapperX();
        }

        ~PDFUtils()
        {
            PDF.Dispose();
            PDF = null;
        }

        public void SaveImage(string FileName, ImageFormat Formato)
        {
        }
    }
}
