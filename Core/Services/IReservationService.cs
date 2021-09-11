using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IReservationService
    {
        public Reservation Get(int reservationId);
        public int Add(Reservation reservation);
        public void Remove(Reservation reservation);
        public void SetPeriodic(Reservation reservation, PeriodicDetail periodic);
    }
}
