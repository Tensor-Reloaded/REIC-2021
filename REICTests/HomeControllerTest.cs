using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using RenewableEnergyCalculator.Controllers;

namespace REICTests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void TestAboutView()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("About", result.ViewName);
        }

        [TestMethod]
        public void TestContactView()
        {
            var controller = new HomeController();
            var result = controller.Contact() as ViewResult;
            Assert.AreEqual("Contact", result.ViewName);
        }
    }
}
