using REIC;
using System.Collections.Generic;
using System;
using System.Linq;

static class Program
{
    static void Main()
    {
        var murmanskPos = new GeographicalPoint(latitude: 69.009358, longitude: 32.946704);
        var iasiPos = new GeographicalPoint(47.156944, 27.590278);
        var melbournePos = new GeographicalPoint(-37.813611, 144.963056);
        var onShoreA1Pos = new GeographicalPoint(28.899, 45.084);
        var onShoreA2Pos = new GeographicalPoint(29.635, 44.952);

        var chicagoPos = new GeographicalPoint(41.881944, -87.627778);

        var location =
            murmanskPos;
       
        var powerCurveA = new PowerCurve(new List<Tuple<double, double>>() {
            new Tuple<double, double>(4, 0),
            //new Tuple<double, double>(1, 5_000),//5 MW

            new Tuple<double, double>(12.5, 5_000),//5 MW
            new Tuple<double, double>(25, 5_000),//5 MW
        });

        var powerCurve12 = new PowerCurve(new List<Tuple<double, double>>() {
            new Tuple<double, double>(0, 0),

            new Tuple<double, double>(4, 0),
            //new Tuple<double, double>(1, 5_000),//5 MW

            new Tuple<double, double>(11.5, 10),//_000),//10 MW
            new Tuple<double, double>(25, 10)//,_000),//10 MW
        });

        var powerCurve = powerCurve12;

        var turbine = new Turbine(
            hubHeight: 100,
            cutInSpeed: 4,
            cutOutSpeed: 25, powerCurve);

        var cal = new WindEnergyCalculator(//prob, 
            turbine, numberOfTurbines: 1, location);

        var res = cal.Calculate();

        var montlyTemps = new HistoricalClimateDataGetter().GetMonthlyMeanTemperatures(location);

        Console.WriteLine($"monthly temps: {string.Join(", ", montlyTemps.Select(x => x.ToString(".0")))}");

        Console.WriteLine($"monthly: {string.Join(", ", res.MonthlyEnergyProduced.Select(x => x.ToString(".0")))}");

        Console.WriteLine($"Total: {res.YearlyEnergyProduced:0.}");
        Console.WriteLine($"Maximum yearly energy: {res.MaximumYearlyEnergy:0.}");

        Console.WriteLine($"Capacity factor: {res.CapacityFactor*100:0.00} (>30% for wind energy is pretty good)");
    }
}
