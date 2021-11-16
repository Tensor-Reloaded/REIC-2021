using System;
using System.Collections.Generic;
namespace REIC
{
   

    /// Contains data about a specific wind turbine model
    public class Turbine
    {
        /// The distance from the ground to the turbine blades. In meters.
        public float HubHeight { get; }
        /// The wind speed at which the turbine starts producing power. In m/s.
        public float CutInSpeed { get; }

        /// The wind speed at which the turbine stops producing power. In m/s.
        /// This is used to protect the turbine from strong winds.
        public float CutOutSpeed { get; }

        /// A curve that show how much energy we produce if the wind blows at x m/s.
        public PowerCurve PowerCurve { get; }

        public Turbine(float hubHeight, float cutInSpeed, float cutOutSpeed, PowerCurve powerCurve)
        {
            HubHeight = hubHeight;
            CutInSpeed = cutInSpeed;
            CutOutSpeed = cutOutSpeed;
            PowerCurve = powerCurve;
        }
    }


   
}
