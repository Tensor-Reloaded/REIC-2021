using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class EnergyCalculator
    {
        public double totalEnergy { get; set; }
        public double REenergy { get; set; }
        public double BioWaste { get; set; }
        public double CalculatePercent(double totalenergy, double rEEnergyRroduced)
        {
            double percent = ((rEEnergyRroduced / totalenergy)) * 100;

            return percent;
        }
        public double CalculateNonReEnergy(double totalenergy, double rEEnergyRroduced, double bioWaste)
        {
            double nonRe = totalenergy - rEEnergyRroduced-bioWaste;
            return nonRe;
        }
    }
}
