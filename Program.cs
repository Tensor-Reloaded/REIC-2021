using System;
using PostSharp.Patterns.Diagnostics;
using System.Collections.Generic;


namespace REIC
{
    class Program
    {
        static void Main(string[] args)
        {
            var backend = new PostSharp.Patterns.Diagnostics.Backends.Console.ConsoleLoggingBackend();
            backend.Options.Theme = PostSharp.Patterns.Diagnostics.Backends.Console.ConsoleThemes.Dark;
            LoggingServices.DefaultBackend = backend;
           

            PanelType solarPanelType = new PanelType("Monocrystaline", 30, 35);
            double eff = solarPanelType.getMedianEff();
            SolarPanel panel = new SolarPanel(eff, 20, 45, "S", 0, "");
            Console.WriteLine(panel.getOrientation());
            try { solarPanelType.setMaxEff(55); }
            catch(Exception e)
            {  }
            solarPanelType.getMaxEff();


            GetSolarRadiationAPI apiRad = new GetSolarRadiationAPI(47.1740, 27.5750);
            apiRad.getAPIData();
            List<double> SI_EF_TILTED_SURFACE_HORIZONTAL = apiRad.SI_EF_TILTED_SURFACE_HORIZONTAL;
            List<double> SI_EF_TILTED_SURFACE_LAT_MINUS15 = apiRad.SI_EF_TILTED_SURFACE_LAT_MINUS15;
            List<double> SI_EF_TILTED_SURFACE_LATITUDE = apiRad.SI_EF_TILTED_SURFACE_LATITUDE;
            List<double> SI_EF_TILTED_SURFACE_LAT_PLUS15 = apiRad.SI_EF_TILTED_SURFACE_LAT_PLUS15;
            List<double> SI_EF_TILTED_SURFACE_VERTICAL = apiRad.SI_EF_TILTED_SURFACE_VERTICAL;
            List<double> SI_EF_TILTED_SURFACE_OPTIMAL = apiRad.SI_EF_TILTED_SURFACE_OPTIMAL;
            List<double> SI_EF_TILTED_SURFACE_OPTIMAL_ANG = apiRad.SI_EF_TILTED_SURFACE_OPTIMAL_ANG;
            string SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT = apiRad.SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT;

            SolarRadiation solarRadiation = new SolarRadiation(SI_EF_TILTED_SURFACE_HORIZONTAL, SI_EF_TILTED_SURFACE_LAT_MINUS15, SI_EF_TILTED_SURFACE_LATITUDE, SI_EF_TILTED_SURFACE_LAT_PLUS15, SI_EF_TILTED_SURFACE_VERTICAL, SI_EF_TILTED_SURFACE_OPTIMAL, SI_EF_TILTED_SURFACE_OPTIMAL_ANG, SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT, panel.getSlope());
            double medTemp = 25;
            PerformanceRatio performanceRatio = new PerformanceRatio(solarRadiation, solarPanelType, medTemp);
            Console.WriteLine(performanceRatio.getPR());
            double PR = performanceRatio.getPR();
            SolarEnergyData solarEnergy = new SolarEnergyData(panel.getArea(), panel.getEff(), solarRadiation.getAverageRadiation(), PR);
            Console.WriteLine(solarEnergy.CalculateEnergy());



        }
    }
}
