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
        float A; //area
        float r; //efficiency
        float H; //solar radiation
        float PR; //performance rate

        void setArea(SolarPanel panel) { A = panel.getArea(); }
        void setEfficiency(SolarPanel panel) { r = panel.getEff(); }

        void CalculatePR(PerformanceRatio performance) { }

        void SetRadiation(SolarRadiation radiation) { H = radiation.getAverageRadiation(); }


        public float getA() { return A; }
        public float getr() { return r; }
        public float getH() { return H; }
        public float getPR() { return PR; }

        public float CalculateEnergy() { float E = A * r * H * PR; return E; }
    }
  
}
