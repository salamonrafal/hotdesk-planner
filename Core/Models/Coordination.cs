using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models
{
    public class Coordination
    {
        [BsonRequired]
        [BsonElement("x")]
        public float? X { get; set; }

        [BsonRequired]
        [BsonElement("y")]
        public float? Y { get; set; }

        public readonly static Coordination Empty = new();
    }
}