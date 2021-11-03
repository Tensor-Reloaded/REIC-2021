using System;
using System.Collections.Generic;
namespace REIC
{
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////
    //FileName: Factor.cs
    //FileType: Visual C# Source file
    //Author : Bucnaru Raluca, Pavel Andrei
    //Description : land factor and geographical point struct
    //////////////////////////////////////////////////////////////////////////////////////
    class Factor
    {
        ILandResource res;
    }


    interface ILandResource
    {
        double GetValueAt(double lat, double long_);
    }

    class TiffLandResource : ILandResource
    {
        private double GetValueAt(double lat, double long_) { return 1; }

        double ILandResource.GetValueAt(double lat, double long_)
        {
            throw new NotImplementedException();
        }
    }
    class GribLandResource : ILandResource
    {
        double GetValueAt(double lat, double long_) { return 1; }
        double ILandResource.GetValueAt(double lat, double long_)
        {
            throw new NotImplementedException();
        }
    }

    struct GeographicalPoint
    {
        public double Longitude { get; }
        public double Latitude { get; }
    }

}
