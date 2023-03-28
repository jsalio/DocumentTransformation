using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf;

namespace Boundaries.PdfEngine
{
    public class AsposeDocumentConverter : IPdfConverter
    {

        public AsposeDocumentConverter()
        {
            byte[] licenseFile = Properties.Resources.Aspose_Total;
            MemoryStream stream = new MemoryStream(licenseFile);
            Aspose.Pdf.License barcodeLicense = new Aspose.Pdf.License();
            barcodeLicense.SetLicense(stream);
        }

        Task<string> IPdfConverter.GenerateDocument(IEnumerable<string> pages)
        {
            try
            {
                return Task<string>.Factory.StartNew(() =>
                {
                    using (MemoryStream mStream = new MemoryStream())
                    {
                        Document doc = new Document();
                        foreach (var page in pages)
                        {
                            InsertPageToDocument(ref doc, page);
                        }

                        doc.OptimizeSize = true;
                        doc.Save(mStream, SaveFormat.Pdf);
                        var byteArr = mStream.ToArray();
                        return Convert.ToBase64String(byteArr);
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        void InsertPageToDocument(ref Document document, string imageSrc)
        {
            System.Drawing.Image srcImage = System.Drawing.Image.FromFile(imageSrc);
            int h = srcImage.Height;
            int w = srcImage.Width;
            Page page = document.Pages.Add();
            Aspose.Pdf.Image image = new Aspose.Pdf.Image();
            image.File = (imageSrc);
            page.PageInfo.Height = (h);
            page.PageInfo.Width = (w);
            page.PageInfo.Margin.Bottom = (0);
            page.PageInfo.Margin.Top = (0);
            page.PageInfo.Margin.Right = (0);
            page.PageInfo.Margin.Left = (0);
            page.Paragraphs.Add(image);
        }
    }
}
