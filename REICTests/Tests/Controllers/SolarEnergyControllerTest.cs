using Microsoft.VisualStudio.TestTools.UnitTesting;
using RenewableEnergyCalculator.Controllers;
using System.Web.Mvc;

namespace REICTests
{
    [TestClass]
    public class SolarEnergyControllerTest
    {
        [TestMethod]
        public void TestSolarEnergyView()
        {
            var controller = new SolarEnergyController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("SolarEnergy", result.ViewName);
        }
    }
}
