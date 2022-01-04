using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RenewableEnergyCalculator.Models.Wind
{
    public class Turbine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public float Name { get; set; }

        /// The distance from the ground to the turbine blades. In meters.
        [BsonElement("HubHeight")]
        public float HubHeight { get; set; }

        /// The wind speed at which the turbine starts producing power. In m/s.
        [BsonElement("CutInSpeed")]
        public float CutInSpeed { get; set; }

        /// The wind speed at which the turbine stops producing power. In m/s.
        /// This is used to protect the turbine from strong winds.
        [BsonElement("CutOutSpeed")]
        public float CutOutSpeed { get; set; }

        /// A curve that show how much energy we produce if the wind blows at x m/s.
        [BsonElement("PowerCurveX")]
        public float[] PowerCurveX { get; set;}

        [BsonElement("PowerCurveX")]
        public float[] PowerCurveY { get; set; }

        public Turbine(float hubHeight, float cutInSpeed, float cutOutSpeed, PowerCurve powerCurve)
        {
            //HubHeight = hubHeight;
            CutInSpeed = cutInSpeed;
            CutOutSpeed = cutOutSpeed;
            //PowerCurve = powerCurve;
        }
    }
}