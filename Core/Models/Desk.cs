using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Desk: BaseModel
    {
        public string Description { get; set; }
        public Localization Localization { get; set; }
        public List<Reservation> Reservations { get; set; }
        public bool IsBlocked { set; get; } 
    }
}
