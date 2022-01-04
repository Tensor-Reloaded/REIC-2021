using System;
using System.ComponentModel.DataAnnotations;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Extensibility;

namespace RenewableEnergyCalculator.Models
{
    [Log(AttributeTargetMemberAttributes = MulticastAttributes.Public)]
    public class InputSolarData
    {
        public string Address { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        [Required(ErrorMessage = "Width is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value grater than 0.")]
        public double Width { get; set; }

        [Required(ErrorMessage = "Length is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value grater than 0.")]
        public double Length { get; set; }

        [Required(ErrorMessage = "Slope is required.")]
        [Range(0, 90, ErrorMessage = "Please enter the slope between 0 and 90 degrees.")]
        public int Slope { get; set; }

        [Required(ErrorMessage = "Orientation is required")]
        [RegularExpression(@"[a-zA-Z]", ErrorMessage = "Please, select a value.")]
        public string Orientation { get; set; }

        [Required(ErrorMessage = "PanelType is required")]
        [RegularExpression(@"^[A-Za-z0-9]{2,}$", ErrorMessage = "Please, select a panel type.")]
        public string PanelType { get; set; }

        [Required(ErrorMessage = "Panel is required")]
        [RegularExpression(@"^[A-Za-z0-9]{2,}$", ErrorMessage = "Please, select a panel.")]
        public string Panel { get; set; }

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