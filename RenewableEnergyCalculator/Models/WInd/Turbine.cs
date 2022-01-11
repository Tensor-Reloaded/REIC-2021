namespace RenewableEnergyCalculator.Models.Wind
{
    /// <summary>
    /// Contains data about a specific wind turbine model
    /// </summary>
    public class Turbine
    {
        /// <summary>
        /// The distance from the ground to the turbine blades. In meters.
        /// </summary>
        public float HubHeight { get; }

        /// <summary>
        /// The wind speed at which the turbine starts producing power. In m/s.
        /// </summary>
        public float CutInSpeed { get; }

        /// <summary>
        /// The wind speed at which the turbine stops producing power. In m/s.
        /// This is used to protect the turbine from strong winds.
        /// </summary>
        public float CutOutSpeed { get; }

        public double Cost { get;  }

        /// <summary>
        /// A curve that show how much energy (kW) we produce if the wind blows at x m/s.
        /// </summary>
        public PowerCurve PowerCurve { get; }

        /// <summary>
        /// The maximal power produced (in kW)
        /// </summary>
        public double RatedPower => PowerCurve.MaxValue;

        public Turbine(float hubHeight, float cutInSpeed, float cutOutSpeed, PowerCurve powerCurve, double cost)
        {
            HubHeight = hubHeight;
            CutInSpeed = cutInSpeed;
            CutOutSpeed = cutOutSpeed;
            PowerCurve = powerCurve;
            Cost = cost;
        }
    }
}