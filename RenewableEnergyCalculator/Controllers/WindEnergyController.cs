using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RenewableEnergyCalculator.Controllers
{
    //////////////////////////////////////////////////////////////////////////////////////////
    // FileName: SolarEnergyController.cs
    // Author : Vasilita Irina
    // Description : This controller contains the flow control logic for the WindEnergy view 
    //////////////////////////////////////////////////////////////////////////////////////////
    
    public class WindEnergyController : Controller
    {
        // GET: WindEnergy
        public ActionResult Index()
        {
            return View("WindEnergy");
        }
    }
}