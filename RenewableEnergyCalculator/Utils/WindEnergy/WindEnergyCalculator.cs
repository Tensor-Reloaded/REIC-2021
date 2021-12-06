using System;
using System.Linq;
using System.Collections.Generic;
using MathNet.Numerics;

namespace REIC
{
    public class WindEnergyCalculator : EnergyCalculator
    {
        WindProbabilityDistribution windDistribution;
        Turbine turbine;
        int numberOfTurbines;

        /// @param turbine The model of turbine to use
        /// @param numberOfTurbines The number of turbines to be built in the region of interest
        public WindEnergyCalculator(WindProbabilityDistribution windDistribution, Turbine turbine, int numberOfTurbines)
        {
            this.windDistribution = windDistribution;
            this.turbine = turbine;
            this.numberOfTurbines = numberOfTurbines;
        }
        public WindEnergyCalculator()
        {
        }

        [FunctionEntryLoggerAspect]
        public override ResultEnergyData Calculate()
        {
            // ...
            //return new WindEnergyData(...);
            //throw new NotImplementedException();
            Func<double, double> f = (x) => windDistribution.At(x) * turbine.PowerCurve.At(x);
            double resultEnergy = 365* Integrate.OnClosedInterval(f, turbine.CutInSpeed, turbine.CutOutSpeed);

            


            return new ResultWindEnergyData(resultEnergy,
                turbine: turbine,
                turbineLocation: null);
        }

        public override bool ShouldReject()
        {
            throw new NotImplementedException();
        }
    }
}
