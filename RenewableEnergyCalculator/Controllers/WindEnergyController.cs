using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using MongoDB.Driver;
using RenewableEnergyCalculator.Calculator;
using RenewableEnergyCalculator.MailSystem;
using RenewableEnergyCalculator.Models;
using RenewableEnergyCalculator.Models.Wind;
using static RenewableEnergyCalculator.Controllers.SolarEnergyController;
using Image = iTextSharp.text.Image;

namespace RenewableEnergyCalculator.Controllers
{
    //////////////////////////////////////////////////////////////////////////////////////////
    // FileName: SolarEnergyController.cs
    // Author : Vasilita Irina
    // Description : This controller contains the flow control logic for the WindEnergy view 
    //////////////////////////////////////////////////////////////////////////////////////////
    
    public class WindEnergyController : Controller
    {
        private MongoDbContext db = new MongoDbContext();

        // GET: WindEnergy
        public ActionResult Index()
        {
            var turbines = GetTurbines();
            ViewBag.Turbines = turbines;

            var listCurrencies = new[] {
                new { Text = "Select currency...", Value = "0" },
                new { Text = "EUR", Value = "EUR" },
                new { Text = "RON", Value = "RON" }
            };

            var currencies = new SelectList(listCurrencies, "Value", "Text", 2);

            ViewBag.Currencies = currencies;
            return View("WindEnergy");
        }

        [HttpPost]
        public ActionResult InputData(InputWindData inputWindData)
        {
            if (ModelState.IsValid)
            {
                var dbturbine = db.TurbinesCollection.AsQueryable().ToList().First(x => x.Id == inputWindData.Turbine);
                

                var powerCurve = new PowerCurve(dbturbine.PowerCurveX.Select(x=>(double)x),
                    dbturbine.PowerCurveY.Select(x => (double)x));

                var turbine = new Turbine(
                    hubHeight: dbturbine.HubHeight,
                    cutInSpeed: dbturbine.CutInSpeed,
                    cutOutSpeed: dbturbine.CutOutSpeed,
                    powerCurve: powerCurve);

                var nrOfTurbines = inputWindData.NumberOfTurbines;

                var location = new GeographicalPoint(inputWindData.Lat, inputWindData.Lng);

                var cal = new WindEnergyCalculator( turbine, nrOfTurbines, location);


                var result = cal.Calculate();

                ViewBag.Location = inputWindData.Address;

                double w_to_gw(double w) => w / 1_000_000_000;

                ViewBag.MonthlyEnergyArray = result.MonthlyEnergyProduced.ToArray();
                ViewBag.AnnualEnergy = Math.Round(w_to_gw(result.YearlyEnergyProduced), 2);
                ViewBag.Turbine = dbturbine.Name;
                ViewBag.CapacityFactor = (result.CapacityFactor*100).ToString(".00");
                ViewBag.NumberOfTurbines = result.NumberOfTurbines;


                ViewBag.Currency = inputWindData.Currency;
                ViewBag.AnnualEnConsumption = inputWindData.AnnualEnConsumption;
                ViewBag.AnnualElPrice = inputWindData.AnnualElPrice;
                ViewBag.MonthlyEnergy = string.Join(",", result.MonthlyEnergyProduced.Select(w_to_gw));

            }
            else
            {
                ViewBag.Message = "Data is invalid.";
            }

            return View("WindResults");
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

            StringReader title = new StringReader("<h2 style='text-align:center;'>Wind Energy Report</h2><br>");
            StringReader solarTable = new StringReader(decodedString);

            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            var htmlparser = new HTMLWorker(pdfDoc);

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();
                htmlparser.Parse(title);
                image.ScaleAbsolute(image.Width / 3, image.Height / 3);
                image.SetAbsolutePosition((PageSize.A4.Width - image.ScaledWidth) / 2, (PageSize.A4.Height - image.ScaledHeight) / 3);
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
                mail.TryAddAtachement1(new MemoryStream(bytes1), "WindEnergyReport.pdf");

                mail.TrySendEmail();
            }

            return Json(true);
        }

        private List<SelectListItem> GetTurbines()
        {
            var turbinesList = db.TurbinesCollection.AsQueryable().ToList();
            //Find(turbine => true).ToList();

            List<SelectListItem> turbines = turbinesList.ConvertAll(a => {
                return new SelectListItem() {
                    Text = a.Name.ToString(),
                    Value = a.Id.ToString()
                };
            });

            turbines.Insert(0, new SelectListItem() {
                Text = "Select a turbine...",
                Value = "0"
            });

            return turbines;
        }
    }
}