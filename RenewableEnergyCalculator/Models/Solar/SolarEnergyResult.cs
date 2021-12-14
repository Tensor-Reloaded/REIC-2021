using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RenewableEnergyCalculator.Models.Solar
{
    public class SolarEnergyResult
    {
        public string Address { get; set; }
        public double Area { get; set; }

        public int Slope { get; set; }

        public double Efficiency { get; set; }

        public double AnnualRadiation { get; set; }

        public double PerformanceRatio { get; set; }


    }
}