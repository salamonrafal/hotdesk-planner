using Core.Models;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Api.Commands.Users
{
    public class UpdateUserCommand : CommonUserCommand, IRequest<bool>
    {
        [JsonIgnore] public Guid Id { get; set; }
        public readonly static UpdateUserCommand Empty = new();

        public void UpdateId(Guid userId)
        {
            Id = userId;
        }

        public static implicit operator User(UpdateUserCommand command) => new()
        {
            Name = command.Name,
            Surname = command.Surname,
            UrlAvatar = command.UrlAvatar,
            Role = command.Role,
            Password = command.Password,
            Email = command.Email
        };
    }
}
