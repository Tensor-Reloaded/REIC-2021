using System;
using System.Collections.Generic;
namespace REIC
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    //FileName: PerformanceRatio.cs
    //FileType: Visual C# Source file
    //Author : Bucnaru Raluca
    //Description :will be used to find out the performance ratio of a specific panel
    //////////////////////////////////////////////////////////////////////////////////////

    public class PerformanceRatio {

        double temperature;
        double perfLossPerDegree;
        double PR = 0.75;
        double NOCT = 48;
        double irr; //should be in MW/cm^2, not kW/m^2
        double tCell;

        void getPerfLossPerDegree(PanelType panelType)
        {
            if (panelType.getType() == "Monocrystalline" || panelType.getType() == "Polycrystalline" || panelType.getType() == "PERC")
                perfLossPerDegree = 0.5;
            else
                perfLossPerDegree = 0.2;
        }

        void setIrradiation(SolarRadiation radiation)
        {
            int month = DateTime.Now.Month;
            irr = radiation.getMonthRadiation(month) * 10; //convert kW/m2 in MW/cm2
        }
        public PerformanceRatio(SolarRadiation radiation, PanelType panelType, double medTemp = 30, double NOCT = 48)
        {
            setIrradiation(radiation);
            temperature = medTemp;
            this.NOCT = NOCT;
            getPerfLossPerDegree(panelType);
        }
        public PerformanceRatio() { PR = 0.75; }

        void CalculateTCell()
        {
            //http://www.ingmecc.uniroma1.it/attachments/2396_Lesson%2014%20MENR.pdf
            //https://www.pveducation.org/pvcdrom/modules-and-arrays/nominal-operating-cell-temperature
            tCell =  temperature + ((NOCT - 20) / 80) * irr;
        }

        void CalculatePR()
        {
            if(tCell >= 25)
            PR = 1 - (perfLossPerDegree * (tCell - 25))/100 - 0.07 - 0.02 - 0.02 - 0.05 - 0.02; //temperature, inverter, dc cables, ac cables, weak radiation, dust + snow + others
            else
            PR = 0.75;
        }

        public double getPR() { CalculateTCell(); CalculatePR(); return PR; }

    }
}