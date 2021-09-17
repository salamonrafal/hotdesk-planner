using Core.Models;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Api.Commands.Reservations
{
    public class UpdateReservationCommand : CommonReservationCommand, IRequest<bool>
    {
        [JsonIgnore] public Guid Id { get; set; }
        [JsonIgnore] public new Guid AssignedTo { get; set; }
        [JsonIgnore] public new Guid DeskId { get; set; }
        public readonly static UpdateReservationCommand Empty = new();

        public void UpdateId(Guid deskId)
        {
            Id = deskId;
        }

        public static implicit operator Reservation(UpdateReservationCommand command) => new()
        {
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            IsPeriodical = command.IsPeriodical,
            PeriodicDetail = command.PeriodicDetail
        };
    }
}
