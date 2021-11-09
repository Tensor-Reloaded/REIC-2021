using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RenewableEnergyCalculator.Models
{
    public class Panel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Model")]
        public string Model { get; set; }

        [BsonElement("Manufacturer")]
        public string Manufacturer { get; set; }

        [BsonElement("Efficency")]
        public double  Efficency { get; set; }
    }
}