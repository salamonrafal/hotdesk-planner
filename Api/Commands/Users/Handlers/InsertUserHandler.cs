using Api.Mappers;
using Core.Exceptions;
using Core.Models;
using Core.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Commands.Users.Handlers
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, Guid>
    {
        public readonly IUserService _service;

        public InsertUserHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<Guid> Handle(InsertUserCommand command, CancellationToken cancellationToken)
        {
            if (command == InsertUserCommand.Empty) throw new CommandEmptyException("Command is Empty");

            GenericUserCommandMapper<InsertUserCommand, User> mapper = new();
            User desk = mapper.ConvertToModel(command);
            return await _service.Add(desk);
        }
    }
}
