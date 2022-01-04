using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RenewableEnergyCalculator.Models.Wind
{
    public class ResultWindEnergyData : ResultEnergyData
    {
        public override IEnumerable<double> MonthlyEnergyProduced =>
    MonthlyEnergyProducedPerTurbine.Select(x => x * NumberOfTurbines);

        public List<double> MonthlyEnergyProducedPerTurbine { get; }

        /// The energy produced with 100% efficiency (in kW)
        public override double MaximumYearlyEnergy => Turbine.RatedPower * 356 * 24 * NumberOfTurbines;
        /// The ratio between actual energy produced and the theorethical maximum.
        public override double CapacityFactor => YearlyEnergyProduced / MaximumYearlyEnergy;

        /// The turbine model used
        public Turbine Turbine { get; }

        public GeographicalPoint Location { get; }

        public int NumberOfTurbines { get; }

        // things about the suitability could be added
        // eg.
        // bool IsSuitable;
        // string NotSuitableReason;

        public ResultWindEnergyData(List<double> montlyEnergyProducedPerTurbine, Turbine turbine,
                                    GeographicalPoint location, int numberOfTurbines)
        {
            this.MonthlyEnergyProducedPerTurbine = montlyEnergyProducedPerTurbine;
            this.Turbine = turbine;
            this.Location = location;
            this.NumberOfTurbines = numberOfTurbines;
        }

    }
}