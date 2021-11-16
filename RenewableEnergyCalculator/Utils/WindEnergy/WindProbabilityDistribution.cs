using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.RootFinding;
using static System.Math;

namespace REIC
{
    /// Holds the parameters for the wind distribution at a particular point.
    public class WindProbabilityDistribution
    {
        /// A parameter for the distribution
        public float K { get; }
        /// A parameter for the distribution
        public float C { get; }

        public WindProbabilityDistribution(float k, float c)
        {
            K = k;
            C = c;
        }



        /// Fit historical wind data to a probability distribution.
        /// Wind speeds should to be at 10m
        public static WindProbabilityDistribution FitData(IEnumerable<double> windSpeeds, double turbineHeight, double roughnessLength)
        {
            double sampleWindAltitude = 10;

            double scalingFactor = Math.Log(turbineHeight / roughnessLength) / Math.Log(sampleWindAltitude / roughnessLength);

            return FitData(windSpeeds.Select(s => s * scalingFactor));
        }

        /// Fit historical wind data to a probability distribution.
        /// Wind speeds should to be at turbine height.
        [FunctionEntryLoggerAspect]
        public static WindProbabilityDistribution FitData(IEnumerable<double> windSpeeds)
        {

            //from https://www.intechopen.com/chapters/17938

            var xs = windSpeeds;//.ToList();


            double mu = xs.Average();
            var n = xs.Count();

            Func<double, double> f = (k) => {
                var frac = xs.Select(x => Pow(x, k) * Log(x)).Sum() / xs.Select(x=>Pow(x, k)).Sum();
                var rhs = mu/n;

                return frac - 1.0 / k - mu / n;
            };
            Func<double, double> df = (k) => xs.Select(x => Pow(x, k) * Pow(Log(x), 2)).Sum()
                - (xs.Select(x=> Pow(x, k) * (k * Log(x) -1)).Sum() / (k * k))
                - (xs.Select(x=>Log(x)).Sum()/n * xs.Select(x=> Pow(x, k) * Log(x)).Sum());

            //double estimatedK = NewtonRaphson.FindRootNearGuess(f, df, initialGuess: 5//it should be about this size
            //    );

            //first moment
            //third moment

            return null;
            //throw new NotImplementedException();
        }
    }
}
