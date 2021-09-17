using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Core.Models
{
    public class Reservation : BaseModel
    {
        [BsonRequired]
        [BsonElement("start_date")]
        public DateTime? StartDate { get; set; }

        [BsonRequired]
        [BsonElement("end_date")]
        public DateTime? EndDate { get; set; }

        [BsonElement("is_periodical")]
        public bool? IsPeriodical { get; set; }

        [BsonRequired]
        [BsonElement("assigned_to")]
        public Guid? AssignedTo { get; set; }

        [BsonElement("periodic_detail")]
        public PeriodicDetail PeriodicDetail { get; set; }

        [BsonRequired]
        [BsonElement("desk_id")]
        public Guid? DeskId { get; set; }

        public readonly static Reservation Empty = new();
    }
}
