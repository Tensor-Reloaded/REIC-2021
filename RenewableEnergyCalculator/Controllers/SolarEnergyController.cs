using MongoDB.Driver;
using RenewableEnergyCalculator.Models;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using RenewableEnergyCalculator.Calculator;
using RenewableEnergyCalculator.MailSystem;
using RenewableEnergyCalculator.Models.Solar;
using System;
using System.IO;
using System.Net.Mail;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace RenewableEnergyCalculator.Controllers
{

    //////////////////////////////////////////////////////////////////////////////////////////
    // FileName: SolarEnergyController.cs
    // Author : Vasilita Irina
    // Description : This controller contains the flow control logic for the SolarEnergy view 
    //////////////////////////////////////////////////////////////////////////////////////////

    public class SolarEnergyController : Controller
    {
        private MongoDbContext db = new MongoDbContext();

        // GET: SolarEnergy
        public ActionResult Index()
        {
            /*PanelType panelType1 = new PanelType(Type.Monocrystalline, 25, 25);
            PanelType panelType2 = new PanelType(Type.Passivated_Emitter_and_Rear_Cell_PERC, 20, 20);
            PanelType panelType3 = new PanelType(Type.Polycrystalline, 15, 17);
            PanelType panelType4 = new PanelType(Type.Thin_Film_Cadmium_Telluride_CdTe, 9, 11);


            var panelTypes = new List<PanelType>() { panelType1, panelType2, panelType3, panelType4};

            var panel1 = new SolarPanel();
            panel1.Id = "1";
            panel1.Model = "Evervolt";
            panel1.Efficiency = 21.2;
            panel1.Cost = 120;
            panel1.Manufacturer = "Panasonic";

            var panel2 = new SolarPanel();
            panel2.Id = "2";
            panel2.Model = "LAM60S20";
            panel2.Efficiency = 21;
            panel2.Cost = 120;
            panel2.Manufacturer = "JA Solar";

            var panels = new List<SolarPanel>() { panel1, panel2 };*/

            var panelTypes = db.PanelTypesCollection.Find(panelType => true).ToList();
            var panels = db.PanelsCollection.Find(panel => true).ToList();

            var model = new ViewModel() {
                Panels = panels,
                PanelTypes = panelTypes
            };

            return View("SolarEnergy", model);
        }

        [HttpPost]
        public ActionResult InputData(InputSolarData inputSolarData)
        {
            if (ModelState.IsValid)
            {
                GetSolarRadiationAPI apiRad = new GetSolarRadiationAPI(inputSolarData.Lat, inputSolarData.Lng);
                apiRad.getAPIData();

                List<double> SI_EF_TILTED_SURFACE_HORIZONTAL = apiRad.SI_EF_TILTED_SURFACE_HORIZONTAL;
                List<double> SI_EF_TILTED_SURFACE_LAT_MINUS15 = apiRad.SI_EF_TILTED_SURFACE_LAT_MINUS15;
                List<double> SI_EF_TILTED_SURFACE_LATITUDE = apiRad.SI_EF_TILTED_SURFACE_LATITUDE;
                List<double> SI_EF_TILTED_SURFACE_LAT_PLUS15 = apiRad.SI_EF_TILTED_SURFACE_LAT_PLUS15;
                List<double> SI_EF_TILTED_SURFACE_VERTICAL = apiRad.SI_EF_TILTED_SURFACE_VERTICAL;
                List<double> SI_EF_TILTED_SURFACE_OPTIMAL = apiRad.SI_EF_TILTED_SURFACE_OPTIMAL;
                List<double> SI_EF_TILTED_SURFACE_OPTIMAL_ANG = apiRad.SI_EF_TILTED_SURFACE_OPTIMAL_ANG;
                string SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT = apiRad.SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT;

                SolarRadiation solarRadiation = new SolarRadiation(SI_EF_TILTED_SURFACE_HORIZONTAL, SI_EF_TILTED_SURFACE_LAT_MINUS15, SI_EF_TILTED_SURFACE_LATITUDE, SI_EF_TILTED_SURFACE_LAT_PLUS15, 
                    SI_EF_TILTED_SURFACE_VERTICAL, SI_EF_TILTED_SURFACE_OPTIMAL, SI_EF_TILTED_SURFACE_OPTIMAL_ANG, SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT, inputSolarData.Slope, inputSolarData.Lat, inputSolarData.Orientation);

                
                var panel = db.PanelsCollection.Find(x => x.Id == inputSolarData.Panel).ToList().First();
                var panelType = db.PanelTypesCollection.Find(x => x.Id == inputSolarData.PanelType).ToList().First();

                var averageMonthlyRadiation = solarRadiation.GetAverageMonthlyRadiation();
                var averageAnnualRadiation = solarRadiation.GetAverageAnnualRadiation(averageMonthlyRadiation);

                SolarEnergyCalculator calculator = new SolarEnergyCalculator();

                var monthlyEnergy = calculator.CalculateMonthlyEnergy(inputSolarData.Width, inputSolarData.Length, panel.Area, panel.Efficiency, averageMonthlyRadiation, 0.75);
                var annualEnergy = calculator.CalculateAnnualEnergy(monthlyEnergy);

                ViewBag.Location = inputSolarData.Address;
                ViewBag.RoofWidth = inputSolarData.Width;
                ViewBag.RoofLength = inputSolarData.Length;
                ViewBag.Slope = inputSolarData.Slope;
                ViewBag.Orientation = inputSolarData.Orientation;
                ViewBag.Panel = panel.Model + " ("+panel.Manufacturer+")";
                ViewBag.PanelEfficieny = panel.Efficiency;
                ViewBag.PanelArea = panel.Area;
                ViewBag.PanelCost = panel.Cost;
                ViewBag.PanelType = panelType.PType;

                ViewBag.MonthlyEnergyArray = monthlyEnergy;
                ViewBag.AnnualEnergy = Math.Round(annualEnergy, 2);



                ViewBag.Currency = inputSolarData.Currency;
                ViewBag.AnnualEnConsumption = inputSolarData.AnnualEnConsumption;
                ViewBag.AnnualElPrice = inputSolarData.AnnualElPrice;
                ViewBag.MonthlyRadiation = string.Join(",", averageMonthlyRadiation);
                ViewBag.AnnualRadiation = averageAnnualRadiation;
                ViewBag.MonthlyEnergy = string.Join(",", monthlyEnergy);
                
                
            }
            else
            {
                ViewBag.Message = "Data is invalid.";
            }

            return View("SolarResults");
        }

        [HttpPost]
        [Obsolete]
        public ActionResult SendEmail(string email, string info, string imageURI)
        {
            // imageURI is not complete 

            byte[] bytes = Convert.FromBase64String(imageURI.Split(',')[1]);
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(bytes);

            ViewBag.Email = email;
            StringReader sr = new StringReader("<b> Solar Energy Output Report <b> \n");

            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            var htmlparser = new HTMLWorker(pdfDoc);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Add(image);
                pdfDoc.Close();
                byte[] bytes1 = memoryStream.ToArray();
                memoryStream.Close();

                // prepare the mail
                var mail = new Mail();

                var recipient = new Recepient("User", email);

                mail.TrySetRecipient(recipient);

                mail.TrySetSubject();

                mail.TrySetBody("The solar energy output is" + info);

                mail.TryAddAtachement1(new MemoryStream(bytes1), "SolarEnergyReport.pdf");

                mail.TrySendEmail();
            }

            return View("EmailSent");
        }

        public class ViewModel
        {
            public List<SolarPanel> Panels { get; set; }
            public List<PanelType> PanelTypes { get; set; }
        }
    }
}