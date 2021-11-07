using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RenewableEnergyCalculator.Controllers
{
    public class SolarEnergyController : Controller
    {
        // GET: SolarEnergy
        public ActionResult Index()
        {
            return View("SolarEnergy");
        }
    }
}