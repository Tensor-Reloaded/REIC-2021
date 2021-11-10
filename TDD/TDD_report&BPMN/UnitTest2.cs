using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests;

namespace PercentCalculatorTest
{
    
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            double totalenergy = 7409; //kilowatt-hour per person
            double REEnergyRroduced = 1141;
            double BioWaste = 261;
            double expected = 6007;
            //Act
            EnergyCalculator energyCalculator = new EnergyCalculator();
            double percent = energyCalculator.CalculateNonReEnergy(totalenergy, REEnergyRroduced, BioWaste);
            //Assert
            Assert.AreEqual(expected, percent, 0.1);
        }
    }
}
