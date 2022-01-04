using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using RenewableEnergyCalculator.Calculator;
using RenewableEnergyCalculator.Models;
using RenewableEnergyCalculator.Models.WInd;

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

            return View("WindEnergy");
        }

        [HttpPost]
        public ActionResult InputData(InputWindData inputWindData)
        {
            if (ModelState.IsValid)
            {
                
                var turbine = db.TurbinesCollection.Find(x => x.Id == inputWindData.Turbine).ToList().First();
                

                var powerCurve = new PowerCurve(new List<Tuple<double, double>>() {
                    new Tuple<double, double>(turbine.PowerCurveX[0], turbine.PowerCurveY[0]),
                    new Tuple<double, double>(turbine.PowerCurveX[1], turbine.PowerCurveY[1]),//5 MW

                    new Tuple<double, double>(turbine.PowerCurveX[2], turbine.PowerCurveY[2]),//5 MW
                    new Tuple<double, double>(turbine.PowerCurveX[3], turbine.PowerCurveY[3]),//5 MW)
                });

                var nrOfTurbines = Int32.Parse(inputWindData.NumberOfTurbines);

                var cal = new WindEnergyCalculator( turbine, nrOfTurbines, location);
            }
            else
            {
                ViewBag.Message = "Data is invalid.";
            }

            return View("WindResults");
        }

        private List<SelectListItem> GetTurbines()
        {
            var turbinesList = db.TurbinesCollection.Find(turbine => true).ToList();

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