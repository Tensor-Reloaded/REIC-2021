using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace RenewableEnergyCalculator.Models.Wind
{
    // Gives historical data at a particular point
    public class HistoricalClimateDataGetter
    {
        // https://openweathermap.org/our-initiatives/student-initiative
        string Key { get; set; }
        public HistoricalClimateDataGetter()
        {
            Key = System.Configuration.ConfigurationManager.AppSettings["ClimateApiKey"];
            //File.ReadAllText(@"C:\reic\key.txt").Trim();
        }
        public HistoricalClimateDataGetter(string key)
        {
            Key = key;
        }

        /// Get the mean temperature for every month at the location `point`
        /// Note: the temperatures are in celsius
        /// index 0 is January, 1 February ...
        public List<double> GetMonthlyMeanTemperatures(GeographicalPoint point)
        {
            var values = GetValuesAtPoint(point);
            return Enumerable.Range(1, 12).Select(m =>
                values.Where(x => x.Month == m).Select(x => x.Temperature).Average()).ToList();
        }

        public List<HistoricalClimateSample> GetValuesAtPoint(GeographicalPoint point)
        {
            var uri = new Uri($@"http://history.openweathermap.org/data/2.5/aggregated/year?"
                + $"lat={point.Latitude}&"
                + $"lon={point.Longitude}&"
                + $"appid={Key}");

            Console.WriteLine($"uri {uri}");

            var client = new HttpClient();
            var response = client.GetAsync(uri).Result;
            var str = response.Content.ReadAsStringAsync().Result;


            dynamic json = JObject.Parse(str);

            if (json.cod != "200")
            {
                throw new Exception((string)json.message);
            }

            double KelvinToCelsius(double k) => k - 273.15;
            //double MetersPerSecondToKmh(double x) => x * 3.6;

            return ((JArray)json.result).Select((dynamic x) => new HistoricalClimateSample() {

                WindSpeed_p75 = x.wind.p75,//mean,
                WindSpeed_p25 = x.wind.p25,//mean,
                WindSpeed = x.wind.mean,



                Day = x.day,
                Month = x.month,
                Temperature = KelvinToCelsius(x.temp.mean),
                CloudCover = x.clouds.mean,
                RelativeHumidity = x.humidity.mean,
            }).ToList();
        }
    }
}