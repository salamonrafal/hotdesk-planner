using System;

namespace Core.Models
{
    public class Reservation : BaseModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPeriodical { get; set; }
        public User AssignedTo { get; set; }
        public PeriodicDetail PeriodicDetail { get; set; }
        public Desk Desk { get; set; }

    }
}
