using Core.Models;
using System;

namespace Api.Commands.Reservations
{
    public class CommonReservationCommand
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool? IsPeriodical { get; set; }

        public Guid AssignedTo { get; set; }

        public PeriodicDetail PeriodicDetail { get; set; }

        public Guid DeskId { get; set; }

        public static implicit operator Reservation(CommonReservationCommand command) => new()
        {
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            IsPeriodical = command.IsPeriodical,
            AssignedTo = command.AssignedTo,
            PeriodicDetail = command.PeriodicDetail,
            DeskId = command.DeskId,
        };
    }
}
