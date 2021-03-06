using Api.Mappers;
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
        private readonly IUserService _service;

        public InsertUserHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<Guid> Handle(InsertUserCommand command, CancellationToken cancellationToken)
        {
            GenericUserCommandMapper<InsertUserCommand, User> mapper = new();
            var desk = mapper.ConvertToModel(command);
            return await _service.Add(desk);
        }
    }
}
