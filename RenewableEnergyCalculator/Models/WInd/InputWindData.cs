using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RenewableEnergyCalculator.Models
{
    public class InputWindData
    {
        public string Address { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public string Turbine { get; set; }

        [Required(ErrorMessage = "Slope is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a number grater than 0.")]
        public string NumberOfTurbines { get; set; }

        [Required(ErrorMessage = "Currency is required")]
        public string Currency { get; set; }

        [Required(ErrorMessage = "Annual energy consumption is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value grater than 0.")]
        public double AnnualEnConsumption { get; set; }

        [Required(ErrorMessage = "Annual electricity price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value grater than 0.")]
        public double AnnualElPrice { get; set; }

    }
}