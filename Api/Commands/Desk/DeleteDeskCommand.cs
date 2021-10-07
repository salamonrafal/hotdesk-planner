using MediatR;
using System;
using System.Text.Json.Serialization;
using DeskModel = Core.Models.Desk;

namespace Api.Commands.Desk
{
    public class DeleteDeskCommand : CommonDeskCommand, IRequest<bool>
    {
        [JsonIgnore] public Guid Id { get; set; }

        public static implicit operator DeskModel(DeleteDeskCommand command) => new()
        {
            Id = command.Id
        };
    }
}
