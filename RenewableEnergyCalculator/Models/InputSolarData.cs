using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RenewableEnergyCalculator.Models
{
    public class InputSolarData
    {
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        [Required(ErrorMessage = "Area is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value grater than 0.")]
        public double Area { get; set; }

        [Required(ErrorMessage = "Slope is required.")]
        [Range(0, 90, ErrorMessage = "Please enter the slope between 0 and 90 degrees.")]
        public int Slope { get; set; }

        [Required(ErrorMessage = "Orientation is required")]
        public string Orientation { get; set; }

        [Required(ErrorMessage = "Panel type is required")]
        public string PanelType { get; set; }

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