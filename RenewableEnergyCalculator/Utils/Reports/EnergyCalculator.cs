using System;

namespace chart
{
    public class EnergyCalculator
    {
        public double totalEnergy { get; set; }
        public double REenergy { get; set; }
        [ExceptionAspect]
        [LogAspect]
        public double CalculatePercent(double totalenergy, double rEEnergyRroduced)
        {
            if (totalenergy == 0)
                throw new ArgumentException("Total Energy MUST BE > 0!!!");

            double percent = ((rEEnergyRroduced / totalenergy)) * 100;

            return percent;
        }
    }
}
