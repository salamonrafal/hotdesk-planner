using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Core.Models
{
    public class BaseModel
    {
        [BsonElement("id")]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("_id")]
        [BsonId()]
        public ObjectId DocumentId { get; set; }
    }
}
