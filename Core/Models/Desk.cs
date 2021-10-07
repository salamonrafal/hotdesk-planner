using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models
{
    public class Desk : BaseModel
    {
        [BsonRequired]
        [BsonElement("description")]
        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        [BsonElement("localization")]
        public Localization Localization { get; set; }

        [BsonElement("is_blocked")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool? IsBlocked { set; get; }
    }
}
