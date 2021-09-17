using Core.Models;
using MediatR;
using System.Collections.Generic;

namespace Api.Commands.Users
{
    public class GetAllUserCommand : CommonUserCommand, IRequest<List<User>>
    {
    }
}
