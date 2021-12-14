using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RenewableEnergyCalculator.Models
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // FileName: PanelType.cs
    // Author : Bucnaru Raluca
    // Description : type of panel: can be monoctystalinne, polycrystalline, thin-film (multiple types of thin film, a-Si for example)
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class PanelType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string Id { get; set; }

        [BsonElement("PType")]
        [JsonConverter(typeof(StringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        public Type PType { get; set; }

        [BsonElement("MinEfficiency")]
        public double MinEfficiency { get; set; }

        [BsonElement("MaxEfficiency")]
        public double MaxEfficiency { get; set; }

        public PanelType(Type PType, double MinEfficiency, double MaxEfficiency)
        {
            this.PType = PType;
            this.MinEfficiency = MinEfficiency;
            this.MaxEfficiency = MaxEfficiency;
        }
    }

    public enum Type
    {
        [BsonRepresentation(BsonType.String)]
        Monocrystalline = 1,
        [BsonRepresentation(BsonType.String)]
        Polycrystalline,
        [BsonRepresentation(BsonType.String)]
        Passivated_Emitter_and_Rear_Cell_PERC,
        [BsonRepresentation(BsonType.String)]
        Thin_Film_Cadmium_Telluride_CdTe,
        [BsonRepresentation(BsonType.String)]
        Thin_Film_Amorphous_Silicon_a_Si,
        [BsonRepresentation(BsonType.String)]
        Thin_Film_Copper_Indium_Gallium_Selenide_CIGS
    }
}