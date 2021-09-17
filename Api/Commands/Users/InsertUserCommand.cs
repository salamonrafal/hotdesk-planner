using Core.Models;
using MediatR;
using System;

namespace Api.Commands.Users
{
    public class InsertUserCommand : CommonUserCommand, IRequest<Guid>
    {
        public readonly static InsertUserCommand Empty = new();

        public static implicit operator User(InsertUserCommand command) => new()
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
