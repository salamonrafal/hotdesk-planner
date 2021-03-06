using Core.Models;
using MediatR;
using System.Collections.Generic;

namespace Api.Commands.Users
{
    public class SearchUserCommand : IRequest<List<User>>
    {
        public string Query { get; set; }
    }
}
