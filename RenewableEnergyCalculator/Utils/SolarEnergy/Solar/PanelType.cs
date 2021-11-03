using System;
using System.Collections.Generic;
namespace REIC
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    //FileName: PanelType.cs
    //FileType: Visual C# Source file
    //Author : Bucnaru Raluca
    //Description : type of panel: can be monoctystalinne, polycrystalline, thin-film (multiple types of thin film, a-Si for example)
    //////////////////////////////////////////////////////////////////////////////////////
    class PanelType
    {
        string type;  //how the panel type is called
        float minEff; // minimum efficiency of said panel type
        float maxEff; // maximum efficiency of said panel type

        public float getMinEff() { return minEff; }
        public float getMaxEff() { return maxEff; }
        public float getMedianEff() { return minEff + (maxEff - minEff) / 2; }

        public PanelType(string type, float minEff, float maxEff) { this.type = type; this.minEff = minEff; this.maxEff = maxEff; }
        public PanelType() { }
    }
}
