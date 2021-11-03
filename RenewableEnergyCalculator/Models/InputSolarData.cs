using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RenewableEnergyCalculator.Models
{
    public class InputSolarData
    {
        //public Address Address { get; set; }
        //public Coordinate Coordinate { get; set; }
        public float Are { get; set; }
        public int Slope { get; set; }
        public string Orientation { get; set; }
        public string PanelType { get; set; }
        public string Currency { get; set; }
        public double AnnualEnConsumption { get; set; }
        public double AnnualElPrice { get; set; }

    }
}