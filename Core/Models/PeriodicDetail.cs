using System.Collections.Generic;
using Core.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models
{
    public class PeriodicDetail
    {
        [BsonElement("days")]
        public List<PeriodDays> Days { get; set; }
        
        [BsonElement("repeat_type")]
        public PeriodRepeatType? RepeatType { get; set; }
        
        [BsonElement("repeat_count")]
        public int? RepeatCount { get; set; }
    }
}