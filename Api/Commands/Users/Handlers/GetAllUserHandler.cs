using Core.Models;
using Core.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Commands.Users.Handlers
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserCommand, List<User>>
    {
        private readonly IUserService _service;

        public GetAllUserHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<List<User>> Handle(GetAllUserCommand command, CancellationToken cancellationToken)
        {
            return await _service.Get();
        }
    }
}
