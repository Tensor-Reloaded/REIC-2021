using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RenewableEnergyCalculator.Models
{
    public class SolarRadiation
    {
        //using the same names as those from the api
        readonly List<double> SI_EF_TILTED_SURFACE_HORIZONTAL;
        readonly List<double> SI_EF_TILTED_SURFACE_LAT_MINUS15;
        readonly List<double> SI_EF_TILTED_SURFACE_LATITUDE;
        readonly List<double> SI_EF_TILTED_SURFACE_LAT_PLUS15;
        readonly List<double> SI_EF_TILTED_SURFACE_VERTICAL;
        readonly List<double> SI_EF_TILTED_SURFACE_OPTIMAL;
        readonly List<double> SI_EF_TILTED_SURFACE_OPTIMAL_ANG;
        readonly string SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT;
        readonly double slope;
        readonly double latitude;
        readonly string orientation;

        private const double PI = Math.PI;
        private const double declination_factor1 = 360.0 / 365.0;
        private const double declination_factor2 = 284;
        private const double declination_N = 23.45;
        private const double declination_S = -23.45;


        public SolarRadiation(List<double> SI_EF_TILTED_SURFACE_HORIZONTAL,
        List<double> SI_EF_TILTED_SURFACE_LAT_MINUS15,
        List<double> SI_EF_TILTED_SURFACE_LATITUDE,
        List<double> SI_EF_TILTED_SURFACE_LAT_PLUS15,
        List<double> SI_EF_TILTED_SURFACE_VERTICAL,
        List<double> SI_EF_TILTED_SURFACE_OPTIMAL,
        List<double> SI_EF_TILTED_SURFACE_OPTIMAL_ANG,
        string SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT, double slope, double latitude, string orientation)
        {
            this.SI_EF_TILTED_SURFACE_HORIZONTAL = SI_EF_TILTED_SURFACE_HORIZONTAL;
            this.SI_EF_TILTED_SURFACE_LATITUDE = SI_EF_TILTED_SURFACE_LATITUDE;
            this.SI_EF_TILTED_SURFACE_LAT_MINUS15 = SI_EF_TILTED_SURFACE_LAT_MINUS15;
            this.SI_EF_TILTED_SURFACE_LAT_PLUS15 = SI_EF_TILTED_SURFACE_LAT_PLUS15;
            this.SI_EF_TILTED_SURFACE_OPTIMAL = SI_EF_TILTED_SURFACE_OPTIMAL;
            this.SI_EF_TILTED_SURFACE_OPTIMAL_ANG = SI_EF_TILTED_SURFACE_OPTIMAL_ANG;
            this.SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT = SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT;
            this.SI_EF_TILTED_SURFACE_VERTICAL = SI_EF_TILTED_SURFACE_VERTICAL;
            this.slope = slope;
            this.latitude = latitude;
            this.orientation = orientation;
        }

        private double GetCurrentDayNr()
        {
            // current year and date
            int year = DateTime.Now.Year;
            DateTime currentDate = DateTime.Now;

            // first day of the current year (i.e. January 1)
            DateTime firstDay = new DateTime(year, 1, 1);

            // d = current day of the year 
            double d = Math.Round((currentDate - firstDay).TotalDays);
            //System.Diagnostics.Debug.WriteLine("Number of the current day =" + d);
            //System.Diagnostics.Debug.WriteLine("lat =" + latitude);

            return d;
        }

        private double DegreesToRad(double degrees)
        {
            return degrees * (PI / 180);
        }

        private double GetDeclinationAngle(double d)
        {
            // declination angle
            var degrees = declination_factor1 * (declination_factor2 + d);
            var rad = DegreesToRad(degrees);
            double declination_angle;

            if (orientation.Equals("N"))
            {
                declination_angle = declination_N * Math.Sin(rad);
            }
            else
            {
                declination_angle = declination_S * Math.Sin(rad);
            }
            
            //System.Diagnostics.Debug.WriteLine("Declination angle =" + declination_angle);
            return declination_angle;
        }

        private double GetElevationAngle(double declination_angle)
        {
            var elevation_angle = 90 - latitude + (declination_angle);
            //System.Diagnostics.Debug.WriteLine("elevation =" + elevation_angle);
            //System.Diagnostics.Debug.WriteLine("slope =" + slope);
            return elevation_angle;
        }

        public List<double> GetAverageMonthlyRadiation()
        {
            List<double> monthlyAverageRadiation = new List<double>();

            // d = current day of the year 
            var d = GetCurrentDayNr();
           
            // declination angle
            var declination_angle = GetDeclinationAngle(d);

            // elevation angle
            var elevation_angle = GetElevationAngle(declination_angle);

            foreach (var it in SI_EF_TILTED_SURFACE_HORIZONTAL)
            {
                // average annual radiation on tilted surfice (according to slope)
                var s_module = (it * Math.Sin(DegreesToRad(elevation_angle + slope)) / Math.Sin(DegreesToRad(elevation_angle)));
                //System.Diagnostics.Debug.WriteLine("Anual average radiation (according to slope) =" + s_module);
                monthlyAverageRadiation.Add(s_module);
            }

            return monthlyAverageRadiation;
        }

        public double GetAverageAnnualRadiation(List<double> averageMonthlyRadiation)
        {
            var sum = 0.0;

            foreach(var rad in averageMonthlyRadiation)
            {
                sum += rad;
            }

            return sum/12;
        }

        public string GetOptimalAngOrt() { 
            return SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT; 
        }
    }
}