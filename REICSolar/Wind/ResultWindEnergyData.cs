using System;
using System.Collections.Generic;
namespace REIC
{
    class ResultWindEnergyData : ResultEnergyData
    {
        public override double YearlyEnergyProduced { get; }

        /// The turbine model used
        public Turbine Turbine { get; }

        /// The optimal wind turbine placements
        public List<GeographicalPoint> TurbineLocation { get; }

        // things about the suitability could be added
        // eg.
        // bool IsSuitable;
        // string NotSuitableReason;
    }
}
