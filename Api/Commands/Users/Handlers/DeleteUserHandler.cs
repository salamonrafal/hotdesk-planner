using Api.Mappers;
using Core.Models;
using Core.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Commands.Users.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserService _service;

        public DeleteUserHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            GenericUserCommandMapper<DeleteUserCommand, User> mapper = new();
            var model = mapper.ConvertToModel(command);
            model.Id = command.Id;
            return await _service.Remove(model);
        }
    }
}
