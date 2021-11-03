using System;
using System.Collections.Generic;
namespace REIC
{
    class SolarEnergyCalculator : EnergyCalculator
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //FileName: SolarEnergyCalculator.cs
        //FileType: Visual C# Source file
        //Author : Bucnaru Raluca
        //Description : part of the strategy design pattern, used to calculate solar energy yield
        //////////////////////////////////////////////////////////////////////////////////////
        private void AddPanel(SolarPanel sp) { }
        private void AddLocation(List<GeographicalPoint> geodata) { }


        //collection of energy for all panels
        Collection SolarCollection; //used for testing iterator design pattern
        private float TotalEnergy()
        { //iterate through collection and get the sum of all the energy values
            float sumE = 0;
            Iterator iterator = SolarCollection.CreateIterator();
            iterator.Step = 1;
            for (Item item = iterator.First(); !iterator.IsDone; item = iterator.Next())
            {
                sumE += item.Value;
            }
            return sumE;
        }

        /// @param panel The model of solar panel to use
        /// @param numberOfPanels The number of panels to be built in the region of interest
        public SolarEnergyCalculator(SolarPanel panel, int numberOfPanels)
        {
        }
        public SolarEnergyCalculator()
        {
        }

        public override ResultEnergyData Calculate()
        {
            Console.WriteLine("Calculator");
            throw new NotImplementedException();
       

        }

        public override bool ShouldReject()
        {
            throw new NotImplementedException();
        }
    }
}