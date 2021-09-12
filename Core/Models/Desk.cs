using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models
{
    public class Desk: BaseModel
    {
        [BsonElement("description")]
        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }
        [BsonElement("localization")]
        public Localization Localization { get; set; }
        [BsonElement("reservations")]
        [BsonRepresentation(BsonType.Array)]
        public List<Reservation> Reservations { get; set; }
        [BsonElement("is_blocked")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool IsBlocked { set; get; } 
    }
}
