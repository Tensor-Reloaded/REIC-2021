using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RenewableEnergyCalculator.Controllers
{
    public class WindEnergyController : Controller
    {
        // GET: WindEnergy
        public ActionResult Index()
        {
            return View("WindEnergy");
        }
    }
}