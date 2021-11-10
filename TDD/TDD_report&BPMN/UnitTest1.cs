using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTests;

namespace PercentCalculatorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            double totalenergy = 7409; //kilowatt-hour per person
            double REEnergyRroduced = 1402;
            double expected = 18.92;
            //Act
            EnergyCalculator energyCalculator = new EnergyCalculator();
            double percent = energyCalculator.CalculatePercent(totalenergy, REEnergyRroduced);
            //Assert
            Assert.AreEqual(expected, percent,0.1);
        }
    }
}
