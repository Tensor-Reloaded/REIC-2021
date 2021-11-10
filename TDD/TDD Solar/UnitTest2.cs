using Microsoft.VisualStudio.TestTools.UnitTesting;
using REIC;

namespace SolarTests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod2()
        {
            //Arrange
            double minEff = 15;
            double maxEff = 20;
            PanelType panelType = new PanelType("monocrystalline", minEff, maxEff);

            //Act
            panelType.setMaxEff(2);

            //Assert
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => panelType.getMedianEff());


        }
    }
}