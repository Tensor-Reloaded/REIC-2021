﻿using System;
using System.Linq;
using System.Collections.Generic;
using MathNet.Numerics;

namespace REIC
{
    public class WindEnergyCalculator : EnergyCalculator
    {
        List<WindProbabilityDistribution> monthlyWindDistributions;
        Turbine turbine;
        int numberOfTurbines;

        GeographicalPoint location;

        public WindEnergyCalculator(Turbine turbine, int numberOfTurbines, GeographicalPoint location)
        {
            this.turbine = turbine;
            this.numberOfTurbines = numberOfTurbines;
            this.location = location;

            var values = new HistoricalClimateDataGetter().GetValuesAtPoint(location);

            var monthlyWindSpeeds = Enumerable.Range(1, 12).Select(i => 
                values.Where(x => x.Month == i).Select(x=>x.WindSpeed));

            //var windSpeeds = values.Select(x=>x.WindSpeed_p25).Concat(
            //    values.Select(x => x.WindSpeed)).Concat(
            //    values.Select(x => x.WindSpeed_p75));

            this.monthlyWindDistributions = monthlyWindSpeeds.Select(
                windSpeeds => WindProbabilityDistribution.FitData(windSpeeds,
                    turbineHeight: turbine.HubHeight)).ToList();
        }

        /// @param turbine The model of turbine to use
        /// @param numberOfTurbines The number of turbines to be built in the region of interest
        public WindEnergyCalculator(List<WindProbabilityDistribution> monthlyWindDistributions, 
            Turbine turbine, int numberOfTurbines, GeographicalPoint location)
        {
            this.monthlyWindDistributions = monthlyWindDistributions;
            this.turbine = turbine;
            this.numberOfTurbines = numberOfTurbines;
            this.location = location;
        }
        public WindEnergyCalculator()
        {
        }

        [FunctionEntryLoggerAspect]
        public override ResultEnergyData Calculate()
        {
            double[] monthSizes = new [] {
                31, 28.25, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31
            };
            // ...
            //return new WindEnergyData(...);
            //throw new NotImplementedException();
            double operatingPercentage = 1;// .90;

            var integrals = monthlyWindDistributions.Select(
                windDistribution =>
                Integrate.OnClosedInterval(
                    (x) => windDistribution.At(x) * turbine.PowerCurve.At(x), 
                    turbine.CutInSpeed, turbine.CutOutSpeed));

            var resultEnergy = integrals.Zip(monthSizes,
                    (val, monthSize) => 24 * val * monthSize
                ).ToList();

           
            //double resultEnergy = operatingPercentage * 365 * 24*integral;

            


            return new ResultWindEnergyData(resultEnergy,
                numberOfTurbines: this.numberOfTurbines,
                turbine: turbine,
                location: location);
        }

        public override bool ShouldReject()
        {
            throw new NotImplementedException();
        }
    }
}
