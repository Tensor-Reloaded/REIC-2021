using System;
using System.Collections.Generic;
namespace REIC
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    //FileName: PanelType.cs
    //FileType: Visual C# Source file
    //Author : Bucnaru Raluca
    //Description : type of panel: can be monocrystalline, polycrystalline, thin-film (multiple types of thin film, a-Si for example)
    //////////////////////////////////////////////////////////////////////////////////////
    
    
    public class PanelType
    {
        public const string EfficiencyLessThanZeroMessage = "Efficiency can't be negative, but it is in this scope";
        public const string MinimumEfficiencyGreaterThanMaximumEfficiency = "The minimum value of the efficiency exceeds the maximum value";
        public const string OutOfBoundsEfficiency = "Efficiency should be less than 100";
        string type;  //how the panel type is called
        double minEff; // minimum efficiency of said panel type
        double maxEff; // maximum efficiency of said panel type

        public string getType() { return type; }
        public double getMinEff() { return minEff; }
        public double getMaxEff() { return maxEff; }
        public double getMedianEff() {
            double avgEff = minEff + (maxEff - minEff) / 2;
            if (avgEff < minEff || avgEff > maxEff)
            {
                throw new System.ArgumentOutOfRangeException("efficiency", avgEff, MinimumEfficiencyGreaterThanMaximumEfficiency);
            }
            return avgEff; }

        public PanelType(string type, double minEff, double maxEff)
        {
            if (minEff > maxEff)
            {
                throw new System.ArgumentOutOfRangeException("efficiency", maxEff, MinimumEfficiencyGreaterThanMaximumEfficiency);
            }
            if (minEff < 0)
            {
                throw new System.ArgumentOutOfRangeException("efficiency", minEff, EfficiencyLessThanZeroMessage);
            }
            if (maxEff < 0)
            {
                throw new System.ArgumentOutOfRangeException("efficiency", maxEff, EfficiencyLessThanZeroMessage);
            }
            if(maxEff > 100 || minEff > 100)
            {
                throw new System.ArgumentOutOfRangeException("efficiency", maxEff, OutOfBoundsEfficiency);
            }

            this.type = type;
            this.minEff = minEff;
            this.maxEff = maxEff;

        }
        public void setMinEff(double minEff)
        {
            if (minEff > this.maxEff)
            {
                throw new System.ArgumentOutOfRangeException("efficiency", minEff, MinimumEfficiencyGreaterThanMaximumEfficiency);
            }
            else
            {
                if(minEff > 100)
                {
                    throw new System.ArgumentOutOfRangeException("efficiency", minEff, OutOfBoundsEfficiency);
                }
                else
                {
                    this.minEff = minEff;
                }
                
            }
            

        }
        public void setMaxEff(double maxEff)
        {
            if (maxEff < this.minEff)
            {
                throw new System.ArgumentOutOfRangeException("efficiency", maxEff, MinimumEfficiencyGreaterThanMaximumEfficiency);
            }
            else
            {
                if (maxEff > 100)
                {
                    throw new System.ArgumentOutOfRangeException("efficiency", maxEff, OutOfBoundsEfficiency);
                }
                else
                {
                    this.maxEff = maxEff;
                }
                
            }
                
            
        
        }


        public PanelType() { }
    }
}
