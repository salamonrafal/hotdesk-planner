using Api.Mappers;
using Core.Exceptions;
using Core.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DeskModel = Core.Models.Desk;

namespace Api.Commands.Desk.Handlers
{
    public class UpdateDeskHandler : IRequestHandler<UpdateDeskCommand, bool>
    {
        public readonly IDeskService _service;

        public UpdateDeskHandler(IDeskService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateDeskCommand command, CancellationToken cancellationToken)
        {
            if (command == UpdateDeskCommand.Empty) throw new CommandEmptyException("Command is Empty");

            GenericDeskCommandMapper<UpdateDeskCommand, DeskModel> mapper = new();
            DeskModel desk = mapper.ConvertToModel(command);
            desk.Id = command.Id;
            return await _service.Update(desk);
        }
    }
}
