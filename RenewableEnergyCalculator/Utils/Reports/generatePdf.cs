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
using REIC;
using iText.Kernel.Colors;
using System.Threading;

namespace pdf_writer
{
    public class generatePdf
    {
        String REICmethod = "turbine";
        public void Write_pdf()
        {
            PdfWriter writer = new PdfWriter(@"C:\Users\teodo\Desktop\Master\REIC_report\Report.pdf");
            PdfDocument report = new PdfDocument(writer);
            Document document = new Document(report);
            Paragraph header = new Paragraph("Report").SetTextAlignment(TextAlignment.CENTER).SetFontSize(14);
            document.Add(header);

            Paragraph space = new Paragraph("").SetTextAlignment(TextAlignment.CENTER).SetFontSize(1.0f);
            document.Add(space);

            PowerCurve powerCurve = new PowerCurve();
            Turbine turbine = new Turbine(87, 37, 40, new PowerCurve());

            if (REICmethod.Equals("turbine"))
            {
                Paragraph turbine_paragraph = new Paragraph("Turbine data: \n").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12);
                document.Add(turbine_paragraph);

                Paragraph turbine_content_paragraph = new Paragraph("hubHeight: " + turbine.HubHeight + "\n cutInSpeed: " + turbine.CutInSpeed + "\n cutOutSpeed: " + turbine.CutOutSpeed + "\n").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12);
                document.Add(turbine_content_paragraph);
            
                String chart = @"C:\Users\teodo\Desktop\Master\REIC_report\chart.png";
                ImageData _chartData = ImageDataFactory.Create(chart);
                Image chartImg = new Image(_chartData);
                chartImg.SetWidth(450);
                chartImg.SetHeight(300);
                document.Add(chartImg);

                ResultWindEnergyData resultWindEnergyData = new ResultWindEnergyData();
                Paragraph resultWindEnergy_paragraph = new Paragraph("Yearly Energy Produced: "+ resultWindEnergyData.YearlyEnergyProduced + "\n").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12);
                document.Add(resultWindEnergy_paragraph);
            }
            document.Add(space);
            document.Add(space);

            Paragraph location_paragraph = new Paragraph("Location: \n").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12);
            document.Add(location_paragraph);


            Thread.Sleep(5000);
            String map = @"C:\Users\teodo\Desktop\Master\REIC_report\map.png";
            ImageData _mapData = ImageDataFactory.Create(map);
            Image mapImg = new Image(_mapData).SetHorizontalAlignment(HorizontalAlignment.RIGHT);
            mapImg.SetWidth(400);
            mapImg.SetHeight(250);
            document.Add(mapImg);

            int n = report.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String.Format(i + "/" + n)), 559, 10, i, TextAlignment.RIGHT, VerticalAlignment.BOTTOM, 0);
            }

            document.Close();
        }
    }
}
