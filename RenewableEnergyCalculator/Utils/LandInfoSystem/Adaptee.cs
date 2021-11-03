using System;
using System.Collections.Generic;
using System.Text;

namespace LandInfoSystem
{
    class Adaptee
    {
        public string LoadDataPython()
        {
            return "Data Loaded.";
        }

        public string ConvertPointPython(float longitute, float latitude)
        {
            return "Point Converted.";
        }

        public string GetTypePython(float longitute, float latitude)
        {
            return "Type Found.";
        }
    }
}
