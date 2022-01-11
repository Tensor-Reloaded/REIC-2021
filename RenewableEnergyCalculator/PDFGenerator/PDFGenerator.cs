using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using RenewableEnergyCalculator.Models.Wind;

namespace RenewableEnergyCalculator.PDFGenerator
{
    public static class PDFGenerator
    {
        public static byte[] GeneratePDF(MemoryStream memoryStream, Image image, StringReader title, StringReader table)
        {
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            var htmlparser = new HTMLWorker(pdfDoc);

            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            pdfDoc.Open();
            htmlparser.Parse(title);
            image.ScaleAbsolute(image.Width / 3, image.Height / 3);
            image.SetAbsolutePosition((PageSize.A4.Width - image.ScaledWidth) / 2, (PageSize.A4.Height - image.ScaledHeight) / 3);
            pdfDoc.Add(image);
            htmlparser.Parse(table);
            pdfDoc.Close();
            byte[] bytes1 = memoryStream.ToArray();
            memoryStream.Close();

            return bytes1;
        }
        
    }
}