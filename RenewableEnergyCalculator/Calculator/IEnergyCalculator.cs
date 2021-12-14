using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RenewableEnergyCalculator.Calculator
{
    public interface IEnergyCalculator
    {
        double CalculateEnergy();
    }
}