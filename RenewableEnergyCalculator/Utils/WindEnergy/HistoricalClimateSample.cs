using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace REIC
{
    internal sealed class HistoricalClimateSampleMap : ClassMap<HistoricalClimateSample>
    {
        public HistoricalClimateSampleMap()
        {
           // var doubleNullableConverter = new NullableConverter(typeof(double?), new TypeConverterCache());

            Map(x => x.DateTime).Name("Date time");
            Map(x => x.MaximumTemperature).Name("Maximum Temperature");
            Map(x => x.MinimumTemperature).Name("Minimum Temperature");
            Map(x => x.Precipitation).Name("Precipitation");
            Map(x => x._snowDepth).Name("Snow Depth");
            Map(x => x.WindSpeed).Name("Wind Speed");
            Map(x => x._windGust).Name("Wind Gust");
            Map(x => x.CloudCover).Name("Cloud Cover");//.TypeConverter(doubleNullableConverter);
            Map(x => x.RelativeHumidity).Name("Relative Humidity");
        }
    }
    public class HistoricalClimateSample
    {
        public string DateTime { get; set; }
        public double MaximumTemperature { get; private set; }
        public double MinimumTemperature { get; private set; }

        public double AverageTemperature => (MaximumTemperature + MinimumTemperature) / 2;

        /// In cm
        public double Precipitation { get; private set; }

        // in the csv data, it can be null
        internal double? _snowDepth;
        /// In cm
        public double SnowDepth => _snowDepth ?? 0;
        /// In km/h; speed at 10m above ground
        public double WindSpeed { get; set; }

        // in the csv data, it can be null
        internal double? _windGust;
        /// The highest recorded wind speed for that day
        public double WindGust => _windGust ?? WindSpeed;
        /// In % - range 0 to 100
        public double CloudCover { get; set; }
        /// In % - range 0 - 100
        public double RelativeHumidity { get; set; }
    }
}
