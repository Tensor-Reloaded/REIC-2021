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

    class SolarRadiation
    {
        //using the same names as those from the api
        List<float> SI_EF_TILTED_SURFACE_HORIZONTAL;
        List<float> SI_EF_TILTED_SURFACE_LAT_MINUS15;
        List<float> SI_EF_TILTED_SURFACE_LATITUDE;
        List<float> SI_EF_TILTED_SURFACE_LAT_PLUS15;
        List<float> SI_EF_TILTED_SURFACE_VERTICAL;
        List<float> SI_EF_TILTED_SURFACE_OPTIMAL;
        List<float> SI_EF_TILTED_SURFACE_OPTIMAL_ANG;
        string SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT;

        float annualSumOfRadiation;

        //we get the average radiation of each month, so we just have to sum them all up to get the yearly radiation
        void annualRadiation() { foreach (var it in SI_EF_TILTED_SURFACE_HORIZONTAL) { annualSumOfRadiation += it; } } //this is just an example, we can probably use all those variables depending on the case
        public float getAverageRadiation() { return annualSumOfRadiation; }
        public string getOptimalAngOrt() { return SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT; }

    }
}
