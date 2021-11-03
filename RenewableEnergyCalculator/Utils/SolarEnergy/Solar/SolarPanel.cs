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
        float efficiency; //panel efficiency
        float cost; //panel cost
        float area; //dimensions of the panel
        string manufacturer; //who made the panel
        PanelType type; //panel type (can get the name, min, max, avg efficiency
        float slope; //land slope
        string orientaion; //Can also use Enum, but string should work for now; panel orientation ex:S for south, N for north 
        private int v1;
        private int v2;
        private string v3;
        private Func<object, object> p;
        private int v4;
        private string v5;

        void setMinEfficiency(PanelType type) { efficiency = type.getMinEff(); }
        void setMaxEfficiency(PanelType type) { }

        void setAvgEfficiency(PanelType type) { }

        void setEfficiency(float eff) { efficiency = eff; }

        void setOrientation(SolarRadiation radiation) { orientaion = radiation.getOptimalAngOrt(); }

        public float getEff() { return efficiency; }
        public float getCost() { return cost; }
        public float getArea() { return area; }

        //constructor
        public SolarPanel(float Efficiency,float Cost,float Area,string Manufacturer,PanelType Type, float Slope,string Orientaion )
        {
            this.efficiency = Efficiency;
            this.cost = Cost;
            this.area = Area;
            this.manufacturer = Manufacturer;
            this.type = Type;
            this.slope = Slope;
            this.orientaion = Orientaion;
        }
       
    }
}
