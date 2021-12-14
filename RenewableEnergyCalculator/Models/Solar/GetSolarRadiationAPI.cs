using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace RenewableEnergyCalculator.Models.Solar
{
    public class GetSolarRadiationAPI
    {
        double longitude;
        double latitude;
        string apiUrl;
        string strOutputJSON;
        public List<double> SI_EF_TILTED_SURFACE_HORIZONTAL = new List<double>(new double[12]);
        public List<double> SI_EF_TILTED_SURFACE_LAT_MINUS15 = new List<double>(new double[12]);
        public List<double> SI_EF_TILTED_SURFACE_LATITUDE = new List<double>(new double[12]);
        public List<double> SI_EF_TILTED_SURFACE_LAT_PLUS15 = new List<double>(new double[12]);
        public List<double> SI_EF_TILTED_SURFACE_VERTICAL = new List<double>(new double[12]);
        public List<double> SI_EF_TILTED_SURFACE_OPTIMAL = new List<double>(new double[12]);
        public List<double> SI_EF_TILTED_SURFACE_OPTIMAL_ANG = new List<double>(new double[12]);
        public string SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT = " ";
        public void setLatitude(double latitude) { this.latitude = latitude; }

        public void setLongitude(double longitude) { this.longitude = longitude; }
        public void createURL(double latitude, double longitude)
        {
            this.apiUrl = "https://power.larc.nasa.gov/api/temporal/climatology/point?parameters=SI_EF_TILTED_SURFACE&community=RE&longitude=" + longitude + "&latitude=" + latitude + "&format=JSON&start=1990&end=2020";
        }

        public GetSolarRadiationAPI(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public void getAPIData()
        {
            createURL(latitude, longitude);
            Uri address = new Uri(apiUrl);

            using (var webClient = new System.Net.WebClient())
            {
                strOutputJSON = webClient.DownloadString(address);
                // Now parse with JSON.Net
                //var data = JsonSerializer.Deserialize<Radiation>(strOutputJSON);
                dynamic data = JObject.Parse(strOutputJSON);

                var d1 = data.properties.parameter.SI_EF_TILTED_SURFACE_HORIZONTAL;
                var d = d1.JAN;
                double v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_HORIZONTAL[0] = v;
                d = d1.FEB;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_HORIZONTAL[1] = v;
                d = d1.MAR;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_HORIZONTAL[2] = v;
                d = d1.APR;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_HORIZONTAL[3] = v;
                d = d1.MAY;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_HORIZONTAL[4] = v;
                d = d1.JUN;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_HORIZONTAL[5] = v;
                d = d1.JUL;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_HORIZONTAL[6] = v;
                d = d1.AUG;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_HORIZONTAL[7] = v;
                d = d1.SEP;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_HORIZONTAL[8] = v;
                d = d1.OCT;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_HORIZONTAL[9] = v;
                d = d1.NOV;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_HORIZONTAL[10] = v;
                d = d1.DEC;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_HORIZONTAL[11] = v;

                d1 = data.properties.parameter.SI_EF_TILTED_SURFACE_LAT_MINUS15;
                d = d1.JAN;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_MINUS15[0] = v;
                d = d1.FEB;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_MINUS15[1] = v;
                d = d1.MAR;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_MINUS15[2] = v;
                d = d1.APR;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_MINUS15[3] = v;
                d = d1.MAY;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_MINUS15[4] = v;
                d = d1.JUN;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_MINUS15[5] = v;
                d = d1.JUL;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_MINUS15[6] = v;
                d = d1.AUG;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_MINUS15[7] = v;
                d = d1.SEP;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_MINUS15[8] = v;
                d = d1.OCT;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_MINUS15[9] = v;
                d = d1.NOV;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_MINUS15[10] = v;
                d = d1.DEC;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_MINUS15[11] = v;

                d1 = data.properties.parameter.SI_EF_TILTED_SURFACE_LATITUDE;
                d = d1.JAN;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LATITUDE[0] = v;
                d = d1.FEB;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LATITUDE[1] = v;
                d = d1.MAR;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LATITUDE[2] = v;
                d = d1.APR;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LATITUDE[3] = v;
                d = d1.MAY;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LATITUDE[4] = v;
                d = d1.JUN;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LATITUDE[5] = v;
                d = d1.JUL;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LATITUDE[6] = v;
                d = d1.AUG;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LATITUDE[7] = v;
                d = d1.SEP;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LATITUDE[8] = v;
                d = d1.OCT;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LATITUDE[9] = v;
                d = d1.NOV;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LATITUDE[10] = v;
                d = d1.DEC;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LATITUDE[11] = v;


                d1 = data.properties.parameter.SI_EF_TILTED_SURFACE_LAT_PLUS15;
                d = d1.JAN;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_PLUS15[0] = v;
                d = d1.FEB;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_PLUS15[1] = v;
                d = d1.MAR;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_PLUS15[2] = v;
                d = d1.APR;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_PLUS15[3] = v;
                d = d1.MAY;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_PLUS15[4] = v;
                d = d1.JUN;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_PLUS15[5] = v;
                d = d1.JUL;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_PLUS15[6] = v;
                d = d1.AUG;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_PLUS15[7] = v;
                d = d1.SEP;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_PLUS15[8] = v;
                d = d1.OCT;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_PLUS15[9] = v;
                d = d1.NOV;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_PLUS15[10] = v;
                d = d1.DEC;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_LAT_PLUS15[11] = v;

                d1 = data.properties.parameter.SI_EF_TILTED_SURFACE_VERTICAL;
                d = d1.JAN;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_VERTICAL[0] = v;
                d = d1.FEB;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_VERTICAL[1] = v;
                d = d1.MAR;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_VERTICAL[2] = v;
                d = d1.APR;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_VERTICAL[3] = v;
                d = d1.MAY;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_VERTICAL[4] = v;
                d = d1.JUN;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_VERTICAL[5] = v;
                d = d1.JUL;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_VERTICAL[6] = v;
                d = d1.AUG;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_VERTICAL[7] = v;
                d = d1.SEP;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_VERTICAL[8] = v;
                d = d1.OCT;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_VERTICAL[9] = v;
                d = d1.NOV;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_VERTICAL[10] = v;
                d = d1.DEC;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_VERTICAL[11] = v;

                d1 = data.properties.parameter.SI_EF_TILTED_SURFACE_OPTIMAL;
                d = d1.JAN;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL[0] = v;
                d = d1.FEB;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL[1] = v;
                d = d1.MAR;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL[2] = v;
                d = d1.APR;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL[3] = v;
                d = d1.MAY;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL[4] = v;
                d = d1.JUN;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL[5] = v;
                d = d1.JUL;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL[6] = v;
                d = d1.AUG;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL[7] = v;
                d = d1.SEP;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL[8] = v;
                d = d1.OCT;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL[9] = v;
                d = d1.NOV;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL[10] = v;
                d = d1.DEC;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL[11] = v;

                d1 = data.properties.parameter.SI_EF_TILTED_SURFACE_OPTIMAL_ANG;
                d = d1.JAN;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL_ANG[0] = v;
                d = d1.FEB;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL_ANG[1] = v;
                d = d1.MAR;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL_ANG[2] = v;
                d = d1.APR;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL_ANG[3] = v;
                d = d1.MAY;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL_ANG[4] = v;
                d = d1.JUN;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL_ANG[5] = v;
                d = d1.JUL;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL_ANG[6] = v;
                d = d1.AUG;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL_ANG[7] = v;
                d = d1.SEP;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL_ANG[8] = v;
                d = d1.OCT;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL_ANG[9] = v;
                d = d1.NOV;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL_ANG[10] = v;
                d = d1.DEC;
                v = Convert.ToDouble(d);
                SI_EF_TILTED_SURFACE_OPTIMAL_ANG[11] = v;

                SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT = data.properties.parameter.SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT.JAN.ToString();

                Console.WriteLine(data.properties.parameter.SI_EF_TILTED_SURFACE_OPTIMAL_ANG_ORT);
            }
        }
    }
}