using System;
using System.Collections.Generic;
namespace REIC
{

    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////
    //FileName: SolarPanel.cs
    //FileType: Visual C# Source file
    //Author : Bucnaru Raluca
    //Description : Contains data about a specific solar panel model
    //////////////////////////////////////////////////////////////////////////////////////
    class SolarPanel
    {
        double efficiency; //panel efficiency
        double cost; //panel cost
        double area; //dimensions of the panel
        string manufacturer; //who made the panel
        //PanelType type; //panel type (can get the name, min, max, avg efficiency
        double slope; //land slope
        string orientaion; //Can also use Enum, but string should work for now; panel orientation ex:S for south, N for north 
        //private Func<object, object> p;

        void setMinEfficiency(PanelType type) { efficiency = type.getMinEff(); }
        void setMaxEfficiency(PanelType type) { efficiency = type.getMaxEff(); }

        void setAvgEfficiency(PanelType type) { efficiency = type.getMedianEff(); }

        void setEfficiency(double eff) { efficiency = eff; }

        void setOrientation(SolarRadiation radiation) { orientaion = radiation.getOptimalAngOrt(); }

        void setArea(double area) { this.area = area; }

        void setManufacturer(string manufacturer) { this.manufacturer = manufacturer; }

        void setSlope(double slope) { this.slope = slope; }


        public double getEff() { return efficiency; }
        public double getCost() { return cost; }
        public double getArea() { return area; }
        public double getSlope() { return slope; }
        public string getManufacturer() { return manufacturer; }
        public string getOrientation() { return orientaion; }

        //constructor
        public SolarPanel(double Efficiency,double Area, double Slope,string Orientaion, double Cost = 0, string Manufacturer = "")
        {
            this.efficiency = Efficiency;
            this.cost = Cost;
            this.area = Area;
            this.manufacturer = Manufacturer;
            //this.type = Type;
            this.slope = Slope;
            this.orientaion = Orientaion;
        }
       
    }
}
