using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Extensibility;
using System.ComponentModel;
using PostSharp.Patterns.Model;

namespace RenewableEnergyCalculator.Models
{

    //////////////////////////////////////////////////////////////////////////////////////
    // FileName: SolarPanel.cs
    // Author : Bucnaru Raluca
    // Description : Contains data about a specific solar panel model
    /////////////////////////////////////////////////////////////////////////////////////


    [Log(AttributeTargetMemberAttributes = MulticastAttributes.Public)]
    [NotifyPropertyChanged]
    public class SolarPanel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Model")]
        public string Model { get; set; }

        [BsonElement("Manufacturer")]
        public string Manufacturer { get; set; }

        [BsonElement("Efficiency")]
        public double Efficiency { get; set; }

        [BsonElement("Cost")]
        public double Cost { get; set; }

        [BsonElement("Area")]
        public double Area { get; set; }
    }
}