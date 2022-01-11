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
        private readonly MongoDbContext db = new MongoDbContext();

        // GET: SolarEnergy
        public ActionResult Index()
        {
            ViewBag.Orientations = GetOrientations();
            ViewBag.PanelTypes = GetPanelTypes();
            ViewBag.Panels = GetPanels();
            ViewBag.Currencies = GetCurrencies();

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
                var payback = SolarEnergyCalculator.CalculateROI(panelsCost, inputSolarData.AnnualEnConsumption, inputSolarData.AnnualElPrice);


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

            var solarTableData = Convert.FromBase64String(emailData.SolarTable);
            string decodedString = Encoding.UTF8.GetString(solarTableData);

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                StringReader title = new StringReader("<h2 style='text-align:center;'>Solar Energy Report</h2><br>");
                StringReader table = new StringReader(decodedString);
                Image image = Image.GetInstance(bytes);

                var emailBytes = PDFGenerator.PDFGenerator.GeneratePDF(memoryStream, image, title, table);

                // prepare the mail
                var mail = new Mail();
                
                var recipient = new Recepient("User", emailData.Email);

                mail.TrySetRecipient(recipient);
                mail.TrySetSubject();
                mail.TrySetBody("Thank you for choosing our app!\n\nPlease, find the requested report attached to this email.");
                mail.TryAddAtachement1(new MemoryStream(emailBytes), "SolarEnergyReport.pdf");

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


        private List<SelectListItem> GetPanels()
        {
            var panels = db.PanelsCollection.Find(panel => true).ToList();

            List<SelectListItem> panelsList = panels.ConvertAll(a => {
                return new SelectListItem() {
                    Text = a.Model.ToString() + "(" + a.Manufacturer.ToString() + ")",
                    Value = a.Id.ToString()
                };
            });

            panelsList.Insert(0, new SelectListItem() {
                Text = "Select a panel...",
                Value = "0"
            });

            return panelsList;
        }

        public static List<SelectListItem> GetCurrencies()
        {
            var listCurrencies = new List<SelectListItem> {
                new SelectListItem { Text = "Select currency...", Value = "0" },
                new SelectListItem { Text = "EUR", Value = "EUR" },
            };

            return listCurrencies;
        }

        public class EmailData
        {
            public string Email { get; set; }
            public string SolarChart { get; set; }
            public string SolarTable { get; set; }
        }

    }
}