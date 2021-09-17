using Api.Mappers;
using Core.Exceptions;
using Core.Models;
using Core.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Commands.Users.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        public readonly IUserService _service;

        public DeleteUserHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            if (command == DeleteUserCommand.Empty) throw new CommandEmptyException("Command is Empty");

            GenericUserCommandMapper<DeleteUserCommand, User> mapper = new();
            User model = mapper.ConvertToModel(command);
            model.Id = command.Id;
            return await _service.Remove(model);
        }
    }
}
