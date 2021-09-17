using Core.Models;
using MediatR;
using System;

namespace Api.Commands.Reservations
{
    public class InsertReservationCommand : CommonReservationCommand, IRequest<Guid>
    {
        public readonly static InsertReservationCommand Empty = new();

        public static implicit operator Reservation(InsertReservationCommand command) => new()
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
