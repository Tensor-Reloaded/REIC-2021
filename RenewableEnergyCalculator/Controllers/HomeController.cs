using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace RenewableEnergyCalculator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("About");
        }

        public ActionResult Contact()
        {
            return View("Contact");
        }
    }
}