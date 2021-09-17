using Api.Mappers;
using Core.Exceptions;
using Core.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using DeskModel = Core.Models.Desk;

namespace Api.Commands.Desk.Handlers
{
    public class InsertDeskHandler : IRequestHandler<InsertDeskCommand, Guid>
    {
        public readonly IDeskService _service;

        public InsertDeskHandler(IDeskService service)
        {
            _service = service;
        }

        public async Task<Guid> Handle(InsertDeskCommand command, CancellationToken cancellationToken)
        {
            if (command == InsertDeskCommand.Empty) throw new CommandEmptyException("Command is Empty");

            GenericDeskCommandMapper<InsertDeskCommand, DeskModel> mapper = new();
            DeskModel desk = mapper.ConvertToModel(command);
            return await _service.Add(desk);
        }
    }
}
