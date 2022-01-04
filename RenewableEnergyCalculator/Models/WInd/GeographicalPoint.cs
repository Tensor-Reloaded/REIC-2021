namespace RenewableEnergyCalculator.Models.Wind
{

    public struct GeographicalPoint
    {
        public double Latitude { get; }
        public double Longitude { get; }
        public GeographicalPoint(double latitude, double longitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
        }
    }
}