using MediatR;
using System;
using DeskModel = Core.Models.Desk;

namespace Api.Commands.Desk
{
    public class InsertDeskCommand : CommonDeskCommand, IRequest<Guid>
    {
        public static readonly InsertDeskCommand Empty = new();

        public static implicit operator DeskModel(InsertDeskCommand command) => new()
        {
            Description = command.Description,
            Localization = command.Localization,
            IsBlocked = command.IsBlocked
        };
    }
}
