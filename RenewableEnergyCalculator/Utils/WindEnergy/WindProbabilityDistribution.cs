using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Distributions;
using static System.Math;

namespace REIC
{
    /// Holds the parameters for the wind distribution at a particular point.
    public class WindProbabilityDistribution
    {
        /// A parameter for the distribution
        public double K { get; }
        /// A parameter for the distribution
        public double C { get; }

        public WindProbabilityDistribution(double k, double c)
        {
            K = k;
            C = c;
        }

        public double At(double x)
        {
            return Weibull.PDF(shape: K, scale: C, x: x);
        }


        /// Fit historical wind data to a probability distribution.
        /// Wind speeds should to be at 10m
        public static WindProbabilityDistribution FitData(IEnumerable<double> windSpeeds, double turbineHeight, double roughnessLength)
        {
            double sampleWindAltitude = 10;

            double scalingFactor = Log(turbineHeight / roughnessLength) / Log(sampleWindAltitude / roughnessLength);

            return FitData(windSpeeds.Select(s => s * scalingFactor));
        }

        /// Fit historical wind data to a probability distribution.
        /// Wind speeds should to be at turbine height.
        [FunctionEntryLoggerAspect]
        public static WindProbabilityDistribution FitData(IEnumerable<double> windSpeeds)
        {
            var res = Weibull.Estimate(windSpeeds.Select(x=>x/3.6));
            return new WindProbabilityDistribution(k: res.Shape, c: res.Scale);
        }
    }
}
