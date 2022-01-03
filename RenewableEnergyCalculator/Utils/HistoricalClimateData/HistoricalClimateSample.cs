using System;

namespace REIC
{
    public class HistoricalClimateSample
    {
        public int Day { get; set; }
        public int Month { get; set; }

        public double Temperature { get; set; }

        /// In m/s; speed at 10m above ground; 25-percentile
        public double WindSpeed_p25 { get; set; }

        public double WindSpeed  { get; set; }

        /// In m/s; speed at 10m above ground; 75-percentile

        public double WindSpeed_p75 { get; set; }


        public double CloudCover { get; set; }
        /// In % - range 0 - 100
        public double RelativeHumidity { get; set; }

        public HistoricalClimateSample()
        {

        }
    }
}
