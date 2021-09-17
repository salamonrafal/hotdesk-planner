using Core.Models;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Api.Commands.Reservations
{
    public class DeleteReservationCommand: CommonReservationCommand, IRequest<bool>
    {
        [JsonIgnore] public Guid Id { get; set; }

        public readonly static DeleteReservationCommand Empty = new();

        public static implicit operator Reservation(DeleteReservationCommand command) => new()
        {
            Id = command.Id
        };
    }
}
