using Api.Mappers;
using Core.Exceptions;
using Core.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DeskModel = Core.Models.Desk;

namespace Api.Commands.Desk.Handlers
{
    public class DeleteDeskHandler : IRequestHandler<DeleteDeskCommand, bool>
    {
        public readonly IDeskService _service;

        public DeleteDeskHandler(IDeskService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteDeskCommand command, CancellationToken cancellationToken)
        {
            if (command == DeleteDeskCommand.Empty) throw new CommandEmptyException("Command is Empty");

            GenericDeskCommandMapper<DeleteDeskCommand, DeskModel> mapper = new();
            DeskModel desk = mapper.ConvertToModel(command);
            desk.Id = command.Id;
            return await _service.Remove(desk);
        }
    }
}
