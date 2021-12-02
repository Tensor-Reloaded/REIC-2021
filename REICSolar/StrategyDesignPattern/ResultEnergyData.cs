using System;
using System.Collections.Generic;
namespace REIC
{
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////
    //FileName: ResultEnergyData.cs
    //FileType: Visual C# Source file
    //Author : Bucnaru Raluca, Pavel Andrei
    //Description : Informations about the energy produced in a location
    //////////////////////////////////////////////////////////////////////////////////////
    public abstract class ResultEnergyData
    {
        /// The amount of energy produced in a year (in kW)
        public abstract double YearlyEnergyProduced { get; }
    }
}
