using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RenewableEnergyCalculator.Models.WInd
{
    /// The wind turbine power curves are given as pairs of samples.
    public class PowerCurve
    {
        public Tuple<double, double> Values { get; }
    }
}