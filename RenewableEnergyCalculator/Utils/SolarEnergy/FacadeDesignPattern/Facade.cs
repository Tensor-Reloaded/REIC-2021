using System;
using System.Collections.Generic;

namespace REIC
{
	/// ////////////////////////////////////////////////////////////////////////////////////////////////////////
	//FileName: REIC.cs
	//FileType: Visual C# Source file
	//Author : Bucnaru Raluca
	//Description : The 'Facade' class. because there's only the calculator subsystem here, i can't fully implement it, but this is the general idea of the design pattern
	//////////////////////////////////////////////////////////////////////////////////////
	public class REIC
	{
        //strategy - calculator subsystem
		
		ReportGenerator context1 = new ReportGenerator(new SolarEnergyCalculator(new SolarPanel(0, 0, 0, "", new PanelType("monocrystalline", 15, 17), 0, "S"), 10));
		ReportGenerator context2 = new ReportGenerator(new WindEnergyCalculator());
		//other subsystems are-report generation, map, mailing, etc, 
		//i only have the calculator subsystem here, so i can only use parts of the subsystem as separate subsystems, but Facade works as an unified interface for all subsystems
		//
		public void DoSomethingWithSubsystems()
		{
			Console.WriteLine("Facade");
			ResultEnergyData data1 = context1.Calculate();
			ResultEnergyData data2 = context2.Calculate();
			throw new NotImplementedException(); }
		
	}
}
