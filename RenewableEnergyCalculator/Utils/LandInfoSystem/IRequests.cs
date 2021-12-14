using System;
using System.Collections.Generic;
using System.Text;

namespace LandInfoSystem
{
    interface IRequests
    {
        string LoadData();

        string GetType(float longitute, float latitude);

        string ConvertPoint(float longitute, float latitude);

    }
}
