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
using System.Text;
using System.Web;
using System.Net;
using System.Text.Json;
using System.Globalization;

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
            ViewBag.Orientations = GetOrientations();
            ViewBag.PanelTypes = GetPanelTypes();

            var panels = db.PanelsCollection.Find(panel => true).ToList();

            List<SelectListItem> panelsList = panels.ConvertAll(a => {
                return new SelectListItem() {
                    Text = a.Model.ToString()+ "("+a.Manufacturer.ToString()+")",
                    Value = a.Id.ToString()
                };
            });

            panelsList.Insert(0, new SelectListItem() {
                Text = "Select a panel...",
                Value = "0"
            });

            var panelss = new SelectList(panelsList, "Value", "Text", 0);

            ViewBag.Panels = panelss;


            var listCurrencies = new[] {
                new { Text = "Select currency...", Value = "0" },
                new { Text = "EUR", Value = "EUR" },
            };

            var currencies = new SelectList(listCurrencies, "Value", "Text", 2);

            ViewBag.Currencies = currencies;

            return View("SolarEnergy");
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

                var panelsCost = panel.Cost * calculator.GetNrOfPanels(inputSolarData.Width * inputSolarData.Length, panel.Area);

                var payback = calculator.CalculateROI(panelsCost, inputSolarData.AnnualEnConsumption, inputSolarData.AnnualElPrice);


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
                ViewBag.Payback = payback;

                //ViewBag.MonthlyRadiation = string.Join(",", averageMonthlyRadiation);
                //ViewBag.AnnualRadiation = averageAnnualRadiation;
                //ViewBag.MonthlyEnergy = string.Join(",", monthlyEnergy);
                
                
            }
            else
            {
                ViewBag.Message = "Data is invalid.";
            }

            return View("SolarResults");
        }


        [HttpPost]
        [Obsolete]
        public ActionResult SendEmail(string json)
        {

            EmailData emailData = JsonSerializer.Deserialize<EmailData>(json);

            byte[] bytes = Convert.FromBase64String(emailData.SolarChart.Split(',')[1]);
            Image image = Image.GetInstance(bytes);

            var solarTableData = Convert.FromBase64String(emailData.SolarTable);
            string decodedString = Encoding.UTF8.GetString(solarTableData);

            StringReader title = new StringReader("<h2 style='text-align:center;'>Solar Energy Report</h2><br>");
            StringReader solarTable = new StringReader(decodedString);

            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            var htmlparser = new HTMLWorker(pdfDoc);

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();
                htmlparser.Parse(title);
                image.ScaleAbsolute(image.Width/3, image.Height/3);
                image.SetAbsolutePosition((PageSize.A4.Width - image.ScaledWidth) / 2, (PageSize.A4.Height - image.ScaledHeight)/3);
                pdfDoc.Add(image);
                htmlparser.Parse(solarTable);
                pdfDoc.Close();
                byte[] bytes1 = memoryStream.ToArray();
                memoryStream.Close();


                // prepare the mail
                var mail = new Mail();
                
                var recipient = new Recepient("User", emailData.Email);

                mail.TrySetRecipient(recipient);
                mail.TrySetSubject();
                mail.TrySetBody("Thank you for choosing our app!\n\nPlease, find the requested report attached to this email.");
                mail.TryAddAtachement1(new MemoryStream(bytes1), "SolarEnergyReport.pdf");

                mail.TrySendEmail();
            }

            return Json(true);
        }

        private List<SelectListItem> GetOrientations()
        {
            var orientations = new List<SelectListItem> {
                new SelectListItem { Text = "Select orientation...", Value = "0", Selected=true},
                new SelectListItem { Text = "N", Value = "N"},
                new SelectListItem { Text = "S", Value = "S"}
            };

            return orientations;
        }

        private List<SelectListItem> GetPanelTypes()
        {
            var panelTypes = db.PanelTypesCollection.Find(panelType => true).ToList();

            List<SelectListItem> panels = panelTypes.ConvertAll(a => {
                return new SelectListItem() {
                    Text = a.PType.ToString(),
                    Value = a.Id.ToString()
                };
            });

            panels.Insert(0, new SelectListItem() {
                Text = "Select a panel type...",
                Value = "0"
            });

            return panels;
        }

        public class EmailData
        {
            public string Email { get; set; }
            public string SolarChart { get; set; }
            public string SolarTable { get; set; }
        }

    }
}