using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using RenewableEnergyCalculator.Calculator;
using RenewableEnergyCalculator.Models;
using RenewableEnergyCalculator.Models.Wind;

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