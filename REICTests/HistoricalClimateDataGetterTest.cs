using Microsoft.VisualStudio.TestTools.UnitTesting;
using REIC;
using System;
using System.Linq;

namespace REICTests
{
    [TestClass]
    public class HistoricalClimateDataGetterTest
    {
        [TestMethod]
        public void TestResultsFetch()
        {
            var getter = new HistoricalClimateDataGetter();

            var iasiPos = new GeographicalPoint(47.156944, 27.590278);

            var res = getter.GetValuesAtPoint(iasiPos);
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void TestResultsCount()
        {
            var t0 = new DateTime(2021, 11, 1);
            var t1 = new DateTime(2021, 11, 5);
            var getter = new HistoricalClimateDataGetter();

            var iasiPos = new GeographicalPoint(47.156944, 27.590278);

            var res = getter.GetValuesAtPoint(iasiPos, t0, t1);

            Assert.AreEqual(5, res.Count());
            Assert.AreEqual("11/01/2021", res.First().DateTime);
            Assert.AreEqual("11/05/2021", res.Last().DateTime);


            Assert.IsNotNull(res);
        }


        [TestMethod]
        public void TestYear()
        {
            var t0 = new DateTime(2020, 1, 1);
            var t1 = new DateTime(2020, 12, 31);
            var getter = new HistoricalClimateDataGetter();

            var iasiPos = new GeographicalPoint(47.156944, 27.590278);

            var res = getter.GetValuesAtPoint(iasiPos, t0, t1);

            Assert.AreEqual((int)(t1 - t0).TotalDays, res.Count() + 1);


            int i = 0;

            for (var t = t0; t <= t1; t = t.AddDays(1))
            {
                Assert.AreEqual($"{t.Month:00}/{t.Day:00}/{t.Year:0000}", res[i].DateTime);
            }

            Assert.IsNotNull(res);
            
        }
        
        [TestMethod]
        public void TestWrongKey()
        {
            var getter = new HistoricalClimateDataGetter();
            getter.Key = "";

            var iasiPos = new GeographicalPoint(47.156944, 27.590278);

            void f()
            {
                getter.GetValuesAtPoint(iasiPos);
            }

            Assert.ThrowsException<Exception>(f);

        }
    }
}
