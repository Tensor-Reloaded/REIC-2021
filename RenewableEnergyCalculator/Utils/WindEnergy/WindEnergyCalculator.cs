using System;
using System.Collections.Generic;
namespace REIC
{
    class WindEnergyCalculator : EnergyCalculator
    {
        WindProbabilityDistribution windDistribution;

        /// @param turbine The model of turbine to use
        /// @param numberOfTurbines The number of turbines to be built in the region of interest
        public WindEnergyCalculator(WindProbabilityDistribution windDistribution, Turbine turbine, int numberOfTurbines)
        {
        }
        public WindEnergyCalculator()
        {
        }

        public override ResultEnergyData Calculate()
        {
            // ...
            //return new WindEnergyData(...);
            throw new NotImplementedException();
        }

        public override bool ShouldReject()
        {
            throw new NotImplementedException();
        }
    }
}
