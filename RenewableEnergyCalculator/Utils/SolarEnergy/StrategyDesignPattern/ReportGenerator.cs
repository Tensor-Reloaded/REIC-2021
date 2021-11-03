using System;
using System.Collections.Generic;
namespace REIC
{
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////
    //FileName: ReportGenerator.cs
    //FileType: Visual C# Source file
    //Author : Bucnaru Raluca, Pavel Andrei
    //Description : this helps select a strategy for the energy calculator
    //////////////////////////////////////////////////////////////////////////////////////
    public class ReportGenerator
    {
        private EnergyCalculator strategy;


        public void SetCalculatorStrategy(EnergyCalculator strategy)
        {
            this.strategy = strategy;
        }


        public ResultEnergyData Calculate()
        {
            Console.WriteLine("Generator");
            return strategy.Calculate();

        }
        // Constructor
        public ReportGenerator(EnergyCalculator strategy)
        {
            this.strategy = strategy;
        }

    }
}
