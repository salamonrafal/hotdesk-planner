using MediatR;
using System;
using System.Text.Json.Serialization;
using DeskModel = Core.Models.Desk;

namespace Api.Commands.Desk
{
    public class InsertDeskCommand : CommonDeskCommand, IRequest<Guid>
    {
        public readonly static InsertDeskCommand Empty = new();

        public static implicit operator DeskModel(InsertDeskCommand command) => new()
        {
            Description = command.Description,
            Localization = command.Localization,
            IsBlocked = command.IsBlocked
        };
    }
}
