using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace Api.Commands.Reservations
{
    public class GetReservationCommand : CommonReservationCommand, IRequest<Reservation>
    {
        public Guid Id { get; set; }

        public readonly static GetReservationCommand Empty = new();

        public static implicit operator Reservation(GetReservationCommand command) => new()
        {
            Id = command.Id
        };
    }
}
