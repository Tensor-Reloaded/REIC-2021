using System;
using System.Collections.Generic;

namespace REIC
{


    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    //FileName: SolarEnergyData.cs
    //FileType: Visual C# Source file
    //Author : Bucnaru Raluca
    //Description : yearly energy generation for one panel; here we have all the variables we need for the equation
    //////////////////////////////////////////////////////////////////////////////////////
    class SolarEnergyData
    {
        //same names as in the equation from Irina's part
        double A; //area
        double r; //efficiency
        double H; //solar radiation
        double PR; //performance rate

        void setArea(SolarPanel panel) { A = panel.getArea(); }
        void setEfficiency(SolarPanel panel) { r = panel.getEff()/100; }

        void CalculatePR(PerformanceRatio performance) { PR = performance.getPR(); }

        void SetRadiation(SolarRadiation radiation) { H = radiation.getAverageRadiation(); }
        
        public SolarEnergyData(double A = 0, double r = 0, double H = 0, double PR = 0)
        {
            this.A = A;
            this.r = r/100;
            this.H = H;
            this.PR = PR;
        }

        public double getA() { return A; }
        public double getr() { return r; }
        public double getH() { return H; }
        public double getPR() { return PR; }

        public double CalculateEnergy() {  
            double E = A * r * H * PR;
            if (E < 0)
            {
                throw new ArgumentOutOfRangeException("energy");
            }
            else
            {
                return E;
            }
        }

    }
  
}
