using System;
using Api.Mappers;
using Core.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DeskModel = Core.Models.Desk;

namespace Api.Commands.Desk.Handlers
{
    public class UpdateDeskHandler : IRequestHandler<UpdateDeskCommand, bool>
    {
        private readonly IDeskService _service;

        public UpdateDeskHandler(IDeskService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateDeskCommand command, CancellationToken cancellationToken)
        {
            GenericDeskCommandMapper<UpdateDeskCommand, DeskModel> mapper = new();
            var desk = mapper.ConvertToModel(command);
            desk.Id = command.Id ?? Guid.Empty;
            return await _service.Update(desk);
        }
    }
}
