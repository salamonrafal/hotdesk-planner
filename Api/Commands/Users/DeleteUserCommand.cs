using Core.Models;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Api.Commands.Users
{
    public class DeleteUserCommand : CommonUserCommand, IRequest<bool>
    {
        [JsonIgnore] public Guid Id { get; set; }

        public readonly static DeleteUserCommand Empty = new();

        public static implicit operator User(DeleteUserCommand command) => new()
        {
            Id = command.Id
        };
    }
}
