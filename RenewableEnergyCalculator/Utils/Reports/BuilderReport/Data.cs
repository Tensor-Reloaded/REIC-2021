using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace builderReport
{
    public class Data
    {
        public GeographicalPoint geographicalPoint { get; set; }
        public PanelType panelType{ get; set; }
        public ResultEnergyData resultEnergyData { get; set; }
        public PowerCurve powerCurve { get; set; }

    }
}
