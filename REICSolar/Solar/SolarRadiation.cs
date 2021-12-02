using System;
using System.Collections.Generic;
namespace REIC
{
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////
    //FileName: SolarRadiation.cs
    //FileType: Visual C# Source file
    //Author : Bucnaru Raluca
    //Description : get the solar radiation from the nasa api; we'll apply specific data from all those variable lists depending on how and where our panel is placed
    //////////////////////////////////////////////////////////////////////////////////////

    public class SolarRadiation
    {
        //using the same names as those from the api
        List<double> SI_EF_TILTED_SURFACE_HORIZONTAL;
        List<double> SI_EF_TILTED_SURFACE_LAT_MINUS15;
        List<double> SI_EF_TILTED_SURFACE_LATITUDE;
        List<double> SI_EF_TILTED_SURFACE_LAT_PLUS15;
        List<double> SI_EF_TILTED_SURFACE_VERTICAL;
        List<double> SI_EF_TILTED_SURFACE_OPTIMAL;
        List<double> SI_EF_TILTED_SURFACE_OPTIMAL_ANG;
        string SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT;
        double slope;

        double annualSumOfRadiation;

        public SolarRadiation(List<double> SI_EF_TILTED_SURFACE_HORIZONTAL,
        List<double> SI_EF_TILTED_SURFACE_LAT_MINUS15,
        List<double> SI_EF_TILTED_SURFACE_LATITUDE,
        List<double> SI_EF_TILTED_SURFACE_LAT_PLUS15,
        List<double> SI_EF_TILTED_SURFACE_VERTICAL,
        List<double> SI_EF_TILTED_SURFACE_OPTIMAL,
        List<double> SI_EF_TILTED_SURFACE_OPTIMAL_ANG,
        string SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT, double slope)
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
        }

        //we get the average radiation of each month, so we just have to sum them all up to get the yearly radiation
        void annualRadiation() 
        {
            foreach (var it in SI_EF_TILTED_SURFACE_OPTIMAL) { annualSumOfRadiation += it; }
            annualSumOfRadiation = annualSumOfRadiation * 30;
        } //this is just an example, we can probably use all those variables depending on the case
        public double getAverageRadiation() { annualRadiation(); return annualSumOfRadiation; }
        public string getOptimalAngOrt() { return SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT; }

        public double getMonthRadiation(int month) { double last = SI_EF_TILTED_SURFACE_OPTIMAL[month - 1]; return last; }

    }
}
