using Api.Mappers;
using Core.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DeskModel = Core.Models.Desk;

namespace Api.Commands.Desk.Handlers
{
    public class DeleteDeskHandler : IRequestHandler<DeleteDeskCommand, bool>
    {
        private readonly IDeskService _service;

        public DeleteDeskHandler(IDeskService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteDeskCommand command, CancellationToken cancellationToken)
        {
            GenericDeskCommandMapper<DeleteDeskCommand, DeskModel> mapper = new();
            var desk = mapper.ConvertToModel(command);
            desk.Id = command.Id;
            return await _service.Remove(desk);
        }
    }
}
