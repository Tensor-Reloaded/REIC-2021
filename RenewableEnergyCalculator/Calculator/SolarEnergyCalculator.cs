using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RenewableEnergyCalculator.Calculator
{
    public class SolarEnergyCalculator
    {
        private readonly double border = 0.3;
        private readonly double averageInverterPrice = 1324.24;
        public List<double> CalculateMonthlyEnergy(double width, double length, double panelArea, double r, List<double> H, double PR)
        {
            var totalPanelArea = GetPanelArea(width, length);
            var nrOfPanels = GetNrOfPanels(totalPanelArea, panelArea);
            var A = panelArea * nrOfPanels;

            List<double> monthlyEnergy = new List<double>();

            foreach(var h in H)
            {
                double E = A * r * h * PR;

                if (E < 0)
                {
                    throw new ArgumentOutOfRangeException("energy");
                }

                monthlyEnergy.Add(Math.Round(E));
            }

            return monthlyEnergy;
        }

        public double CalculateAnnualEnergy(List<double> monthlyEnergy)
        {
            var sum = 0.0;

            foreach (var e in monthlyEnergy)
            {
                sum += e;
            }

            return sum/12;
        }

        private double GetPanelArea(double width, double length)
        {
            var totalPanelArea = (width - 2 * border) * (length - 2 * border);
            return totalPanelArea;
        }

        public double GetNrOfPanels(double totalPanelArea, double panelArea)
        {
            var nrOfPanels = (int)(totalPanelArea / panelArea); 
            return nrOfPanels;
        }

        public double CalculateROI(double panelsCost, double averageAnnualConsumption, double electricityPrice)
        {
            // Annual Electric Bills Cost
            var annualElectricityCost = averageAnnualConsumption * electricityPrice;

            //
            var systemCost = panelsCost + averageInverterPrice + 0.30* panelsCost;

            var payback = Math.Round(systemCost / annualElectricityCost);

            return payback;

        }
    }
}