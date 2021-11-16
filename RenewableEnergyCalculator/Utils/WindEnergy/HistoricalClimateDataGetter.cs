using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using CsvHelper;
using System.IO;
using System.Globalization;

namespace REIC
{
    /// Gives historical data at a particular point
    public class HistoricalClimateDataGetter
    {
        public string Key { get; set; } = "9MC7E2CBAS876XGWWCSKA599F";//"VCUQ8ZMDKB3JZN3KSQR5SMGC4";
        public HistoricalClimateDataGetter()
        {

        }


        public List<HistoricalClimateSample> GetValuesAtPoint(GeographicalPoint point)
        {
            return GetValuesAtPoint(point, new DateTime(2020, 1, 1), new DateTime(2020, 12, 31));
        }

        public List<HistoricalClimateSample> GetValuesAtPoint(GeographicalPoint point, DateTime t0, DateTime t1)
        {
            if (t0 > t1)
            {
                var t = t0;
                t0 = t1;
                t1 = t;
            }
            int totalDays = (int)(t1 - t0).TotalDays;
            int maxDaysPerRequest = 249;
            if (totalDays > maxDaysPerRequest)
            {
                var result = new List<HistoricalClimateSample>();
                var a = t0;
                for (; ; )
                {
                    var b = a.AddDays(maxDaysPerRequest);
                    var isLastBatch = false;
                    if (b >= t1)
                    {
                        b = t1;
                        isLastBatch = true;
                    }

                    result.AddRange(GetValuesAtPoint(point, a, b));
                    if (isLastBatch)
                        break;
                    a = b.AddDays(1);
                }
                return result;
            }



            string FormatDate(DateTime t) => $"{t.Year:0000}-{t.Month:00}-{t.Day:00}";

            var requestContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("aggregateHours", "24"),
                new KeyValuePair<string, string>("combinationMethod","aggregate"),
                new KeyValuePair<string, string>("startDateTime", FormatDate(t0)),// TODO
                new KeyValuePair<string, string>("endDateTime", FormatDate(t1)),
                new KeyValuePair<string, string>("maxStations","-1"),
                new KeyValuePair<string, string>("maxDistance","-1"),
                new KeyValuePair<string, string>("contentType","csv"),
                new KeyValuePair<string, string>("unitGroup","metric"),
                new KeyValuePair<string, string>("locationMode","single"),
                new KeyValuePair<string, string>("key",Key),
                new KeyValuePair<string, string>("dataElements","default"),
                new KeyValuePair<string, string>("locations", $"{point.Latitude},{point.Longitude}"),//todo
            });
            var client = new HttpClient();

            var response = client.PostAsync(
                "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/weatherdata/history",
                requestContent).Result;

            var str = response.Content.ReadAsStringAsync().Result;
            if (!str.StartsWith("Name,"))
            {
                throw new Exception(str);
            }

            using (var stream = new StringReader(str))
            {
                using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<HistoricalClimateSampleMap>();
                    return csv.GetRecords<HistoricalClimateSample>().ToList();
                }
            }
        }
    }
}
