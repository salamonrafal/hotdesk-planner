using Api.Mappers;
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
        private readonly IDeskService _service;

        public InsertDeskHandler(IDeskService service)
        {
            _service = service;
        }

        public async Task<Guid> Handle(InsertDeskCommand command, CancellationToken cancellationToken)
        {
            GenericDeskCommandMapper<InsertDeskCommand, DeskModel> mapper = new();
            var desk = mapper.ConvertToModel(command);
            return await _service.Add(desk);
        }
    }
}
