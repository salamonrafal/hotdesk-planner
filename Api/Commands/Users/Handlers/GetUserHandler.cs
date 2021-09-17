using Api.Mappers;
using Core.Exceptions;
using Core.Models;
using Core.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Commands.Users.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUserCommand, User>
    {
        public readonly IUserService _service;

        public GetUserHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<User> Handle(GetUserCommand command, CancellationToken cancellationToken)
        {
            if (command == GetUserCommand.Empty) throw new CommandEmptyException("Command is Empty");

            GenericUserCommandMapper<GetUserCommand, User> mapper = new();
            User model = mapper.ConvertToModel(command);
            model.Id = command.Id;
            return await _service.Get(model);
        }
    }
}
