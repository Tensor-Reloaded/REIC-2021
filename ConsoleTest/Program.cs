using REIC;
using System.Collections.Generic;
using System;
using System.Linq;

static class Program
{
    static void Main()
    {
        /*
        //TODO test the loop
        var g = new HistoricalClimateDataGetter();

     
        var pos = melbournePos;

        var data = g.GetValuesAtPoint(onShore
            //, new DateTime(2021, 11, 1)
            //, 1
            );

        foreach (var v in data)
        {
            Console.WriteLine($"{v.Temperature}; {v.WindSpeed}");
        }

        Console.WriteLine(data);
        */


        ///return;
        /*
        List<double> windSpeeds = new List<double>() { //data for the last year
            12.024,12.24,10.584,12.708,12.6,11.592,10.224,14.04,12.312,12.672,14.58,14.004,13.644,14.58,13.68,14.004,
            14.544,13.572,13.968,14.256,11.7,13.068,13.824,12.528,12.456,15.588,15.048,16.056,14.148,11.484,10.224,
            14.184,12.564,11.808,12.816,14.436,12.816,13.248,13.572,13.248,11.628,10.368,14.328,12.06,12.42,13.248,
            16.596,14.328,12.96,12.78,11.268,11.088,13.212,11.916,13.788,14.688,18.072,12.24,10.26,16.236,14.544,10.8,
            9.468,15.984,14.796,15.588,14.328,11.772,13.212,11.628,12.6,13.572,13.536,13.248,10.548,14.004,15.84,18.036,
            13.824,13.824,11.952,14.724,15.3,13.788,15.408,14.472,14.184,16.344,16.38,16.236,15.588,17.316,14.328,13.284,
            13.32,14.796,13.86,12.168,11.556,12.312,16.056,15.48,14.256,15.192,16.128,12.42,13.176,14.688,15.516,14.616,
            12.96,13.068,16.128,14.148,15.804,16.128,15.552,13.392,12.276,12.456,12.384,11.7,13.104,12.564,14.688,15.696,
            14.544,12.348,13.032,15.444,13.572,13.932,14.616,15.876,17.712,19.044,17.676,12.168,12.312,13.644,15.876,
            12.888,13.824,15.444,16.38,18.288,13.356,13.572,13.644,14.328,17.892,20.232,17.1,17.064,21.168,21.6,18.252,
            17.1,21.276,21.816,17.64,19.404,20.772,24.012,22.284,20.916,18.864,21.168,23.616,20.052,19.872,20.952,22.716,
            23.868,24.012,23.148,21.528,20.844,19.728,18.432,18.144,19.044,21.06,22.032,18.972,18.36,18.972,16.956,15.84,
            15.84,20.736,22.536,22.176,21.672,19.656,15.948,15.48,19.368,22.032,20.736,20.484,21.708,19.98,18.504,18.324,
            15.876,16.308,21.168,21.996,19.764,16.02,17.028,18.612,16.632,18.324,18.036,16.956,15.804,17.748,20.124,21.348,
            19.908,19.44,15.84,16.956,17.964,19.656,19.44,17.856,16.776,16.056,15.48,16.884,17.676,16.308,14.184,12.744,
            15.3,16.02,15.804,14.724,14.004,12.924,11.808,14.004,13.068,13.212,14.004,13.716,14.508,15.012,16.524,14.544,
            13.608,15.336,16.308,15.084,13.5,13.644,13.068,14.292,12.42,14.364,15.984,14.4,15.804,14.76,16.632,14.724,
            14.652,12.672,10.764,11.232,11.988,11.088,10.008,11.34,12.78,12.06,11.916,13.176,12.492,14.184,12.78,12.996,
            12.24,11.196,10.368,10.512,10.872,12.96,13.824,11.484,15.012,14.616,14.616,12.384,11.016,11.916,11.88,11.016,
            11.304,10.296,11.808,11.196,9.612,10.656,10.044,12.276,12.384,12.852,12.384,11.34,10.656,10.404,11.664,
            12.132,13.788,12.78,10.944,13.284,15.552,12.78,13.32,17.604,13.932,13.536,11.916,13.32,10.872,9.972,11.484,
            14.364,12.636,13.608,13.788,15.552,12.96,12.06,11.16,15.012,12.852,12.42,13.428,11.664,11.664,13.428,14.94,
            14.076,14.58,11.196,11.124,11.628,11.304,14.04,13.284,11.916,10.872,11.844,15.516,13.896,12.78,11.052,9.576,
        };
        */

        var murmanskPos = new GeographicalPoint(latitude: 69.009358, longitude: 32.946704);
        var iasiPos = new GeographicalPoint(47.156944, 27.590278);
        var melbournePos = new GeographicalPoint(-37.813611, 144.963056);
        var onShoreA1Pos = new GeographicalPoint(28.899, 45.084);
        var onShoreA2Pos = new GeographicalPoint(29.635, 44.952);

        var chicagoPos = new GeographicalPoint(41.881944, -87.627778);

        var location =
            //chicagoPos;
            //murmanskPos;
            //iasiPos;
            //onShoreA1Pos;
            onShoreA2Pos;
        
        /*
        var values = new HistoricalClimateDataGetter().GetValuesAtPoint(location);

        var windSpeeds = values.Select(x => x.WindSpeed_p25).Concat
            (values.Select(x => x.WindSpeed_p75)).Concat
            (values.Select(x => x.WindSpeed));


        foreach (var windSpeed in windSpeeds)
        {
            Console.Write($"{windSpeed}, ");
        }
        Console.WriteLine();


        var prob = WindProbabilityDistribution.FitData(windSpeeds,
            turbineHeight: 100 //100m 
            );
        
        //todo AEP (gw/h)
        //var prob = new WindProbabilityDistribution(2, 7.5);
        Console.WriteLine($"p {prob.At(5)}; {prob.Mean}; {prob.Cumulative(5)}");

        Console.WriteLine($"k {prob.K}; c {prob.C}");
        */
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

        Console.WriteLine($"monthly: {string.Join(", ", res.MonthlyEnergyProduced.Select(x => x.ToString()))}");

        Console.WriteLine($"Total: {res.YearlyEnergyProduced:0.}");
        Console.WriteLine($"Maximum yearly energy: {res.MaximumYearlyEnergy:0.}");

        Console.WriteLine($"Capacity factor: {res.CapacityFactor*100:0.00} (>30% for wind energy is pretty good)");
    }
}
