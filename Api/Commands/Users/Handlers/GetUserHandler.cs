using Api.Mappers;
using Core.Models;
using Core.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Commands.Users.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUserCommand, User>
    {
        private readonly IUserService _service;

        public GetUserHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<User> Handle(GetUserCommand command, CancellationToken cancellationToken)
        {
            GenericUserCommandMapper<GetUserCommand, User> mapper = new();
            var model = mapper.ConvertToModel(command);
            model.Id = command.Id;
            return await _service.Get(model);
        }
    }
}
