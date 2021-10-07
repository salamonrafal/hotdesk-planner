using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Core.Models
{
    public class User : BaseModel
    {
        [BsonRequired]
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonRequired]
        [BsonElement("surname")]
        public string Surname { get; set; }

        [BsonElement("url_avatar")]
        public string UrlAvatar { get; set; }

        [BsonRequired]
        [BsonElement("role")]
        public List<UserRole> Role { get; set; }

        [BsonRequired]
        [BsonElement("password")]
        public string Password { get; set; }

        [BsonRequired]
        [BsonElement("email")]
        public string Email { get; set; }
    }
}
