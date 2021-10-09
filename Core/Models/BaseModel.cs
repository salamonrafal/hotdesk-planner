using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace Core.Models
{
    public class BaseModel
    {
        [BsonElement("id")]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("_documentId")]
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public ObjectId DocumentId { get; set; }
    }
}
