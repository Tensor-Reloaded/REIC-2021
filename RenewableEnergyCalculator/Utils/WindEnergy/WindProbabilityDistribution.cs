using System;
using System.Collections.Generic;
namespace REIC
{
    /// Holds the parameters for the wind distribution at a particular point.
    class WindProbabilityDistribution
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
        /// @param windData To be defined in the map module (we need 3d data: (longitude, latitude, time) -> wind value)
        public static WindProbabilityDistribution FitData(List<HistoricalClimateSample> historicalData)
        {
           // List<double> data =;
            //return new WindProbabilityDistribution(...);
            throw new NotImplementedException();
        }
    }
}
