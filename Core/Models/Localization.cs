using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models
{
    public class Localization
    {
        [BsonRequired]
        [BsonElement("floor")]
        public int? Floor { get; set; }

        [BsonRequired]
        [BsonElement("outbuilding")]
        public string Outbuilding { get; set; }

        [BsonRequired]
        [BsonElement("coordination")]
        public Coordination Coordination { get; set; }

    }
}
