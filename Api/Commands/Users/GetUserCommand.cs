using Core.Models;
using MediatR;
using System;

namespace Api.Commands.Users
{
    public class GetUserCommand : CommonUserCommand, IRequest<User>
    {
        public Guid Id { get; set; }

        public readonly static GetUserCommand Empty = new();

        public static implicit operator User(GetUserCommand command) => new()
        {
            Id = command.Id
        };
    }
}
