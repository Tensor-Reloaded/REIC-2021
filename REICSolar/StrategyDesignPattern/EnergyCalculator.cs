using System;
using System.Collections.Generic;
namespace REIC
{
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////
    //FileName: EnergyCalculator.cs
    //FileType: Visual C# Source file
    //Author : Bucnaru Raluca, Pavel Andrei
    //Description : the base class for the calculator, follows strategy design pattern
    //////////////////////////////////////////////////////////////////////////////////////
    public abstract class EnergyCalculator
    {
        public abstract bool ShouldReject();
        public abstract ResultEnergyData Calculate();
    }
}