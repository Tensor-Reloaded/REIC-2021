using Microsoft.VisualStudio.TestTools.UnitTesting;
using RenewableEnergyCalculator.Controllers;
using System.Web.Mvc;

namespace REICTests
{
    [TestClass]
    public class WindEnergyControllerTest
    {
        [TestMethod]
        public void TestWinEnergyView()
        {
            var controller = new WindEnergyController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("WindEnergy", result.ViewName);
        }
    }
}
