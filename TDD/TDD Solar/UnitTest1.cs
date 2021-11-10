using Microsoft.VisualStudio.TestTools.UnitTesting;
using REIC;

namespace SolarTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            double minEff = 15;
            double maxEff = 20;
            double expected = 17.5;
            PanelType panelType = new PanelType("monocrystalline", minEff, maxEff);

            //Act
            double avgEff = panelType.getMedianEff();

            //Assert
            Assert.AreEqual(expected, avgEff, 0.001, "Average efficiency not calculated correctly");


        }
    }
}
