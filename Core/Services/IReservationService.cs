using Core.Models;

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
