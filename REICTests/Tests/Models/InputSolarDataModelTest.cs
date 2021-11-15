using Microsoft.VisualStudio.TestTools.UnitTesting;
using REICTests.Tests.Helpers;
using RenewableEnergyCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace REICTests
{
    /// <summary>
    /// Summary description for InputSolarDataModelTest
    /// </summary>
    [TestClass]
    public class InputSolarDataModelTest
    {
        [TestMethod]
        public void Validate_Model_Given_Valid_Model_ExpectNoValidationErrors()
        {
            var model = new InputSolarData()
            {
                Address = "Iasi, Romania",
                Lat = 24.513,
                Lng = 60.634,
                Orientation = "N",
                Slope = 30,
                Area = 20,
                Currency = "RON",
                PanelType = "Panel1",
                AnnualEnConsumption = 100,
                AnnualElPrice = 100.23
            };

            var results = TestModelHelper.Validate(model);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Validate_Model_Given_Invalid_Model_ExpectNoValidationErrors()
        {
            var model = new InputSolarData()
            {
                Address = "Iasi, Romania",
                Lat = 24.513,
                Lng = 60.634,
                Orientation = "N",
                Slope = -90,
                Area = -9,
                Currency = "RON",
                PanelType = "Panel1",
                AnnualEnConsumption = -100,
                AnnualElPrice = -100.23
            };

            var results = TestModelHelper.Validate(model);

            Assert.AreEqual(4, results.Count);
            Assert.AreEqual("Please enter a value grater than 0.", results[0].ErrorMessage);
        }
    }
}
