using System;
using System.Collections.Generic;
using System.Text;

namespace LandInfoSystem
{
    class Adapter: IRequests
    {
        private readonly Adaptee _adaptee;

        public Adapter(Adaptee adaptee)
        {
            this._adaptee = adaptee;
        }

        public string LoadData()
        {
            return $"This is '{this._adaptee.LoadDataPython()}'";
        }

        public string GetType(float longitute, float latitude)
        {
            return $"This is '{this._adaptee.GetTypePython(longitute, latitude)}'";
        }

        public string ConvertPoint(float longitute, float latitude)
        {
            return $"This is '{this._adaptee.ConvertPointPython(longitute, latitude)}'";
        }
    }
}
