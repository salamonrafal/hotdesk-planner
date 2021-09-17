using Api.Mappers;
using Core.Exceptions;
using Core.Models;
using Core.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Commands.Users.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        public readonly IUserService _service;

        public UpdateUserHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            if (command == UpdateUserCommand.Empty) throw new CommandEmptyException("Command is Empty");

            GenericUserCommandMapper<UpdateUserCommand, User> mapper = new();
            User desk = mapper.ConvertToModel(command);
            desk.Id = command.Id;
            return await _service.Update(desk);
        }
    }
}
