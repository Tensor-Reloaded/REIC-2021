using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;

namespace builderReport
{
    public class Generate
    {
        static PdfDocument Write_pdf()
        {
            var products = new List<ResultEnergyData>
        {
            new ResultEnergyData{ ... },
            new ResultEnergyData { ... }

        };
            var builder = new DataReportBuilder(products);
            var director = new DataReportDirector(builder);
            director.BuildReport();
            var report = builder.GetReport();

            PdfWriter writer = new PdfWriter("C:\Report.pdf");
            PdfDocument pdfreport = new PdfDocument(writer);
            Document document = new Document(pdfreport);
            Paragraph header = new Paragraph("Report").SetTextAlignment(TextAlignment.CENTER).SetFontSize(14);
            document.Add(header);

            Paragraph body = new Paragraph(report.ToString()).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(12);
            int n = pdfreport.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String.Format(i + "/" + n)), 559, 10, i, TextAlignment.RIGHT, VerticalAlignment.BOTTOM, 0);
            }

            document.Close();
            return pdfreport;
        }

        static void Pdf_to_jpg(string filepath)
        {
            PdfDocument reportPdf = Write_pdf();
            string ext = System.IO.Path.GetExtension(filepath);
            for (int i = 0; i < reportPdf.GetNumberOfPages(); i++)
            {
                // System.Drawing.Image image = reportPdf.SaveAsImage(i, 96, 96);
                //  image.Save(string.Format("ImagePage{0}.png", i), System.Drawing.Imaging.ImageFormat.Png);

            }
        }
        static void CreatePdf(string[] args)
        {
            /*  var products = new List<Data>
          {
              new Data { ... },
              new Data { ... },

          };
              var builder = new DataReportBuilder(products);
              var director = new DataReportDirector(builder);
              director.BuildReport();
              var report = builder.GetReport();
              Console.WriteLine(report);*/
              PdfDocument _report = Write_pdf();
        }
    }
}
