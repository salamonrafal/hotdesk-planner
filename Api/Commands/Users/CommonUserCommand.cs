using Core.Models;
using System.Collections.Generic;

namespace Api.Commands.Users
{
    public class CommonUserCommand
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UrlAvatar { get; set; }
        public List<UserRole> Role { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public static implicit operator User(CommonUserCommand command) => new()
        {
            Name = command.Name,
            Surname = command.Surname,
            UrlAvatar = command.UrlAvatar,
            Role = command.Role,
            Password = command.Password,
            Email = command.Email,
        };
    }
}
