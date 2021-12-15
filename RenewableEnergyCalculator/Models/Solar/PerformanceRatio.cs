using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RenewableEnergyCalculator.Models
{
    ///////////////////////////////////////////////////////////////////////////////////////
    // FileName: PerformanceRatio.cs
    // Author : Bucnaru Raluca
    // Description :will be used to find out the performance ratio of a specific panel
    //////////////////////////////////////////////////////////////////////////////////////

    public class PerformanceRatio
    {
        double Temperature;
        double PerfLossPerDegree;
        double PR = 0.75;
        double NOCT = 48;
        double Irr; //should be in MW/cm^2, not kW/m^2
        double TCell;

        public void getPerfLossPerDegree(PanelType panelType)
        {
            if (panelType.PType.Equals(1) || panelType.PType.Equals(2) || panelType.PType.Equals(3))
                PerfLossPerDegree = 0.5;
            else
                PerfLossPerDegree = 0.2;
        }

        void setIrradiation(SolarRadiation radiation)
        {
            int month = DateTime.Now.Month;
            //Irr = radiation.GetMonthRadiation(month) * 10; //convert kW/m2 in MW/cm2
        }

        public PerformanceRatio(SolarRadiation radiation, PanelType panelType, double medTemp = 30, double NOCT = 48)
        {
            setIrradiation(radiation);
            Temperature = medTemp;
            this.NOCT = NOCT;
            getPerfLossPerDegree(panelType);
        }
        public PerformanceRatio() { PR = 0.75; }

        void CalculateTCell()
        {
            //http://www.ingmecc.uniroma1.it/attachments/2396_Lesson%2014%20MENR.pdf
            //https://www.pveducation.org/pvcdrom/modules-and-arrays/nominal-operating-cell-temperature
            TCell = Temperature + ((NOCT - 20) / 80) * Irr;
        }

        void CalculatePR()
        {
            if (TCell >= 25)
                PR = 1 - (PerfLossPerDegree * (TCell - 25)) / 100 - 0.07 - 0.02 - 0.02 - 0.05 - 0.02; //temperature, inverter, dc cables, ac cables, weak radiation, dust + snow + others
            else
                PR = 0.75;
        }

        public double getPR() { CalculateTCell(); CalculatePR(); return PR; }

    }
}