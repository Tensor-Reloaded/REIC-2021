using System;
using System.Linq;
using System.Collections.Generic;

namespace RenewableEnergyCalculator.Models.Wind
{
    ///////////////////////////////////////////////////////////////////////////////////////
    //FileName: ResultEnergyData.cs
    //FileType: Visual C# Source file
    //Author : Bucnaru Raluca, Pavel Andrei
    //Description : Informations about the energy produced in a location
    //////////////////////////////////////////////////////////////////////////////////////
    public abstract class ResultEnergyData
    {

        /// The amount of energy produced in a year (in kW)
        public abstract IEnumerable<double> MonthlyEnergyProduced { get; }
        public double YearlyEnergyProduced => MonthlyEnergyProduced.Sum();
        /// The energy produced with 100% efficiency (in kW)
        public abstract double MaximumYearlyEnergy { get; }

        /// The ratio between actual energy produced and the theorethical maximum.
        public virtual double CapacityFactor => YearlyEnergyProduced / MaximumYearlyEnergy;

    }
}