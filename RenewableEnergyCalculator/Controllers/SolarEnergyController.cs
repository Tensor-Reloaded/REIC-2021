using MongoDB.Driver;
using RenewableEnergyCalculator.Models;
using RenewableEnergyCalculator.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace RenewableEnergyCalculator.Controllers
{
    public class SolarEnergyController : Controller
    {
        private MongoDbContext db = new MongoDbContext();

        // GET: SolarEnergy
        public ActionResult Index()
        {
            var panels = db.PanelsCollection.Find(panel => true).ToList();

            return View("SolarEnergy", panels);
        }


        [HttpPost]
        public ActionResult InputData(InputSolarData inputSolarData)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Address = inputSolarData.Address;
                ViewBag.Area = inputSolarData.Area;
                ViewBag.Slope = inputSolarData.Slope;
                ViewBag.Orientation = inputSolarData.Orientation;
                ViewBag.PanelType = inputSolarData.PanelType;
                ViewBag.Currency = inputSolarData.Currency;
                ViewBag.AnnualEnConsumption = inputSolarData.AnnualEnConsumption;
                ViewBag.AnnualElPrice = inputSolarData.AnnualElPrice;
            }
            else
            {
                ViewBag.Message = "Data is invalid.";
            }

            return View("SolarResults", inputSolarData);
        }
    }
}