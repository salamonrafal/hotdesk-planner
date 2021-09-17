using MediatR;
using System;
using DeskModel = Core.Models.Desk;

namespace Api.Commands.Desk
{
    public class GetDeskCommand : CommonDeskCommand, IRequest<DeskModel>
    {
        public Guid Id { get; set; }

        public readonly static GetDeskCommand Empty = new();

        public static implicit operator DeskModel(GetDeskCommand command) => new()
        {
            Id = command.Id
        };
    }
}
