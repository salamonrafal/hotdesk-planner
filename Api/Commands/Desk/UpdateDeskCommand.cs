using Core.Models;
using MediatR;
using System;
using System.Text.Json.Serialization;
using DeskModel = Core.Models.Desk;

namespace Api.Commands.Desk
{
    public class UpdateDeskCommand : CommonDeskCommand, IRequest<bool>
    { 
        [JsonIgnore] public Guid Id { get; set; }
        public readonly static UpdateDeskCommand Empty = new();

        public void UpdateId(Guid deskId)
        {
            Id = deskId;
        }

        public static implicit operator DeskModel(UpdateDeskCommand command) => new()
        {
            Id = command.Id,
            Description = command.Description,
            Localization = command.Localization,
            IsBlocked = command.IsBlocked
        };
    }
}
