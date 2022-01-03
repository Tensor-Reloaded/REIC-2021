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
        Weibull d;

        /// A parameter for the distribution
        public double K => d.Shape;
        /// A parameter for the distribution
        public double C => d.Scale;

        public double Mean => d.Mean;

        WindProbabilityDistribution(Weibull d)
        {
            this.d = d;
        }
        public WindProbabilityDistribution(double k, double c)
        {
            d = new Weibull(k, c);
        }

        public double At(double x)
        {
            return d.Density(x);
        }

        public double Cumulative(double x) => d.CumulativeDistribution(x);


        /// Fit historical wind data to a probability distribution.
        /// Wind speeds should to be at 10m
        public static WindProbabilityDistribution FitData(IEnumerable<double> windSpeeds, double turbineHeight)
        {
            //https://en.wikipedia.org/wiki/Wind_profile_power_law
            double sampleWindAltitude = 10;
            double alpha = 1.0 / 5;

            //double scalingFactor = Log(turbineHeight / roughnessLength) / Log(sampleWindAltitude / roughnessLength);

            double scalingFactor = Pow(turbineHeight / sampleWindAltitude, alpha);

            return FitData(windSpeeds.Select(s => s * scalingFactor));
        }

        /// Fit historical wind data to a probability distribution.
        /// Wind speeds should to be at turbine height.
        [FunctionEntryLoggerAspect]
        public static WindProbabilityDistribution FitData(IEnumerable<double> windSpeeds)
        {
            var res = Weibull.Estimate(windSpeeds);
            return new WindProbabilityDistribution(res);
        }
    }
}
